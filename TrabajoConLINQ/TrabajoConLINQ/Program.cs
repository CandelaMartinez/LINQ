using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabajoConLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] valoresN = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine("numeros pares:");

            /* List<int> pares = new List<int>();

             foreach (int i in valoresN)
             {
                 if(i%2==0)
                 {
                     pares.Add(i);
                 }
             }*/

            IEnumerable<int> pares = from numero in valoresN where numero % 2 == 0 select numero;

            foreach (int i in pares)
            {
                Console.WriteLine(i);
            }
        }
    }
}
