namespace Lec03LibN
{
    public class SalaryForL2 : IFactory
    {
        private readonly float A;
        public SalaryForL2(float A)
        {
            this.A = A;
        }
        public IBonus getA(float cost1hour)
        {
            return new BonusAL2(cost1hour, A);
        }
        public IBonus getB(float cost1hour, float x)
        {
            return new BonusBL2(cost1hour, x, A);
        }
        public IBonus getC(float cost1hour, float x, float y)
        {
            return new BonusCL2(cost1hour, x, y, A);
        }
        /*public IBonus getD(float cost1hour, float y, float x, float z)
        {
            return new BonusDL2(cost1hour,  y, x, z, A);
        }*/
    }
}
