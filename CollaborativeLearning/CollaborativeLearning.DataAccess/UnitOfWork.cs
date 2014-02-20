using System;

using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private DataContext context = new DataContext();
        private ActionPlanRepository actionPlanRepository;
        private FeedbackRepository feedbackRepository;
        private GroupRepository groupRepository;
        private GroupWorkFileRepository groupWorkFileRepository;
        private GroupWorkRepository groupWorkRepository;
        private GroupWorkSubmittedStatusRepository groupWorkSubmittedStatusRepository;
        private MeetingNoteRepository meetingNoteRepository;
        private MeetingNoteFileRepository meetingNoteFileRepository;
        private ReflectionRepository reflectionRepository;
        private ResourceRepository resourceRepository;
        private ResourceFileRepository resourceFileRepository;
        private RoleRepository roleRepository;
        private ScenarioRepository scenarioRepository;
        private SemesterRepository semesterRepository;
        private TaskRepository taskRepository;
        private UserRepository userRepository;
        private WorkRepository workRepository;
        private WorkSemesterDueDateRepository workSemesterDueDateRepository;


        public ActionPlanRepository ActionPlanRepository
        {
            get
            {

                if (this.actionPlanRepository == null)
                {
                    this.actionPlanRepository = new ActionPlanRepository(context);
                }
                return actionPlanRepository;
            }
        }
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
        public GroupWorkRepository GroupWorkRepository
        {
            get
            {

                if (this.groupWorkRepository == null)
                {
                    this.groupWorkRepository = new GroupWorkRepository(context);
                }
                return groupWorkRepository;
            }
        }
        public GroupWorkFileRepository GroupWorkFileRepository
        {
            get
            {

                if (this.groupWorkFileRepository == null)
                {
                    this.groupWorkFileRepository = new GroupWorkFileRepository(context);
                }
                return groupWorkFileRepository;
            }
        }
        public GroupWorkSubmittedStatusRepository GroupWorkSubmittedStatusRepository
        {
            get
            {

                if (this.groupWorkSubmittedStatusRepository == null)
                {
                    this.groupWorkSubmittedStatusRepository = new GroupWorkSubmittedStatusRepository(context);
                }
                return groupWorkSubmittedStatusRepository;
            }
        }
        public MeetingNoteRepository MeetingNoteRepository
        {
            get
            {

                if (this.meetingNoteRepository == null)
                {
                    this.meetingNoteRepository = new MeetingNoteRepository(context);
                }
                return meetingNoteRepository;
            }
        }
        public MeetingNoteFileRepository MeetingNoteFileRepository
        {
            get
            {

                if (this.meetingNoteFileRepository == null)
                {
                    this.meetingNoteFileRepository = new MeetingNoteFileRepository(context);
                }
                return meetingNoteFileRepository;
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
        public ResourceRepository ResourceRepository
        {
            get
            {

                if (this.resourceRepository == null)
                {
                    this.resourceRepository = new ResourceRepository(context);
                }
                return resourceRepository;
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
        public TaskRepository TaskRepository
        {
            get
            {

                if (this.taskRepository == null)
                {
                    this.taskRepository = new TaskRepository(context);
                }
                return taskRepository;
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

        public WorkSemesterDueDateRepository WorkSemesterDueDateRepository
        {
            get
            {

                if (this.workSemesterDueDateRepository == null)
                {
                    this.workSemesterDueDateRepository = new WorkSemesterDueDateRepository(context);
                }
                return workSemesterDueDateRepository;
            }
        }
      
        // General
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
