using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.QRCode
{
    public class RegisterWebhook
    {
        private readonly HttpClient _httpClient;
        private readonly VietQrOptions _options;
        private readonly ILogger<RegisterWebhook> _logger;

        public RegisterWebhook(IOptions<VietQrOptions> options, HttpClient httpClient, ILogger<RegisterWebhook> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<string> RegiterWebHook(RegisterWebhookRequest request)
        {
            try
            {
                var registerUrl = "https://api.vietqr.io/v2/paymentGateway/confirmWebhook";

                var payload = new
                {
                    webhook_url = request.WebhookUrl
                };

                var jsonPayload = JsonConvert.SerializeObject(payload);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("x-client-id", _options.ClientId);
                _httpClient.DefaultRequestHeaders.Add("x-api-key", _options.ApiKey);

                var response = await _httpClient.PostAsync(registerUrl, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Webhook registered successfully.");
                    return "Success";
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to register webhook. Status code: {StatusCode}, Response: {ResponseContent}", response.StatusCode, responseContent);
                    return "Failed!";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "An HTTP error occurred while registering the webhook.");
                return "HTTP Error";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the webhook.");
                throw; // Re-throw the exception to be caught by the controller
            }
        }
    }
}
