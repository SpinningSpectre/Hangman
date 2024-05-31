namespace HangMan
{
    internal class Board
    {
        public bool settingUp = false;


        //You can only get word if you are setting up
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

        //Saves the right guesses
        public char[] currentGuessed;

        //Whatever the players have guessed
        public List<char> guessedLetters = new List<char>();

        public Board(string wor)
        {
            //Sets the word
            word = wor.ToLower();
            currentGuessed = new char[wor.Length];
            for(int i = 0; i < word.Length; i++)
            {
                currentGuessed[i] = '*';
            }
        }

        public void TellState()
        {
            //Tells the users what letters they currently have
            string guessed = "";
            for (int i = 0; i < currentGuessed.Length; i++)
            {
                guessed += currentGuessed[i];
            }
            Console.WriteLine($"Guessed: {guessed}");
        }

        public bool CheckLetter(char letter, out bool won)
        {
            //Adds your guess to the list
            guessedLetters.Add(letter);
            letter = char.ToLower(letter);
            won = false;

            //Checks if its in the word
            if (word.Contains(letter))
            {
                for(int i = 0;i < word.Length;i++)
                {
                    if (word[i] == letter)
                    {
                        currentGuessed[i] = letter;
                    }
                }

                //Checks if the players have won
                if (!currentGuessed.Contains('*'))
                {
                    won = true;
                }

                //returns false if the letter is right
                return false;
            }
            else
            {
                //returns true if the letter is wrong
                return true;
            }
        }
    }
}