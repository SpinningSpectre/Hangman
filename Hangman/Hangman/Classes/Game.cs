namespace HangMan
{
    internal class Game
    {
        private const int maxPlayers = 5;
        private const int maxHP = 5;
        public Player[] currentPlayers;
        public Board currentBoard;
        public int playersTurn;
        public void StartGame()
        {
            SetWord();
            AskAmountOfPlayer();
            MainGame();
        }

        private void SetWord()
        {
            currentBoard = new Board("abcdefghijklmnopqrstuvwxyz");
        }


        private void AskAmountOfPlayer()
        {
            int amount = 0;
            while(amount < 1)
            {
                Console.WriteLine("How many players will be playing this game of hangman?");
                string input = Console.ReadLine();
                try
                {
                    amount = int.Parse(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("That is not a number!");
                }
                Console.Clear();
                if(amount > maxPlayers)
                {
                    amount = 0;
                    Console.WriteLine("Too many players!");
                    Console.WriteLine($"You can play with up to {maxPlayers} players!");
                }
                else if(amount < 1)
                {
                    Console.WriteLine("Too little players!");
                }
                Console.WriteLine();
            }
            currentPlayers = new Player[amount];
            for(int i = 0; i < amount; i++)
            {
                currentPlayers[i] = new Player(maxHP);
                Console.WriteLine($"What is your name \"Player{i + 1}\"");

                string input = Console.ReadLine();
                bool allowed = currentPlayers[i].SetName(input);
                if (!allowed)
                {
                    Console.WriteLine("That name was not accepted. Please try again.");
                    i--;
                }
            }
        }


        private void MainGame()
        {
            bool winner = false;
            while (!winner)
            {
                Console.WriteLine($"{currentPlayers[playersTurn].currentName} tell us your guess");

                string input = Console.ReadLine();
                char guess = '\'';
                try 
                { 
                    if(input.Length != 1)
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        guess = input[0];
                        if (currentBoard.guessedLetters.Contains(guess))
                            throw new FormatException();
                    }
                }catch (FormatException)
                {
                    Console.WriteLine("Please only write a character");
                    continue;
                }
                Console.WriteLine(currentPlayers[playersTurn].hp);
                Console.WriteLine("Continues");
                Console.WriteLine($"You guessed {guess}");
                bool won;
                bool loseHP = currentBoard.CheckLetter(guess, out won);
                if (loseHP) { currentPlayers[playersTurn].hp--; }
                if (won)
                {
                    Console.WriteLine("You win!!!!!");
                    winner = true;
                }
                currentBoard.TellState();

                bool someoneHasHp = false;
                for(int i =0; i < currentPlayers.Length; i++)
                {
                    if (currentPlayers[i].hp > 0) { someoneHasHp = true;}
                }
                if (!someoneHasHp)
                {
                    winner = true;
                }
                else
                {
                    bool gotPlayer = false;
                    while (!gotPlayer)
                    {
                        playersTurn++;
                        if (playersTurn > currentPlayers.Length - 1)
                        {
                            playersTurn = 0;
                        }
                        if (currentPlayers[playersTurn].hp > 0)
                        {
                            gotPlayer = true;
                        }
                    }
                }
            }
        }

        
    }
}