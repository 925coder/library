using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coder925.library;

namespace library.tests
{
  [TestFixture]
  public class IEnumerableExTests
  {
    [Test]
    public void TestEmpty()
    {
      var l = new List<int> { };
      Assert.IsTrue(l.IsEmpty());

      l = null;
      Assert.IsTrue(l.IsEmpty());

      l = new List<int> { 1 };
      Assert.IsFalse(l.IsEmpty());
    }

    [Test]
    public void TestToCsv()
    {
      var l = new List<int> { };
      Assert.IsTrue(l.ToCsv() == string.Empty);

      l = null;
      Assert.IsTrue(l.ToCsv() == string.Empty);

      l = new List<int> {1,2,3};
      Assert.IsTrue(l.ToCsv() == "1,2,3");
    }
  }
}
