using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace SWD.OrderingKioskSystem.Application.VnPay
{
    public class Util
    {
        public static string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        public static string GetIpAddress(HttpContext httpContext)
        {
            string ipAddress;
            try
            {
                ipAddress = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown") || ipAddress.Length > 45)
                    ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }

            return ipAddress;
        }
    }
}
