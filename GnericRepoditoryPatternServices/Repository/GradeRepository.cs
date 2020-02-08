using System.Collections.Generic;
using System.Linq;
using GenericRepositoryPatternServices.Interfaces;
using GenericRepositoryPatternData.Model;

namespace GenericRepositoryPatternServices.Repository
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public IEnumerable<Grade> GetGradesBySection(string section)
        {
            return _context.Grades.Where(p => p.Section == section).ToList();
        }
    }
}
