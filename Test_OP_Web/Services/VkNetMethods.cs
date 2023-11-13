using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Test_OP_Web.Logging;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
namespace Test_OP_Web.Services
{
    public class VkNetMethods
    {
        readonly VkApi vkapi = new VkApi();
        public bool BoolAuth;

        ILogger Logger { get; set; }
        public VkNetMethods(ILogger logger)
        {
            Logger = logger;
            try
            {
                vkapi.Authorize(new ApiAuthParams
                {
                    //Login = Login,
                    //Password = Password,
                    //Settings = Settings.All,
                    AccessToken = "vk1.a.69MZDriCxUUVk9_eiEJ9jAj5xSR9nKeppthvEuCK3K2RzEYEUeNMAozpfX4VpcfuBf70cFPsYjuQhKbzePB1zNl6GmXQOy3vfuVSX5NyJyWf43I8hCk4m0mq2tCI5V5-1c5QT4_W8Cw7H_Z_69auIFUs-PHoDX1_JXn5Il6MT26rNwsoNq0e8j8lBO3KKHoY_-K09NKHXr63LnVN-Ue9UQ",
                    Settings = Settings.All
                });
                BoolAuth = true;


            }
            catch
            {
                Logger.WriteLine("Ошибка авторизации");
                BoolAuth = false;
            }

        }

        public VkCollection<User> GetFriends(long UserId)
        {
            VkCollection<User> Friends = vkapi.Friends.Get(new FriendsGetParams
            {
                UserId = UserId,
                Fields = ProfileFields.FirstName | ProfileFields.LastName,
                Order = FriendsOrder.Name
            });

            for (int i = 0; i < Friends.Count(); i++)
            {
                Logger.WriteLine(i + " " + Friends[i].FirstName + " " + Friends[i].LastName);
            }
            return Friends;
        }
        public void Send(long UserId, string message)
        {
            try
            {
                var rand = new Random();
                int RandomId = rand.Next();
                vkapi.Messages.Send(new MessagesSendParams
                {
                    UserId = UserId,
                    Message = message,
                    RandomId = RandomId
                });
                Logger.WriteLine($"Message was sended to {UserId} by vkService");
            }
            catch (Exception exc)
            {
                throw new Exception($"ERROR!: Message not sended to {UserId} by vkService\n{exc.Message}");
            }


        }
        public void SendChat(long ChatId, string message)
        {
            var rand = new Random();
            int RandomId = rand.Next();
            vkapi.Messages.Send(new MessagesSendParams
            {
                ChatId = ChatId,
                Message = message,
                RandomId = RandomId
            });
        }
        public ReadOnlyCollection<ConversationAndLastMessage> GetMessage()
        {

            var message = vkapi.Messages.GetConversations(new GetConversationsParams
            {
                Filter = GetConversationFilter.Unread,
                Count = 1,

            }).Items;
            return message;
        }


    }
}
