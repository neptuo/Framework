using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class TestDisposable
    {
        public static void Test()
        {

        }
    }
}

namespace Neptuo.Testing
{
    public class Test : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
