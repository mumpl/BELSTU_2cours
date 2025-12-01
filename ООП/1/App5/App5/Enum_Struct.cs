using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App5
{
    public abstract partial class Software
    {
        public enum SoftwareType
        {
            TextProcessor,
            Game,
            Virus,
            Utility
        }
        public struct SystemRequirements
        {
            public string OS { get; set; }
            public int RAM { get; set; }
            public int Storage { get; set; }
            public SystemRequirements(string os, int ram, int storage)
            {
                OS = os;
                RAM = ram;
                Storage = storage;
            }
            public override string ToString()
            {
                return $"OS: {OS}, RAM: {RAM} MB, Storage: {Storage} MB";
            }
        }
    }
}
