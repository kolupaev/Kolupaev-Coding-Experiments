using System;
using Autofac;

namespace IocPerfTest
{
    public class AutofacTest : IIocUnnderTest
    {
        class Resolver : IResolver
        {
            private readonly ILifetimeScope _scope;

            public Resolver(ILifetimeScope scope)
            {
                _scope = scope;
            }

            public ILifetimeScope Scope
            {
                get { return _scope; }
            }

            public void Resolve<T>()
            {
                _scope.Resolve<T>();
            }
        }
        private readonly ContainerBuilder _builder;
        private IContainer _container;

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

        public IResolver StartScope()
        {
            return new Resolver(_container.BeginLifetimeScope());
        }

        public void EndScope(IResolver r)
        {
            ((Resolver)r).Scope.Dispose();
        }
    }
}