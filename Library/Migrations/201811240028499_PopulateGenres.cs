namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Drama')");
            Sql("INSERT INTO Genres (Name) VALUES ('Mystery')");
            Sql("INSERT INTO Genres (Name) VALUES ('Satire')");
            Sql("INSERT INTO Genres (Name) VALUES ('Science fiction')");
            Sql("INSERT INTO Genres (Name) VALUES ('Action')");

        }

        public override void Down()
        {
        }
    }
}
