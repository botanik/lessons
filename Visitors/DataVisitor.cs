using System;
using OpenHardwareMonitor.Hardware;

namespace botanikClient.Visitors
{
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