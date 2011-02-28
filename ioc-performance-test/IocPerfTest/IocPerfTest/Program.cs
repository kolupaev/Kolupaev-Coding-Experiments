using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IocPerfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int iterationCount = 1000;
            var targets = new List<IIocUnnderTest> {new AutofacTest(), new UnityTest(), new NinjectTest(), new StructureMapTest()};

            foreach (var target in targets)
            {
                target.RegisterSingleton<TargetStructure.Sigleton1>();
                target.RegisterSingleton<TargetStructure.Sigleton2>();
                target.RegisterSingleton<TargetStructure.Sigleton3>();
                target.RegisterSingleton<TargetStructure.Sigleton4>();
                target.RegisterPerScope<TargetStructure.PerHttpRequest1>();
                target.RegisterPerScope<TargetStructure.PerHttpRequest2>();
                target.RegisterPerScope<TargetStructure.PerHttpRequest3>();
                target.RegisterPerScope<TargetStructure.PerHttpRequest4>();
                target.Register<TargetStructure>();

                target.Buildup();
                target.StartScope();
                target.Resolve<TargetStructure>();
                target.EndScope();
                System.GC.Collect();
                System.GC.WaitForFullGCComplete();
                var sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < iterationCount; i++)
                {
                    target.StartScope();
                    for (int j = 0; j < 20; j++)
                    {
                        target.Resolve<TargetStructure>();
                    }
                    target.EndScope();
                }
                sw.Stop();
                Console.WriteLine("{0}\t{1}ms", target.Name, sw.ElapsedMilliseconds);
            }
        }
    }

    interface IIocUnnderTest
    {
        string Name { get; }
        void Register<T>();
        void RegisterSingleton<T>();
        void RegisterPerScope<T>();

        void Buildup();
        void StartScope();
        void EndScope();
        void Resolve<T>();
    }

    class TargetStructure
    {
        public class Sigleton1{}
        public class Sigleton2
        {
            private Sigleton1 _s1;

            public Sigleton2(Sigleton1 s1)
            {
                _s1 = s1;
            }
        }
        public class Sigleton3
        {
            private Sigleton1 _s1;
            private Sigleton2 _s2;

            public Sigleton3(Sigleton1 s1, Sigleton2 s2)
            {
                _s1 = s1;
                _s2 = s2;
            }
        }
        public class Sigleton4
        {
            private Sigleton1 _s1;
            private Sigleton2 _s2;
            private Sigleton3 _s3;

            public Sigleton4(Sigleton1 s1, Sigleton2 s2, Sigleton3 s3)
            {
                _s1 = s1;
                _s2 = s2;
                _s3 = s3;
            }
        }

        public class PerHttpRequest1
        {
            private Sigleton1 _s1;
            private Sigleton2 _s2;
            private Sigleton3 _s3;
            private Sigleton4 _s4;

            public PerHttpRequest1(Sigleton1 s1, Sigleton2 s2, Sigleton3 s3, Sigleton4 s4)
            {
                _s1 = s1;
                _s2 = s2;
                _s3 = s3;
                _s4 = s4;
            }
        }
        public class PerHttpRequest2
        {
            private Sigleton1 _s1;
            private Sigleton2 _s2;
            private Sigleton3 _s3;
            private Sigleton4 _s4;

            private PerHttpRequest1 _p1;

            public PerHttpRequest2(Sigleton1 s1, Sigleton2 s2, Sigleton3 s3, Sigleton4 s4, PerHttpRequest1 p1)
            {
                _s1 = s1;
                _s2 = s2;
                _s3 = s3;
                _s4 = s4;
                _p1 = p1;
            }
        }
        public class PerHttpRequest3
        {
            private Sigleton1 _s1;
            private Sigleton2 _s2;
            private Sigleton3 _s3;
            private Sigleton4 _s4;

            private PerHttpRequest1 _p1;
            private PerHttpRequest2 _p2;

            public PerHttpRequest3(Sigleton1 s1, Sigleton2 s2, Sigleton3 s3, Sigleton4 s4, PerHttpRequest1 p1, PerHttpRequest2 p2)
            {
                _s1 = s1;
                _s2 = s2;
                _s3 = s3;
                _s4 = s4;
                _p1 = p1;
                _p2 = p2;
            }
        }
        public class PerHttpRequest4
        {
            private Sigleton1 _s1;
            private Sigleton2 _s2;
            private Sigleton3 _s3;
            private Sigleton4 _s4;
        
            private PerHttpRequest1 _p1;
            private PerHttpRequest2 _p2;
            private PerHttpRequest3 _p3;

            public PerHttpRequest4(Sigleton1 s1, Sigleton2 s2, Sigleton3 s3, Sigleton4 s4, PerHttpRequest1 p1, PerHttpRequest2 p2, PerHttpRequest3 p3)
            {
                _s1 = s1;
                _s2 = s2;
                _s3 = s3;
                _s4 = s4;
                _p1 = p1;
                _p2 = p2;
                _p3 = p3;
            }
        }

        private Sigleton1 _s1;
        private Sigleton2 _s2;
        private Sigleton3 _s3;
        private Sigleton4 _s4;
        private PerHttpRequest1 _p1;
        private PerHttpRequest2 _p2;
        private PerHttpRequest3 _p3;
        private PerHttpRequest4 _p4;

        public TargetStructure(Sigleton1 s1, Sigleton2 s2, Sigleton3 s3, Sigleton4 s4, PerHttpRequest1 p1, PerHttpRequest2 p2, PerHttpRequest3 p3, PerHttpRequest4 p4)
        {
            _s1 = s1;
            _s2 = s2;
            _s3 = s3;
            _s4 = s4;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;
        }
    }
}
