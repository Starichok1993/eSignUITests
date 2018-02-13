using eSignUITest.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            Console.WriteLine("Enter the number of tests examples: ");
            Int32.TryParse(Console.ReadLine(), out count);
            Console.WriteLine("The number is: " + count);

            var testManager = new LoadTestManager(count);
            testManager.RunTestsInThreads();

            Console.ReadLine();
        }

        public static async Task MainAsync()
        {
            var testManager = new LoadTestManager(3);
            await testManager.RunTestsAsync();
        }
    }
}
