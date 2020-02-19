using System.Threading.Tasks;
using MyProject.Core.Entities;

namespace MyProject.Core.Repositories
{
    public interface IInviteRepository
    {
        Task<int> Add(Invite invite);
        Task<string> IsInvited(string email, string code);
    }
}