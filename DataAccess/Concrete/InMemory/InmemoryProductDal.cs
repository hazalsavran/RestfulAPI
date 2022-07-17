using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    public class InmemoryProductDal : IProductDal
    {
        //Bu kısım ise IProductDal da tanımladığımız operasyonların kodlamalarını içerir burada metotlar işlevlendirilir.

        List<Product> _products; // constructer metot (console app için)
        public InmemoryProductDal()//constructer metot <<--
        {
            // sanki sql den ya da başka veri tabanlarından geliyormuş gibi simüle ettik
            _products = new List<Product>
            {
                // veri olmadığı için elle tabloya veri girdik
                new Product{CategoryID=1, ProductID=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15},
                new Product{CategoryID=2, ProductID=2, ProductName="Kitap", UnitPrice=13, UnitsInStock=20},
                new Product{CategoryID=2, ProductID=3, ProductName="Tabak", UnitPrice=19, UnitsInStock=34},
                new Product{CategoryID=1, ProductID=4, ProductName="Kamera", UnitPrice=5, UnitsInStock=15},
                new Product{CategoryID=1, ProductID=5, ProductName="Laptop", UnitPrice=25, UnitsInStock=15}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product producttodelete = _products.SingleOrDefault(p => p.ProductID == product.ProductID);

            // her p için (product tablasunda) for döngüsünde gezip girilen idyi arar 
            // singleordefault linq sorgusunda for açar
            //SingleOrDefault anahtar sözcüğü ile dizi içerisinde bulunan elemanlardan
            //belirlenen koşula göre sadece BİR tanesinin gelmesini sağlar. Örnekte int dizisinde bulunan elemanlardan belirlenen koşul uyuyor ise o eleman, belirlenen koşulda eleman yok ise sıfır döner.

            _products.Remove(producttodelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {

            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryid)
        {
            return _products.Where(p => p.CategoryID == categoryid).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product producttoUpdate = _products.SingleOrDefault(p => p.ProductID == product.ProductID);
            //güncellenecek veriyi ait ıd değerine göre tüm verisini getirir.
            producttoUpdate.ProductName = product.ProductName;
            producttoUpdate.CategoryID = product.CategoryID;
            producttoUpdate.UnitPrice = product.UnitPrice;
            producttoUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
