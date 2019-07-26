using BoxOfVegs.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Services
{
    public class RepositoryWork : IDisposable
    {
        private BOVContext Entity = new BOVContext();
        public IRepository<EntityTbl> GetRepositoryInstance<EntityTbl>() where EntityTbl : class
        {
            return new Repository<EntityTbl>(Entity);
        }

        public void SaveChanges()
        {
            Entity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Entity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}
