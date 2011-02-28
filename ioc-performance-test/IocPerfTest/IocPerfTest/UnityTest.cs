using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace IocPerfTest
{
    public class UnityTest : IIocUnnderTest
    {
        private UnityContainer _container;
        private List<Type> _perScope = new List<Type>();
        private IUnityContainer _child;

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
            _perScope.Add(typeof(T));
        }

        public void Buildup()
        {
        }

        public void StartScope()
        {
            _child = _container.CreateChildContainer();
            foreach (var type in _perScope)
            {
                _child.RegisterType(type, new ContainerControlledLifetimeManager());
            }
        }

        public void EndScope()
        {
            _child.Dispose();
        }

        public void Resolve<T>()
        {
            _child.Resolve<T>();
        }
    }
}