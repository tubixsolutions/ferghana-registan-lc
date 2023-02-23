using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
