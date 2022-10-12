using System.Net.Http;

namespace WebServiceClient
{
    public class Client
    {
        private static readonly Object s_lock = new Object();
        private static Client instance = null;

        private HttpClient client;

        private Client()
        {
           client = new HttpClient();
            client.BaseAddress = new Uri("http://databaseprovider:80");
        }

        public static Client Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                Monitor.Enter(s_lock);
                Client temp = new Client();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);
                return instance;
            }
        }

        public async Task<int> AddDocumentToQueue(string filename)
        {
            var result = await client.PostAsync($"/api/Queue/{filename}", null);
            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                return Convert.ToInt32(resultContent);
            }

            return -1;
           
        } 
    }
}
