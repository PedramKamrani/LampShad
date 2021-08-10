using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _0_FrameWork.BaseClass
{
  public interface IRepositoryBaseClass<TKey,T> where T:class
    {

        void Create(T entity);
        bool Exists(Expression<Func<T,bool>> expression);
        T Get(TKey id);
        List<T> GetAll();
        void SaveChanges();
    }
}
