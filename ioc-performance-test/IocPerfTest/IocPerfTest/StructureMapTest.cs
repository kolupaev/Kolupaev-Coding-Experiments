using System;
using StructureMap;
using StructureMap.Pipeline;

namespace IocPerfTest
{
    public class StructureMapTest : IIocUnnderTest, IResolver
    {
        
        public class Lifecycle : ILifecycle{
            [ThreadStatic]
            private MainObjectCache _cache;

            public void New()
            {
                _cache = new MainObjectCache();
            }
            public void EjectAll()
            {
            }

            public IObjectCache FindCache()
            {
                return _cache;
            }

            public string Scope
            {
                get { return "s1"; }
            }
        }
        public string Name
        {
            get { return "StructureMap"; }
        }

        private Action<ConfigurationExpression> _config = (x) => { };
        private Container _container;
        private Lifecycle _lifecycle;

        private void Add(Action<ConfigurationExpression> e)
        {
            var old = _config;
            _config = (x) =>
                          {
                              e(x);
                              old(x);
                          };
        }

        public StructureMapTest()
        {
            _lifecycle = new Lifecycle();
        }

        public void Register<T>()
        {
            Add(x => x.ForConcreteType<T>());
        }

        public void RegisterSingleton<T>()
        {
            Add(x => x.For<T>().Singleton().Use<T>());
        }

        public void RegisterPerScope<T>()
        {
            Add(x => x.For<T>().LifecycleIs(_lifecycle).Use<T>());
        }

        public void Buildup()
        {
            _container = new Container(_config);
        }

        IResolver IIocUnnderTest.StartScope()
        {
            _lifecycle.New();
            return this;
        }

        public void EndScope(IResolver scope)
        {
        }

        

        public void Resolve<T>()
        {
            _container.GetInstance<T>();
        }
    }
}