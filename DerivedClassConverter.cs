using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtonsoft.Json
{
  public class DerivedClassConverter<I,J>: Newtonsoft.Json.JsonConverter
  {
    // I-InterfaceType
    // J-ConcreteType

    private Type iType = typeof(I);
    private Type jType = typeof(J);

    public override bool CanConvert(Type objectType)
    {
      if (iType.IsAssignableFrom(objectType) && !jType.IsAssignableFrom(objectType))
        return true;
      return false;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      var r = serializer.Deserialize<J>(reader);
      return r;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      serializer.Serialize(writer, value);
    }
  }
}
