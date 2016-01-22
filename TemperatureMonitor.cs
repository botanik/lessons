
using System;
using System.Timers;
using OpenHardwareMonitor.Hardware;
using botanikClient.Visitors;

namespace botanikClient
{
    public class TemperatureMonitor
    {
        public Computer Computer { get; private set; }

        private readonly IVisitor _dataVisitor = new DataVisitor();
        private readonly IVisitor _updateVisitor = new UpdateVisitor();

        private Timer _timer;

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

            _timer = new Timer(60000);
            _timer.Elapsed += Update;

            Update(null, null);
            _timer.Start();            
        }

        public void Stop()
        {
            _timer.Stop();
            Computer.Close();            
        }

        private void Update(object source, ElapsedEventArgs e)
        {
            Computer.Accept(_updateVisitor);
            Computer.Traverse(_dataVisitor);
        }
    }
}
