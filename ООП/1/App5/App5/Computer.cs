using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App5
{
    public partial class Computer
    {

        private List<Software> softwareList;
        public List<Software> SoftwareList
        {
            get { return softwareList; }
            set { softwareList = value; }
        }
        public Computer()
        {
            softwareList = new List<Software>();
        }
        public void AddSoftware(Software software)
        {
            softwareList.Add(software);
            Console.WriteLine($"{software.Name} установлено на компьютер");
        }
        public void RemoveSoftware(Software software)
        {
            if (softwareList.Remove(software))
            {
                Console.WriteLine($"{software.Name} удалено");
            }
            else
            {
                Console.WriteLine($"{software.Name} not found.");
            }
        }
        public List<Software> GetSoftwareList()
        {
            return softwareList;
        }
        public void SetSoftwareList(List<Software> newSoftwareList)
        {
            softwareList = newSoftwareList;
        }
        public void PrintSoftwareList()
        {
            if (softwareList.Count == 0)
            {
                Console.WriteLine("На компьютере не установлено ПО");
                return;
            }
            Console.WriteLine("На компьютере установлено ПО: ");
            foreach (Software software in softwareList)
            {
                Console.WriteLine(software.ToString());
            }
        }
    }
}

