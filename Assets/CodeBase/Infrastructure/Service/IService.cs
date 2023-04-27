namespace CodeBase.Infrastructure.Service
{
    public interface IService
    {
    }

    public interface IPlayableService : IService
    {
        public void Start();
        public void Stop();
    }
}