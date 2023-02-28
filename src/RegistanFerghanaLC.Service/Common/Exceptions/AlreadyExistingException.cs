namespace RegistanFerghanaLC.Service.Common.Exceptions
{
    public class AlreadyExistingException: Exception
    {
        public string Point { get; set; } = String.Empty;

        public AlreadyExistingException(string point, string message)
            : base(message)
        {
            this.Point = point;
        }
    }
}
