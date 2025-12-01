using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lec03LibN;

namespace PP03
{
    internal class Emploee
    {
        public IBonus bonus { get; private set; }
        public Emploee(IBonus bonus) {

            this.bonus = bonus;
        }

        public float CalculateBonus(float numberHours)
        {
            return bonus.Calculate(numberHours);
        }
    }
}
