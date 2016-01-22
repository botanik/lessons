using System;
using botanikClient.Visitors;
using OpenHardwareMonitor.Hardware;
using Topshelf;

namespace botanikClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // PLUS TOPSHELF

            HostFactory.Run(x =>
            {
                x.Service<TemperatureMonitor>(s =>
                {
                    s.ConstructUsing(name => new TemperatureMonitor());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Monitoring the temp of CPU and another temp sensor's on our PC");
                x.SetDisplayName("TemperatureMonitor");
                x.SetServiceName("TempMon");
            });

            // W/O TOPSHELF

            //var tm = new TemperatureMonitor();
            //tm.Start();

            //Console.WriteLine("Press any key to stop...");
            //Console.ReadKey();

            //tm.Stop();
        }
    }
}
