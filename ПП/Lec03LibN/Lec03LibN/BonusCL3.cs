using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec03LibN
{
    public class BonusCL3 : IBonus
    {
        public float costOneHour { get; set; }
        private float x { get; set; }
        private float y { get; set; }
        private float A { get; set; }
        private float B { get; set; }

        public BonusCL3(float costOneHour, float x, float y, float A, float B)
        {
            this.costOneHour = costOneHour;
            this.x = x;
            this.y = y;
            this.A = A;
            this.B = B;
        }

        public float Calculate(float hoursOfWork)
        {
            return (hoursOfWork + A) * (costOneHour + B) * x + y;
        }
    }
}
