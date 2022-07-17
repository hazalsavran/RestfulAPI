using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
   public class ProductValidator: AbstractValidator<Product> //product tablosu için validator , kısıtlama
    {
        //bu kurallar bir ctor içine yazılır
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Lütfen geçerli bir birim fiyat giriniz");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p=>p.CategoryID==1); // product nesnesinin fiyatı kategorı id 1 ise 10 dan büyük olsun kısıtlaması


        }
    }
}
