using System;
using Autofac;

namespace IocPerfTest
{
    public class AutofacTest : IIocUnnderTest
    {
        private ContainerBuilder _builder;
        private IContainer _container;
        private ILifetimeScope _scope;

        public AutofacTest()
        {
            _builder = new ContainerBuilder();

        }

        public string Name
        {
            get { return "Autofac"; }
        }

        public void Register<T>()
        {
            _builder.RegisterType<T>();
        }

        public void RegisterSingleton<T>()
        {
            _builder.RegisterType<T>().SingleInstance();
        }

        public void RegisterPerScope<T>()
        {
            _builder.RegisterType<T>().InstancePerLifetimeScope();
        }

        public void Buildup()
        {
            _container = _builder.Build();
        }

        public void StartScope()
        {
            _scope = _container.BeginLifetimeScope();
        }

        public void EndScope()
        {
            _scope.Dispose();
        }

        public void Resolve<T>()
        {
            _scope.Resolve<T>();
        }
    }
}