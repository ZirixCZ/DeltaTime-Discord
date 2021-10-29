using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace DeltaTimeCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "env",
                TokenType = TokenType.Bot,
            });


            discord.MessageCreated += async (s, e) =>
            {
                Console.WriteLine(DateTime.Now.ToString(@"HH\:mm\:ss"));
                
                if (e.Message.Content.ToLower().StartsWith("info"))
                    await e.Message.RespondAsync(CurrentTime());
                if (e.Message.Content.ToLower().StartsWith("rozvrh"))
                    await e.Message.RespondAsync(Subjects());
                if (e.Message.Content.ToLower().StartsWith("about"))
                    await e.Message.RespondAsync("Jsem discordový bot inspirovaný mobilní aplikací Delta Time. Zobrazuji informace o probíhajících událostech týkající se našeho rozvrhu. Všechen kód můžete naleznout v tomto repozitáři: *https://github.com/ZirixCZ/DeltaTime-Discord*\n*S pozdravem, Michal.*");
                if (e.Message.Content.ToLower().StartsWith("!pomoc"))
                    await e.Message.RespondAsync("**info** pro zobrazení informací o probíhajících a nastávajících událostech.\n**rozvrh** pro zobrazení dnešního rozvrhu.\n**about** zobrazí informace o botovi.\n**!pomoc** pro zobrazení nápovědy.");
            };
            // waiting to be ended
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
        private static string Subjects()
        {
            string[] subjects = {"MA","DJ","PK","CH","AJ","PS","VT","ČJ","PR","TV","PF","pauza"};
            var weekDay = DateTime.Now.DayOfWeek;
            string subjectList;
            if (weekDay == DayOfWeek.Monday)
                subjectList = $"{subjects[1]}, {subjects[0]}, {subjects[2]}, {subjects[3]}, {subjects[11]}, {subjects[4]}";
            else if (weekDay == DayOfWeek.Tuesday)
                subjectList = $"{subjects[5]}, {subjects[5]}, {subjects[6]}, {subjects[0]}, {subjects[1]}, {subjects[7]}";
            else if (weekDay == DayOfWeek.Wednesday)
                subjectList = $"**První skupina**: {subjects[1]}, {subjects[7]}, {subjects[0]}, {subjects[8]}, {subjects[8]}, {subjects[4]}, {subjects[11]}, {subjects[9]}, {subjects[9]}\n**Druhá Skupina**: {subjects[1]}, {subjects[7]}, {subjects[0]}, {subjects[4]}, {subjects[8]}, {subjects[8]}, {subjects[11]}, {subjects[9]}, {subjects[9]}";
            else if (weekDay == DayOfWeek.Thursday)
                subjectList = $"**První skupina**: {subjects[2]}, {subjects[6]}, {subjects[6]}, {subjects[4]}, {subjects[10]}, {subjects[10]}, {subjects[11]}, {subjects[0]}\n**Druhá Skupina**: {subjects[2]}, {subjects[6]}, {subjects[6]}, {subjects[8]}, {subjects[8]}, {subjects[4]}, {subjects[11]}, {subjects[0]}";
            else if (weekDay == DayOfWeek.Friday)
                subjectList = $"**První skupina**: {subjects[0]}, {subjects[3]}, {subjects[7]}, {subjects[8]}, {subjects[8]}\n**Druhá Skupina**: {subjects[0]}, {subjects[3]}, {subjects[7]}, {subjects[4]}, {subjects[10]}, {subjects[10]}";
            else if (weekDay == DayOfWeek.Saturday)
                subjectList = "Dneska není škola";
            else if (weekDay == DayOfWeek.Sunday)
                subjectList = "Škola začíná zítra";
            else
                subjectList = "Vyskytl se problém";
            return subjectList;
        }
        static string CurrentTime()
        {
            string CurrentTime;
            bool ShortestBreak, ShortBreak, MediumBreak, FirstLesson, SecondLesson, ThirdLesson, FourthLesson, FifthLesson, SixthLesson, SeventhLesson, EightLesson, NinethLesson;
            ShortestBreak = ShortBreak = MediumBreak = FirstLesson = SecondLesson = ThirdLesson = FourthLesson = FifthLesson = SixthLesson = SeventhLesson = EightLesson = NinethLesson = false;
            
            if (isbetween(8,00,8,45))
                FirstLesson = true;
            // checking, if the time is 8:45 or greater
            else if (isbetween(8,45,9,50))
                ShortestBreak = true;
            // checking, if the time is 8:50 or greater
            else if (isbetween(8,50,9,35))
                SecondLesson = true;
            else if (isbetween(9,35,9,50))
                MediumBreak = true;
            else if (isbetween(9,50,10,35))
                ThirdLesson = true;
            else if (isbetween(10,35,10,40))
                ShortestBreak = true;
            else if (isbetween(10,40,11,25))
                FourthLesson = true;
            else if (isbetween(11,25,11,35))
                ShortBreak = true;
            else if (isbetween(11,35,12,20))
                FifthLesson = true;
            else if (isbetween(12,20,12,25))
                ShortestBreak = true;
            else if (isbetween(12,25,13,10))
                SixthLesson = true;
            else if (isbetween(13,10,14,00))
                SeventhLesson = true;
            else if (isbetween(14,00,14,45))
                EightLesson = true;
            else if (isbetween(14,45,15,30))
                NinethLesson = true;

            // breaks
            if (ShortestBreak)
                CurrentTime = "5 minutovka";
            else if (ShortBreak)
                CurrentTime = "10 minutovka";
            else if (MediumBreak)
                CurrentTime = "15 minutovka";
            // lessons
            
            // if it is 8:00 or greater, this code shall execute
            else if (FirstLesson)
            {
                var CurrentLessonInt = CustomTime(8,45,0);
                CurrentTime = CurrentLessonInt.ToString(@"hh\:mm\:ss") + " do konce první hodiny.";;
            }
             // if it is 8:50 or greater, this code shall execute
            else if (SecondLesson)
            {
                var CurrentLessonInt = CustomTime(9,35,0);
                CurrentTime = CurrentLessonInt.ToString(@"hh\:mm\:ss") + " do konce druhé hodiny.";
            }
            else if (ThirdLesson)
            {
                var CurrentLessonInt = CustomTime(10,35,0);
                CurrentTime = CurrentLessonInt.ToString(@"hh\:mm\:ss") + " do konce třetí hodiny.";
            }
            else if (FourthLesson)
            {
                var CurrentLessonInt = CustomTime(11,25,0);
                CurrentTime = CurrentLessonInt.ToString(@"hh\:mm\:ss") + " do konce čtvrté hodiny.";
            }
            else if (FifthLesson)
            {
                var CurrentLessonInt = CustomTime(12,20,0);
                CurrentTime = CurrentLessonInt.ToString(@"hh\:mm\:ss") + " do konce páté hodiny.";
            }
            else if (SixthLesson)
            {
                var CurrentLessonInt = CustomTime(13,10,0);
                CurrentTime = CurrentLessonInt.ToString(@"hh\:mm\:ss") + " do konce šesté hodiny.";
            }
            else if (SeventhLesson)
            {
                var CurrentLessonInt = CustomTime(14,00,0);
                CurrentTime = "Volná hodina končí za " + CurrentLessonInt.ToString(@"hh\:mm\:ss");
            }
            else if (EightLesson)
            {
                var CurrentLessonInt = CustomTime(14,45,0);
                CurrentTime = CurrentLessonInt.ToString(@"hh\:mm\:ss") + " do konce osmé hodiny.";
            }
            else if (NinethLesson)
            {
                var CurrentLessonInt = CustomTime(15,30,0);
                CurrentTime = CurrentLessonInt.ToString(@"hh\:mm\:ss") + " do konce deváté hodiny.";
            }
            else
            {
                CurrentTime = "Škola právě neprobíhá.";
            }

                return CurrentTime;
        }
        private static bool isbetween(int h, int m, int h1, int m1)
        {
            DateTime now = DateTime.Now;
            return now >= now.Date.AddHours(h).AddMinutes(m) && now < now.Date.AddHours(h1).AddMinutes(m1);
        }
        private static TimeSpan CustomTime(int h, int m, int s)
        {
            var timeToCheck = new TimeSpan(h, m, s);
            var theActualDate = DateTime.Today + timeToCheck;

            return theActualDate - DateTime.Now;
        }
    }
}