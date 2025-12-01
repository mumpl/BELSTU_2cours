using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace App2
{
    public partial class Vector
    {
        //метод для вызова закрытого конструктора через фабричный метод
        public static Vector CreaterandomVector()
        {
            return new Vector(true);
        }

        //метод для получения кол-ва объектов
        public static int GetObjectCount()
        {
            return count;
        }

        //индексатор
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                {
                    errorCode = IndexOfError;// код ошибки при выходе за пределы
                    throw new IndexOutOfRangeException("Индекс вне диапазона");
                }
                errorCode = NoError; //если ошибок нет
                return elements[index];
            }
            set
            {
                if ((index < 0 || index >= size))
                {
                    errorCode = IndexOfError;
                    throw new IndexOutOfRangeException("Индекс вне диапазона");
                }
                elements[index] = value;
                errorCode = NoError; //сброс ошибки при успешном выборе занчения
            }
        }

        public void Add(ref int number, out int result)
        {
            result = 0;
            for (int i = 0; i < size; i++)
            {
                elements[i] += number;
                result += elements[i];
            }
        }

        public void Multipy(int number)
        {
            for (int i = 0; i < size; i++)
            {
                elements[i] *= number;
            }
        }

        public bool ContainZero()
        {
            for (int i = 0; i < size; i++)
            {
                if (elements[i] == 0)
                    return true;
            }
            return false;
        }

        public int GetSumOfAbs()
        {
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += Math.Abs(elements[i]);
            }
            return sum;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vector))
                return false;

            Vector other = (Vector)obj;
            if (this.size != other.size)
                return false;
            for (int i = 0; i < this.size; i++)
            {
                if (this.elements[i] != other.elements[i])
                    return false;
            }
            return true;
        }

        //переопределение GetHashCode
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + size.GetHashCode();
            foreach (int elem in elements)
            {
                hash = hash * 23 + elem.GetHashCode();
            }
            return hash;
        }

        //переопределение ToString  -  вывод инф-и об объекте
        public override string ToString()
        {
            return $"Vector [ID = {id}, Size = {size}, Elements = {string.Join(", ", elements)}]";
        }

        //метод для отображения элементов
        public void Display()
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(elements[i]);
            }
        }
    }
}
