namespace HangMan
{
    internal class Player
    {
        private bool isPlaying = true;
        public string currentName = "John Doe";
        public int hp = 0;

        public Player(int startingHP)
        {
            hp = startingHP;
        }

        public bool SetName(string name)
        {
            string checkName = name.ToLower();
            if(checkName == "niek") return false;
            currentName = name;
            return true;
        }
    }
}