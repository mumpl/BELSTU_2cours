using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App5
{
    public partial class Exceptions
    {
        public class SoftwareException : Exception
        {
            public SoftwareException() { }
            public SoftwareException(string message) : base(message) { }
            public SoftwareException(string message, Exception inner) : base(message, inner) { }
        }
        public class VirusDetectedException : SoftwareException
        {
            public VirusDetectedException() : base("Обнаружен вирус!") { }
            public VirusDetectedException(string message) : base(message) { }
        }
        public class IncompatibleSoftwareException : SoftwareException
        {
            public IncompatibleSoftwareException(string softwareName, string os) : base($"Программное обеспечение {softwareName} не совместимо с ОС {os}.") { }
        }
        public class GameNotFoundException : SoftwareException
        {
            public GameNotFoundException() : base("Игра не найдена!") { }
        }
        public class InvalidSoftwareDataException : SoftwareException
        {
            public InvalidSoftwareDataException(string message) : base(message) { }
        }
    }
}
