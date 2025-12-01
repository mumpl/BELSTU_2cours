namespace Lec03LibN
{
    public class BonusB : IBonus
    {
        private float x { get; set; }
        public float cost1hour { get; set; }
        public BonusB(float cost1hour, float x)
        {
            this.x = x;
            this.cost1hour = cost1hour;
        }
        public float calc(float number_hours)
        {
            return cost1hour * number_hours * x;
        }
    }
    public class BonusBL2 : IBonus
    {
        private float A;
        private float x;
        public float cost1hour { get; set; }

        public BonusBL2(float cost1hour, float x, float A)
        {
            this.cost1hour = cost1hour;
            this.A = A;
            this.x = x;
        }

        public float calc(float number_hours)
        {
            return (number_hours + A) * cost1hour * x;
        }
    }
    public class BonusBL3 : IBonus
    {
        public float cost1hour { get; set; }
        private float x { get; set; }
        private float A { get; set; }
        private float B { get; set; }

        public BonusBL3(float cost1hour, float x, float A, float B)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.A = A;
            this.B = B;
        }
        public float calc(float number_hours)
        {
            return (number_hours + A) * (cost1hour + B) * x;
        }
    }
}
