using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
     public static class Messages // static verince new() lemiyoruz
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductListed="Ürünler Listelendi";
        public static string ProductUpdated ="Ürün Güncellendi";
        public static string ProductCountOfError="Bir kategoride 10 dan fazla ürün olamaz";
        public static string ProductNameAlreadyExists="Bu ürün adıyla aynı olan bir ürün daha önce eklenmiş";

        public static string CategoryLimitExceded = "Eklemek istediğiniz kategorinin limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Bu işlem için yetkiniz yoktur";
        public static string UserRegistered="Kullanıcı Kayıt Oldu";
        public static string UserNotFound="Kullanıcı bulunamadı";
        public static string PasswordError="Parola Hatalı";
        public static string SuccessfulLogin="Giriş Başarılı";
        public static string UserAlreadyExists="Bu kullanıcı zaten kayıtlı";
        public static string AccessTokenCreated="Token yaratıldı";
    }
}
