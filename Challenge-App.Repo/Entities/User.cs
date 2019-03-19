using System.Collections.Generic;

namespace Challenge_App.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<ChallengeUsers> ChallengeUsers {get; set;}
        public ICollection<ChallengeItemUsers> ChallengeItem {get;set;}
    }
}