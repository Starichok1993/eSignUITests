using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace eSignUITest.Tests
{
    public class LoadTestManager
    {
        List<WizardLoadTestClass> wizardTestsList;

        public LoadTestManager(int count)
        {
            wizardTestsList = new List<WizardLoadTestClass>();
            for (var i = 0; i < count; i++)
            {
                wizardTestsList.Add(new WizardLoadTestClass());
            }
        }

        public async Task RunTestsAsync()
        {
            var task = wizardTestsList.Select(o => o.RunTestAsync());
            
            var result = await Task.WhenAll(task);
            return;
        }

        public void RunTestsInThreads()
        {
            var threads = new List<Thread>();

            foreach (var test in wizardTestsList)
            {
                threads.Add(new Thread(test.RunTest));
                threads.Last().Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            return;
        }
    }
}
