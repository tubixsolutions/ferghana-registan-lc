using RegistanFerghanaLC.Domain.Constants;

namespace RegistanFerghanaLC.Service.Common.Helpers
{
    public class TimeHelper
    {
        public static DateTime GetCurrentServerTime()
        {
            var date = DateTime.UtcNow;
            return date.AddHours(TimeConstants.UTC);
        }
    }
}
