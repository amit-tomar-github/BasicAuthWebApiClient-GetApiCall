using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuthWebApiClient
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Enter to continue, api calling");
            Console.ReadLine();
            //both are working
            //CallApi();
            CallApiClass();
           // CallApiNew();
            Console.ReadLine();
        }
        private static async void CallApi()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //Both are working unless schema which is Basic and Authorization is not forced in api
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "amit", "amit@123"))));
                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "amit", "amit@123"))));
                    // 2. Consume the GET command
                    var response = await client.GetAsync("http://localhost:53184/api/service/GetTestValue");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Result from GET command: " + resultString);
                    }
                    else
                        Console.WriteLine("Response Status: " + response.StatusCode);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }
        //Call get api with httpclient helper
        private static async void CallApiNew()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //Both are working unless schema which is Basic and Authorization is not forced in api
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "amit", "amit@123"))));
                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "amit", "amit@123"))));
                    // 2. Consume the GET command
                    //var response = await client.GetAsync("http://localhost:53184/api/service/GetTestValue");
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    var resultString = await response.Content.ReadAsStringAsync();
                    //    Console.WriteLine("Result from GET command: " + resultString);
                    //}
                    //else
                    //    Console.WriteLine("Response Status: " + response.StatusCode);

                    /*
                     * Above lines can be replaced with new helper method
                     * below method GetStringAsync will replace GetAsync and await response.Content.ReadAsStringAsync(); line
                     * below method will enforce IsSuccessCode so if success code does not return it will throw expection
                     */
                    string resultString = await client.GetStringAsync("http://localhost:53184/api/service/GetTestValue");
                    Console.WriteLine("Result from GET command: " + resultString);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }

        static async void CallApiClass()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization",
                        Convert.ToBase64String(Encoding.Default.GetBytes("amit:amit@123")));
            //Need to change the PORT number where your WEB API service is running
            // var result = await client.GetAsync("http://localhost:53184/api/service/GetValue");
            var result = await client.GetAsync("http://localhost/AuthApi/api/service/GetValue");

            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("Done " + result.StatusCode);
                var JsonContent = result.Content.ReadAsStringAsync().Result;
                List<Part> empList = JsonConvert.DeserializeObject<List<Part>>(JsonContent);
                foreach (var emp in empList)
                {
                    Console.WriteLine("BackNo = " + emp.BackNo + " Description = " + emp.Description);
                }
            }
            else
                Console.WriteLine("Error " + result.StatusCode);
        }
    }
    public class Part
    {
        public string BackNo { get; set; }
        public string Description { get; set; }
        public int StandardBinQty { get; set; }
        public string PartNo { get; set; }
        public string CustomerPartNo { get; set; }
        public bool IsBarcodeAvailable { get; set; }
        public string KanBanBarcode { get; set; }
        public string Response { get; set; }
        public string ErrorMessage { get; set; }
    }
}


