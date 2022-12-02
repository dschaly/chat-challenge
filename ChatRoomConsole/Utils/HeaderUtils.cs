namespace ChatRoomConsole.Utils
{
    public static class HeaderUtils
    {
        public static void WriteHeader()
        {
            string header = "Welcome to the Chat Room Admin Area";
            string builtBy = "Built by Davidson Schaly\n\n";
            Console.SetCursorPosition((Console.WindowWidth - header.Length) / 2, Console.CursorTop);
            Console.WriteLine(header);
            Console.SetCursorPosition((Console.WindowWidth - builtBy.Length) / 2, Console.CursorTop);
            Console.WriteLine(builtBy);
        }

    }
}
