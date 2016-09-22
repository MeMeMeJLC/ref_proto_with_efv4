namespace RefereePrototypeWithEFv4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameIncidents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IncidentType = c.String(),
                        IncidentTime = c.Time(nullable: false, precision: 7),
                        GamePlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GamePlayers", t => t.GamePlayerId, cascadeDelete: true)
                .Index(t => t.GamePlayerId);
            
            CreateTable(
                "dbo.GamePlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        IsCaptain = c.Boolean(nullable: false),
                        IsStartingSubstitute = c.Boolean(nullable: false),
                        TeamId = c.Int(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.TeamId)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsOwnGoal = c.Boolean(nullable: false),
                        TimeScored = c.Time(nullable: false, precision: 7),
                        GamePlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GamePlayers", t => t.GamePlayerId, cascadeDelete: true)
                .Index(t => t.GamePlayerId);
            
            CreateTable(
                "dbo.Substitutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubstitutionTime = c.Time(nullable: false, precision: 7),
                        GamePlayerGoingOnId = c.Int(nullable: false),
                        GamePlayerGoingOffId = c.Int(nullable: false),
                        GamePlayer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GamePlayers", t => t.GamePlayer_Id)
                .Index(t => t.GamePlayer_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GamePlayers", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GamePlayers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Substitutions", "GamePlayer_Id", "dbo.GamePlayers");
            DropForeignKey("dbo.Goals", "GamePlayerId", "dbo.GamePlayers");
            DropForeignKey("dbo.GameIncidents", "GamePlayerId", "dbo.GamePlayers");
            DropIndex("dbo.Substitutions", new[] { "GamePlayer_Id" });
            DropIndex("dbo.Goals", new[] { "GamePlayerId" });
            DropIndex("dbo.GamePlayers", new[] { "Game_Id" });
            DropIndex("dbo.GamePlayers", new[] { "TeamId" });
            DropIndex("dbo.GameIncidents", new[] { "GamePlayerId" });
            DropTable("dbo.Games");
            DropTable("dbo.Teams");
            DropTable("dbo.Substitutions");
            DropTable("dbo.Goals");
            DropTable("dbo.GamePlayers");
            DropTable("dbo.GameIncidents");
        }
    }
}
