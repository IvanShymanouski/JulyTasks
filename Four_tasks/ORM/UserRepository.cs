using System;
using System.Linq;
using System.Data.Entity;
using ORM.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace ORM
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbContext _context;        

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public User Find(Func<User, bool> f)
        {   
            return _context.Set<User>().AsNoTracking().FirstOrDefault(f);
        }

        public void Add(User entity)
        {            
            DbEntityEntry<User> dbEntity = _context.Entry<User>(entity);
            dbEntity.State = EntityState.Added;
            if (_context != null)
            {
                _context.SaveChanges();
                foreach (var ent in _context.ChangeTracker.Entries())
                {
                    ((IObjectContextAdapter)_context).ObjectContext.Detach(ent.Entity);
                }
            }
        }

        public User GetById(Guid Id)
        {            
            return _context.Set<User>().AsNoTracking().Where(x => x.Id == Id).FirstOrDefault();
        } 

    }
}