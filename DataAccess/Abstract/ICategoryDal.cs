using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category> //IEntityRepository category tablosu için yapılandırmış olduk
    {

    }
}
