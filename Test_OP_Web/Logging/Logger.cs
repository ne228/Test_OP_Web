using System;
using System.IO;

namespace Test_OP_Web.Logging
{
    class Logger : ILogger
    {
        private string Path { get; set; }
        public Logger()
        {

            string DirectoryPath = $"{Directory.GetCurrentDirectory()}\\logs";
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);



            string path =
                $"{DirectoryPath}\\{DateTime.Now.Day} {DateTime.Now.Month}  {DateTime.Now.Year}.log";

            Path = path;
        }
        public void WriteLine(string str)
        {
            Console.WriteLine(str);

            using (StreamWriter sw = new StreamWriter(Path, true))
            {
                sw.WriteLine(string.Format("{0,-23} {1}", DateTime.Now.ToString() + ":", str));
            }
        }
    }
}
