namespace Lec03LibN
{
    public class SalaryForL1 : IFactory
    {
        public IBonus getA (float cost1hour)
        {
            return new BonusA (cost1hour);
        }
        public IBonus getB (float cost1hour, float x)
        {
            return new BonusB (cost1hour, x);
        }
        public IBonus getC (float cost1hour, float x, float y)
        {
            return new BonusC (cost1hour, x, y);
        }
        /*public IBonus getL4(float cost1hour, float a, float b, float c)
        {
            return new BonusL4(cost1hour, a, b, c);
        }*/
    }
}
