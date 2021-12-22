using Stella.DEVO.Esame.DataAccess.Models;
using System.Collections.Generic;

namespace Stella.DEVO.Esame.DataAccess.Services
{
    public interface IInsertData
    {
        IEnumerable<SqlModel> GetData();
        void Insert(SqlModel model);
    }
}