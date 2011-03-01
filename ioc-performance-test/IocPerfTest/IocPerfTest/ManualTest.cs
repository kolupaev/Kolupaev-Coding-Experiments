using System;

namespace IocPerfTest
{
    public class ManualTest : IIocUnnderTest, IResolver

    {
        private TargetStructure.Sigleton1 _s1;
        private TargetStructure.Sigleton2 _s2;
        private TargetStructure.Sigleton3 _s3;
        private TargetStructure.Sigleton4 _s4;

        [ThreadStatic]
        private TargetStructure.PerHttpRequest1 _p1;
        [ThreadStatic]
        private TargetStructure.PerHttpRequest2 _p2;
        [ThreadStatic]
        private TargetStructure.PerHttpRequest3 _p3;
        [ThreadStatic]
        private TargetStructure.PerHttpRequest4 _p4;

        public string Name
        {
            get { return "Manual"; }
        }

        public void Register<T>()
        {
        }

        public void RegisterSingleton<T>()
        {
        }

        public void RegisterPerScope<T>()
        {
        }

        public void Buildup()
        {
            _s1 = new TargetStructure.Sigleton1();
            _s2 = new TargetStructure.Sigleton2(_s1);
            _s3 = new TargetStructure.Sigleton3(_s1, _s2);
            _s4 = new TargetStructure.Sigleton4(_s1, _s2, _s3);
        }

        public IResolver StartScope()
        {
            _p1 = new TargetStructure.PerHttpRequest1(_s1, _s2, _s3, _s4);
            _p2 = new TargetStructure.PerHttpRequest2(_s1, _s2, _s3, _s4, _p1);
            _p3 = new TargetStructure.PerHttpRequest3(_s1, _s2, _s3, _s4, _p1, _p2);
            _p4 = new TargetStructure.PerHttpRequest4(_s1, _s2, _s3, _s4, _p1, _p2, _p3);
            return this;
        }

        public void EndScope(IResolver scope)
        {
        }

        public void Resolve<T>()
        {
            new TargetStructure(_s1, _s2, _s3, _s4, _p1, _p2, _p3, _p4);
        }
    }
}