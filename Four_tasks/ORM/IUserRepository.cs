using System;

namespace ORM.Interfaces
{
    public interface IUserRepository
    {
        void Add(User entity);
        User GetById(Guid Id);
        User Find(Func<User, bool> f);
    }
}
