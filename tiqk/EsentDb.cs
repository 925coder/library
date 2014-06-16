using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Interop;
using System.IO;
using coder925.library;
using Newtonsoft.Json;

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
      var serialised = JsonConvert.SerializeObject(value);
    }

    string dbname;
    Instance instance;
    Session session;
    JET_DBID dbid;
    JET_TABLEID table;

    public EsentDb(string dbname)
    {
      this.dbname = dbname;
      instance = OpenInstance(dbname);
      session = CreateSession(instance);
      dbid = CreateDatabase(dbname, instance, session);
      table = CreateTable(session, dbid, "store", new Tuple<string, JET_coltyp>[] { Tuple.Create("key", JET_coltyp.Long), Tuple.Create("data", JET_coltyp.LongText) });
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

    private JET_DBID CreateDatabase(string database, Instance instance, Session session)
    {
      JET_DBID dbid;
      if (File.Exists(database))
      {
        Api.JetAttachDatabase(session, database, AttachDatabaseGrbit.None);
        Api.JetOpenDatabase(session, database, null, out dbid, OpenDatabaseGrbit.None);
      }
      else
        Api.JetCreateDatabase(session, database, null, out dbid, CreateDatabaseGrbit.None);

      return dbid;
    }

    private JET_TABLEID CreateTable(Session session, JET_DBID database, string table, params Tuple<string, JET_coltyp>[] columns)
    {
      JET_TABLEID tableid;

      if (!Api.TryOpenTable(session, database, table, OpenTableGrbit.Updatable, out tableid))
      {
        Api.JetCreateTable(session, database, table, 16, 80, out tableid);

        foreach (var column in columns)
        {
          JET_COLUMNID columnid;
          Api.JetAddColumn(session, tableid, column.Item1, new JET_COLUMNDEF { coltyp = column.Item2 }, null, 0, out columnid);
        }
      }

      return tableid;
    }

    private void InsertRecord(Session session, Table table, params Tuple<string, object>[] columnValues )
    {
      if (!columnValues.IsEmpty())
      {
        var columnids = Api.GetColumnDictionary(session, table);
        foreach (var value in columnValues)
        {
          Api.SetColumn(session, table, columnids[value.Item1], (string)value.Item2, Encoding.Unicode);
        }
      }
    }

    public void Dispose()
    {
      session.Dispose();
      instance.Term();
      instance.Dispose();
    }
  }
}
