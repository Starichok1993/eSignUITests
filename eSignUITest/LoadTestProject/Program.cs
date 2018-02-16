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
            int type = 0;
            Console.WriteLine("Select type of test: 1) Wizard; 2) Resend; 3)ESignature");
            Int32.TryParse(Console.ReadLine(), out type);
            switch (type)
            {
                case 1:
                    {
                        int count = 0;
                        Console.WriteLine("Enter the number of tests examples: ");
                        Int32.TryParse(Console.ReadLine(), out count);
                        Console.WriteLine("The number is: " + count);

                        var testManager = new LoadTestManager(count);
                        testManager.RunTestsInThreads();
                        
                        break;
                    };
                case 2:
                    {
                        var testManager = new LoadTestManager(0);
                        testManager.RunResendTest();
                        break;
                    }
                case 3:
                    {
                        int count = 0;
                        Console.WriteLine("Enter the number of tests examples(50 max): ");
                        Int32.TryParse(Console.ReadLine(), out count);
                        if (count > 50) count = 50;
                        Console.WriteLine("The number is: " + count);

                        var testManager = new LoadTestManager(0);
                        testManager.RunESignatureTest(count);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Incorrect input;");
                        break;
                    }
            }
            
            Console.ReadLine();
        }

        public static async Task MainAsync()
        {
            var testManager = new LoadTestManager(3);
            await testManager.RunTestsAsync();
        }
    }
}
