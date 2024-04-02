using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text;

namespace AnecdoteAboutStirlitzCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Test\Test.txt";
            //FileInfo file = new FileInfo(path);
            //file.Write
            Random rnd = new Random();

            int Max = 0;

            Console.WriteLine("Введите количество анекдотов:");
            while (!int.TryParse(Console.ReadLine(), out Max))
            {
                Console.WriteLine("!");
            }


            using (FileStream FileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                for (int i = Max; i > 0; i--)
                {
                    string stringnot1024 = "\"";
                    if (i == Max) stringnot1024 = string.Empty;//Если 1024 раза

                    string raz = "раза";
                    if (i % 10 == 1 || i >= 10 && i <= 19) raz = "раз";
                    Console.WriteLine($"В дверь постучали {i} {raz}. Кто постучал?");
                    string Guess = Console.ReadLine()!;//!
                    string Answer = "Не догадался";
                    string Someone = "некто";
                    switch (rnd.NextInt64(0, 1))
                    {
                        case 0: Someone = "Не догадался"; break;
                    }
                    switch (rnd.NextInt64(0, 2))
                    {
                        case 0: Someone = "некто"; break;
                        case 1: Someone = "кто-то"; break;
                    }



                    WriteInFile(FileStream, $"{stringnot1024}В дверь постучали {i} раза.\r\n" +
                        $"– {Guess}, – подумал Штирлиц.\r\n" +
                        $"– {Answer} - подумал {Someone} за дверью.\r\n" +
                        $"- Так кто же за дверью?! - вокликнул Штирлиц\r\n" +
                        $"- Давай расскажешь анекдот про Штирлица, и я скажу тебе, кто я.\r\n" +
                        $"Делать нечего, и Штирлиц начал рассказывать анекдот:\r\n");


                }
                WriteInFile(FileStream, $"В дверь постучали 0 раза.\r\n" +
                    $"Никто не ответил, и Штирлиц сам открыл дверь. За дверью стоял мужик с запиской\".\r\n");
                for (int i = Max; i > 0; i--)
                {
                    WriteInFile(FileStream, $"Никто не ответил, и Штирлиц сам открыл дверь. За дверью стоял мужик с запиской\".");
                }
            }
        }

        public static void WriteInFile(FileStream FileStream, string Text)
        {
            // преобразуем строку в байты
            byte[] buffer = Encoding.Default.GetBytes(Text);
            // запись массива байтов в файл
            FileStream.Write(buffer, 0, buffer.Length);
        }
    }
}