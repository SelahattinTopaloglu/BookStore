using Core.Entities.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Constants
{
   public static class Messages
    {
        public static string BookAdded = "Kitap Eklendi" ;
        public static string BookPageNumberInvalid = "Kitap Sayfası Geçersiz" ;

        public static string BooksListed = "Kitaplar Listelendi";
        public static string MaintenanceTime = "Bakım saati";
        public static string AuthorizationDenied = "Yetkisiz Giriş ";
        public static string UserRegistered = "Kullanıcı kayıtlı";
        public static string UserNotFound = "kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Başarılı Giriş";
        public static string UserAlreadyExists = "Kullanıcı zaten var" ;
        public static string AccessTokenCreated = "Access Token Oluşturuldu" ;
        public static string Updated = "Kitap Güncellendi";

        public static string BookDeleted = "Kitap Silindi";
    }
}
