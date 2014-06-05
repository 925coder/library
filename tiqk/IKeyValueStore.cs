using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coder925.tiqk
{
  public interface IKeyValueStore
  {
    object this[object key] { get; }
    void Store(object key, object value);
  }

  public interface ISerialiser
  {
    byte[] Serialise(object obj);
    object Deserialize(byte[] bytes);
  }
}
