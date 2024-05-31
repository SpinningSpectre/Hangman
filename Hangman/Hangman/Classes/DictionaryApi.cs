namespace HangMan
{
    internal class DictionaryApi
    {

        private readonly HttpClient client = new HttpClient();
        public List<string> words = new List<string>();
        public async Task Try(Game gam)
        {
            gam.isWaiting = true;
            string url = "http://www.wordgenerator.net/application/p.php?id=dictionary_words&type=1&spaceflag=false";

            try
            {
                string resp = await client.GetStringAsync(url);
                string[] currentWords = resp.Split(',');
                foreach(string word in currentWords)
                {
                    words.Add(word);
                }
                gam.isWaiting = false;
            }catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}