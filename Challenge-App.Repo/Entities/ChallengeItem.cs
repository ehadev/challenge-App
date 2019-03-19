using System;
using System.Collections.Generic;

namespace Challenge_App.Data
{
    public class ChallengeItem
    {
        public int Id { get; set; }
        public DateTime OnDate {get;set;}

        public int ChallengeId {get;set;}
        public Challenge Challenge {get;set;}
        public ICollection<ChallengeItemUsers> User {get;set;}

    }
}