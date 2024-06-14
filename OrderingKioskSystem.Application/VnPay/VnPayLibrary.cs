using Microsoft.Extensions.Configuration;
using System.Globalization;

using System.Net;
using System.Text;


namespace SWD.OrderingKioskSystem.Application.VnPay
{
    public class VnPayLibrary
    {
        private readonly IConfiguration _configuration;
        public VnPayLibrary(IConfiguration configuration) {
            _configuration = configuration;
        }
        public const string VERSION = "2.1.0";
        private SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
        private SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());
        //------------------------REQUEST DATA----------------------------------------
        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (_requestData.ContainsKey(key))
                {
                    _requestData[key] = value; // Update the value if the key already exists
                }
                else
                {
                    _requestData.Add(key, value); // Add the key-value pair if the key doesn't exist
                }
            }
        }
        public string CreatePaymentUrl(string baseUrl, string vnp_HashSecret, string returnUrl)
        {   
            
            _requestData.Clear(); // Clear existing data
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var timenow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            AddRequestData("vnp_Version", VERSION);
            AddRequestData("vnp_Command", "pay");
            AddRequestData("vnp_TmnCode", _configuration.GetSection("VNPay").GetValue<string>("TmnCode")); 
            AddRequestData("vnp_Locale", "vn");
            AddRequestData("vnp_CurrCode", "VND");
            AddRequestData("vnp_CreateDate", timenow.ToString("yyyyMMddHhmmss"));
            AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString());
            AddRequestData("vnp_OrderInfo", "Payment for Order ID: " + DateTime.Now.Ticks);
            AddRequestData("vnp_OrderType", "other");
            AddRequestData("vnp_Amount", (1000000 * 100).ToString()); // Amount in VND
            AddRequestData("vnp_ReturnUrl", returnUrl);
            AddRequestData("vnp_IpAddr", "127.0.0.1");

            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string queryString = data.ToString();

            baseUrl += "?" + queryString;
            String signData = queryString;
            if (signData.Length > 0)
            {
                signData = signData.Remove(data.Length - 1, 1);
            }
            string vnp_SecureHash = Util.HmacSHA512(vnp_HashSecret, signData);
            baseUrl += "vnp_SecureHash=" + vnp_SecureHash;

            return baseUrl;
        }

        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            _requestData.Clear(); // Clear existing data

            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string queryString = data.ToString();

            baseUrl += "?" + queryString;
            String signData = queryString;
            if (signData.Length > 0)
            {
                signData = signData.Remove(data.Length - 1, 1);
            }
            string vnp_SecureHash = Util.HmacSHA512(vnp_HashSecret, signData);
            baseUrl += "vnp_SecureHash=" + vnp_SecureHash;

            return baseUrl;
        }



        #region Request



        #endregion

        #region Response
        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }

        public string GetResponseData(string key)
        {
            string retValue;
            if (_responseData.TryGetValue(key, out retValue))
            {
                return retValue;
            }
            else
            {
                return string.Empty;
            }
        }
        public bool ValidateSignature(string inputHash, string secretKey)
        {
            string rspRaw = GetResponseData();
            string myChecksum = Util.HmacSHA512(secretKey, rspRaw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
        private string GetResponseData()
        {

            StringBuilder data = new StringBuilder();
            if (_responseData.ContainsKey("vnp_SecureHashType"))
            {
                _responseData.Remove("vnp_SecureHashType");
            }
            if (_responseData.ContainsKey("vnp_SecureHash"))
            {
                _responseData.Remove("vnp_SecureHash");
            }
            foreach (KeyValuePair<string, string> kv in _responseData)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            //remove last '&'
            if (data.Length > 0)
            {
                data.Remove(data.Length - 1, 1);
            }
            return data.ToString();
        }



        #endregion
    }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}
