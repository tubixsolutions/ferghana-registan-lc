using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.FileViewModels;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Files;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;

namespace RegistanFerghanaLC.Web.Controllers.Admins;

[Route("adminstudents")]
public class AdminStudentController : Controller
{
    private readonly IAdminStudentService _adminStudentService;
    private readonly IAdminSubjectService _subjectService;
    private readonly IMapper _mapper;
    private readonly IExcelService _excelService;
    private readonly string _rootPath;
    private readonly int _pageSize = 10;

    public AdminStudentController(IAdminStudentService adminStudentService, IAdminSubjectService subjectService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IExcelService excelService)
    {
        _rootPath = webHostEnvironment.WebRootPath;
        _adminStudentService = adminStudentService;
        _subjectService = subjectService;
        _mapper = mapper;
        _excelService = excelService;
    }

    [HttpGet("register")]
    public ViewResult Register()
    {
        ViewBag.Subjects = _subjectService.GetAllAsync();
        return View("Register");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudentAsync(StudentRegisterDto studentRegisterDto)
    {
        if (ModelState.IsValid)
        {
            var result = await _adminStudentService.RegisterStudentAsync(studentRegisterDto);
            if (result)
            {
                return RedirectToAction("index", "home", new { area = "" });
            }
            else
            {
                return Register();
            }
        }
        else return Register();
    }

    [HttpGet]
    public async Task<IActionResult> Index(string search, int page = 1)
    {
        if (String.IsNullOrEmpty(search))
        {
            FileModeldto students = new FileModeldto()
            {
                Students = await _adminStudentService.GetAllAsync(new PaginationParams(page, _pageSize)),
            };
            ViewBag.HomeTitle = "Students";
            ViewBag.AdminStudentSearch = search;
            return View("Index", students);
        }
        else
        {
            FileModeldto students = new FileModeldto()
            {
                Students = await _adminStudentService.GetByNameAsync(new PaginationParams(page, _pageSize), search)
            };
            ViewBag.HomeTitle = "Students";
            ViewBag.AdminStudentSearch = search;
            return View("Index", students);
        }
    }

    [HttpGet("delete")]
    public async Task<ViewResult> Delete(int id)
    {
        var student = await _adminStudentService.GetByIdAsync(id);
        if (student != null)
        {
            return View("Delete", student);
        }
        return View();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        var res = await _adminStudentService.DeleteAsync(Id);
        return RedirectToAction("Index");
    }

    [HttpGet("update")]
    public async Task<ViewResult> Update(int id)
    {
        var student = await _adminStudentService.GetByIdAsync(id);
        ViewBag.Subjects = _subjectService.GetAllAsync();
        var dto = new StudentAllUpdateDto()
        {
            FirstName = student.FirstName,
            LastName = student.LastName,
            PhoneNumber = student.PhoneNumber,
            BirthDate = student.BirthDate,
            StudentLevel = student.StudentLevel,
        };
        ViewBag.HomeTittle = "Admin/Student/Update";
        ViewBag.studentId = id;
        return View("Update", dto);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(int id, StudentAllUpdateDto dto)
    {
        var res = await _adminStudentService.UpdateAsync(id, dto);
        if (res)
        {
            return await Update(id);
        }
        else return await Update(id);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _adminStudentService.GetByIdAsync(id);
        if (student is not null) return View("GetById", student);
        return View("Index");
    }

    [HttpGet("duplicate")]
    public async Task<IActionResult> Duplicate()
    {

        using (var stream = new FileStream(Path.Combine(_rootPath, "files", "student.xlsx"), FileMode.Open))
        {
            byte[] file = new byte[stream.Length];
            await stream.ReadAsync(file, 0, file.Length);
            return new FileContentResult(file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"brands_{DateTime.UtcNow.ToShortDateString()}.xlsx"
            };
        }
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportAsync(FileModeldto filemodel)
    {
        if (filemodel.File is not null)
        {
            try
            {
                List<StudentRegisterDto> dtos = await _excelService.ReadStudentFileAsync(filemodel);

                if (dtos.Count > 0) return View("Unsaved", dtos);

                return RedirectToAction("Index", "adminstudents", new { area = "" });
            }
            catch (InvalidExcel ex)
            {
                return BadRequest(ex.Mes);
            }
            catch (AlreadyExistingException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        else
        {
            return RedirectToAction("Index", "adminstudents", new { area = "" });
        }
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export()
    {
        List<StudentViewModel> students = await _adminStudentService.GetFileAllAsync();


        string path = Path.Combine("files", "DownloadStudent.xlsx");
        string fullPath = Path.Combine(_rootPath, path);

        List<StudentRegisterDto> dtos = new();
        using (IXLWorkbook workbook = new XLWorkbook(fullPath, XLEventTracking.Disabled))
        {
            IXLWorksheet worksheet = workbook.Worksheets.First();
            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Full Name";
            worksheet.Cell("C1").Value = "Birth Data";
            worksheet.Cell("D1").Value = "Phone Number";
            worksheet.Cell("E1").Value = "Student Level";
            worksheet.Row(1).Style.Font.Bold = true;

            for (int i = 1; i <= students.Count; i++)
            {
                var student = students[i - 1];
                worksheet.Cell(i + 1, 1).Value = student.Id;
                worksheet.Cell(i + 1, 2).Value = student.FirstName + " " + student.LastName;
                worksheet.Cell(i + 1, 3).Value = student.BirthDate;
                worksheet.Cell(i + 1, 4).Value = student.PhoneNumber;
                worksheet.Cell(i + 1, 5).Value = student.StudentLevel;
            }
            workbook.SaveAs(fullPath);

            using (var stream = new FileStream(Path.Combine(_rootPath, "files", "DownloadStudent.xlsx"), FileMode.Open))
            {
                byte[] file = new byte[stream.Length];
                await stream.ReadAsync(file, 0, file.Length);
                return new FileContentResult(file,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = $"brands_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                };
            }
        }

        //using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
        //{
        //    var worksheet = workbook.Worksheets.Add("Brands");

        //    worksheet.Cell("A1").Value = "Id";
        //    worksheet.Cell("B1").Value = "Full Name";
        //    worksheet.Cell("C1").Value = "Birth Data";
        //    worksheet.Cell("D1").Value = "Phone Number";
        //    worksheet.Cell("E1").Value = "Student Level";
        //    worksheet.Row(1).Style.Font.Bold = true;

        //    for (int i = 1; i <= students.Count; i++)
        //    {
        //        var student = students[i - 1];
        //        worksheet.Cell(i + 1, 1).Value = student.Id;
        //        worksheet.Cell(i + 1, 2).Value = student.FirstName + " " + student.LastName;
        //        worksheet.Cell(i + 1, 3).Value = student.BirthDate;
        //        worksheet.Cell(i + 1, 4).Value = student.PhoneNumber;
        //        worksheet.Cell(i + 1, 5).Value = student.StudentLevel;
        //    }


        //    using (var stream = new MemoryStream())
        //    {
        //        workbook.SaveAs(stream);
        //        stream.Flush();

        //        return new FileContentResult(stream.ToArray(),
        //            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //        {
        //            FileDownloadName = $"brands_{DateTime.UtcNow.ToShortDateString()}.xlsx"
        //        };
        //    }
        //}
    }

}