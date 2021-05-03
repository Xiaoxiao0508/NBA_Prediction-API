namespace DotNetAuthentication.DB
{
    public class PlayerSelection
    {
        public PlayerSelection()
        {
        }

        public PlayerSelection(string teamName, int player_Key, int id)
        {
            TeamName = teamName;
            Player_key = player_Key;
            Id = id;
        }

        public string TeamName { get; set; }
        public int Player_key { get; set; }

        public int Id { get; set; }

    }
}