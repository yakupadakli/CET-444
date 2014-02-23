namespace CollaborativeLearning.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkIsActive : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResourceFiles", "fileID", "dbo.ResourceFiles");
            DropIndex("dbo.ResourceFiles", new[] { "fileID" });
            AddColumn("dbo.Works", "isActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.ResourceFiles", "fileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResourceFiles", "fileID", c => c.Int(nullable: false));
            DropColumn("dbo.Works", "isActive");
            CreateIndex("dbo.ResourceFiles", "fileID");
            AddForeignKey("dbo.ResourceFiles", "fileID", "dbo.ResourceFiles", "Id");
        }
    }
}
