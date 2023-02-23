namespace RegistanFerghanaLC.Service.Common.Exceptions;

public class NotFoundException: Exception
{
    public string Point { get; set; } = String.Empty;

    public NotFoundException(string point, string message)
        : base(message)
    {
        this.Point = point;
    }
}
