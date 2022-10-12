using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HtmlPdfConverter
{
    internal class Client
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

        /*
         * Remove from queue
        */
        public async Task<string> GetCurrentDocument(int id)
        {
            var result = await client.GetAsync($"/api/Queue/{id}");
            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                return resultContent;
            }
            return null;
        }
        public async Task<string> GetNewDocument(int id)
        {
            var result = await client.PutAsync($"/api/Queue/{id}", null);
            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                return resultContent;
            }
            return null;
        }

        public async Task<string> RemoveDocument(int id)
        {
            var result = await client.DeleteAsync($"/api/Queue/{id}");
            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                return resultContent;
            }
            return null;
        }

    }

}
