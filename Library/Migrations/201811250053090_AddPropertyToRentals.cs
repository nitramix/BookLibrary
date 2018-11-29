namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyToRentals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "NumberInStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "NumberInStock");
        }
    }
}
