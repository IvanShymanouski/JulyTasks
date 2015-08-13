using System;
using System.Security.Principal;

namespace Four_tasks.Authentification
{
    public class Principal : IPrincipal
    {
        private readonly Identity identity;

        public Principal(Identity identity)
        {
            this.identity = identity;
        }

        #region IPrincipal Members

        public bool IsInRole(string roleName)
        {
            return roleName == "Guest" && !string.IsNullOrWhiteSpace(identity.Email);
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        #endregion
    }
}