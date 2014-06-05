using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Interop;
using System.IO;

namespace coder925.tiqk.proto
{

  public class Instrument
  {
    public int InstrumentId { get; set; }
    public string Name { get; set; }
  }

  public class Order
  {
    public int OrderId { get; set; }
    public double Quantity { get; set; }
    public Instrument Instrument { get; set; }

    static Random r = new Random();
    
    public static IList<Order> MakeOrders(int numOrders)
    {
      var orders = new Order[numOrders];
      var names = new string[]{"VOD","GOOG","MSFT","APPL","TCS"};
      var instruments = new Instrument[names.Length];
      var numInstruments = names.Length;

      for (int i = 0; i < numInstruments; i++)
        instruments[i] = new Instrument {Name = names[i] , InstrumentId=i};
      
      for (int i = 0; i < numOrders; i++)
			{
        var instrument = instruments[r.Next() % numInstruments];
        var order = new Order { Quantity = i * 1000 , Instrument = instrument };
        orders[i] = order;
			}

      return orders;
    }

  }

  class Program
  {
    static void Main(string[] args)
    {
      Console.Write("Enter db name : ");
      var db = Console.ReadLine();

      CreateDatabase(db);
      var orders = Order.MakeOrders(1000000);

      Console.ReadKey();
    }

    private static void CreateDatabase(string db)
    {
      // create an instance of the database engine
      using (var instance = new Instance(db))
      {
        var parameters = instance.Parameters;
        parameters.CircularLog = true;

        instance.Init();
        using (var session = new Session(instance))
        {
          JET_DBID dbid;
          Api.JetCreateDatabase(session, db, null, out dbid, CreateDatabaseGrbit.OverwriteExisting);
        }
      }
    }
  }
}
