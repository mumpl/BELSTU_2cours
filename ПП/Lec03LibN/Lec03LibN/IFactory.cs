namespace Lec03LibN
{
    public interface IFactory
    {
        IBonus getA(float cost1hour);
        IBonus getB(float cost1hour, float x);
        IBonus getC(float cost1hour, float x, float y);
        //IBonus getD(float cost1hour, float x, float y, float z);
        //IBonus getL4(float cost1hour, float a, float b, float c);
    }
}
