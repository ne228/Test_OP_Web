using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Test_OP_Web.Data;
using Test_OP_Web.Services;

namespace Test_OP_Web.Controllers
{

    public class VkServiceController : Controller
    {

        private readonly ILogger<VkServiceController> _logger;
        private VkNetMethods VkNetMethods;

        public VkServiceController(ILogger<VkServiceController> logger, VkNetMethods vkNetMethods)
        {
            _logger = logger;
            VkNetMethods = vkNetMethods;
        }

        [Authorize(Roles = "admin")]
        public string Index(string id, string message)
        {

            if (!VkNetMethods.BoolAuth)
                return "ошибка авторизации";

            try
            {
                VkNetMethods.Send(Convert.ToInt64(id), message);
                return "ok";
            }
            catch (Exception exc)
            {

                return exc.Message;
            }

        }


        public string GetPassword(string email)
        {
            if (!VkNetMethods.BoolAuth)
                return "ошибка авторизации";

            if (email == "" || email == null)
                return "Введите email";

            var users = DbInitializer.ParseUsersJson();

            var user = users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());

            if (user == null)
                return "нет user с таким email";

            try
            {
                VkNetMethods.Send(user.VkId, $"email\n{user.Email}\npass\n{user.Password}");
                return "сообщение отправлено вам на vk";
            }
            catch (Exception exc)
            {
                return "ошиюка " + exc.Message;
            }

        }


        [Authorize(Roles = "admin")]
        public string SendAll()
        {
            if (!VkNetMethods.BoolAuth)
                return "ошибка авторизации";

            var users = DbInitializer.ParseUsersJson();

            string res = "";

            foreach (var user in users)
            {
                string message = "я както писал прогу для тестов по ОП. Завтра видимо ОП.\n" +
                    "поэтому я ее запускаю\n" +
                    "сейчас там стоит авторизация по логину и паролю\n" +
                    "для взвода создал сам аккаунты и сгенерировал пароли\n" +
                    "конечно можете сами создать аккаунт, но думаю всем в падлу\n" +
                    "кому надо юзайте\n" +
                    "ссылку на сайт просите у меня, либо скину в группу\n";

                try
                {
                    message += $"\n{user.Email}\n{user.Password}";
                    VkNetMethods.Send(user.VkId, message);
                    //res += $"ok {user.Email}\n";

                    res += message + "\n\n";
                }
                catch (Exception exc)
                {
                    res += $"not ok {user.Email} + {exc.Message}\n";
                }

            }
            return res;
        }

    }
}
