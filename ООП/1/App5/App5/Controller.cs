using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App5
{
    public partial class Controller
    {
        private Computer computer;
        public Controller(Computer computer)
        {
            this.computer = computer;
        }
        public List<Game> FindGamesByType(Software.SoftwareType type)
        {
            var games = computer.GetSoftwareList().OfType<Game>().Where(game => game.Type == type).ToList();
            return games;
        }
        public TextProcessor FindTextProcessorByVersion(string version)
        {
            var textProcessor = computer.GetSoftwareList().OfType<TextProcessor>().FirstOrDefault(tp => tp.Version == version);
            return textProcessor;
        }
        public void PrintSoftwareAlphabet()
        {
            var sortedSoftware = computer.GetSoftwareList().OrderBy(software => software.Name).ToList();
            Console.WriteLine("ПО в алфавитном порядке: ");
            foreach (var software in sortedSoftware)
            {
                Console.WriteLine(software.ToString());
            }
        }
    }
}
