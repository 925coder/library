using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Interop;

namespace coder925.tiqk.proto
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Write("Enter db name : ");
      var db = Console.ReadLine();

      CreateDatabase(db);
    }

    private static void CreateDatabase(string db)
    {
      // create an instance of the database engine
      using (var instance = new Instance(db))
      {
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
