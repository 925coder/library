using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Interop;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

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
      //CreateDatabase(db);
      int numOrders = 10;

      Stopwatch sw = new Stopwatch();
      sw.Start();
      var orders = Order.MakeOrders(numOrders);
      var serialized = new string[numOrders];
      var essentdb = new EsentDb(db);
      for (int i = 0; i < numOrders; i++)
      {
        essentdb.Store(orders[i].OrderId, orders[i]);
      }
      essentdb.Dispose();

      sw.Stop();
      Console.WriteLine("Time taken = " + sw.ElapsedMilliseconds); 
      Console.ReadKey();
    }

  }
}
