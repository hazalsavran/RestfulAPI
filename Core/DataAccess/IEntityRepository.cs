using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
        // where T:class dediğimizde IEntityRepo içindeki T parametresi sadece class olabilir, buna generic constraint diyoruz
        // IEntity'yi implemente eden bir nesne de olabilir.
        // new() : new 'lenebilsin diye (IEntity newlenemediği için sadece class bazlı çalışmasını sağlar)
    public interface IEntityRepository<T> where T:class, IEntity, new()
    {
        //bu kısım tablolara ait operasyonların TANIMINI içerir (veri tabanına ekler siler günceller ya da select komutları gibi liste türünden veriler getirtebiliyoruz.)
        // kısaca veri tabanında yapılacak her türlü işlemleri içerir
        List<T> GetAll(Expression<Func<T,bool>> filter=null); //GetAll(p=>p.CategoryID==id) tarzında (bunun adı expressiondur) filtreler yazmamızı sağlıyor
                                                              
        T Get(Expression<Func<T, bool>> filter );                                            
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        
    }
}
