using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msit115BestOne.Areas.TotalCase.Models
{
    interface ITotalCasesGR<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Create(T _entity);
        void Update(T _entity);
        void Delete(T _entity);
    }
}
