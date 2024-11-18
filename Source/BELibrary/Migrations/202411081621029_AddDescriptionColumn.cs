namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Description", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Description");
        }
    }
}
