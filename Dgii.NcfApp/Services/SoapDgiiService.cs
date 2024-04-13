using RestClient.Net;
using RestClientDotNet;

namespace Dgii.NcfApp.Services
{
    public interface ISoapDgiiService
    {
        Task<string> GetNcfAsync(string rcn, string ncf);
    }


    public class SoapDgiiService : ISoapDgiiService
    {
        public SoapDgiiService()
        {

        }
        public async Task<string> GetNcfAsync(string rnc, string ncf)
        {
            string soapXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <GetNCF xmlns=""http://dgii.gov.do/"">
                                  <RNC>your_RNC_here</RNC>
                                  <NCF>your_NCF_here</NCF>
                                  <IMEI>your_IMEI_here</IMEI>
                                </GetNCF>
                              </soap:Body>
                            </soap:Envelope>";


            // Create RestClient instance
            var client = new RestClient();

            // Define the request options
            var requestOptions = new RequestOptions()
            {
                ContentType = "text/xml",
                Content = new StringContent(soapXml),
                Headers = 
            };
            requestOptions.Headers.Add("SOAPAction", "http://dgii.gov.do/GetNCF");

            // Send the POST request
            var response = await client.PostAsync("https://dgii.gov.do/wsMovilDGII/WSMovilDGII.asmx", requestOptions);

            // Ensure a successful response
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Output the response
                Console.WriteLine("Response:");
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }
        }
    }
}