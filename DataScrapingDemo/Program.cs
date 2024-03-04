using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Management;

namespace DataScrapingDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Entering Main Thread");
            Console.WriteLine("Main Thread Id " + Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                var thread = new Thread(NewThreadMethods);
                thread.IsBackground = true;
                thread.Start(i);
            }
            Console.WriteLine("Leaving Main Thread");*/

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.upstox.com/v2/market-quote/ltp");
            request.Headers.Add("Accept", "application/json");
            //var response = await client.SendAsync(request);
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response.Content.ReadAsStringAsync());
        }

        static void NewThreadMethods(object sent)
        {
            Console.WriteLine("Entering Thread " + Thread.CurrentThread.ManagedThreadId);
            long i = 0;
            while (true)
            {
                Console.WriteLine("Thread no. " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Leaving Thread");
            }
        }

        public void GetWifiData()
        {
            string strComputer = ".";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\wmi", "Select * From MSNdis_80211_ReceivedSignalStrength");

            foreach (ManagementObject obj in searcher.Get())
            {
                int intStrength = Convert.ToInt32(obj["NDIS80211ReceivedSignalStrength"]);

                string strBars;
                if (intStrength > -57)
                    strBars = "5 Bars";
                else if (intStrength > -68)
                    strBars = "4 Bars";
                else if (intStrength > -72)
                    strBars = "3 Bars";
                else if (intStrength > -80)
                    strBars = "2 Bars";
                else if (intStrength > -90)
                    strBars = "1 Bar";
                else
                    strBars = "Strength cannot be determined";

                Console.WriteLine(obj["InstanceName"] + " — " + strBars);
            }
        }
    }
}