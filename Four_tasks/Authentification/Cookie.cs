using System;
using System.Collections.Generic;

namespace Four_tasks.Authentification
{
    public class Cookie
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool RememberMe { get; set; }
    }
}