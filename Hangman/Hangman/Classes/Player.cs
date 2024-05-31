namespace HangMan
{
    internal class Player
    {
        public string currentName = "John Doe";
        public int hp = 0;

        public Player(int startingHP)
        {
            //Sets the hp
            hp = startingHP;
        }

        public bool SetName(string name)
        {
            //Sets the name, unless its niek. I have not made the profanity filter so this will just be an easter egg ig.
            string checkName = name.ToLower();
            if(checkName == "niek") return false;
            currentName = name;
            return true;
        }
    }
}