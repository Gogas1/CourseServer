namespace CourseServer.Api.Commands
{
    public abstract class Command
    {
        public abstract Task<MasterMessage> Execute(string content);
    }
}
