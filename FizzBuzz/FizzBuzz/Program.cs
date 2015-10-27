using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("GOODBYE");
            int i;
            for (i = 1; i <= 100; i++)
            {
                if ((i % 3) == 0)
                {
                    System.Console.Write("Fizz");
                }
                if((i % 5) == 0)
                {
                    System.Console.Write("Buzz");
                }
                if((i % 3) != 0 && (i % 5) != 0)
                {
                    System.Console.Write(i);
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("END");

            System.Console.WriteLine("cs");
        }
    }
}
