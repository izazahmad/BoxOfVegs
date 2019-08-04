using System;
using System.Collections;
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

            int GetCountByWhere(string search, Expression<Func<EntityTbl, bool>> wherePredict);

            void UpdateByWhereClause(Expression<Func<EntityTbl, bool>> wherePredict, Action<EntityTbl> ForEachPredict);
            EntityTbl GetRecord(int recordID);
            void Remove(EntityTbl entity);
            void RemovebyWhereClause(List<EntityTbl> entity);
            void RemoveRangeBywhereClause(Expression<Func<EntityTbl, IList>> wherePredict);
            void InactiveAndDeleteMarkByWhereClause(Expression<Func<EntityTbl, bool>> wherePredict, Action<EntityTbl> ForEachPredict);
            EntityTbl GetRecordByParameter(Expression<Func<EntityTbl, bool>> wherePredict, Expression<Func<EntityTbl, IList>> whereList);
            IEnumerable<EntityTbl> GetListParameter(Expression<Func<EntityTbl, bool>> wherePredict);
            IEnumerable<EntityTbl> GetResultBySqlprocedure(string query, params object[] parameters);

            List<EntityTbl> GetResultSqlprocedure(string query, params object[] parameters);
            List<EntityTbl> GetListBySearch(Expression<Func<EntityTbl, bool>> wherePredict);
            List<EntityTbl> GetToShow(string search, int PageNo, int PageSize, Expression<Func<EntityTbl, bool>> wherePredict, Expression<Func<EntityTbl, int>> orderByPredict, Expression<Func<EntityTbl, IList>> whereinclude);


            IEnumerable<EntityTbl> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<EntityTbl, bool>> wherePredict, Expression<Func<EntityTbl, int>> orderByPredict);
        
        }
    
}
