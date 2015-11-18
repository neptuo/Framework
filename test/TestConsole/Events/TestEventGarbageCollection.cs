using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Events
{
    class TestEventGarbageCollection : TestClass
    {
        public static void Test()
        {
            Target t = new Target();
            CreateChild(t);
            GC.Collect();
            t.RunX();
        }

        static void CreateChild(Target t)
        {
            Child c = new Child(t);
            t.RunX();
            //c.Remove(t);
        }
    }

    class Target
    {
        public List<WeakReference<ISubscribeX>> Listeners;

        public Target()
        {
            Listeners = new List<WeakReference<ISubscribeX>>();
        }

        public void RunX()
        {
            foreach (WeakReference<ISubscribeX> item in Listeners)
            {
                ISubscribeX x;
                if (item.TryGetTarget(out x))
                    x.OnX(this, EventArgs.Empty);
            }
        }
    }

    interface ISubscribeX
    {
        void OnX(object sender, EventArgs e);
    }

    class Child : ISubscribeX
    {
        private WeakReference<ISubscribeX> weak;

        public Child(Target t)
        {
            weak = new WeakReference<ISubscribeX>(this, true);
            t.Listeners.Add(weak);
        }

        ~Child()
        {
            Console.WriteLine("Destruct child");
        }

        public void OnX(object sender, EventArgs e)
        {
            Console.WriteLine("On X in child.");
        }

        public void Remove(Target t)
        {
            t.Listeners.Remove(weak);
        }
    }
}
