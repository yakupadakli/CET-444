using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DataContext")
        {

        }
        public DbSet<ActionPlan> ActionPlans { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupWork> GroupWorks { get; set; }
        public DbSet<GroupWorkFile> GroupWorkFiles { get; set; }
        public DbSet<GroupWorkSubmittedStatus> GroupWorkSubmittedStatus { get; set; }
        public DbSet<MeetingNote> MeetingNotes { get; set; }
        public DbSet<MeetingNoteFile> MeetingNoteFiles { get; set; }
        public DbSet<Reflection> Reflections { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceFile> ResourceFiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkSemesterDueDate> WorkSemesterDueDates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

          

            //modelBuilder.Entity<Work>()
            //    .HasRequired(w => w.Scenarios)
            //    .WithMany(s => s.Works)
            //    .WillCascadeOnDelete(false);

          

            //modelBuilder.Entity<ActionPlanList>()
            //    .HasRequired(apl => apl.Status)
            //    .WithMany(s => s.ActionPlanLists)
            //    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
