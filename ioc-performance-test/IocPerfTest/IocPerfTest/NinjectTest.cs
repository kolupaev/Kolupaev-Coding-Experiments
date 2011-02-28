using System;
using Ninject;

namespace IocPerfTest
{
    public class NinjectTest : IIocUnnderTest
    {
        private readonly StandardKernel _container;
        private object _scope;

        public string Name
        {
            get { return "Ninject"; }
        }

        public NinjectTest()
        {
            _container = new StandardKernel();
        }

        public void Register<T>()
        {
            _container.Bind<T>().ToSelf();
        }

        public void RegisterSingleton<T>()
        {
            _container.Bind<T>().ToSelf().InSingletonScope();
        }

        public void RegisterPerScope<T>()
        {
            _container.Bind<T>().ToSelf().InScope(q => _scope);
        }

        public void Buildup()
        {
            
        }

        public void StartScope()
        {
            _scope = new object();
        }

        public void EndScope()
        {
            _scope = null;
        }

        public void Resolve<T>()
        {
            _container.Get<T>();
        }
    }
}