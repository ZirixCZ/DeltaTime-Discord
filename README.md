# Delta Time D#+ Bot <a href="https://dsharpplus.github.io/index.html"><img src="https://img.shields.io/badge/DSharpPlus-gray.svg?logo=C#&style=for-the-badge" /></a>
<p align="center">
  
  ### 💻 Self-Hosting
- Make sure you've got .Net 5 installed on your machine. You can get it here https://dotnet.microsoft.com/download/dotnet/5.0
- Change the timetable to fit your own
> ⚙️ Program.cs
```cs
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
```
