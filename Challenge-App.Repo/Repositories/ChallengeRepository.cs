using Challenge_App.Data;
using Challenge_App.Repo.Helper;
using Challenge_App.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_App.Repo.Repositories
{
    public class ChallengeRepository : Repository<Challenge>, IChallengeRepository
    {
        public ChallengeRepository(MyContext context) : base(context)
        {
        } 
        public async Task<PagedList<Challenge>> GetChallengesAsync(ChallengeParameters challengeParameters, int userId)
        {
            var challenges = _context.Challenge.Include(x=>x.ChallengeUsers).Where(x=>x.ChallengeUsers.Any(s =>s.UserId == userId));

            return await PagedList<Challenge>.CreateAsync(challenges, challengeParameters.PageNumber, challengeParameters.PageSize);
        }
        public async Task<IEnumerable<Challenge>> GetChallengesOfUser(int userId)
        {
            return await _context.Challenge.Include(x => x.ChallengeUsers).Include(x => x.challengeItem).Where(s => s.ChallengeUsers.Any(d => d.UserId == userId)).ToListAsync();
        }
        public async Task<IEnumerable<ChallengeItem>> GetChallengeItems(int challengeId)
        {
            return await _context.ChallengeItem.Where(s => s.ChallengeId == challengeId).ToListAsync();
        }

        public Task<IEnumerable<ChallengeItem>> GetChallengeItemsOfUser(int challengeId, int userId)
        {
            throw new NotImplementedException();
        }

        public void AddChallengeItems(IList<ChallengeItem> entities)
        {
            _context.Set<ChallengeItem>().AddRange(entities);
        }
        public void AddChallengeItemUsers(IList<ChallengeItemUsers> entities)
        {
            _context.Set<ChallengeItemUsers>().AddRange(entities);
        }

        public int GetChallengeItemsCount(int challengeId)
        {
            return  _context.ChallengeItem.Where(s => s.ChallengeId == challengeId).Count();
        }

        public void AddChallengeUser(ChallengeUsers entity)
        {
            _context.Set<ChallengeUsers>().Add(entity);
        }

     
    }
}
