namespace HangMan
{
    internal class Game
    {
        //Constant values
        private const int maxPlayers = 5;
        private const int maxHP = 7;

        //Current game
        public Player[] currentPlayers;
        public Board currentBoard;
        public int playersTurn;

        //Awaiting word
        public bool isWaiting = true;


        public void StartGame()
        {
            AskAmountOfPlayer();
            SetWord();
            MainGame();
        }

        private void SetWord()
        {
            
            Console.Clear();
            Console.WriteLine("Finding Word.");

            //Gets a list of words
            DictionaryApi api = new DictionaryApi();
            api.Try(this);

            //Waits to get word
            while (isWaiting) { }

            //Chooses a random one
            List<string> words = api.words;
            Random rand = new Random();
            int rnd = rand.Next(0,words.Count - 1);

            //Sets it
            currentBoard = new Board(words[rnd]);

            //Text
            Console.WriteLine("Word found!");
            Thread.Sleep(1000);
            Console.Clear();
        }


        private void AskAmountOfPlayer()
        {
            int amount = 0;
            while(amount < 1)
            {
                //Asks the amount
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

                //Too little too much
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

            //Makes the players
            currentPlayers = new Player[amount];
            for(int i = 0; i < amount; i++)
            {
                //Sets the hp
                currentPlayers[i] = new Player(maxHP);

                //Sets the players name, I was planning a profanity filter but I didnt quite get to it.
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
            //Tells you the game starts
            Console.Beep(500, 300);

            //This bool is called winner but it is not only called when there is a winner.
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
                        //Checks if the guess is the full word
                        if(input == currentBoard.Word)
                        {
                            Console.WriteLine($"{currentPlayers[playersTurn].currentName} wins!!!!!");
                            winner = true;
                            continue;
                        }

                        //Removes health if it isnt
                        currentPlayers[playersTurn].hp--;

                        //Checks if everyone is still alive
                        bool someoneHasHp2 = false;
                        for (int i = 0; i < currentPlayers.Length; i++)
                        {
                            if (currentPlayers[i].hp > 0) { someoneHasHp2 = true; }
                        }

                        //If there arent it stops the game
                        if (!someoneHasHp2)
                        {
                            winner = true;
                            continue;
                        }

                        //Finds the next living player
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

                        //Tells you the state of the game for the next round
                        currentBoard.TellState();
                        Console.WriteLine("Incorrect guess!");
                        throw new FormatException();
                    }
                    else
                    {
                        //Makes the guess and tells you if your letter has already been guessed.
                        guess = input[0];
                        if (currentBoard.guessedLetters.Contains(guess))
                        {
                            Console.WriteLine("This has already been guessed");
                            throw new FormatException();
                        }
                    }
                }catch (FormatException)
                {
                    continue;
                }

                //Checks your guess and removes your health if it was wrong
                Console.WriteLine();
                Console.WriteLine($"You guessed {guess}");

                bool won;
                bool loseHP = currentBoard.CheckLetter(guess, out won);
                if (loseHP) { currentPlayers[playersTurn].hp--; }

                //Tells the player their current health and checks if they died
                Console.WriteLine(currentPlayers[playersTurn].hp);
                if (currentPlayers[playersTurn].hp < 1) 
                { 
                    Console.WriteLine($"{currentPlayers[playersTurn].currentName} died!");
                    Thread.Sleep(1000);
                }

                //Tells the player(s) if anyone has won
                if (won)
                {
                    Console.WriteLine($"{currentPlayers[playersTurn].currentName} wins!!!!!");
                    winner = true;
                }

                currentBoard.TellState();

                //Checks if the players are still alive
                bool someoneHasHp = false;
                for(int i =0; i < currentPlayers.Length; i++)
                {
                    if (currentPlayers[i].hp > 0) { someoneHasHp = true;}
                }
                if (!someoneHasHp)
                {
                    winner = true;
                    Console.WriteLine(currentBoard.Word);
                }
                else
                {
                    //Makes the next player the.... player
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