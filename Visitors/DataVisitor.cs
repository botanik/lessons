using System;
using OpenHardwareMonitor.Hardware;
using NLog;

namespace botanikClient.Visitors
{
    public class DataVisitor : IVisitor
    {
        private ILogger Logger = LogManager.GetLogger("TempMon");

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
                Logger.Warn($"{sensor.Identifier} : {sensor.Name} : {sensor.Value}");
                Console.WriteLine($"{sensor.Identifier} : {sensor.Name} : {sensor.Value}");
            }
        }

        public void VisitParameter(IParameter parameter) { }
    }
}