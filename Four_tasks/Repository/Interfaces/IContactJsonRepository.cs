using System.Collections.Generic;
using Four_tasks.Models;
using System.Threading.Tasks;

namespace Four_tasks.Repository.Interfaces
{
    public interface IContactJsonRepository
    {
        Task writeAsync(Contact data);
        Task<List<Contact>> readAsync();
    }
}