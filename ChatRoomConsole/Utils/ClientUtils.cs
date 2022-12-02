using System.Net.Http.Headers;

namespace ChatRoomConsole.Utils
{
    public static class ClientUtils
    {
        public static string BASE_ADRESS = "https://localhost:7073";

        public static void SetClientHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
