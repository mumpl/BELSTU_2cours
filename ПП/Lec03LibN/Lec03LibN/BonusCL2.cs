using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec03LibN
{
    internal class BonusCL2 : IBonus
    {
        private float A;
        private float x;
        private float y;
        public float costOneHour { get; set; }

        public BonusCL2(float costOneHour, float x, float y, float A)
        {
            this.costOneHour = costOneHour;
            this.A = A;
            this.x = x;
            this.y = y;
        }

        public float Calculate(float hoursOfWork)
        {
            return (hoursOfWork + A) * costOneHour * x + y;
        }
    }
}
