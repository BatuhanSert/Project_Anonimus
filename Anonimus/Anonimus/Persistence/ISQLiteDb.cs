using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonimus.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
