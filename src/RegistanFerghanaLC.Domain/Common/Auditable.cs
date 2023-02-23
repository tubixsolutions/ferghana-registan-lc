namespace RegistanFerghanaLC.Domain.Common
{
    public class Auditable : BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
