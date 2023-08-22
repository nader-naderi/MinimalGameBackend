namespace GameDataLibrary
{
    [System.Serializable]
    public class UserData
    {
        public int Id { get; set; }
        public int PlayerID;
        public string Username { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return "Username: " + Username + "\nEmail: " + Email;
        }
    }
}
