using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestProject
{
    public static class Counter
    {
        private static object syncronizer = new Object();
        private static int count;
        public static void Increment()
        {
            lock (syncronizer)
            {
                count++;
            }
        }
        public static void Decrement()
        {
            lock (syncronizer)
            {
                count--;
            }
        }
        public static int Value { get { return count; } set { } }
    }

}
