namespace CollaborativeLearning.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Semesters", "CourseName", c => c.String(nullable: false));
            AddColumn("dbo.Semesters", "Section", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Semesters", "Section");
            DropColumn("dbo.Semesters", "CourseName");
        }
    }
}
