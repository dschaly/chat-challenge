using ChatRoomConsole.DTOs.Requests;
using ChatRoomConsole.DTOs.Responses;
using ChatRoomConsole.Utils;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ChatRoomConsole.Actions
{
    public static class RoomActions
    {
        /// <summary>
        /// Handles "enter the room" user action
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task HandleEnterRoomActionAsync(HttpClient client)
        {
            Console.WriteLine(" \nWhat is the Username to enter the Chat Room?");
            var userName = Console.ReadLine();

            var request = new EnterTheRoomRequest
            {
                UserName = userName
            };

            var jsonContent = JsonConvert.SerializeObject(request);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new
            MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("/ChatActions/enter-the-room", contentString);

            if (response.IsSuccessStatusCode)
                Console.WriteLine(" \nThe user entered the chat room successfully!");
            else
                Console.WriteLine(" \nSomething went wrong. Please, try again.");

            Console.WriteLine(" \nPress any key to continue...");
            Console.ReadKey(false);

            await MenuActions.HandleChatActionMenuAsync(client);
        }

        /// <summary>
        /// Handles "leave the room" user action
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task HandleLeaveRoomActionAsync(HttpClient client)
        {
            Console.WriteLine(" \nWhat is the UserId to leave the Chat Room?");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int userId))
            {
                var request = new LeaveTheRoomRequest
                {
                    UserId = userId
                };

                var jsonContent = JsonConvert.SerializeObject(request);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new
                MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("/ChatActions/leave-the-room", contentString);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine(" \nThe user left the chat room successfully!");
                else
                    Console.WriteLine(" \nSomething went wrong. Please, try again.");
            }
            else
            {
                Console.WriteLine(" \nInvalid input. Plaese, try again.");
            }

            Console.WriteLine(" \nPress any key to continue...");
            Console.ReadKey(false);

            await MenuActions.HandleChatActionMenuAsync(client);
        }

        /// <summary>
        /// Handles "comment" user action
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task HandleCommentActionAsync(HttpClient client)
        {
            var users = await GetAllUsersAsync(client);

            if (users.Any())
            {
                Console.WriteLine(" \n Online Registered users:");

                foreach (var user in users.Where(x => x.IsOnline))
                {
                    Console.WriteLine($" {user.Id} - {user.UserName}");
                }

                Console.WriteLine(" \nWhat is the User Id to Comment?");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int userId))
                {
                    Console.WriteLine(" \nPlease, enter the message:");
                    var message = Console.ReadLine();

                    var request = new CommentRequest
                    {
                        UserId = userId,
                        Comment = message
                    };

                    var jsonContent = JsonConvert.SerializeObject(request);
                    var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    contentString.Headers.ContentType = new
                    MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync("/ChatActions/comment", contentString);

                    if (response.IsSuccessStatusCode)
                        Console.WriteLine(" \nThe user has commented successfully!");
                    else
                        Console.WriteLine(" \nSomething went wrong. Please, try again.");
                }
                else
                {
                    Console.WriteLine(" \nInvalid input. Plaese, try again.");
                }
            }
            else
            {
                Console.WriteLine(" \nThere are no users registered to comment.");
            }


            Console.WriteLine(" \nPress any key to continue...");
            Console.ReadKey(false);

            await MenuActions.HandleChatActionMenuAsync(client);
        }

        /// <summary>
        /// Handles "high-five" user action
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task HandleHighFiveActionAsync(HttpClient client)
        {
            var users = await GetAllUsersAsync(client);

            if (users.Any())
            {
                Console.WriteLine(" \n Online Registered users:");

                foreach (var user in users.Where(x => x.IsOnline))
                {
                    Console.WriteLine($" {user.Id} - {user.UserName}");
                }

                Console.WriteLine(" \nWhat is the UserId who will send the High Five?");
                var firstInput = Console.ReadLine();

                if (int.TryParse(firstInput, out int userIdFrom))
                {
                    Console.WriteLine(" \nWhat is the UserId who will send the High Five?");
                    var secondInput = Console.ReadLine();

                    if (int.TryParse(secondInput, out int userIdTo))
                    {

                        var request = new HighFiveRequest
                        {
                            UserIdFrom = userIdFrom,
                            UserIdTo = userIdTo,
                        };

                        var jsonContent = JsonConvert.SerializeObject(request);
                        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        contentString.Headers.ContentType = new
                        MediaTypeHeaderValue("application/json");

                        var response = await client.PostAsync("/ChatActions/high-five", contentString);

                        if (response.IsSuccessStatusCode)
                            Console.WriteLine(" \nThe user has sent the high five successfully!");
                        else
                            Console.WriteLine(" \nSomething went wrong. Please, try again.");
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
            }
            else
            {
                Console.WriteLine(" \nThere are no users registered to high-five.");
            }

            Console.WriteLine(" \nPress any key to continue...");
            Console.ReadKey(false);

            await MenuActions.HandleChatActionMenuAsync(client);
        }

        /// <summary>
        /// Fetches all registered users
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task<List<UserResponse>> GetAllUsersAsync(HttpClient client)
        {
            List<UserResponse> users;

            var response = await client
                .GetAsync($"{ClientUtils.BASE_ADRESS}" +
                $"/ChatActions/get-all-users");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<UserResponse>>(resultString);
            }
            else
            {
                users = new List<UserResponse>();
            }

            return users;
        }
    }
}
