using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coder925.library
{
    public static class IEnumerableEx
    {
      public static bool IsEmpty(this IEnumerable items)
      {
        if (items == null)
          return true;

        var en = items.GetEnumerator();
        if (!en.MoveNext())
          return true;

        return false;
      }

      public static string ToCsv(this IEnumerable items)
      {
        if (items.IsEmpty())
          return String.Empty;

        var en = items.GetEnumerator();
        en.MoveNext(); // start the enumerator
        var sb = new StringBuilder(en.Current.ToString());
        while (en.MoveNext())
        {
          if (en.Current != null)
            sb.Append("," + en.Current );
        }

        return sb.ToString();
        
      }
    }
}
