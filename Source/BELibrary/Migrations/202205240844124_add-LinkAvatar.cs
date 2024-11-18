namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLinkAvatar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admin", "LinkAvatar", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admin", "LinkAvatar");
        }
    }
}
