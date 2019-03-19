namespace Challenge_App.Data
{
    public class ChallengeItemUsers
    {
        public int Id {get;set;}
        public int ChallengeItemId {get;set;}
        public ChallengeItem ChallengeItem {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public bool Completed {get;set;}
    }
}