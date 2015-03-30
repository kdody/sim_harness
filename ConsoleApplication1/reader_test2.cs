using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hats.Reader;

namespace reader_test2
{
    class reader_test2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter file name: ");
            string file = Console.ReadLine();
            Reader readConfig = new Reader();
            toVerify toBeChecked = readConfig.readFile(file);
            foreach(string item in toBeChecked.userNames)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
    }
}
