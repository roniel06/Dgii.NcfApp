using System.Xml.Serialization;
using Dgii.NcfApp.Helpers;
using Dgii.NcfApp.Models;
using Microsoft.AspNetCore.SignalR.Protocol;
using RestSharp;

namespace Dgii.NcfApp.Services
{
    public interface ISoapDgiiService
    {
        Task<NcfResult> GetNcfAsync(string rnc, string ncf);
        Task<RncResult> GetRNC(string rnc);
        Task<GetPlacaResponse> GetPlacaResult(string rnc, string placa);
    }


    public class SoapDgiiService : ISoapDgiiService
    {
        private IDgiiHistoryService _dgiiHistoryService
            ;
        public SoapDgiiService(IDgiiHistoryService dgiiHistoryService)
        {
            _dgiiHistoryService = dgiiHistoryService;
        }
        public async Task<NcfResult> GetNcfAsync(string rnc, string ncf)
        {
            // Define the SOAP XML content
            string soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                    <soap:Body>
                                        <GetNCF xmlns=""http://dgii.gov.do/"">
                                            <RNC>{rnc}</RNC>
                                            <NCF>{ncf}</NCF>
                                            <IMEI>your_IMEI_here</IMEI>
                                        </GetNCF>
                                    </soap:Body>
                                </soap:Envelope>";

            // Create RestClient instance
            var client = new RestClient("https://dgii.gov.do/wsMovilDGII/WSMovilDGII.asmx");

            // Create RestRequest instance
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://dgii.gov.do/GetNCF");
            request.AddParameter("text/xml", soapXml, ParameterType.RequestBody);

            // Execute the request
            var response = await client.ExecuteAsync(request);

            // Check response status
            if (response.IsSuccessful)
            {
                try
                {
                    NcfResult ncfResult = XmlClassHelper.DeserializeXml<NcfResult>(response.Content, "GetNCFResult");
                    await _dgiiHistoryService.CreateNcfHistory(ncfResult);
                    return ncfResult;
                }
                catch
                {
                    return null;
                }

            }
            else
            {

                // Cambia esto para que devuelva algun error
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                return null;
            }
        }

        public async Task<GetPlacaResponse> GetPlacaResult(string rnc, string placa)
        {
            var soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                             <soap12:Envelope xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                 <soap12:Body>
                                     <GetPlaca xmlns=""http://dgii.gov.do/"">
                                         <RNC>{rnc}</RNC>
                                         <Placa>{placa}</Placa>
                                         <IMEI></IMEI>
                                     </GetPlaca>
                                 </soap12:Body>
                             </soap12:Envelope>";

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://dgii.gov.do/wsMovilDGII/WSMovilDGII.asmx");
                request.Content = new StringContent(soapXml, System.Text.Encoding.UTF8, "application/soap+xml");

                request.Headers.Add("SOAPAction", "http://dgii.gov.do/GetPlaca");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var result = XmlClassHelper.MapXmlToGetPlacaResult(responseContent);
                        await _dgiiHistoryService.CreatePlacaHistory(result);
                        return result;
                    }
                    catch (Exception)
                    {

                        return null;
                    }

                }
                else
                {
                    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                    return null;
                }
            }
        }
        public async Task<RncResult> GetRNC(string rnc)
        {
            var soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                                  <soap12:Body>
                                    <GetContribuyentes xmlns=""http://dgii.gov.do/"">
                                      <value>{rnc}</value>
                                      <IMEI></IMEI>
                                    </GetContribuyentes>
                                  </soap12:Body>
                                </soap12:Envelope>";

            // Create RestClient instance
            var client = new RestClient("https://dgii.gov.do/wsMovilDGII/WSMovilDGII.asmx");

            // Create RestRequest instance
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://dgii.gov.do/GetContribuyentes");
            request.AddParameter("text/xml", soapXml, ParameterType.RequestBody);

            // Execute the request
            var response = await client.ExecuteAsync(request);

            // Check response status
            if (response.IsSuccessful)
            {
                var rncResult = XmlClassHelper.DeserializeXml<RncResult>(response.Content, "GetContribuyentesResult");
                return rncResult;
            }
            else
            {

                // Cambia esto para que devuelva algun error
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                return null;
            }
        }
    }
}