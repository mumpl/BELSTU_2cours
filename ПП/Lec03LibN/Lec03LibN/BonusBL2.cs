using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec03LibN
{
    internal class BonusBL2 : IBonus
    {
        private float A;
        private float x;
        public float costOneHour { get; set; }

        public BonusBL2(float costOneHour, float x, float A) { 
            this.costOneHour = costOneHour;
            this.A = A;
            this.x = x;
        }

        public float Calculate(float hoursOfWork)
        {
            return costOneHour * (hoursOfWork + A) * x;
        }
    }
}
