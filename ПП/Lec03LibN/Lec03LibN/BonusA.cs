namespace Lec03LibN
{
    public class BonusA : IBonus
    {
        public float cost1hour { get; set; }

        public BonusA (float cost1hour)
        {
            this.cost1hour = cost1hour;
        }
        public float calc(float number_hours)
        {
            return cost1hour * number_hours;
        }
    }
    public class BonusAL2 : IBonus
    {
        public float cost1hour { get; set; }
        private float A { get; set; }

        public BonusAL2(float cost1hour, float A)
        {
            this.cost1hour = cost1hour;
            this.A = A;
        }
        public float calc(float number_hours)
        {
            return (number_hours + A) * cost1hour;
        }
    }
    public class BonusAL3 : IBonus
    {
        private float A { get; set; }
        private float B { get; set; }
        public float cost1hour { get; set; }
        public BonusAL3(float cost1hour, float A, float B)
        {
            this.cost1hour = cost1hour;
            this.A = A;
            this.B = B;
        }
        public float calc(float number_hours)
        {
            return (number_hours + A) * (cost1hour + B);
        }
    }
}
