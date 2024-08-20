using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ChatPDFCom.Services
{
    public class ChatPDFService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ChatPDFService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ChatPDF:ApiKey"];
        }

        public async Task<string> UploadPdfAsync(Stream fileStream, string fileName)
        {
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            content.Add(fileContent, "file", fileName);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);

            var response = await _httpClient.PostAsync("https://api.chatpdf.com/v1/sources/add-file", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<dynamic>(responseData);
                return responseJson.sourceId;
            }
            else
            {
                throw new HttpRequestException($"Status: {response.StatusCode}, Error: {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<string> SendMessageAsync(string sourceId, string content)
        {
            var data = new
            {
                sourceId = sourceId,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = content
                    }
                }
            };

            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.PostAsync("https://api.chatpdf.com/v1/chats/message", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<dynamic>(responseData);
                return responseJson.content;
            }
            else
            {
                throw new HttpRequestException($"Status: {response.StatusCode}, Error: {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
