namespace Lec03LibN
{
    public class SalaryForL3 : IFactory
    {
        private float A { get; set; }
        private float B { get; set; }
        public SalaryForL3(float A, float B)
        {
            this.A = A;
            this.B = B;
        }
        public IBonus getA(float cost1hour)
        {
            return new BonusAL3(cost1hour, A, B);
        }
        public IBonus getB(float cost1hour, float x)
        {
            return new BonusBL3(cost1hour, x, A, B);
        }
        public IBonus getC(float cost1hour, float x, float y)
        {
            return new BonusCL3(cost1hour, x, y, A, B);
        }
    }
}
