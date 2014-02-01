using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public DbSet<ActionPlanList> ActionPlanLists { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Reflection> Reflections { get; set; }
        public DbSet<ResourceFile> ResourceFiles { get; set; }
        public DbSet<ResourceList> ResourceLists { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<ScenarioTask> ScenarioTasks { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SemesterWorkDueDate> SemesterWorkDueDates { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<SubmittedWork> SubmittedWorks { get; set; }
        public DbSet<SubmittedWorkStatus> SubmittedWorkStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Work>()
                .HasRequired(w => w.Status)
                .WithMany(s => s.Works)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Work>()
                .HasRequired(w => w.Scenario)
                .WithMany(s => s.Works)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasRequired(s => s.Status)
                .WithMany(s => s.Semesters)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupWork>()
                .HasRequired(gw => gw.Status)
                .WithMany(s => s.GroupWorks)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ActionPlanList>()
            //    .HasRequired(apl => apl.Status)
            //    .WithMany(s => s.ActionPlanLists)
            //    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
