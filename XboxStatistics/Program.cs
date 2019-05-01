using System;
using System.Linq;

namespace XboxStatistics
{
    class Program
    {
        private static readonly MyXboxOneGames Xbox = new MyXboxOneGames();

        static void Main(string[] args)
        {
            Question("How many games do I have?", HowManyGamesDoIHave);
            Question("How many games have I completed?", HowManyGamesHaveICompleted);
            Question("How much Gamerscore do I have?", HowMuchGamescoreDoIHave);
            Question("How many days did I play?", HowManyDaysDidIPlay);
            //Question("Which game have I spent the most hours playing?", WhichGameHaveISpentTheMostHoursPlaying);
            //Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
            //Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            //Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
            //Question("List all of my games where I have earned a rare achievement", ListAllOfMyGamesWhereIHaveEarnedARareAchievement);
            //Question("List the top 3 games where I have earned the most rare achievements", ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements);
            //Question("Which is my rarest achievement?", WhichIsMyRarestAchievement);

            Console.ReadLine();
        }

        static void Question(string question, Func<string> answer)
        {
            Console.WriteLine($"Q: {question}");
            Console.WriteLine($"A: {answer()}");
            Console.WriteLine();
        }

        static string HowManyGamesDoIHave()
        {
            return Xbox.GameDetails.Count().ToString();
        }

        static string HowManyGamesHaveICompleted()
        {
            //HINT: you need to count the games where I reached the maximum Gamerscore
            return Xbox.MyGames.Where(g => g.CurrentGamerscore == g.MaxGamerscore).Count().ToString();
        }

        static string HowManyDaysDidIPlay()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            //return Xbox.GameStats.Sum(s => s.Value.Sum())
            throw new NotImplementedException();
        }

        static string WhichGameHaveISpentTheMostHoursPlaying()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            throw new NotImplementedException();
        }

        static string HowMuchGamescoreDoIHave()
        {
            return Xbox.MyGames.Sum(g => g.CurrentGamerscore).ToString() + "G";
        }

        static string InWhichGameDidIUnlockMyLatestAchievement()
        {
            throw new NotImplementedException();
        }

        static string ListAllOfMyStatisticsInBindingOfIsaac()
        {
            throw new NotImplementedException();
        }

        static string HowManyAchievementsDidIEarnPerYear()
        {
            //HINT: unlocked achievements have an "Achieved" progress state
            throw new NotImplementedException();
        }

        static string ListAllOfMyGamesWhereIHaveEarnedARareAchievement()
        {
            //HINT: rare achievements have a rarity category called "Rare"
            throw new NotImplementedException();
        }

        static string ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements()
        {
            throw new NotImplementedException();
        }

        static string WhichIsMyRarestAchievement()
        {
            throw new NotImplementedException();
        }
    }
}