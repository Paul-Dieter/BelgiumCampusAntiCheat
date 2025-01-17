using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BelgiumCampusAntiCheat.Operations
{
    internal class RefresherCounter
    {
        public delegate void EventHandler();
        public static event EventHandler OnRefresh;

        static int  counter = 0;

        public static void UpdateCounter()
        {
            counter++;
            Console.WriteLine($"DataMonitor {counter} ");
        }

        public static void InvokeEventHandler()
        {
            OnRefresh?.Invoke();
        }

    }
}
