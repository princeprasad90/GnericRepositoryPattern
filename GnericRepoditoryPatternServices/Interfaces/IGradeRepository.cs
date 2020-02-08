using GenericRepositoryPatternData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryPatternServices.Interfaces
{
    public interface IGradeRepository : IRepository<Grade>
    {
        IEnumerable<Grade> GetGradesBySection(string section);
    }
}
