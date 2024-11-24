namespace StudentService.Client.Core.Utils
{
    public class HttpWebClient
    {
        private static HttpClient? _instance;

        public static HttpClient GetInstance(string baseUri)
        {
            if (_instance == null)
            {
                _instance = new HttpClient { BaseAddress = new Uri(baseUri) };
            }
            else if (_instance.BaseAddress == null || _instance.BaseAddress.OriginalString != baseUri)
            {
                _instance.BaseAddress = new Uri(baseUri);
            }

            return _instance;
        }

        public static HttpClient Create(string baseUri)
        {
            return new HttpClient { BaseAddress = new Uri(baseUri) };
        }
    }
}
