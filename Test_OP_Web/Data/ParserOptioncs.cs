using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Data
{
    public static class ParserOptioncs
    {
        static bool Right(string text)
        {
            if (text == null)
                return false;
            if (text.Contains("*") || text.Contains("="))
                return true;
            else
                return false;
        }

        public static List<Option> ParseLegacy()
        {
            List<Option> options = new();
            string path = @"C:\Users\smallaxe\source\repos\TEST_OP\TEST_OP\test1.txt";

            int numVar = 0;
            int numQ = 1;

            if (!File.Exists(path))
                return null;

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


                    question.Anwsers = new();
                    question.Anwsers.Add(new());
                    question.Anwsers.Add(new());
                    question.Anwsers.Add(new());
                    question.Anwsers.Add(new());


                    question.Anwsers[0].Text = lines[i + 1];
                    question.Anwsers[1].Text = lines[i + 2];
                    question.Anwsers[2].Text = lines[i + 3];

                    if (lines[i + 4].Contains($"{numQ}.\t"))
                        question.Anwsers[2].Text = " ";
                    else
                        question.Anwsers[3].Text = lines[i + 4];

                    // Выбор верных ответов
                    foreach (var anwser in question.Anwsers)
                    {
                        if (Right(anwser.Text))
                        {
                            anwser.Right = true;
                            anwser.Text = anwser.Text.Substring(1);
                        }
                        else
                            anwser.Right = false;
                    }


                    question.NumVar = numVar;
                    question.NumQ = numQ;

                    options[numVar - 1].Questions.Add(question);
                    numQ++;

                }

            }

            return options;
        }

        public static List<Option> ParseOptions30()
        {

            //await GetAnswerFronJson.GenerateTestJson();

            var answersPath = Directory.GetFiles(@"C:\Users\smallaxe\source\repos\Test_OP_Web\Test_OP_Web\wwwroot\anwsers2\");

            var anwsers = new List<GetAnswerFronJson>();
            foreach (var answerPath in answersPath)
                anwsers.Add(new GetAnswerFronJson(answerPath));





            string path = @"C:\Users\smallaxe\source\repos\Test_OP_Web\Test_OP_Web\wwwroot\test2.txt";

            List<Option> options = new();

            if (!File.Exists(path))
                return null;

            string lines = File.ReadAllText(path);


            int numV = 1;
            var variants = lines.Split("Вариант:").ToList();
            variants.RemoveAt(0);
            foreach (var variant in variants)
            {

                var questions = variant.Split("Вопрос ").ToList();
                questions.RemoveAt(0);

                var option = new Option() { NumVar = numV };
                option.Questions = new();

                int numQ = 1;
                var answersVariant = anwsers.FirstOrDefault(x => x.NumVar == numV);
                foreach (var question in questions)
                {
                    // Создание экзмепляра Question
                    var optionQuestion = new Question();
                    optionQuestion.Anwsers = new();
                    optionQuestion.NumVar = numV;
                    optionQuestion.NumQ = numQ;


                    // Парсинг вопроса
                    var qLines = question.Split("\n");

                    optionQuestion.QuestionString = qLines[1];


                    for (int i = 2; i < qLines.Length; i++)
                    {
                        if (qLines[i] == "\r")
                            break;

                        optionQuestion.Anwsers.Add(new Anwser() { Text = qLines[i] });
                    }

                    // Проверка на отсутсвие вариантов ответа в вопросе
                    if (optionQuestion.Anwsers.Count == 1 || optionQuestion.Anwsers.Count == 0)
                        optionQuestion.NoVariant = true;

                    // Вставка ответа
                    var currAnswers = answersVariant.Anwsers.FirstOrDefault(x => x.NumQ == optionQuestion.NumQ);
                    var insert = currAnswers.ToAnswer();


                    if (optionQuestion.NoVariant)
                    {
                        optionQuestion.Anwsers.Clear();

                        if (insert.Count > 1)
                        {
                            for (int i = 1; i < insert.Count; i++)
                            {
                                Anwser item = insert[i];
                                insert.FirstOrDefault().Text += "," + item.Text;
                            }
                            optionQuestion.Anwsers.Add(insert.FirstOrDefault());
                        }
                        else
                            optionQuestion.Anwsers.AddRange(insert);
                    }
                    else
                    {
                        // Вставляем true или false по номеру 
                        foreach (var item in insert)
                        {
                            int numAns = 0;
                            try
                            {
                                numAns = Convert.ToInt32(item.Text);
                                optionQuestion.Anwsers[numAns - 1].Right = true;
                            }
                            catch (Exception)
                            {

                                throw;
                            }


                        }
                    }


                    option.Questions.Add(optionQuestion);
                    numQ++;

                }
                options.Add(option);
                numV++;
            }

            SaveOptions(options);
            return options;
        }


        static void SaveOptions(List<Option> options)
        {
            try
            {
                string path = Path.Combine("wwwroot/options.json");
                Console.OutputEncoding = Encoding.UTF8;
                JsonSerializerOptions jsoptions = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };


                var json = JsonSerializer.Serialize(options, jsoptions);
                // полная перезапись файла 
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.WriteLine(json);
                }
            }
            catch (Exception exc)
            {

                throw new Exception($"Erorr SaveOptions\n{exc.Message}");
            }

        }
        public static List<Option> GetOptionsFromTxtFile()
        {
            try
            {
                string path = Path.Combine("wwwroot/options_22_08_2023.json");
                // асинхронное чтение
                string jsonString;
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    jsonString = reader.ReadToEnd();
                }
                var options = JsonSerializer.Deserialize<List<Option>>(jsonString);
                return options;
            }
            catch (Exception exc)
            {

                throw new Exception($"Erorr GetOptions\n{exc.Message}");
            }

        }

    }
}
