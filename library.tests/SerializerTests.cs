using coder925.library;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.tests
{

  interface IFoo { }
  class BarBase { }

  class Foo : IFoo
  {
    public int A { get; set; }
    public string S  { get; set; }
    public IBar Bar { get; set; }

    int b = 100;
    public Foo()
    {

    }
  }

  class Foo2 : IFoo
  {
    public int A { get; set; }
    public string S { get; set; }
    public BarBase Bar { get; set; }

    public Foo2()
    {

    }
  }

  interface IBar { }

  class Bar : BarBase, IBar
  {
    public int A { get; set; }
    public IFoo Foo { get; set; }
  }

  class Bar2 : BarBase, IBar
  {
    public string A { get; set; }
  }

  [TestFixture]
  public class SerializerTests
  {
    [Test]
    public void SerializePrimitive()
    {
      double i = 20.8;
      var j = JsonConvert.SerializeObject(i);
      var o = JsonConvert.DeserializeObject(j);
      Assert.AreEqual(j, o);
    }

    [Test]
    public void SerializeInterfaceProperties()
    {
      var foo = new Foo {A=1,S="ilias"};
      var bar = new Bar { A = 10 };
      var bar2 = new Bar2 { A = "10" };
      foo.Bar = bar;
      var settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore};

      var j = JsonConvert.SerializeObject(foo);
      var o = JsonConvert.DeserializeObject<Foo>(j, new DerivedClassConverter<IBar,Bar>());//, new InterfaceConverter<IFoo, Foo>());
      Assert.AreEqual(foo.A, o.A);
      Assert.AreEqual(foo.S, o.S);
      Assert.AreEqual(((Bar)foo.Bar).A, ((Bar)o.Bar).A);
      Assert.AreEqual(foo.A, o.A);

      foo.Bar = bar2;
      j = JsonConvert.SerializeObject(foo);
      o = JsonConvert.DeserializeObject<Foo>(j, new DerivedClassConverter<IBar, Bar2>());
      Assert.AreEqual(foo.A, o.A);
      Assert.AreEqual(foo.S, o.S);
      Assert.AreEqual(((Bar2)foo.Bar).A, ((Bar2)o.Bar).A);
      Assert.AreEqual(foo.A, o.A);
    }

    [Test]
    public void SerializeBaseClass()
    {
      var foo = new Foo2 { A = 1, S = "ilias" };
      var bar = new Bar { A = 10 };
      var bar2 = new Bar2 { A = "10" };
      foo.Bar = bar;
      var settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

      var j = JsonConvert.SerializeObject(foo);
      var o = JsonConvert.DeserializeObject<Foo2>(j, new DerivedClassConverter<BarBase, Bar>());//, new InterfaceConverter<IFoo, Foo>());
      Assert.AreEqual(foo.A, o.A);
      Assert.AreEqual(foo.S, o.S);
      Assert.AreEqual(((Bar)foo.Bar).A, ((Bar)o.Bar).A);
      Assert.AreEqual(foo.A, o.A);

      foo.Bar = bar2;
      j = JsonConvert.SerializeObject(foo);
      o = JsonConvert.DeserializeObject<Foo2>(j, new DerivedClassConverter<BarBase, Bar2>());
      Assert.AreEqual(foo.A, o.A);
      Assert.AreEqual(foo.S, o.S);
      Assert.AreEqual(((Bar2)foo.Bar).A, ((Bar2)o.Bar).A);
      Assert.AreEqual(foo.A, o.A);
    }
    
    [Test]
    public void SerializePrivateFields()
    {
      var foo = new Foo();
      var j = JsonConvert.SerializeObject(foo);
      var o = JsonConvert.DeserializeObject<Foo>(j);
      Assert.AreEqual(Reflect.GetFieldValue(foo, "b"),Reflect.GetFieldValue(o, "b"));
    }
  }
}
