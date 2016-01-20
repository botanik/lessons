using System;
using OpenHardwareMonitor.Hardware;

namespace botanikTest
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

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (var subHardware in hardware.SubHardware)
                subHardware.Accept(this);
        }

        public void VisitSensor(ISensor sensor) { }

        public void VisitParameter(IParameter parameter) { }
    }

    public class DataVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Traverse(this);
        }

        public void VisitSensor(ISensor sensor)
        {
            if (sensor.SensorType == SensorType.Temperature)
            {
                Console.WriteLine($"{sensor.Identifier} : {sensor.Name} : {sensor.Value}");
            }
        }

        public void VisitParameter(IParameter parameter) { }
    }
}
