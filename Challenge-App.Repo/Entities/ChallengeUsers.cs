namespace Challenge_App.Data
{
    public class ChallengeUsers
    {
        public int Id {get;set;}
        public string Role {get;set;}
        public int ChallengeId {get;set;} 
        public Challenge Challenge {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}

    }
}