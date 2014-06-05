using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coder925.tiqk
{

  public interface IDb
  {
    IKeyValueStore StorageEngine { get; set; }

    void Insert<T>(T obj);
    void Update<T>(Func<T> condition, T obj);
    void Delete<T>(Func<T> condition);
    T Get<T>(Func<T> condition);
  }

  class Db : IDb
  {
    IKeyValueStore storageEngine;
    public Db(IKeyValueStore storageEngine)
    {
      this.storageEngine = storageEngine;
    }

    public void Insert<T>(T obj)
    {
      
    }

    public void Update<T>(Func<T> condition, T obj)
    {
      throw new NotImplementedException();
    }

    public void Delete<T>(Func<T> condition)
    {
      throw new NotImplementedException();
    }

    public T Get<T>(Func<T> condition)
    {
      throw new NotImplementedException();
    }

    public IKeyValueStore StorageEngine
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }
  }
}
