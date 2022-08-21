using Anonimus.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anonimus.ViewModel
{
    public interface IPasswordStore
    {
        Task<IEnumerable<Password>> GetPasswordsAsync();

        Task<Password> GetPassword(int id);

        Task AddPassword(Password password);

        Task UpdatePassword(Password password);

        Task DeletePassword(Password password);

    }
}
