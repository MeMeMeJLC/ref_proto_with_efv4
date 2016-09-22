namespace RefereePrototypeWithEFv4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GamePlayers", "Game_Id", "dbo.Games");
            DropIndex("dbo.GamePlayers", new[] { "Game_Id" });
            RenameColumn(table: "dbo.GamePlayers", name: "Game_Id", newName: "GameId");
            AlterColumn("dbo.GamePlayers", "GameId", c => c.Int(nullable: false));
            CreateIndex("dbo.GamePlayers", "GameId");
            AddForeignKey("dbo.GamePlayers", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GamePlayers", "GameId", "dbo.Games");
            DropIndex("dbo.GamePlayers", new[] { "GameId" });
            AlterColumn("dbo.GamePlayers", "GameId", c => c.Int());
            RenameColumn(table: "dbo.GamePlayers", name: "GameId", newName: "Game_Id");
            CreateIndex("dbo.GamePlayers", "Game_Id");
            AddForeignKey("dbo.GamePlayers", "Game_Id", "dbo.Games", "Id");
        }
    }
}
