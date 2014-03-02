namespace CollaborativeLearning.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class liveInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 500),
                        OrderID = c.Int(nullable: false),
                        RegUserId = c.Int(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scenarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ShortDescription = c.String(maxLength: 140),
                        isActive = c.Boolean(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        RegUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false, maxLength: 50),
                        SemesterID = c.Int(nullable: false),
                        RegUserID = c.Int(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .Index(t => t.SemesterID);
            
            CreateTable(
                "dbo.GroupWorks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkId = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        Content = c.String(),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Works", t => t.WorkId, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.WorkId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        GroupWorkId = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 500),
                        parentID = c.Int(),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feedbacks", t => t.parentID)
                .ForeignKey("dbo.GroupWorks", t => t.GroupWorkId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.parentID)
                .Index(t => t.GroupWorkId)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        StudentNo = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
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
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Reflections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        semesterID = c.Int(nullable: false),
                        userID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false, maxLength: 500),
                        isRead = c.Boolean(nullable: false),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.semesterID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.userID, cascadeDelete: true)
                .Index(t => t.semesterID)
                .Index(t => t.userID);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        year = c.Int(nullable: false),
                        semester = c.Int(nullable: false),
                        CourseName = c.String(nullable: false),
                        Section = c.Int(nullable: false),
                        registerCode = c.String(),
                        mentorRegisterCode = c.String(),
                        isActive = c.Boolean(nullable: false),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        type = c.String(nullable: false, maxLength: 25),
                        RegDate = c.DateTime(nullable: false),
                        RegUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResourceFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceID = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        FileSize = c.Int(nullable: false),
                        FileType = c.String(),
                        FileUrl = c.String(),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resources", t => t.ResourceID, cascadeDelete: true)
                .Index(t => t.ResourceID);
            
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
                "dbo.GroupWorkFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupWorkID = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        FileSize = c.Int(nullable: false),
                        FileType = c.String(),
                        FileUrl = c.String(),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupWorks", t => t.GroupWorkID, cascadeDelete: true)
                .Index(t => t.GroupWorkID);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        OrderID = c.Int(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        RegUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkSemesterDueDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SemesterID = c.Int(nullable: false),
                        WorkID = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        RegUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .ForeignKey("dbo.Works", t => t.WorkID, cascadeDelete: true)
                .Index(t => t.SemesterID)
                .Index(t => t.WorkID);
            
            CreateTable(
                "dbo.MeetingNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.MeetingNoteFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeetingNoteID = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        FileSize = c.Int(nullable: false),
                        FileType = c.String(),
                        FileUrl = c.String(),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingNotes", t => t.MeetingNoteID, cascadeDelete: true)
                .Index(t => t.MeetingNoteID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        OrderID = c.Int(nullable: false),
                        RegUserId = c.Int(nullable: false),
                        RegDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeedbackSeenLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        GroupWorkkID = c.Int(nullable: false),
                        isSeen = c.Boolean(nullable: false),
                        lastSeenDate = c.DateTime(nullable: false),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        GroupWork_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupWorks", t => t.GroupWork_Id)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.GroupWork_Id)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.GroupWorkSubmittedStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        GroupWorkkID = c.Int(nullable: false),
                        isSeen = c.Boolean(nullable: false),
                        lastSeenDate = c.DateTime(nullable: false),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                        GroupWork_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupWorks", t => t.GroupWork_Id)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.GroupWork_Id)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.StudentCourseRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        isApproved = c.Boolean(nullable: false),
                        reqDate = c.DateTime(nullable: false),
                        regDate = c.DateTime(nullable: false),
                        regUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SemesterId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ScenarioActionPlans",
                c => new
                    {
                        Scenario_Id = c.Int(nullable: false),
                        ActionPlan_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Scenario_Id, t.ActionPlan_Id })
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id, cascadeDelete: true)
                .ForeignKey("dbo.ActionPlans", t => t.ActionPlan_Id, cascadeDelete: true)
                .Index(t => t.Scenario_Id)
                .Index(t => t.ActionPlan_Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Group_Id })
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.ResourceScenarios",
                c => new
                    {
                        Resource_Id = c.Int(nullable: false),
                        Scenario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resource_Id, t.Scenario_Id })
                .ForeignKey("dbo.Resources", t => t.Resource_Id, cascadeDelete: true)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id, cascadeDelete: true)
                .Index(t => t.Resource_Id)
                .Index(t => t.Scenario_Id);
            
            CreateTable(
                "dbo.ResourceSemesters",
                c => new
                    {
                        Resource_Id = c.Int(nullable: false),
                        Semester_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resource_Id, t.Semester_Id })
                .ForeignKey("dbo.Resources", t => t.Resource_Id, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.Semester_Id, cascadeDelete: true)
                .Index(t => t.Resource_Id)
                .Index(t => t.Semester_Id);
            
            CreateTable(
                "dbo.SemesterScenarios",
                c => new
                    {
                        Semester_Id = c.Int(nullable: false),
                        Scenario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Semester_Id, t.Scenario_Id })
                .ForeignKey("dbo.Semesters", t => t.Semester_Id, cascadeDelete: true)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id, cascadeDelete: true)
                .Index(t => t.Semester_Id)
                .Index(t => t.Scenario_Id);
            
            CreateTable(
                "dbo.SemesterUsers",
                c => new
                    {
                        Semester_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Semester_Id, t.User_Id })
                .ForeignKey("dbo.Semesters", t => t.Semester_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Semester_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.WorkScenarios",
                c => new
                    {
                        Work_Id = c.Int(nullable: false),
                        Scenario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Work_Id, t.Scenario_Id })
                .ForeignKey("dbo.Works", t => t.Work_Id, cascadeDelete: true)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id, cascadeDelete: true)
                .Index(t => t.Work_Id)
                .Index(t => t.Scenario_Id);
            
            CreateTable(
                "dbo.GroupScenarios",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Scenario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Scenario_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Scenario_Id);
            
            CreateTable(
                "dbo.TaskScenarios",
                c => new
                    {
                        Task_Id = c.Int(nullable: false),
                        Scenario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_Id, t.Scenario_Id })
                .ForeignKey("dbo.Tasks", t => t.Task_Id, cascadeDelete: true)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id, cascadeDelete: true)
                .Index(t => t.Task_Id)
                .Index(t => t.Scenario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourseRequests", "UserId", "dbo.User");
            DropForeignKey("dbo.StudentCourseRequests", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.GroupWorkSubmittedStatus", "UserID", "dbo.User");
            DropForeignKey("dbo.GroupWorkSubmittedStatus", "GroupWork_Id", "dbo.GroupWorks");
            DropForeignKey("dbo.FeedbackSeenLogs", "UserID", "dbo.User");
            DropForeignKey("dbo.FeedbackSeenLogs", "GroupWork_Id", "dbo.GroupWorks");
            DropForeignKey("dbo.TaskScenarios", "Scenario_Id", "dbo.Scenarios");
            DropForeignKey("dbo.TaskScenarios", "Task_Id", "dbo.Tasks");
            DropForeignKey("dbo.GroupScenarios", "Scenario_Id", "dbo.Scenarios");
            DropForeignKey("dbo.GroupScenarios", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.MeetingNoteFiles", "MeetingNoteID", "dbo.MeetingNotes");
            DropForeignKey("dbo.MeetingNotes", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.WorkSemesterDueDates", "WorkID", "dbo.Works");
            DropForeignKey("dbo.WorkSemesterDueDates", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.WorkScenarios", "Scenario_Id", "dbo.Scenarios");
            DropForeignKey("dbo.WorkScenarios", "Work_Id", "dbo.Works");
            DropForeignKey("dbo.GroupWorks", "WorkId", "dbo.Works");
            DropForeignKey("dbo.GroupWorkFiles", "GroupWorkID", "dbo.GroupWorks");
            DropForeignKey("dbo.GroupWorks", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Feedbacks", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Reflections", "userID", "dbo.User");
            DropForeignKey("dbo.SemesterUsers", "User_Id", "dbo.User");
            DropForeignKey("dbo.SemesterUsers", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.SemesterScenarios", "Scenario_Id", "dbo.Scenarios");
            DropForeignKey("dbo.SemesterScenarios", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.ResourceSemesters", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.ResourceSemesters", "Resource_Id", "dbo.Resources");
            DropForeignKey("dbo.ResourceScenarios", "Scenario_Id", "dbo.Scenarios");
            DropForeignKey("dbo.ResourceScenarios", "Resource_Id", "dbo.Resources");
            DropForeignKey("dbo.ResourceFiles", "ResourceID", "dbo.Resources");
            DropForeignKey("dbo.Reflections", "semesterID", "dbo.Semesters");
            DropForeignKey("dbo.Groups", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.UserGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.UserGroups", "User_Id", "dbo.User");
            DropForeignKey("dbo.Feedbacks", "GroupWorkId", "dbo.GroupWorks");
            DropForeignKey("dbo.Feedbacks", "parentID", "dbo.Feedbacks");
            DropForeignKey("dbo.ScenarioActionPlans", "ActionPlan_Id", "dbo.ActionPlans");
            DropForeignKey("dbo.ScenarioActionPlans", "Scenario_Id", "dbo.Scenarios");
            DropIndex("dbo.StudentCourseRequests", new[] { "UserId" });
            DropIndex("dbo.StudentCourseRequests", new[] { "SemesterId" });
            DropIndex("dbo.GroupWorkSubmittedStatus", new[] { "UserID" });
            DropIndex("dbo.GroupWorkSubmittedStatus", new[] { "GroupWork_Id" });
            DropIndex("dbo.FeedbackSeenLogs", new[] { "UserID" });
            DropIndex("dbo.FeedbackSeenLogs", new[] { "GroupWork_Id" });
            DropIndex("dbo.TaskScenarios", new[] { "Scenario_Id" });
            DropIndex("dbo.TaskScenarios", new[] { "Task_Id" });
            DropIndex("dbo.GroupScenarios", new[] { "Scenario_Id" });
            DropIndex("dbo.GroupScenarios", new[] { "Group_Id" });
            DropIndex("dbo.MeetingNoteFiles", new[] { "MeetingNoteID" });
            DropIndex("dbo.MeetingNotes", new[] { "GroupID" });
            DropIndex("dbo.WorkSemesterDueDates", new[] { "WorkID" });
            DropIndex("dbo.WorkSemesterDueDates", new[] { "SemesterID" });
            DropIndex("dbo.WorkScenarios", new[] { "Scenario_Id" });
            DropIndex("dbo.WorkScenarios", new[] { "Work_Id" });
            DropIndex("dbo.GroupWorks", new[] { "WorkId" });
            DropIndex("dbo.GroupWorkFiles", new[] { "GroupWorkID" });
            DropIndex("dbo.GroupWorks", new[] { "GroupID" });
            DropIndex("dbo.Feedbacks", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.Reflections", new[] { "userID" });
            DropIndex("dbo.SemesterUsers", new[] { "User_Id" });
            DropIndex("dbo.SemesterUsers", new[] { "Semester_Id" });
            DropIndex("dbo.SemesterScenarios", new[] { "Scenario_Id" });
            DropIndex("dbo.SemesterScenarios", new[] { "Semester_Id" });
            DropIndex("dbo.ResourceSemesters", new[] { "Semester_Id" });
            DropIndex("dbo.ResourceSemesters", new[] { "Resource_Id" });
            DropIndex("dbo.ResourceScenarios", new[] { "Scenario_Id" });
            DropIndex("dbo.ResourceScenarios", new[] { "Resource_Id" });
            DropIndex("dbo.ResourceFiles", new[] { "ResourceID" });
            DropIndex("dbo.Reflections", new[] { "semesterID" });
            DropIndex("dbo.Groups", new[] { "SemesterID" });
            DropIndex("dbo.UserGroups", new[] { "Group_Id" });
            DropIndex("dbo.UserGroups", new[] { "User_Id" });
            DropIndex("dbo.Feedbacks", new[] { "GroupWorkId" });
            DropIndex("dbo.Feedbacks", new[] { "parentID" });
            DropIndex("dbo.ScenarioActionPlans", new[] { "ActionPlan_Id" });
            DropIndex("dbo.ScenarioActionPlans", new[] { "Scenario_Id" });
            DropTable("dbo.TaskScenarios");
            DropTable("dbo.GroupScenarios");
            DropTable("dbo.WorkScenarios");
            DropTable("dbo.SemesterUsers");
            DropTable("dbo.SemesterScenarios");
            DropTable("dbo.ResourceSemesters");
            DropTable("dbo.ResourceScenarios");
            DropTable("dbo.UserGroups");
            DropTable("dbo.ScenarioActionPlans");
            DropTable("dbo.StudentCourseRequests");
            DropTable("dbo.GroupWorkSubmittedStatus");
            DropTable("dbo.FeedbackSeenLogs");
            DropTable("dbo.Tasks");
            DropTable("dbo.MeetingNoteFiles");
            DropTable("dbo.MeetingNotes");
            DropTable("dbo.WorkSemesterDueDates");
            DropTable("dbo.Works");
            DropTable("dbo.GroupWorkFiles");
            DropTable("dbo.Roles");
            DropTable("dbo.ResourceFiles");
            DropTable("dbo.Resources");
            DropTable("dbo.Semesters");
            DropTable("dbo.Reflections");
            DropTable("dbo.User");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.GroupWorks");
            DropTable("dbo.Groups");
            DropTable("dbo.Scenarios");
            DropTable("dbo.ActionPlans");
        }
    }
}
