using System;
using System.Collections.Generic;
using System.Linq;
using XboxStatistics.Models;

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
            Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
            Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
            Question("List all of my games where I have earned a rare achievement", ListAllOfMyGamesWhereIHaveEarnedARareAchievement);
            Question("List the top 3 games where I have earned the most rare achievements", ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements);
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
            var minutes = Xbox.GameStats.Values
                        .SelectMany(g => g)
                        .Where(g => g.Name == "MinutesPlayed")
                        .Where(g => g.Value != null)
                        .Sum(g => double.Parse(g.Value));
            return ((int)(minutes / 60) / 24).ToString();
        }

        static string WhichGameHaveISpentTheMostHoursPlaying()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            //return Xbox.GameStats.Values
            //            .SelectMany(g => g)
            //            .Where(g => g.Name == "MinutesPlayed")
            //            .Where(g => g.Value != null)
            //            .
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
                        GameTitle = x,
                        Stat = s
                    };
            return null; // isaacs.Aggregate("", (retval, stat) => retval += $"{stat.GameTitle.Name} = {stat.Stat.Value}");
        }

        static string HowManyAchievementsDidIEarnPerYear()
        {
            //HINT: unlocked achievements have an "Achieved" progress state
            var achs = from a in (Xbox.Achievements.SelectMany(a => a.Value).Where(a => a.Progression.TimeUnlocked > DateTime.MinValue))
                       group a by a.Progression.TimeUnlocked.Year into groupedAch
                       orderby groupedAch.Key
                       select new
                       {
                           year = groupedAch.Key,
                           count = groupedAch.Count()
                       };
            return achs.Aggregate("", (retval, a) => retval += $"{a.year.ToString()}: {a.count.ToString()}\n");
        }

        static string ListAllOfMyGamesWhereIHaveEarnedARareAchievement()
        {
            //HINT: rare achievements have a rarity category called "Rare"
            var games = Xbox.Achievements
                            .SelectMany(a => a.Value)
                            .Where(r => r.Rarity.CurrentCategory == "Rare")
                            .SelectMany(a => a.TitleAssociations)
                            .Select(a => a.Name)
                            .Distinct();
            return null;// games.Aggregate("", (retval, ach) => retval += $"{ach}\n");
        }

        static string ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements()
        {
            var games = Xbox.Achievements
                            .SelectMany(a => a.Value)
                            .Where(r => r.Rarity.CurrentCategory == "Rare" && r.Rarity.CurrentPercentage > 0)
                            .Select(s => new { Game = s.TitleAssociations[0].Name })
                            .GroupBy(s => s.Game)
                            .Select(a => new { Game = a, Count = a.Count() })
                            .OrderByDescending(a => a.Count)
                            .Take(3);
            return null;
        }

        static string WhichIsMyRarestAchievement()
        {
            throw new NotImplementedException();
        }
    }
}