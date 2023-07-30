namespace RegistanFerghanaLC.Service.ViewModels.SalaryViewModels
{
    public class SalaryBaseViewModel
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int LessonsNumber { get; set; }
        public double AverageRank { get; set; }
    }
}
