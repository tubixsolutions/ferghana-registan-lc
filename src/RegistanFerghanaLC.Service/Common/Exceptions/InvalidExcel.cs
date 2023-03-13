namespace RegistanFerghanaLC.Service.Common.Exceptions
{
    public class InvalidExcel : Exception
    {
        public string Mes { get; set; } = String.Empty;
        public InvalidExcel()
        {
            this.Mes = "Invalid excel table";
        }
    }
}
