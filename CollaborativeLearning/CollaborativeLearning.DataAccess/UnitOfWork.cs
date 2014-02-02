using System;

using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private DataContext context = new DataContext();
        private FeedbackRepository feedbackRepository;
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

        public FeedbackRepository FeedbackRepository
        {
            get
            {

                if (this.feedbackRepository == null)
                {
                    this.feedbackRepository = new FeedbackRepository(context);
                }
                return feedbackRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }
        public ActionPlanListRepository ActionPlanListRepository
        {
            get
            {

                if (this.actionPlanListRepository == null)
                {
                    this.actionPlanListRepository = new ActionPlanListRepository(context);
                }
                return actionPlanListRepository;
            }
        }
        public FileRepository FileRepository
        {
            get
            {

                if (this.fileRepository == null)
                {
                    this.fileRepository = new FileRepository(context);
                }
                return fileRepository;
            }
        }
        public GroupRepository GroupRepository
        {
            get
            {

                if (this.groupRepository == null)
                {
                    this.groupRepository = new GroupRepository(context);
                }
                return groupRepository;
            }
        }
        public ReflectionRepository ReflectionRepository
        {
            get
            {

                if (this.reflectionRepository == null)
                {
                    this.reflectionRepository = new ReflectionRepository(context);
                }
                return reflectionRepository;
            }
        }
        public ResourceFileRepository ResourceFileRepository
        {
            get
            {

                if (this.resourceFileRepository == null)
                {
                    this.resourceFileRepository = new ResourceFileRepository(context);
                }
                return resourceFileRepository;
            }
        }
        public ResourceListRepository ResourceListRepository
        {
            get
            {

                if (this.resourceListRepository == null)
                {
                    this.resourceListRepository = new ResourceListRepository(context);
                }
                return resourceListRepository;
            }
        }
        public RoleRepository RoleRepository
        {
            get
            {

                if (this.roleRepository == null)
                {
                    this.roleRepository = new RoleRepository(context);
                }
                return roleRepository;
            }
        }
        public ScenarioRepository ScenarioRepository
        {
            get
            {

                if (this.scenarioRepository == null)
                {
                    this.scenarioRepository = new ScenarioRepository(context);
                }
                return scenarioRepository;
            }
        }
        public ScenarioTaskRepository ScenarioTaskRepository
        {
            get
            {

                if (this.scenarioTaskRepository == null)
                {
                    this.scenarioTaskRepository = new ScenarioTaskRepository(context);
                }
                return scenarioTaskRepository;
            }
        }
        public SemesterRepository SemesterRepository
        {
            get
            {

                if (this.semesterRepository == null)
                {
                    this.semesterRepository = new SemesterRepository(context);
                }
                return semesterRepository;
            }
        }
        public SemesterWorkDueDateRepository SemesterWorkDueDateRepository
        {
            get
            {

                if (this.semesterWorkDueDateRepository == null)
                {
                    this.semesterWorkDueDateRepository = new SemesterWorkDueDateRepository(context);
                }
                return semesterWorkDueDateRepository;
            }
        }
        public StatusRepository StatusRepository
        {
            get
            {

                if (this.statusRepository == null)
                {
                    this.statusRepository = new StatusRepository(context);
                }
                return statusRepository;
            }
        }
        public SubmittedWorkRepository SubmittedWorkRepository
        {
            get
            {

                if (this.submittedWorkRepository == null)
                {
                    this.submittedWorkRepository = new SubmittedWorkRepository(context);
                }
                return submittedWorkRepository;
            }
        }
        public SubmittedWorkStatusRepository SubmittedWorkStatusRepository
        {
            get
            {

                if (this.submittedWorkStatusRepository == null)
                {
                    this.submittedWorkStatusRepository = new SubmittedWorkStatusRepository(context);
                }
                return submittedWorkStatusRepository;
            }
        }
        public WorkRepository WorkRepository
        {
            get
            {

                if (this.workRepository == null)
                {
                    this.workRepository = new WorkRepository(context);
                }
                return workRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public bool disposed = false;

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
