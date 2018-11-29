namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAuthors : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Authors (Name) VALUES ('William Shakespeare')");
            Sql("INSERT INTO Authors (Name) VALUES ('Agatha Christie')");
            Sql("INSERT INTO Authors (Name) VALUES ('Barbara Cartland')");
            Sql("INSERT INTO Authors (Name) VALUES ('Harold Robbins')");
            Sql("INSERT INTO Authors (Name) VALUES ('Georges Simenon')");

        }

        public override void Down()
        {
        }
    }
}
