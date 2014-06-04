using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace coder925.library
{
  public static class Reflect
  {
    public static object GetFieldValue(object obj, string field)
    {
      var bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
      var prop = obj.GetType().GetField(field, bindingFlags);
      var r = prop.GetValue(obj);
      return r;
    }
  }
}
