using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Services
{
    public class Repository<EntityTbl> : IRepository<EntityTbl> where EntityTbl : class
    {
        DbSet<EntityTbl> table;
        private BOVContext context;
        public Repository(BOVContext Entity)
        {
            context = Entity;
            table = context.Set<EntityTbl>();
        }
        public void AddRecord(EntityTbl entity)
        {
            table.Add(entity);
            context.SaveChanges();
        }

        public int GetAllrecordCount()
        {
            return table.Count();
        }

        public IEnumerable<EntityTbl> GetAllRecords()
        {
            return table.ToList();
        }

        public IQueryable<EntityTbl> GetAllRecordsIQueryable()
        {
            return table;
        }

        public EntityTbl GetRecord(int recordID)
        {
            return table.Find(recordID);
        }
        //public Product Getproduct(int recordID)
        //{
        //    //return table.Find(recordID);
        //    return context.Products.Where(x => x.ProductID == recordID).Include(x => x.Category).FirstOrDefault();
        //}

        public EntityTbl GetRecordByParameter(Expression<Func<EntityTbl, bool>> wherePredict)
        {
            return table.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<EntityTbl> GetListParameter(Expression<Func<EntityTbl, bool>> wherePredict)
        {
            return table.Where(wherePredict).ToList();
        }



        public IEnumerable<EntityTbl> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return context.Database.SqlQuery<EntityTbl>(query, parameters).ToList();
            }
            else
            {
                return context.Database.SqlQuery<EntityTbl>(query).ToList();
            }
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<EntityTbl, bool>> wherePredict, Action<EntityTbl> ForEachPredict)
        {
            table.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(EntityTbl entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
                table.Attach(entity);
            table.Remove(entity);
            context.SaveChanges();
        }

        public void RemovebyWhereClause(Expression<Func<EntityTbl, bool>> wherePredict)
        {
            EntityTbl entity = table.Where(wherePredict).FirstOrDefault();
        }

        public void RemoveRangeBywhereClause(Expression<Func<EntityTbl, bool>> wherePredict)
        {
            List<EntityTbl> entity = table.Where(wherePredict).ToList();
            foreach (var ent in entity)
            {
                Remove(ent);
            }
        }

        public void Update(EntityTbl entity)
        {
            table.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            
        }

        public void UpdateByWhereClause(Expression<Func<EntityTbl, bool>> wherePredict, Action<EntityTbl> ForEachPredict)
        {
            table.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }
        public IEnumerable<EntityTbl> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<EntityTbl, bool>> wherePredict, Expression<Func<EntityTbl, int>> orderByPredict)
        {
            if (wherePredict != null)
            {
                return table.OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return table.OrderBy(orderByPredict).ToList();
            }


        }

    }
}
