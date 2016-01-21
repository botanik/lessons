using System;
using botanikClient.Visitors;
using OpenHardwareMonitor.Hardware;

namespace botanikClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var visitor = new DataVisitor();
            var updater = new UpdateVisitor();

            var comp = new Computer
            {
                CPUEnabled = true,
                GPUEnabled = true,
                MainboardEnabled = true,
                HDDEnabled = true,
                FanControllerEnabled = true,
                RAMEnabled = true
            };

            comp.Open();

            ConsoleKeyInfo key;
            do
            {
                Console.Clear();

                comp.Accept(updater);
                comp.Traverse(visitor);

                Console.WriteLine("Done! Press any key to repeat, <enter> to stop...");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Enter);

            comp.Close();
        }
    }
}
