using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App5
{
    public abstract partial class Software
    {
        public virtual void Run()
        {
            Console.WriteLine($"{Name} version {Version} is running");
        }
        public abstract void Shutdown();
        public override string ToString()
        {
            return $"ПО:{Name}, Версия:{Version}";
        }
    }
}
