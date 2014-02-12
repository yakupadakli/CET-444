using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.Entities;


namespace CollaborativeLearning.WebUI.Controllers
{   

    /// <summary>
    /// ggg
    /// </summary>
    public static class HelperController 
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        [Authorize]
        public static int GetCurrentUserId() 
        {
            User user = unitOfWork.UserRepository.Get(u => u.Username == WebSecurity.User.Identity.Name).FirstOrDefault();
            if (user != null)
                return user.Id;
            return 0;
           
        }
        public static string GetRandomString(int length)    //Random String Producer
        {
            //rastgele stringimizin oluşturulacağı karakter dizisi
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            + "abcdefghijklmnopqrstuvwxyz"
            + "012345678";

            String randomString = "";
            Random rasgele = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < length; i++)
            {
                randomString += characters[rasgele.Next(characters.Length)];
            }

            return randomString;
        }

    }
}
