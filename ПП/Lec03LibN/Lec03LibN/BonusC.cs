namespace Lec03LibN
{
    public class BonusC : IBonus
    {
        public float cost1hour { get; set; }
        private float x;
        private float y;
        public BonusC(float cost1hour, float x, float y)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.y = y;
        }
        public float calc(float number_hours)
        {
            return number_hours * cost1hour * x + y;
        }
    }
    public class BonusCL2 : IBonus
    {
        private float A;
        private float x;
        private float y;
        public float cost1hour { get; set; }
        public BonusCL2(float cost1hour, float x, float y, float A)
        {
            this.cost1hour = cost1hour;
            this.A = A;
            this.x = x;
            this.y = y;
        }
        public float calc(float number_hours)
        {
            return (number_hours + A) * cost1hour * x + y;
        }
    }
    public class BonusCL3 : IBonus
    {
        public float cost1hour { get; set; }
        private float x { get; set; }
        private float y { get; set; }
        private float A { get; set; }
        private float B { get; set; }
        public BonusCL3(float cost1hour, float x, float y, float A, float B)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.y = y;
            this.A = A;
            this.B = B;
        }
        public float calc(float number_hours)
        {
            return (number_hours + A) * (cost1hour + B) * x + y;
        }
    }
    /*public class BonusDL2 : IBonus
    {
        public float cost1hour { get; set; }
        private float x { get; set; }
        private float y { get; set; }
        private float A { get; set; }
        private float z { get; set; }
        public BonusDL2(float cost1hour, float A, float x, float y, float z)
        {
            this.cost1hour= cost1hour;
            this.x = x;
            this.y = y;
            this.z = z;
            this.A = A;
        }
        public float calc(float number_hours)
        {
            return (number_hours + A) * cost1hour * x + y * z;
        }

    }*/
    /*public class BonusL4 : IBonus
    {
        public float cost1hour { get; set; }
        private float A { get; set; }
        private float B { get; set; }
        private float C { get; set; }
        public BonusL4(float cost1hour, float A, float B, float C)
        {
            this.cost1hour= cost1hour;  
            this.A = A;
            this.B = B;
            this.C = C;
        }
        public float calc(float number_hours)
        {
            return (number_hours + A * C) * (cost1hour + B);
        }
    }*/
}
