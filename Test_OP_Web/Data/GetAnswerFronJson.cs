using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Data
{
    public class GetAnswerFronJson
    {
        public int NumVar { get; set; }

        public List<GetAnwserQuestion> Anwsers { get; set; }

        public async static Task GenerateTestJson()
        {

            var GetAnswerFronJson = new GetAnswerFronJson();
            GetAnswerFronJson.NumVar = 1;



            GetAnswerFronJson.Anwsers = new();
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 1, Answer = "2" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 2, Answer = "4" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 3, Answer = "2" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 4, Answer = "350" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 5, Answer = "1" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 6, Answer = "2" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 7, Answer = "2" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 8, Answer = "2" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 9, Answer = "4" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 10, Answer = "1,5" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 11, Answer = "4" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 12, Answer = "2" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 13, Answer = "2" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 14, Answer = "4" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 15, Answer = "1" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 16, Answer = "4" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 17, Answer = "зеленую" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 18, Answer = "2" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 19, Answer = "4" });
            GetAnswerFronJson.Anwsers.Add(new() { NumQ = 20, Answer = "2" });


            string jsonString = System.Text.Json.JsonSerializer.Serialize(GetAnswerFronJson);
            string path = @"C:\Users\smallaxe\Desktop\Вариант 1.txt";

            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] buffer = Encoding.Default.GetBytes(jsonString);
                // запись массива байтов в файл
                await fstream.WriteAsync(buffer, 0, buffer.Length);
                Console.WriteLine("Текст записан в файл");
            }

        }
        public string NormalizeJson(string input)
        {
            input = input.Replace("\r", "");
            input = input.Replace("\n", "");
            input = input.Replace(" ", "");
            input = input.Replace("\t", "");

            return input;
        }
        public GetAnswerFronJson(string path)
        {
            string json = "";
            try
            {
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();

                }
                //json = Encoding.UTF8.GetString(jsonbyte);

                //json = NormalizeJson(json);
                var root = JsonSerializer.Deserialize<GetAnswerFronJson>(json);
                NumVar = root.NumVar;
                Anwsers = root.Anwsers;
            }
            catch (Exception exc)
            {

                throw;
            }


        }


        public GetAnswerFronJson()
        {

        }

    }

    public class GetAnwserQuestion
    {
        public int NumQ { get; set; }
        public string Answer { get; set; }

        public List<Anwser> ToAnswer()
        {
            Anwser answer = new();


            int tryToInt = -100;

            try
            {
                tryToInt = Convert.ToInt32(Answer);
            }
            catch (Exception)
            {


            }


            // Там где Right == true, это варинаты ответов. где нету - ответ это строка!
            // Ответ либо равен одному числу либо многоразрядному числу
            if (tryToInt != -100)
                if (tryToInt >= 10)
                {
                    var result = new List<Anwser>();
                    result.Add(new Anwser() { Text = Answer, Right = true });
                    return result;
                }
                else
                {
                    var result = new List<Anwser>();
                    result.Add(new Anwser() { Text = Answer, Right = true });
                    return result;
                }

            // Если отве строка или содержит запятые
            if (Answer.Contains(','))
            {
                if (NumQ == 5)
                {
                    var asda = "ASd";
                }
                var result = new List<Anwser>();
                var answers = Answer.Split(',');
                foreach (var item in answers)
                {
                    result.Add(new Anwser() { Text = item, Right = true });
                }
                return result;

            }
            else
            {
                var result = new List<Anwser>();
                result.Add(new Anwser() { Text = Answer, Right = true });
                return result;
            }

        }
    }


}
