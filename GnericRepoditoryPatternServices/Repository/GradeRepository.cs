using System.Collections.Generic;
using System.Linq;
using GenericRepositoryPatternServices.Interfaces;
using GenericRepositoryPatternData.Model;

namespace GenericRepositoryPatternServices.Repository
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }
        public IEnumerable<Grade> GetGradesBySection(string section)
        {
            return table.Where(p => p.Section == section).ToList();
        }
    }
}
