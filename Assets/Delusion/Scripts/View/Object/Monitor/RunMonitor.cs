using System.Numerics;
using NUnit.Framework.Internal;

namespace Dajunctic
{
    public class RunMonitor: BaseMono
    {
        Vector3Data test = new Vector3Data(1, 2, 3);

        void Main()
        {
            Vector3 test2 = test;
        }
    }
}