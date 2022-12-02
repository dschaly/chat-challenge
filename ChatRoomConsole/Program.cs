// See https://aka.ms/new-console-template for more information
using ChatRoomConsole.Actions;
using ChatRoomConsole.Utils;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using HttpClient client = new();
        client.BaseAddress = new Uri(ClientUtils.BASE_ADRESS);

        await MenuActions.HandleMainMenuAsync(client);
        Console.ReadLine();
    }
}