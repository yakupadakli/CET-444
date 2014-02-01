using System;

using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        public DataContext context = new DataContext();
        public FeedbackRepository feedbackRepository;
        private UserRepository userRepository;
        private ActionPlanListRepository actionPlanListRepository;
        private FileRepository fileRepository;
        private GroupRepository groupRepository;
        private ReflectionRepository reflectionRepository;
        private ResourceFileRepository resourceFileRepository;
        private ResourceListRepository resourceListRepository;
        private RoleRepository roleRepository;
        private ScenarioRepository scenarioRepository;
        private ScenarioTaskRepository scenarioTaskRepository;
        private SemesterRepository semesterRepository;
        private SemesterWorkDueDateRepository semesterWorkDueDateRepository;
        private StatusRepository statusRepository;
        private SubmittedWorkRepository submittedWorkRepository;
        private SubmittedWorkStatusRepository submittedWorkStatusRepository;
        private WorkRepository workRepository;

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    
}
