using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SWD.OrderingKioskSystem.Application.QRCode;
using SWD.OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.QRCode
{
    public class VietQrService : IVietQrService
    {
        private readonly HttpClient _httpClient;
        private readonly VietQrOptions _options;

        public VietQrService(IOptions<VietQrOptions> options, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<string> GeneratePaymentQrCode(string accountNo, string accountName, int acqId, int amount, string addInfo, string format = "text", string template = "compact")
        {
            var requestBody = new
            {
                accountNo,
                accountName,
                acqId,
                amount,
                addInfo,
                format,
                template
            };

            var requestJson = JsonConvert.SerializeObject(requestBody);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-client-id", _options.ClientId);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _options.ApiKey);

            var response = await _httpClient.PostAsync("https://api.vietqr.io/v2/generate", requestContent);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return responseObject.data.qrDataURL.ToString();
        }
    }
}
