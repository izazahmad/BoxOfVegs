using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using System;
using System.Collections;
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


        //public List<EntityTbl> GetSearch(string search)
        //{
        //    if(!string.IsNullOrEmpty(search))
        //    {
        //        return table.Contains(search.ToLower());
        //    }
        //    else
        //    {
        //        return table.ToList();
        //    }
        //}

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


        public int GetCountByWhere(string search,Expression<Func<EntityTbl, bool>> wherePredict)
        {
            if(!string.IsNullOrEmpty(search))
            { 
            return table.Where(wherePredict).Count();
            }
            else
            {
                return table.Count();
            }
            //if (!string.IsNullOrEmpty( search))
            //{
            //    return table.Where(EntityTbl => EntityTbl. != null &&
            //         category.Name.ToLower().Contains(search.ToLower())).Count();
            //}
            //else
            //{
            //    return table.Count();
            //}

        }



        //public Product Getproduct(int recordID)
        //{
        //    //return table.Find(recordID);
        //    return context.Products.Where(x => x.ProductID == recordID).Include(x => x.Category).FirstOrDefault();
        //}

        public EntityTbl GetRecordByParameter(Expression<Func<EntityTbl, bool>> wherePredict, Expression<Func<EntityTbl,IList>> whereList)
        {
            return table.Where(wherePredict)
                .Include(whereList)
                .FirstOrDefault();
        }

        public IEnumerable<EntityTbl> GetListParameter(Expression<Func<EntityTbl, bool>> wherePredict)
        {
            
                return table.Where(wherePredict).ToList();
           
            
        }

        public List<EntityTbl> GetListBySearch(Expression<Func<EntityTbl, bool>> wherePredict)
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


        public List<EntityTbl> GetResultSqlprocedure(string query, params object[] parameters)
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

        public void RemovebyWhereClause(List<EntityTbl> entity)
        {
            table.RemoveRange(entity);
        }

        public void RemoveRangeBywhereClause(Expression<Func<EntityTbl, IList>> wherePredict)
        {
            List<EntityTbl> entity = table.Include(wherePredict).ToList();
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
        public List<EntityTbl> GetToShow(string search, int PageNo, int PageSize, Expression<Func<EntityTbl, bool>> wherePredict, Expression<Func<EntityTbl, int>> orderByPredict, Expression<Func<EntityTbl, IList>> whereinclude)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return table.Where(wherePredict)
                    .OrderBy(orderByPredict)
                    .Skip((PageNo-1)*PageSize)
                    .Include(whereinclude)
                    .ToList();
            }
            else
            {
                return table
                    .OrderBy(orderByPredict)
                    
                    .Skip((PageNo - 1) * PageSize)
                    .Take(PageSize)
                    .Include(whereinclude)
                    .ToList();
            }


        }





    }
}
