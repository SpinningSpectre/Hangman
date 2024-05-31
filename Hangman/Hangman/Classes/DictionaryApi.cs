namespace HangMan
{
    internal class DictionaryApi
    {
        //Makes a client
        private readonly HttpClient client = new HttpClient();

        //Used for getting the words out of here
        public List<string> words = new List<string>();
        public async Task Try(Game gam)
        {
            //Makes the game wait until this gets a word (list)
            gam.isWaiting = true;

            //The api with the words
            string url = "http://www.wordgenerator.net/application/p.php?id=dictionary_words&type=1&spaceflag=false";

            try
            {
                //Gets the words from the site
                string resp = await client.GetStringAsync(url);

                //Splits the string into different words
                string[] currentWords = resp.Split(',');

                //Adds every word to the list
                foreach(string word in currentWords)
                {
                    words.Add(word);
                }

                //Resumes the game
                gam.isWaiting = false;
            }catch (HttpRequestException e)
            {
                //Error.
                Console.WriteLine(e.Message);
            }
        }
    }
}