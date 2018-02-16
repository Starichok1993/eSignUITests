using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LoadTestProject;

namespace eSignUITest.Tests
{
    public class LoadTestManager
    {
        List<WizardLoadTestClass> wizardTestsList;
        List<ESignatureTestClass> eSignatureTestsList;
        ResendTestClass resendTest;

        //"http://applicintweb.com/NWM_ExamOne_B2/App/ESignConfirmationPage?AskSSN=true&AppID=7f6c5933-632e-4284-b233-bb08172d125c";
        //"http://applicintweb.com/Sammons_B2/CallCenter/Instructions?AskSSN=true&AppID=bfaf9101-b75a-44da-a4dc-86d199c37984";


        public LoadTestManager(int count)
        {
            wizardTestsList = new List<WizardLoadTestClass>();
            for (var i = 0; i < count; i++)
            {
                wizardTestsList.Add(new WizardLoadTestClass
                {
                    StartPageUrl = "http://192.168.100.232/APP_MVC",
                    StartInterviewURI = "/CallCenter/Instructions?AskSSN=true&AppID=",
                    CaseGUID = "c08bca4d-00b6-4739-8aba-364db7045296"
                });
                //StartPageUrl = "http://192.168.100.135:11222/NWM_APPS_B2",
                //StartInterviewURI = "/App/ESignConfirmationPage?AskSSN=true&AppID=" ,
                //CaseGUID = "1a6f27b6-1c6b-493d-af1a-78b236ad9e1c" });
            }
            //http://192.168.100.232/APP_MVC/CallCenter/Instructions?AskSSN=true&AppID=c08bca4d-00b6-4739-8aba-364db7045296
            resendTest = new ResendTestClass();
            eSignatureTestsList = new List<ESignatureTestClass>();
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

            while (true)
            {
                Console.WriteLine("Active tests: " + Counter.Value);
                Thread.Sleep(5000);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            return;
        }

        public void RunResendTest()
        {
            resendTest.StartPageUrl = "http://192.168.100.135:11222/NWM_APPS_B2";
            resendTest.RunTest();
        }

        private List<string> GetCaseGuidsFromFile()
        {

            var guids = new List<String>();
            var file = System.IO.File.OpenText("../../GuidsForESignTest.txt");

            while (!file.EndOfStream)
            {
                guids.Add(file.ReadLine());
            }
            return guids;
        }

        public void RunESignatureTest(int count)
        {
            var caseGuids = GetCaseGuidsFromFile();
            if (count > caseGuids.Count) count = caseGuids.Count;

            for (var i = 0; i < count; i++)
            {
                eSignatureTestsList.Add(new ESignatureTestClass
                {
                    StartPageUrl = "http://192.168.100.135:11222/NWM_APPS_B2",
                    StartInterviewURI = "/App/ESignConfirmationPage?AskSSN=true&AppID=",
                    CaseGUID = caseGuids.ElementAt(i)
                });
            }
            var threads = new List<Thread>();

            foreach (var test in eSignatureTestsList)
            {
                threads.Add(new Thread(test.RunTest));
                threads.Last().Start();
                Thread.Sleep(2000);
            }
            while (true)
            {
                Console.WriteLine("Active tests: " + Counter.Value);
                Thread.Sleep(5000);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            return;


        }
    }
}
