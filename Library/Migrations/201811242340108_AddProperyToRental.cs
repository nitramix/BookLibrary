namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperyToRental : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rentals", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Rentals", "Client_Id", "dbo.Clients");
            DropIndex("dbo.Rentals", new[] { "Book_Id" });
            DropIndex("dbo.Rentals", new[] { "Client_Id" });
            RenameColumn(table: "dbo.Rentals", name: "Book_Id", newName: "BookId");
            RenameColumn(table: "dbo.Rentals", name: "Client_Id", newName: "ClientId");
            AlterColumn("dbo.Rentals", "BookId", c => c.Int(nullable: false));
            AlterColumn("dbo.Rentals", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "BookId");
            CreateIndex("dbo.Rentals", "ClientId");
            AddForeignKey("dbo.Rentals", "BookId", "dbo.Books", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Rentals", "BookId", "dbo.Books");
            DropIndex("dbo.Rentals", new[] { "ClientId" });
            DropIndex("dbo.Rentals", new[] { "BookId" });
            AlterColumn("dbo.Rentals", "ClientId", c => c.Int());
            AlterColumn("dbo.Rentals", "BookId", c => c.Int());
            RenameColumn(table: "dbo.Rentals", name: "ClientId", newName: "Client_Id");
            RenameColumn(table: "dbo.Rentals", name: "BookId", newName: "Book_Id");
            CreateIndex("dbo.Rentals", "Client_Id");
            CreateIndex("dbo.Rentals", "Book_Id");
            AddForeignKey("dbo.Rentals", "Client_Id", "dbo.Clients", "Id");
            AddForeignKey("dbo.Rentals", "Book_Id", "dbo.Books", "Id");
        }
    }
}
