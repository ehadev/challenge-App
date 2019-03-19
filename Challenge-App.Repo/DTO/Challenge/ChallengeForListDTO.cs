using System;
using System.Threading.Tasks;

namespace Challenge_App.Repo.DTO.Challenge
{
    public class ChallengeForListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int challengeItemCount { get; set; }
    }
}
