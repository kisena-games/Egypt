namespace App.Scripts.Services
{
    public interface IServiceContainer
    {
        public T Register<T>(T implementation) where T : class, IService;

        public T Get<T>() where T : class, IService;
    }
}