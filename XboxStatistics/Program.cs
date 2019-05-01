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
            //Question("How many days did I play?", HowManyDaysDidIPlay);
            //Question("Which game have I spent the most hours playing?", WhichGameHaveISpentTheMostHoursPlaying);
            Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
            Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
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
            //var a = Xbox.MyGames.Where(g => g.Name == "The Binding of Isaac: Rebirth"); // The Binding of Isaac: Rebirth
            //return "";
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
            var game = Xbox.MyGames.OrderByDescending(g => g.LastUnlock).First();
            return $"{game.Name} on {game.LastUnlock.ToString("yyyy-MM-dd HH:mm")}";
        }

        static string ListAllOfMyStatisticsInBindingOfIsaac()
        {
            var isaacs = from x in (Xbox.MyGames
                                   .Where(g => g.Name.Contains("Binding of Isaac")))
                    join s in Xbox.GameStats
                        on x.TitleId equals s.Key
                    select new
                    {
                        xbox = x,
                        stat = s
                    };
            return null;
        }

        static string HowManyAchievementsDidIEarnPerYear()
        {
            //HINT: unlocked achievements have an "Achieved" progress state
            //var achs = Xbox.MyGames.GroupBy(g => g.LastUnlock.Year);
            //var achs = from g in Xbox.MyGames
            //           join a in Xbox.Achievements//.SelectMany(ac => ac.Value)
            //               on g.TitleId equals a.Key
            //           group g by g.LastUnlock.Year into yearGroup
            //           orderby yearGroup.Key
            //           select new
            //           {
            //               year = yearGroup.Key,
            //               count = yearGroup.Count()
            //           };
            var achs = from g in (Xbox.MyGames.Where(g => g.LastUnlock != null))
                       join a in Xbox.Achievements
                           on g.TitleId equals a.Key
                       select new
                       {
                           game = g,
                           ach = a.Value.Count()
                       };
            return null;
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