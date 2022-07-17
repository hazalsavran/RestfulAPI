using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product> //IEntityRepository product tablosu için yapılandırmış olduk
    {

        List<ProductDetailDto> GetProductDetails();
    }
}
