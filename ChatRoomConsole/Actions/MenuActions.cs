using ChatRoomConsole.Utils;

namespace ChatRoomConsole.Actions
{
    public static class MenuActions
    {
        /// <summary>
        /// Handles the first manu of the applcation
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task HandleMainMenuAsync(HttpClient client)
        {
            Console.Clear();
            HeaderUtils.WriteHeader();

            Console.WriteLine(" Select an option:");
            Console.WriteLine(" 1 - Chat Actions for Users");
            Console.WriteLine(" 2 - Display User's Registered Events\n");

            Console.WriteLine(" 0 - Exit");

            var mainMenuKeyPressed = Console.ReadKey(true);

            switch (mainMenuKeyPressed.Key)
            {
                case ConsoleKey.D1 or ConsoleKey.NumPad1:
                    await HandleChatActionMenuAsync(client);
                    break;
                case ConsoleKey.D2 or ConsoleKey.NumPad2:
                    await HandleDisplayRegistersMenuAsync(client);
                    break;
                case ConsoleKey.D0 or ConsoleKey.NumPad0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(" Invalid input. Press any key to try again.");
                    Console.ReadKey(false);

                    await HandleMainMenuAsync(client);
                    break;
            }
        }

        /// <summary>
        /// Handles the Chat Actions menu of the application
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task HandleChatActionMenuAsync(HttpClient client)
        {
            Console.Clear();
            HeaderUtils.WriteHeader();

            Console.WriteLine(" In this section, you can simulate the 4 user interactions with that chat.");
            Console.WriteLine(" Select an option:");
            Console.WriteLine(" 1 - Enter The Chat Room");
            Console.WriteLine(" 2 - Leave The Chat Room");
            Console.WriteLine(" 3 - Comment");
            Console.WriteLine(" 4 - High Five Another User\n");
            Console.WriteLine(" 5 - Go Back\n");

            Console.WriteLine(" 0 - Exit");

            var chatActionKeyPressed = Console.ReadKey(true);

            switch (chatActionKeyPressed.Key)
            {
                case ConsoleKey.D1 or ConsoleKey.NumPad1:
                    ClientUtils.SetClientHeaders(client);
                    await RoomActions.HandleEnterRoomActionAsync(client);
                    break;
                case ConsoleKey.D2 or ConsoleKey.NumPad2:
                    ClientUtils.SetClientHeaders(client);
                    await RoomActions.HandleLeaveRoomActionAsync(client);

                    break;
                case ConsoleKey.D3 or ConsoleKey.NumPad3:
                    ClientUtils.SetClientHeaders(client);
                    await RoomActions.HandleCommentActionAsync(client);
                    break;
                case ConsoleKey.D4 or ConsoleKey.NumPad4:
                    ClientUtils.SetClientHeaders(client);
                    await RoomActions.HandleHighFiveActionAsync(client);
                    break;
                case ConsoleKey.D5 or ConsoleKey.NumPad5:
                    await HandleMainMenuAsync(client);
                    break;
                case ConsoleKey.D0 or ConsoleKey.NumPad0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(" \nInvalid input. Press any key to try again.");
                    Console.ReadKey(false);

                    await HandleChatActionMenuAsync(client);
                    break;
            }
        }

        /// <summary>
        /// Handles the History fetch menu of the application
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task HandleDisplayRegistersMenuAsync(HttpClient client)
        {
            Console.Clear();
            HeaderUtils.WriteHeader();

            Console.WriteLine(" In this section, you can fetch the history of chat actions made by the users");
            Console.WriteLine(" Enter the Start Date of your history search: (mm-dd-yyyy)");
            var inputStartDate = Console.ReadLine();

            if (DateTime.TryParse(inputStartDate, out DateTime startDate))
            {
                Console.WriteLine(" \nEnter the End Date of your history search: (mm-dd-yyyy)");
                var inputEndDate = Console.ReadLine();

                if (DateTime.TryParse(inputEndDate, out DateTime endDate))
                {
                    Console.WriteLine(" \nOk, now, select the granularity of your search:");
                    Console.WriteLine(" 1 - History By Minute");
                    Console.WriteLine(" 2 - History By Hour");
                    Console.WriteLine(" 3 - Go back");

                    var chatActionKeyPressed = Console.ReadKey(true);

                    switch (chatActionKeyPressed.Key)
                    {
                        case ConsoleKey.D1 or ConsoleKey.NumPad1:
                            ClientUtils.SetClientHeaders(client);
                            await HistoryActions.HandleDisplayActionsByMinuteAsync(client, startDate, endDate);
                            break;
                        case ConsoleKey.D2 or ConsoleKey.NumPad2:
                            ClientUtils.SetClientHeaders(client);
                            await HistoryActions.HandleDisplayActionsByHourAsync(client, startDate, endDate);
                            break;
                        case ConsoleKey.D3 or ConsoleKey.NumPad3:
                            await HandleMainMenuAsync(client);
                            break;
                        case ConsoleKey.D0 or ConsoleKey.NumPad0:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine(" \nInvalid input. Press any key to try again.");
                            Console.ReadKey(false);

                            await HandleDisplayRegistersMenuAsync(client);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(" \nInvalid input. Plaese, try again.");
                }
            }
            else
            {
                Console.WriteLine(" \nInvalid input. Plaese, try again.");
            }

            await HandleDisplayRegistersMenuAsync(client);
        }
    }
}
