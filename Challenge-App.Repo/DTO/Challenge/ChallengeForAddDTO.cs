using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_App.Repo.DTO.Challenge
{
    public class ChallengeForAddDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfItems { get; set; }
        public List<int> UsersInvited { get;set; }

        public ChallengeForAddDTO()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
