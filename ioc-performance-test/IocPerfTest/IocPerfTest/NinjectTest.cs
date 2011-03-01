using System;
using Ninject;

namespace IocPerfTest
{
    public class NinjectTest : IIocUnnderTest, IResolver
    {
        private readonly StandardKernel _container;
        [ThreadStatic]
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

        public IResolver StartScope()
        {
            _scope = new object();
            return this;
        }

        public void EndScope(IResolver scope)
        {
            
        }


        public void Resolve<T>()
        {
            _container.Get<T>();
        }
    }
}