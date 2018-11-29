namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPropertyToBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "Rentals_Id", c => c.Int());
            CreateIndex("dbo.Rentals", "Rentals_Id");
            AddForeignKey("dbo.Rentals", "Rentals_Id", "dbo.Rentals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Rentals_Id", "dbo.Rentals");
            DropIndex("dbo.Rentals", new[] { "Rentals_Id" });
            DropColumn("dbo.Rentals", "Rentals_Id");
        }
    }
}
