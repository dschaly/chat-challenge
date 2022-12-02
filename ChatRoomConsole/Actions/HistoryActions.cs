using ChatRoomConsole.DTOs.Responses;
using ChatRoomConsole.Enums;
using ChatRoomConsole.Utils;
using Newtonsoft.Json;

namespace ChatRoomConsole.Actions
{
    public static class HistoryActions
    {
        /// <summary>
        /// Displays the users history by minute
        /// </summary>
        /// <param name="client"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static async Task HandleDisplayActionsByMinuteAsync(HttpClient client, DateTime startDate, DateTime endDate)
        {
            var response = await client
                .GetAsync($"{ClientUtils.BASE_ADRESS}" +
                $"/ChatActions/get-history-by-minute?InitialDate={startDate}&EndDate={endDate}");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var roomActions = JsonConvert.DeserializeObject<List<ByMinuteActionHistoryResponse>>(resultString);

                // Display Logic
                if (roomActions.Any())
                {
                    Console.WriteLine($" \nHistory by minute from {startDate:MM/dd/yyyy} to {endDate:MM/dd/yyyy}\n");
                    foreach (var action in roomActions)
                    {
                        switch (action.ActionId)
                        {
                            case (int)ActionEnum.ENTER_THE_ROOM:
                                Console.WriteLine($"{action.ActionDate:MM/dd/yyyy HH:mm}: {action.UserName} enters the room.");
                                break;
                            case (int)ActionEnum.LEAVE_THE_ROOM:
                                Console.WriteLine($"{action.ActionDate:MM/dd/yyyy HH:mm}: {action.UserName} leaves.");
                                break;
                            case (int)ActionEnum.COMMENT:
                                Console.WriteLine($"{action.ActionDate:MM/dd/yyyy HH:mm}: {action.UserName} comments: \"{action.Comment}\"");
                                break;
                            case (int)ActionEnum.HIGH_FIVE:
                                Console.WriteLine($"{action.ActionDate:MM/dd/yyyy HH:mm}: {action.UserName} high-fives {action.HighFiveToName}.");
                                break;
                            default:
                                Console.WriteLine($"Invalid action on Id {action.Id}. Please, contact Suport.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" \nThe request yielded no results.");
                }
            }
            else
                Console.WriteLine(" \nSomething went wrong. Please, try again.");

            Console.WriteLine(" \nPress any key to continue...");
            Console.ReadKey(false);

            await MenuActions.HandleMainMenuAsync(client);
        }

        /// <summary>
        /// Displays the users history by hour
        /// </summary>
        /// <param name="client"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static async Task HandleDisplayActionsByHourAsync(HttpClient client, DateTime startDate, DateTime endDate)
        {
            var response = await client
                .GetAsync($"{ClientUtils.BASE_ADRESS}" +
                $"/ChatActions/get-history-by-hour?InitialDate={startDate}&EndDate={endDate}");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var roomActions = JsonConvert.DeserializeObject<List<ByHourActionHistoryResponse>>(resultString);

                // Display Logic
                if (roomActions.Any())
                {
                    Console.WriteLine($" \nHistory by hour from {startDate:MM/dd/yyyy} to {endDate:MM/dd/yyyy}\n");

                    foreach (var action in roomActions)
                    {
                        Console.WriteLine($" {action.HourPeriod:MM/dd/yyyy HH:mm}: \n");

                        if (action.EnteredPeopleCount > 0)
                            Console.WriteLine($" {action.EnteredPeopleCount} {(action.EnteredPeopleCount == 1 ? "person" : "people")} entered.");

                        if (action.LeftPeopleCount > 0)
                            Console.WriteLine($" {action.LeftPeopleCount} {(action.EnteredPeopleCount == 1 ? "person" : "people")} left.");

                        if (action.HighFivedFromPeopleCount > 0)
                            Console.WriteLine($" {action.HighFivedFromPeopleCount} {(action.EnteredPeopleCount == 1 ? "person" : "people")}" +
                                $" high-fived {action.HighFivedToPeopleCount} other {(action.EnteredPeopleCount == 1 ? "person" : "people")}");

                        if (action.CommentCount > 0)
                            Console.WriteLine($" {action.CommentCount} comments.");

                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine(" \nThe request yielded no results.");
                }
            }
            else
                Console.WriteLine(" \nSomething went wrong. Please, try again.");

            Console.WriteLine(" \nPress any key to continue...");
            Console.ReadKey(false);

            await MenuActions.HandleMainMenuAsync(client);
        }

    }
}
