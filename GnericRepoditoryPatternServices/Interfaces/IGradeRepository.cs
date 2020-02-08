using GenericRepositoryPatternData.Model;
using System.Collections.Generic;

namespace GenericRepositoryPatternServices.Interfaces
{
    public interface IGradeRepository : IRepository<Grade>
    {
        IEnumerable<Grade> GetGradesBySection(string section);
    }
}
