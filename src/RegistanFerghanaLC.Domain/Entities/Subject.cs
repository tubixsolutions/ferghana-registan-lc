using RegistanFerghanaLC.Domain.Common;

namespace RegistanFerghanaLC.Domain.Entities
{
    public class Subject : Auditable
    {
        public string Name { get; set; } = String.Empty;
    }
}
