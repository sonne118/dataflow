using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Actor
{
    public class Topic
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class HabrParser
    {
        private readonly HttpClient _http;

        public HabrParser()
        {
            _http = new HttpClient();
        }


        public async IAsyncEnumerable<string> GetPages()
        {
            await Task.Delay(100);
            yield return "https://habr.com/ru/all/";
            yield return "https://habr.com/ru/all/page2/";
            yield return "https://habr.com/ru/all/page3/";
            yield return "https://habr.com/ru/all/page4/";
            yield return "https://habr.com/ru/all/page5/";
            yield return "https://habr.com/ru/all/page6/";
            yield return "https://habr.com/ru/all/page7/";
            yield return "https://habr.com/ru/all/page8/";
            yield return "https://habr.com/ru/all/page9/";
            yield return "https://habr.com/ru/all/page10/";
        }

        public async Task<Topic[]> ParseTopics(string url)
        {
            var res = new List<Topic>();
            var body = await _http.GetStringAsync(url);

            var titles = Regex.Matches(body, "(?<=class=\"post__title_link\">)[^</a>]+");
            var urls = Regex.Matches(body, "(?<=<a href=\")[^\"]+(?=\" class=\"post__title_link\">)");

            for (int i = 0; i < titles.Count; i++)
            {
                res.Add(new Topic
                {
                    Title = titles[i].Value,
                    Url = urls[i].Value,
                });
            }

            return res.ToArray();
        }
    }

}