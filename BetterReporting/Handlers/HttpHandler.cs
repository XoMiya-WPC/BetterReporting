using System.Threading.Tasks;
using System.Net.Http;
using Exiled.API.Features;

namespace BetterReporting.Handlers
{
    public class HttpHandler
    {
        private readonly Plugin plugin;
        public HttpHandler(Plugin plugin) => this.plugin = plugin;

        private readonly HttpClient HttpClient = new HttpClient();

        public async Task Send(string url, StringContent data)
        {
            HttpResponseMessage responseMessage = await HttpClient.PostAsync(url, data);
            string responseMessageString = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
            {
                Log.Error($"[{(int)responseMessage.StatusCode} - {responseMessage.StatusCode}] A non-successful status code was returned by Discord when trying to post to webhook. Response Message: {responseMessageString} .");
                return;
            }

            if (plugin.Config.VerboseMode)
                Log.Info("Posted to Discord webhook successfully!");
        }
    }
}
