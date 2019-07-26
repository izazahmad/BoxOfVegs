using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Services
{
    
        public interface IRepository<EntityTbl> where EntityTbl : class
        {
            IEnumerable<EntityTbl> GetAllRecords();
            IQueryable<EntityTbl> GetAllRecordsIQueryable();
            int GetAllrecordCount();
            void AddRecord(EntityTbl entity);
            void Update(EntityTbl entity);
            void UpdateByWhereClause(Expression<Func<EntityTbl, bool>> wherePredict, Action<EntityTbl> ForEachPredict);
            EntityTbl GetRecord(int recordID);
            void Remove(EntityTbl entity);
            void RemovebyWhereClause(Expression<Func<EntityTbl, bool>> wherePredict);
            void RemoveRangeBywhereClause(Expression<Func<EntityTbl, bool>> wherePredict);
            void InactiveAndDeleteMarkByWhereClause(Expression<Func<EntityTbl, bool>> wherePredict, Action<EntityTbl> ForEachPredict);
            EntityTbl GetRecordByParameter(Expression<Func<EntityTbl, bool>> wherePredict);
            IEnumerable<EntityTbl> GetListParameter(Expression<Func<EntityTbl, bool>> wherePredict);
            IEnumerable<EntityTbl> GetResultBySqlprocedure(string query, params object[] parameters);
            IEnumerable<EntityTbl> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<EntityTbl, bool>> wherePredict, Expression<Func<EntityTbl, int>> orderByPredict);
        }
    
}
