using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Test_OP_Web;
using Test_OP_Web.Data;
using Test_OP_Web.Models;

namespace WebAxe.Data
{
    public class DbInitializer
    {

        static bool Right(string text)
        {
            if (text.Contains("*") || text.Contains("="))
                return true;
            else
                return false;
        }


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




        public static async Task RoleInitialize(UserManager<UserAxe> userManager, RoleManager<IdentityRole> roleManager)
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

            foreach (var item in Parse().Result)
            {
                await CreateUserWithoutEmailConfirm(userManager, roleManager, item.Email, item.Password, "user");
            }

        }


        public static void Initialize(OptionContext context)
        {
            //context.Database.EnsureDeleted();


            if (context.Database.CanConnect())
            {
                var sadads = "asd";
            }


            if (!context.Database.EnsureCreated())
                return;




            string path = @"C:\Users\smallaxe\source\repos\TEST_OP\TEST_OP\test.txt";

            int numVar = 0;
            int numQ = 1;


            List<Option> options = new();

            if (!File.Exists(path))
                return;

            string[] lines = File.ReadAllLines(path);
            // асинхронное чтение

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];


                if (line.Contains("ариант №"))
                {
                    numQ = 1;
                    numVar++;
                    options.Add(new Option() { NumVar = numVar });
                    continue;
                }

                if (line.Contains($"{numQ}.\t") || line.Contains($"{numQ}\t"))
                {
                    Question question = new Question();

                    question.QuestionString = line;

                    question.One = lines[i + 1];

                    question.Two = lines[i + 2];
                    question.Three = lines[i + 3];

                    if (lines[i + 4].Contains($"{numQ}.\t"))
                        question.Four = " ";
                    else
                        question.Four = lines[i + 4];


                    if (Right(question.One))
                    {
                        question.Right = 1;
                        question.One = question.One.Substring(1);
                    }

                    if (Right(question.Two))
                    {
                        question.Right = 2;
                        question.Two = question.Two.Substring(1);
                    }
                    if (Right(question.Three))
                    {
                        question.Right = 3;
                        question.Three = question.Three.Substring(1);
                    }
                    if (Right(question.Four))
                    {
                        question.Right = 4;
                        question.Four = question.Four.Substring(1);
                    }


                    question.NumVar = numVar;
                    question.NumQ = numQ;

                    options[numVar - 1].Questions.Add(question);
                    numQ++;

                }

            }

            context.Options.AddRange(options);

            context.SaveChanges();

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
                    string? line;
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
                            Email = str[4].ToLower()
                        }); ;

                    }
                }
            }

            return users;
        }
    }
}

