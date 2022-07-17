using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity:class, IEntity, new()
        where TContext :DbContext, new()
    {
       
            public void Add(TEntity entity)
            {
                //IDispossible pattern implementation of C#
                using (TContext context = new TContext())//Context'in bellekte yer tutmaamsını sağlar
                {
                    var AddEntity = context.Entry(entity);
                    AddEntity.State = EntityState.Added;
                    context.SaveChanges();
                }
            }

            public void Delete(TEntity entity)
            {
                using (TContext context = new TContext())//Context'in bellekte yer tutmaamsını sağlar
                {
                    var DeletedEntity = context.Entry(entity);
                    DeletedEntity.State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }

            public TEntity Get(Expression<Func<TEntity, bool>> filter)
            {
                using (TContext context = new TContext())
                {
                    return context.Set<TEntity>().SingleOrDefault(filter);
                }
            }
            public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
            {
                using (TContext context = new TContext())
                {
                    return filter == null
                        ? context.Set<TEntity>().ToList()
                        : context.Set<TEntity>().Where(filter).ToList();
                    //select * from products ı listeye çevirip getirir (tüm datayı getirmek için filter==null dır )
                    // eğer filtre null değilse where çalışır filtreye göre listeleyip döndürür

                }
            }

            public void Update(TEntity entity)
            {
                using (TContext context = new TContext())//Context'in bellekte yer tutmaamsını sağlar
                {
                    var UpdatedEntity = context.Entry(entity);
                    UpdatedEntity.State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        
    }
}
