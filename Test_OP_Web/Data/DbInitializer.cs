using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Test_OP_Web.Data.Options;
using Test_OP_Web.Logging;

namespace Test_OP_Web.Data
{
    public class DbInitializer
    {
        static async Task CreateUserWithoutEmailConfirm(
            UserManager<UserAxe> userManager,
            RoleManager<IdentityRole> roleManager,
            string email,
            string password,
            string role)
        {

            if (await userManager.FindByNameAsync(email) == null)
            {
                UserAxe user = new UserAxe { Email = email, UserName = email };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await userManager.ConfirmEmailAsync(user, code);

                }
            }

        }




        public static async Task RoleInitialize(UserManager<UserAxe> userManager, RoleManager<IdentityRole> roleManager, ILogger logger)
        {

            //Roles
            string adminEmail = "potter27.05@mail.ru";
            string password = "123456Bb!";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }


            await CreateUserWithoutEmailConfirm(userManager, roleManager, adminEmail, password, "admin");
            logger.WriteLine($"User {adminEmail} was created with role \"admin\"");


            var usersInFile = ParseUsersJson();
            foreach (var item in usersInFile)
            {
                await CreateUserWithoutEmailConfirm(userManager, roleManager, item.Email, item.Password, "user");

                logger.WriteLine($"User {item.Email} was created with role \"user\"");
            }

        }

        public static void Initialize(OptionContext context, ILogger logger)
        {

            //await GetAnswerFronJson.GenerateTeuEstJson();

            if (context.Database.CanConnect())
            {
                logger.WriteLine("goo connect to db");



                if (context.Options.ToList().Count == 0)
                {
                    List<Option> options = ParserOptioncs.GetOptionsFromTxtFile();

                    context.Options.AddRange(options);

                }

                context.SaveChanges();
            }
        }


        static async Task<List<RegUser>> Parse()
        {
            //C:\Users\smallaxe\source\repos\Test_OP_Web\Test_OP_Web\wwwroot\users.json
            string path = "wwwroot\\users.txt";
            List<RegUser> users = new();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        Console.WriteLine(line);

                        var str = line.Split("\t");


                        users.Add(new RegUser()
                        {
                            UserName = str[0],
                            Password = str[1],
                            SurName = str[2],
                            Name = str[3],
                            Email = str[4].ToLower(),
                            VkId = Convert.ToInt64(str[5])

                        }); ;

                    }
                }
            }

            string jsonString = JsonSerializer.Serialize(users);

            using (StreamWriter writer = new StreamWriter("wwwroot\\users.json", false))
            {
                await writer.WriteLineAsync(jsonString);
            }

            return users;
        }

        public static List<RegUser> ParseUsersJson()
        {
            string path = Path.Combine("wwwroot/users.json");


            var jsonString = File.ReadAllText(path);
            var users =
                JsonSerializer.Deserialize<List<RegUser>>(jsonString);

            return users;

        }
    }
}

