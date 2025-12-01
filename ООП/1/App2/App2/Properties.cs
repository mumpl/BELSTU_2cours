using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App2
{
    public partial class Vector
    {
        //свойство для поля id
        public int ID
        {
            get { return id; }
        }

        //свойство для доступа к элементам
        public int[] Elements
        {
            get { return elements; } //возвращает текущее значение массива,  что позволяет его читать извне
            private set { elements = value; }//доступ только внутри класса    
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public int ErrorCode
        {
            get { return errorCode; }
        }
    }
}
