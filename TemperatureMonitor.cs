
using System;
using System.Timers;
using OpenHardwareMonitor.Hardware;
using botanikClient.Visitors;

namespace botanikClient
{
    public class TemperatureMonitor
    {
        public Computer Computer;

        private IVisitor DataVisitor = new DataVisitor();
        private IVisitor UpdateVisitor = new UpdateVisitor();
        private Timer Timer;

        public void Start()
        {
            Computer = new Computer()
            {
                CPUEnabled = true,
                FanControllerEnabled = true,
                GPUEnabled = true,
                HDDEnabled = true,
                MainboardEnabled = true,
                RAMEnabled = true
            };
            Computer.Open();
            Timer = new Timer(60000);
            Timer.Elapsed += Update;
            Timer.Start();
            
        }

        public void Stop()
        {
            Timer.Stop();
            Computer.Close();
            
        }

        private void Update(Object source, ElapsedEventArgs e)
        {
            Computer.Accept(UpdateVisitor);
            Computer.Traverse(DataVisitor);
        }
    }
}
