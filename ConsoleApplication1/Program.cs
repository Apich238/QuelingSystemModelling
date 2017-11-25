using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var v = ContinuousRandomVar.STD();
            double s = 0;
            for(int i = 0; i < 20; i++)
            {
                var t = v.Measure();
                Console.WriteLine(t);
                s += t;
            }
            Console.WriteLine("----------------------------");
            Console.ReadKey();
        }
    }
}
