namespace HangMan
{
    internal class Board
    {
        public bool settingUp = false;

        private string word;
        public string Word {
            get => word;
            set 
            { 
                if (settingUp)
                {
                    word = value;
                }
            } 
        }

        public char[] currentGuessed;

        public List<char> guessedLetters = new List<char>();

        public Board(string wor)
        {
            word = wor.ToLower();
            currentGuessed = new char[wor.Length];
            for(int i = 0; i < word.Length; i++)
            {
                currentGuessed[i] = '*';
            }
        }

        public void TellState()
        {
            string guessed = "";
            for (int i = 0; i < currentGuessed.Length; i++)
            {
                guessed += currentGuessed[i];
            }
            Console.WriteLine($"Guessed: {guessed}");
        }

        public bool CheckLetter(char letter, out bool won)
        {
            guessedLetters.Add(letter);
            letter = char.ToLower(letter);
            won = false;
            if (word.Contains(letter))
            {
                for(int i = 0;i < word.Length;i++)
                {
                    if (word[i] == letter)
                    {
                        currentGuessed[i] = letter;
                    }
                }
                if (!currentGuessed.Contains('*'))
                {
                    won = true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}