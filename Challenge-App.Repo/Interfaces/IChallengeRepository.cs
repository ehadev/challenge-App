using Challenge_App.Data;
using Challenge_App.Repo.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_App.Repo.Interfaces
{
    public interface IChallengeRepository : IRepository<Challenge>
    {
        Task<PagedList<Challenge>> GetChallengesAsync(ChallengeParameters challengeParameters, int userId);
        Task<IEnumerable<ChallengeItem>> GetChallengeItems(int challengeId);
        int GetChallengeItemsCount(int challengeId);

        Task<IEnumerable<Challenge>> GetChallengesOfUser(int userId);
        Task<IEnumerable<ChallengeItem>> GetChallengeItemsOfUser(int challengeId, int userId);

        void AddChallengeUser(ChallengeUsers entity);
        void AddChallengeItems(IList<ChallengeItem> entities);
        void AddChallengeItemUsers(IList<ChallengeItemUsers> entities);


    }
}
