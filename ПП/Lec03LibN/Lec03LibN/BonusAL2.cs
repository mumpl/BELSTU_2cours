using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec03LibN
{
    public class BonusAL2 : IBonus
    {
        public float costOneHour { get; set; }

        private float A { get; set; }

        public BonusAL2(float costOneHour, float A) { 
        this.costOneHour = costOneHour;
            this.A = A;
        }

        public float Calculate(float hoursOfWork)
        {
            return (hoursOfWork + A) * costOneHour;
        }
    }
}
