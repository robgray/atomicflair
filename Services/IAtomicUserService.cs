using flair.Models;

namespace flair.Services
{
    public interface IAtomicUserService
    {
        AtomicUser GetUserInformation(int userId);
        string GetRawAtomicUserData(string uri);
    }
}