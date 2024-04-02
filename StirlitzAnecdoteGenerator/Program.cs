using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text;
using StirlitzAnecdoteGenerator;

namespace AnecdoteAboutStirlitzCreator
{
    internal class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            string path = @"D:\Test\Test.txt";
            //FileInfo file = new FileInfo(path);
            //file.Write


            int Max = 0;

            Console.WriteLine("Введите количество анекдотов:");
            while (!int.TryParse(Console.ReadLine(), out Max))
            {
                Console.WriteLine("!");
            }


            using (FileStream FileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                bool isPhase1Complete = true;
                for (int i = Max; i > 0; i--)
                {
                    string stringnot1024 = "\"";
                    if (i == Max) stringnot1024 = string.Empty;//Если 1024 раза

                    string raz = "раза";
                    if (i % 10 == 1 || i >= 10 && i <= 19) raz = "раз";
                    Console.WriteLine($"В дверь постучали {i} {raz}. Кто постучал? (Введите '`' для генерации случайного ответа)");
                    string Guess = ""; //Console.ReadLine()!;//!
                    if (Guess.Equals("`") || true)//!!!!!!!!!!
                    {
                        Guess = GetGuess(GetGuests(i)!);
                        Console.WriteLine($"Сгенерировано: \"{Guess}\"");
                    }
                    if (Guess.Equals("SAVE"))
                    {
                        Console.WriteLine($"СОХРАНЕНИЕ АНЕКДОТА");
                        isPhase1Complete = false;
                        break;
                    }


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
                        $"Делать нечего, и Штирлиц начал рассказывать анекдот:\r\n");//':' или '-'?


                }
                WriteInFile(FileStream, $"В дверь постучали 0 раза.\r\n" +
                    $"Никто не ответил, и Штирлиц сам открыл дверь. За дверью стоял мужик с запиской\".\r\n");
                if (isPhase1Complete)
                    for (int i = Max; i > 0; i--)
                    {
                        WriteInFile(FileStream, $"Никто не ответил, и Штирлиц сам открыл дверь. За дверью стоял мужик с запиской\".\r\n");
                    }
                Console.ReadLine();
            }
        }

        public static void WriteInFile(FileStream FileStream, string Text)
        {
            // преобразуем строку в байты
            byte[] buffer = Encoding.Default.GetBytes(Text);
            // запись массива байтов в файл
            FileStream.Write(buffer, 0, buffer.Length);
        }


        public static List<Guest>? GetGuests(int GeneralCost)
        {
            List<Guest> HighCost = new List<Guest> {
                new Guest("Килобайт","Килобайт","Килобайт",0,1024),
                new Guest("Сороконожка","Сороконожки","Сороконожек", 0, 40, false, false)};

            List<Guest> MediumCost = new List<Guest> {
                new Guest("Осьминог","Осьминога","Осьминогов",0, 8, false, false),
                new Guest("Байт","Байт","Байт", 0, 8),
                new Guest("Мюллер","Мюллера","Мюллеров", 0, 2, true, false),};

            List<Guest> BitCost = new List<Guest> {
                new Guest("Бит","Бит","Бит",0,1)};

            List<Guest> CurrentList = new List<Guest>
            {
                HighCost.ElementAt(rnd.Next(0, HighCost.Count)),
                MediumCost.ElementAt(rnd.Next(0, MediumCost.Count)),
                BitCost.ElementAt(rnd.Next(0, BitCost.Count))
            };

            int RemainCost = GeneralCost;

            switch (rnd.Next(0, 2))
            {
                case 0://Max

                    log("GetGuests(int GeneralCost): MAX");
                    for (int i = 0; i < CurrentList.Count; i++)
                    {
                        if (CurrentList[i].isDividable)
                            CurrentList[i].Number = RemainCost / CurrentList[i].Cost;
                        else
                            CurrentList[i].Number = (int)Math.Round((GeneralCost / CurrentList[i].Cost) / 2, 3, MidpointRounding.ToNegativeInfinity);
                        RemainCost -= (int)Math.Round(CurrentList[i].Number * CurrentList[i].Cost, 3, MidpointRounding.AwayFromZero);
                    }

                    break;
                case 1://NotMax
                    log("GetGuests(int GeneralCost): NOTMAX");
                    CurrentList[0].Number = (int)Math.Round((GeneralCost / CurrentList[0].Cost) / 2, MidpointRounding.ToNegativeInfinity);
                    RemainCost -= (int)Math.Round(CurrentList[0].Number * CurrentList[0].Cost, MidpointRounding.AwayFromZero);

                    for (int i = 1; i < CurrentList.Count; i++)
                    {
                        if (CurrentList[i].isDividable)
                            CurrentList[i].Number = RemainCost / CurrentList[i].Cost;
                        else
                            CurrentList[i].Number = (int)Math.Round((GeneralCost / CurrentList[i].Cost) / 2, MidpointRounding.ToNegativeInfinity);
                        RemainCost -= (int)Math.Round(CurrentList[i].Number * CurrentList[i].Cost, MidpointRounding.AwayFromZero);
                    }
                    break;
            }
            return CurrentList;
        }

        public static string GetGuess(List<Guest> Guests)
        {
            StringBuilder GuestsString = new StringBuilder();

            for (int i = 0; i < Guests.Count; i++)
            {
                if (Guests[i].Number <= 0) continue;
                //Выбираем нужное склонение
                string GuestName = Guests[i].Name2;
                if (Guests[i].Number >= 10 && Guests[i].Number <= 20 || Guests[i].Number % 10 >= 5 || Guests[i].Number % 10 >= 0) GuestName = Guests[i].Name5;
                if (Guests[i].Number < 10 && Guests[i].Number > 20 && Guests[i].Number % 10 < 5) GuestName = Guests[i].Name;
                if (i != 0 && !Guests[i].isProperName) GuestName = GuestName.ToLower();
                GuestsString.Append($"{Guests[i].Number} {GuestName}, ");
            }
            GuestsString.Remove(GuestsString.Length - 2, 2);

            return GuestsString.ToString();
        }

        public static void log(string Text) 
        {
            Console.WriteLine($"LOG: {Text}");
        }
    }
}

//1 слово 2 слова 3 слова 5 слов 10 слов 11 слов 12 слов 15 слов 20 слов 21 слово 22 слова 23 слова 24 слова 25 слов 31 слово 