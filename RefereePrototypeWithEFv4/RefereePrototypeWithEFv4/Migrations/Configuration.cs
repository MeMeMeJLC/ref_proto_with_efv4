namespace RefereePrototypeWithEFv4.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RefereePrototypeWithEFv4.Models.RefereePrototypeWithEFv4Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RefereePrototypeWithEFv4.Models.RefereePrototypeWithEFv4Context";
        }

        protected override void Seed(RefereePrototypeWithEFv4.Models.RefereePrototypeWithEFv4Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Games.AddOrUpdate(x => x.Id,
                new Game() { Id = 1, GameDateTime = new DateTime(2016, 8, 15) },
                new Game() { Id = 2, GameDateTime = new DateTime(2016, 8, 16) }
                );

            context.Teams.AddOrUpdate(x => x.Id,
                new Team() { Id = 1, Name = "Tigers" },
                new Team() { Id = 2, Name = "Wolves" },
                new Team() { Id = 3, Name = "Bears" }
            );

            context.GamePlayers.AddOrUpdate(x => x.Id,
                new GamePlayer() { Id = 1, TeamId = 1, FirstName = "Jim", LastName = "Williams", IsCaptain = true, IsStartingSubstitute = false, GameId = 5 },
                new GamePlayer() { Id = 2, TeamId = 1, FirstName = "Doug", LastName = "Benson", IsCaptain = false, IsStartingSubstitute = true, GameId = 5 },
                new GamePlayer() { Id = 5, TeamId = 1, FirstName = "Jon", LastName = "Ronson", IsCaptain = false, IsStartingSubstitute = false, GameId = 5 },
                new GamePlayer() { Id = 6, TeamId = 1, FirstName = "Dave", LastName = "Paul", IsCaptain = false, IsStartingSubstitute = false, GameId = 5 },
                new GamePlayer() { Id = 3, TeamId = 2, FirstName = "Phil", LastName = "Hendry", IsCaptain = true, IsStartingSubstitute = false, GameId = 5 },
                new GamePlayer() { Id = 4, TeamId = 2, FirstName = "Bob", LastName = "Newman", IsCaptain = false, IsStartingSubstitute = true, GameId = 5 },
                new GamePlayer() { Id = 7, TeamId = 2, FirstName = "Jeremy", LastName = "Cook", IsCaptain = false, IsStartingSubstitute = false, GameId = 5 },
                new GamePlayer() { Id = 8, TeamId = 2, FirstName = "Tim", LastName = "Simons", IsCaptain = false, IsStartingSubstitute = false, GameId = 5 }
                );

            context.Goals.AddOrUpdate(x => x.Id,
                new Goal() { Id = 1, GamePlayerId = 1, IsOwnGoal = false, TimeScored = new TimeSpan(0, 5, 32) },
                new Goal() { Id = 2, GamePlayerId = 4, IsOwnGoal = true, TimeScored = new TimeSpan(0, 15, 32) },
                new Goal() { Id = 3, GamePlayerId = 6, IsOwnGoal = false, TimeScored = new TimeSpan(0, 32, 16) },
                new Goal() { Id = 4, GamePlayerId = 1, IsOwnGoal = false, TimeScored = new TimeSpan(1, 5, 55) }
            );

            context.GameIncidents.AddOrUpdate(x => x.Id,
                new GameIncident() { Id = 1, GamePlayerId = 2, IncidentType = "y2", IncidentTime = new TimeSpan(0, 10, 20) },
                new GameIncident() { Id = 2, GamePlayerId = 4, IncidentType = "y1", IncidentTime = new TimeSpan(0, 30, 40) },
                new GameIncident() { Id = 3, GamePlayerId = 6, IncidentType = "r1", IncidentTime = new TimeSpan(0, 50, 52) }

                );

            context.Substitutions.AddOrUpdate(x => x.Id,
                new Substitution() { Id = 1, GamePlayerGoingOnId = 2, GamePlayerGoingOffId = 1, SubstitutionTime = new TimeSpan(0,25,10)},
                new Substitution() { Id = 1, GamePlayerGoingOnId = 4, GamePlayerGoingOffId = 5, SubstitutionTime = new TimeSpan(0, 42, 40) }
                );
        }
    }
}
