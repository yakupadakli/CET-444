namespace CollaborativeLearning.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CET444 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionPlanLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regUserID = c.Int(nullable: false),
                        value = c.String(nullable: false, maxLength: 500),
                        regDate = c.DateTime(nullable: false),
                        statusID = c.Int(nullable: false),
                        Scenario_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_ID)
                .ForeignKey("dbo.Status", t => t.statusID, cascadeDelete: true)
                .Index(t => t.Scenario_ID)
                .Index(t => t.statusID);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        year = c.DateTime(nullable: false),
                        semester = c.Int(nullable: false),
                        specialCode = c.String(),
                        statusID = c.Int(nullable: false),
                        ActionPlanList_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Status", t => t.statusID)
                .ForeignKey("dbo.ActionPlanLists", t => t.ActionPlanList_ID)
                .Index(t => t.statusID)
                .Index(t => t.ActionPlanList_ID);
            
            CreateTable(
                "dbo.Scenarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regUserID = c.Int(nullable: false),
                        ScenarioName = c.String(nullable: false, maxLength: 50),
                        ShortDescription = c.String(nullable: false, maxLength: 500),
                        statusID = c.Int(nullable: false),
                        regDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Status", t => t.statusID, cascadeDelete: true)
                .Index(t => t.statusID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GroupWorks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        workID = c.Int(nullable: false),
                        groupID = c.Int(nullable: false),
                        statusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.groupID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.statusID)
                .ForeignKey("dbo.Works", t => t.workID, cascadeDelete: true)
                .Index(t => t.groupID)
                .Index(t => t.statusID)
                .Index(t => t.workID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regUserID = c.Int(nullable: false),
                        groupName = c.String(nullable: false, maxLength: 50),
                        semesterID = c.Int(nullable: false),
                        regDate = c.DateTime(nullable: false),
                        statusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Semesters", t => t.semesterID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.statusID, cascadeDelete: true)
                .Index(t => t.semesterID)
                .Index(t => t.statusID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        PasswordFailuresSinceLastSuccess = c.Int(nullable: false),
                        LastPasswordFailureDate = c.DateTime(),
                        LastActivityDate = c.DateTime(),
                        LastLockoutDate = c.DateTime(),
                        LastLoginDate = c.DateTime(),
                        ConfirmationToken = c.String(),
                        CreateDate = c.DateTime(),
                        LastPasswordChangedDate = c.DateTime(),
                        PasswordVerificationToken = c.String(),
                        PasswordVerificationTokenExpirationDate = c.DateTime(),
                        LastLoginIP = c.String(),
                        Group_ID = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Groups", t => t.Group_ID)
                .Index(t => t.Group_ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        groupName = c.String(nullable: false, maxLength: 50),
                        scenarioID = c.Int(nullable: false),
                        statusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Scenarios", t => t.scenarioID)
                .ForeignKey("dbo.Status", t => t.statusID)
                .Index(t => t.scenarioID)
                .Index(t => t.statusID);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        submittedWorkID = c.Int(nullable: false),
                        value = c.String(nullable: false, maxLength: 500),
                        dueDate = c.DateTime(nullable: false),
                        statusID = c.Int(nullable: false),
                        parentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Feedbacks", t => t.parentID)
                .ForeignKey("dbo.Status", t => t.statusID, cascadeDelete: true)
                .ForeignKey("dbo.SubmittedWorks", t => t.submittedWorkID, cascadeDelete: true)
                .Index(t => t.parentID)
                .Index(t => t.statusID)
                .Index(t => t.submittedWorkID);
            
            CreateTable(
                "dbo.SubmittedWorks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        groupsWorkID = c.Int(nullable: false),
                        submissionDate = c.DateTime(nullable: false),
                        GroupWorks_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GroupWorks", t => t.GroupWorks_ID)
                .Index(t => t.GroupWorks_ID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        fileName = c.String(nullable: false, maxLength: 50),
                        FileSize = c.Int(nullable: false),
                        FileType = c.String(),
                        SubmittedWork_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SubmittedWorks", t => t.SubmittedWork_ID)
                .Index(t => t.SubmittedWork_ID);
            
            CreateTable(
                "dbo.Reflections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        semesterID = c.Int(nullable: false),
                        userID = c.Int(nullable: false),
                        title = c.String(nullable: false, maxLength: 50),
                        content = c.String(nullable: false, maxLength: 500),
                        statusID = c.Int(nullable: false),
                        isRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Semesters", t => t.semesterID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.statusID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.userID, cascadeDelete: true)
                .Index(t => t.semesterID)
                .Index(t => t.statusID)
                .Index(t => t.userID);
            
            CreateTable(
                "dbo.ResourceFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        resourceID = c.Int(nullable: false),
                        fileID = c.Int(nullable: false),
                        ResourceList_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Files", t => t.fileID, cascadeDelete: true)
                .ForeignKey("dbo.ResourceLists", t => t.ResourceList_ID)
                .Index(t => t.fileID)
                .Index(t => t.ResourceList_ID);
            
            CreateTable(
                "dbo.ResourceLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 50),
                        description = c.String(nullable: false, maxLength: 500),
                        link = c.String(nullable: false, maxLength: 250),
                        statusID = c.Int(nullable: false),
                        type = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Status", t => t.statusID, cascadeDelete: true)
                .Index(t => t.statusID);
            
            CreateTable(
                "dbo.ScenarioTasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        SemesterID = c.Int(nullable: false),
                        description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .Index(t => t.SemesterID);
            
            CreateTable(
                "dbo.SemesterWorkDueDates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        semesterID = c.Int(nullable: false),
                        workID = c.Int(nullable: false),
                        dueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Semesters", t => t.semesterID, cascadeDelete: true)
                .ForeignKey("dbo.Works", t => t.workID, cascadeDelete: true)
                .Index(t => t.semesterID)
                .Index(t => t.workID);
            
            CreateTable(
                "dbo.SubmittedWorkStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        submittedWorkID = c.Int(nullable: false),
                        isSeen = c.Boolean(nullable: false),
                        lastSeenDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SubmittedWorks", t => t.submittedWorkID, cascadeDelete: true)
                .Index(t => t.submittedWorkID);
            
            CreateTable(
                "dbo.ScenarioSemesters",
                c => new
                    {
                        Scenario_ID = c.Int(nullable: false),
                        Semester_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Scenario_ID, t.Semester_ID })
                .ForeignKey("dbo.Scenarios", t => t.Scenario_ID, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.Semester_ID, cascadeDelete: true)
                .Index(t => t.Scenario_ID)
                .Index(t => t.Semester_ID);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleId, t.User_UserId })
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Role_RoleId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmittedWorkStatus", "submittedWorkID", "dbo.SubmittedWorks");
            DropForeignKey("dbo.SemesterWorkDueDates", "workID", "dbo.Works");
            DropForeignKey("dbo.SemesterWorkDueDates", "semesterID", "dbo.Semesters");
            DropForeignKey("dbo.ScenarioTasks", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.ResourceFiles", "ResourceList_ID", "dbo.ResourceLists");
            DropForeignKey("dbo.ResourceLists", "statusID", "dbo.Status");
            DropForeignKey("dbo.ResourceFiles", "fileID", "dbo.Files");
            DropForeignKey("dbo.Reflections", "userID", "dbo.User");
            DropForeignKey("dbo.Reflections", "statusID", "dbo.Status");
            DropForeignKey("dbo.Reflections", "semesterID", "dbo.Semesters");
            DropForeignKey("dbo.Feedbacks", "submittedWorkID", "dbo.SubmittedWorks");
            DropForeignKey("dbo.SubmittedWorks", "GroupWorks_ID", "dbo.GroupWorks");
            DropForeignKey("dbo.Files", "SubmittedWork_ID", "dbo.SubmittedWorks");
            DropForeignKey("dbo.Feedbacks", "statusID", "dbo.Status");
            DropForeignKey("dbo.Feedbacks", "parentID", "dbo.Feedbacks");
            DropForeignKey("dbo.Semesters", "ActionPlanList_ID", "dbo.ActionPlanLists");
            DropForeignKey("dbo.Semesters", "statusID", "dbo.Status");
            DropForeignKey("dbo.Scenarios", "statusID", "dbo.Status");
            DropForeignKey("dbo.GroupWorks", "workID", "dbo.Works");
            DropForeignKey("dbo.Works", "statusID", "dbo.Status");
            DropForeignKey("dbo.Works", "scenarioID", "dbo.Scenarios");
            DropForeignKey("dbo.GroupWorks", "statusID", "dbo.Status");
            DropForeignKey("dbo.GroupWorks", "groupID", "dbo.Groups");
            DropForeignKey("dbo.User", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.RoleUsers", "User_UserId", "dbo.User");
            DropForeignKey("dbo.RoleUsers", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.Groups", "statusID", "dbo.Status");
            DropForeignKey("dbo.Groups", "semesterID", "dbo.Semesters");
            DropForeignKey("dbo.ActionPlanLists", "statusID", "dbo.Status");
            DropForeignKey("dbo.ScenarioSemesters", "Semester_ID", "dbo.Semesters");
            DropForeignKey("dbo.ScenarioSemesters", "Scenario_ID", "dbo.Scenarios");
            DropForeignKey("dbo.ActionPlanLists", "Scenario_ID", "dbo.Scenarios");
            DropIndex("dbo.SubmittedWorkStatus", new[] { "submittedWorkID" });
            DropIndex("dbo.SemesterWorkDueDates", new[] { "workID" });
            DropIndex("dbo.SemesterWorkDueDates", new[] { "semesterID" });
            DropIndex("dbo.ScenarioTasks", new[] { "SemesterID" });
            DropIndex("dbo.ResourceFiles", new[] { "ResourceList_ID" });
            DropIndex("dbo.ResourceLists", new[] { "statusID" });
            DropIndex("dbo.ResourceFiles", new[] { "fileID" });
            DropIndex("dbo.Reflections", new[] { "userID" });
            DropIndex("dbo.Reflections", new[] { "statusID" });
            DropIndex("dbo.Reflections", new[] { "semesterID" });
            DropIndex("dbo.Feedbacks", new[] { "submittedWorkID" });
            DropIndex("dbo.SubmittedWorks", new[] { "GroupWorks_ID" });
            DropIndex("dbo.Files", new[] { "SubmittedWork_ID" });
            DropIndex("dbo.Feedbacks", new[] { "statusID" });
            DropIndex("dbo.Feedbacks", new[] { "parentID" });
            DropIndex("dbo.Semesters", new[] { "ActionPlanList_ID" });
            DropIndex("dbo.Semesters", new[] { "statusID" });
            DropIndex("dbo.Scenarios", new[] { "statusID" });
            DropIndex("dbo.GroupWorks", new[] { "workID" });
            DropIndex("dbo.Works", new[] { "statusID" });
            DropIndex("dbo.Works", new[] { "scenarioID" });
            DropIndex("dbo.GroupWorks", new[] { "statusID" });
            DropIndex("dbo.GroupWorks", new[] { "groupID" });
            DropIndex("dbo.User", new[] { "Group_ID" });
            DropIndex("dbo.RoleUsers", new[] { "User_UserId" });
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleId" });
            DropIndex("dbo.Groups", new[] { "statusID" });
            DropIndex("dbo.Groups", new[] { "semesterID" });
            DropIndex("dbo.ActionPlanLists", new[] { "statusID" });
            DropIndex("dbo.ScenarioSemesters", new[] { "Semester_ID" });
            DropIndex("dbo.ScenarioSemesters", new[] { "Scenario_ID" });
            DropIndex("dbo.ActionPlanLists", new[] { "Scenario_ID" });
            DropTable("dbo.RoleUsers");
            DropTable("dbo.ScenarioSemesters");
            DropTable("dbo.SubmittedWorkStatus");
            DropTable("dbo.SemesterWorkDueDates");
            DropTable("dbo.ScenarioTasks");
            DropTable("dbo.ResourceLists");
            DropTable("dbo.ResourceFiles");
            DropTable("dbo.Reflections");
            DropTable("dbo.Files");
            DropTable("dbo.SubmittedWorks");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Works");
            DropTable("dbo.Roles");
            DropTable("dbo.User");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupWorks");
            DropTable("dbo.Status");
            DropTable("dbo.Scenarios");
            DropTable("dbo.Semesters");
            DropTable("dbo.ActionPlanLists");
        }
    }
}
