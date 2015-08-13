using System;
using System.Web.Helpers;
using ORM.Interfaces;
using ORM;

namespace Four_tasks.Providers
{
    public static class CustomMembershipProvider
    {
        public static User ValidateUserAndReturn(string email, string password)
        {
            IUserRepository users = (IUserRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRepository));
            
            return users.Find(x => (x.Email == email) && Crypto.VerifyHashedPassword(x.Password, password));
        }

        /// <summary>
        /// create new user
        /// </summary>
        /// <returns>null if user exist</returns>
        public static User CreateUser(string email, string password)
        {           

            IUserRepository users = (IUserRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRepository));
            User membershipUser = null;

            if (null == FindUser(email))
            {
                User user = new User()
                            {
                                Id = Guid.NewGuid(),
                                Email = email,
                                Password = Crypto.HashPassword(password)
                            };
                users.Add(user);
                membershipUser = user;
            }

            return membershipUser;
        }

        private static User FindUser(string email)
        {
            IUserRepository users = (IUserRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRepository));
            return users.Find(x => x.Email == email);
        }
    }
}