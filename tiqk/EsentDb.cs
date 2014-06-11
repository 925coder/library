using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Interop;
using System.IO;

namespace coder925.tiqk
{

  public class EsentDb : IKeyValueStore, IDisposable
  {

    public object this[object key]
    {
      get { throw new NotImplementedException(); }
    }

    public void Store(object key, object value)
    {
      throw new NotImplementedException();
    }

    string dbname;
    public EsentDb(string dbname)
    {
      this.dbname = dbname;
    }

    private Instance OpenInstance(string instanceName)
    {
      var instance = new Instance(instanceName,instanceName);
      instance.Init();

      return instance;
    }

    private Session CreateSession(Instance instance)
    {
      return new Session(instance);
    }

    private void CreateDatabase(string database, Instance instance, Session session)
    {
      JET_DBID dbid;
      if (File.Exists(database))
      {
        Api.JetAttachDatabase(session, database, AttachDatabaseGrbit.None);
        Api.JetOpenDatabase(session, database, null, out dbid, OpenDatabaseGrbit.None);
      }
      else
        Api.JetCreateDatabase(session, database, null, out dbid, CreateDatabaseGrbit.None);
    }

    private void CreateTable(Session session, JET_DBID database, string table, params Tuple<string, JET_coltyp>[] columns)
    {
      JET_TABLEID tableid;
      Api.JetCreateTable(session, database, table, 16, 80, out tableid);

    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}
