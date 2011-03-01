using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace IocPerfTest
{
    public class UnityTest : IIocUnnderTest, IResolver
    {
       
        private UnityContainer _container;

        public UnityTest()
        {
            _container = new UnityContainer();
        }

        public string Name
        {
            get { return "Unity"; }
        }

        public void Register<T>()
        {
            _container.RegisterType<T>();
        }

        public void RegisterSingleton<T>()
        {
            _container.RegisterType<T>(new ContainerControlledLifetimeManager());
        }

        public void RegisterPerScope<T>()
        {
            _container.RegisterType<T>(new PerThreadLifetimeManager());
        }

        public void Buildup()
        {
        }

        IResolver IIocUnnderTest.StartScope()
        {
            return this;
        }

        public void EndScope(IResolver scope)
        {
        }

        public void Resolve<T>()
        {
            _container.Resolve<T>();
        }
    }
}