using System;
using System.Collections.Generic;

namespace Challenge_App.Data
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt {get;set;}
        public ICollection<ChallengeItem> challengeItem {get;set;}
        public ICollection<ChallengeUsers> ChallengeUsers {get; set;}

    }
}