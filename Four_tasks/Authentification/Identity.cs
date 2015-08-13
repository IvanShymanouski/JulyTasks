using System;
using System.Collections.Generic;
using System.Security.Principal;
using ORM;

namespace Four_tasks.Authentification
{
    public class Identity : IIdentity
    {
        public Guid Id { get; set; }        
        public string Email { get; set; }        
        public bool RememberMe { get; set; }

        public Identity(IPrincipal user)
        {
            var current = (null == user)?null:user.Identity as Identity;
            if (current == null)
            {
                Id = Guid.Empty;
                return;
            }

            Id = current.Id;
            Email = current.Email;            
            RememberMe = current.RememberMe;
        }

        public Identity(User user)
        {            
            if (user == null)
            {
                Email = "Guest";
                return;
            }

            Id = user.Id;            
            Email = user.Email;
        }

        public Identity(Cookie user)
        {
            if (user == null)
            {
                Email = "Guest";
                return;
            }

            Id = user.Id;
            Email = user.Email;
        }       

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "SuperAuthen"; }
        }

        public bool IsAuthenticated
        {
          get { return !(Id == Guid.Empty || string.IsNullOrWhiteSpace(Email)); }
        }

        public string Name { get { return Email; } }

        #endregion
    }
}