using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Enums.ApiEnum
{
    public static class OperationTexts
    {
        public static string INSERT = "Başarıyla eklendi.";
        public static string UPDATE = "Başarıyla güncellendi.";
        public static string DELETE = "Başarıyla silindi.";
        public static string GET = "Data başarıyla getirildi.";
        public static string GET_LIST = "Liste başarıyla getirildi.";


        public static string ERROR_INSERT = "Başarıyla eklendi.";
        public static string ERROR_UPDATE = "Başarıyla güncellendi.";
        public static string ERROR_DELETE = "Başarıyla silindi.";
        public static string ERROR_GET = "Data başarıyla getirildi.";
        public static string ERROR_GET_LIST = "Liste başarıyla getirildi.";

        public static string FILE_SAVED = "Dosya kaydedildi";
        public static string FILE_NOT_SAVED = "Dosya kayedilirken hata oluştu";


        public static string NOT_VALIDATOR_CLASS = "Lütfen geçerli bir validator sınıfı veriniz";


        #region Authentication
        public static string USER_NOT_FOUND = "Kullanıcı bulunamadı";
        public static string WRONG_PASSWORD = "Parola yanlış.Lütfen tekrar deneyin";
        public static string SUCCESSFUL_LOGIN = "Başarıyla giriş yapıldı.";
        public static string USER_CREATED = "Kullanıcı oluşturuldu.";
        public static string TOKEN_CREATED = "Token oluşturuldu";
        #endregion


    }
}
