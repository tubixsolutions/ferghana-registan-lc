using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.FileViewModels;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Files;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    [Route("adminteachers")]
    public class AdminTeacherController : Controller
    {
        private readonly IAdminTeacherService _adminTeacherService;
        private readonly string _rootPath;
        private readonly int _pageSize = 5;
        private readonly IExcelService _excelService;

        public AdminTeacherController(IAdminTeacherService adminTeacherService, IWebHostEnvironment webHostEnvironment, IExcelService excelService)
        {
            this._rootPath = webHostEnvironment.WebRootPath;
            _adminTeacherService = adminTeacherService;
            _excelService = excelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            if (String.IsNullOrEmpty(search))
            {
                FileModeldto teachers = new FileModeldto()
                {
                    Teachers = await _adminTeacherService.GetAllAsync(new PaginationParams(page, _pageSize)),
                };
                ViewBag.HomeTitle = "Teacher";
                ViewBag.AdminTeacherSearch = search;
                return View("Index", teachers);
            }
            else
            {
                FileModeldto teachers = new FileModeldto()
                {
                    Teachers = await _adminTeacherService.SearchAsync(new PaginationParams(page, _pageSize), search)
                };
                ViewBag.HomeTitle = "Teacher";
                ViewBag.AdminTeacherSearch = search;
                return View("Index", teachers);
            }
            //return View("Index", (teachers, filemodeldto ));
        }

        [HttpGet("register")]
        public ViewResult Register() => View("Register");

        [HttpPost("register")]
        public async Task<IActionResult> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminTeacherService.RegisterTeacherAsync(teacherRegisterDto);
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

        [HttpGet("Delete")]
        public async Task<ViewResult> DeleteAsync(int id)
        {
            var teacher = await _adminTeacherService.GetByIdAsync(id);
            if (teacher != null)
            {
                return View("Delete", teacher);
            }
            return View("adminteacher");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTeacherAsync(int Id)
        {
            var res = await _adminTeacherService.DeleteAsync(Id);
            if (res) return RedirectToAction("index", "adminteacher", new { area = "" });
            return View();
        }

        [HttpGet("updateredirect")]
        public async Task<IActionResult> UpdateRedirectAsync(int teacherId)
        {
            var teacher = await _adminTeacherService.GetByIdAsync(teacherId);

            var dto = new TeacherUpdateDto()
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PhoneNumber = teacher.PhoneNumber,
                Image = teacher.ImagePath,
                BirthDate = teacher.BirthDate,
                Subject = teacher.Subject
            };

            ViewBag.HomeTittle = "Admin/Teacher/Update";
            ViewBag.teacherId = teacherId;
            return View("Update", dto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(int teacherId, TeacherUpdateDto dto)
        {
            var res = await _adminTeacherService.UpdateAsync(dto, teacherId);
            if (res)
            {
                return RedirectToAction("Index", "adminteachers", new { area = "" });
            }
            else return await UpdateRedirectAsync(teacherId);

        }

        [HttpGet("duplicate")]
        public async Task<IActionResult> Duplicate()
        {

            using (var stream = new FileStream(Path.Combine(_rootPath, "files", "template.xlsx"), FileMode.Open))
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
            if (ModelState.IsValid)
            {
                try
                {
                    List<TeacherRegisterDto> dtos = await _excelService.ReadTeacherFileAsync(filemodel);
                    
                    if (dtos.Count > 0) return View(dtos);

                    return RedirectToAction("Index", "adminteachers", new { area = "" });
                }
                catch (InvalidExcel ex)
                {
                    return BadRequest(ex.Mes);
                }
            }
            else
            {
                return RedirectToAction("Index", "adminteachers", new { area = "" });
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export(int page = 1)
        {
            List<TeacherViewDto> teachers = await _adminTeacherService.GetFileAllAsync();

            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Brands");
                
                worksheet.Cell("A1").Value = "Id";
                worksheet.Cell("B1").Value = "Full Name";
                worksheet.Cell("C1").Value = "Birth Data";
                worksheet.Cell("D1").Value = "Phone Number";
                worksheet.Cell("E1").Value = "Subject";
                worksheet.Cell("F1").Value = "Teacher Level";
                worksheet.Cell("G1").Value = "Part of Day";
                worksheet.Cell("H1").Value = "Work Days";
                worksheet.Row(1).Style.Font.Bold = true;

                //нумерация строк/столбцов начинается с индекса 1 (не 0)
                for (int i = 1; i <= teachers.Count; i++)
                {
                    var teach = teachers[i - 1];
                    worksheet.Cell(i + 1, 1).Value = teach.Id;
                    worksheet.Cell(i + 1, 2).Value = teach.FirstName + " " + teach.LastName;
                    worksheet.Cell(i + 1, 3).Value = teach.BirthDate;
                    worksheet.Cell(i + 1, 4).Value = teach.PhoneNumber;
                    worksheet.Cell(i + 1, 5).Value = teach.Subject;
                    worksheet.Cell(i + 1, 6).Value = teach.TeacherLevel;
                    worksheet.Cell(i + 1, 7).Value = teach.PartOfDay;
                    if (teach.WorkDays == true) worksheet.Cell(i + 1, 8).Value = "Daytime";
                    else worksheet.Cell(i + 1, 8).Value = "Night";
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"brands_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
    }
}
