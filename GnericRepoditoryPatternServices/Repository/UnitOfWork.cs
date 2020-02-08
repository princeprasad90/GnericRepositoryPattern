using GenericRepositoryPatternData.Model;
using System.Data.Entity;
using GenericRepositoryPatternServices.Interfaces;

namespace GenericRepositoryPatternServices.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleEntities _dbContext;
        public UnitOfWork()
        {
            _dbContext = new SampleEntities();
        }
        public DbContext Db => _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
