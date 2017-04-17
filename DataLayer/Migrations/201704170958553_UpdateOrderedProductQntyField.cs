namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderedProductQntyField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderedProducts", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderedProducts", "Quantity");
        }
    }
}
