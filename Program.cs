using HtmlAgilityPack;

namespace PlaylistParser;
class Program
{
    public static void Main(string[] args)
    {
        List<string> list = new List<string>();
        list.Add("https://www.1001tracklists.com/tracklist/1yr1x139/paul-van-dyk-vonyc-sessions-787-2021-11-30.html");
        list.Add("https://www.1001tracklists.com/tracklist/g9d0t21/paul-van-dyk-vonyc-sessions-788-2021-12-07.html");
        list.Add("https://www.1001tracklists.com/tracklist/1c59m9f1/paul-van-dyk-vonyc-sessions-789-2021-12-14.html");
        list.Add("https://www.1001tracklists.com/tracklist/1xtcvt89/paul-van-dyk-vonyc-sessions-790-2021-12-21.html");
        list.Add("https://www.1001tracklists.com/tracklist/udxl2tk/paul-van-dyk-vonyc-sessions-791-2021-12-28.html");
        foreach (string item in list)
        {
            GetPlaylistInfo(item);
            GetSongInfo(item);
        }
    }
    public static void GetPlaylistInfo(string url)
    {
        HtmlWeb web = new();
        var doc = web.Load(url);
        var playlistName = doc.DocumentNode.SelectSingleNode(".//h1[@class='notranslate']//a[2]").InnerText;
        var recordLabel = doc.DocumentNode.SelectSingleNode(".//h1[@class='notranslate']//a[1]").InnerText;
        var releaseDate = doc.DocumentNode.SelectSingleNode(".//div[@class='sTab c2 cM']//div[2]").InnerText;
        var genre = doc.DocumentNode.SelectSingleNode(".//div[@id='tl_music_styles']").InnerText;
        var duration = doc.DocumentNode.SelectSingleNode(".//li[@class='tBtn mediaTab13 active']").InnerText.Replace(" Player 1 ", "");
        Console.WriteLine($"{recordLabel} - {playlistName}\n" +
            $"Release date: {releaseDate}\n" +
            $"Genre: {genre}\n" +
            $"Duration: {duration}");
    }
    public static void GetSongInfo(string url)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);
        var tables = doc.DocumentNode.SelectNodes(".//div[@class='tlpTog bItm tlpItem trRow1']");
        for (int i = 2; i <= 23; i++)
        {
            tables.Add(doc.DocumentNode.SelectSingleNode($".//div[@class='tlpTog bItm tlpItem trRow{i}']"));
        }
        var artistList = new List<string>();
        var songList = new List<string>();
        var albumList = new List<string>();
        foreach (var table in tables)
        {
            artistList.Add(table.SelectSingleNode(".//span[1]//span[1]").InnerText);
            songList.Add(table.SelectSingleNode(".//span[1]//span[@class='blueTxt']").InnerText);
            albumList.Add(table.SelectSingleNode(".//span[2]//span[1]").InnerText);
        }
        for (int i = 0; i < 23; i++)
        {
            Console.WriteLine($"\t{artistList[i]} - {songList[i]} [{albumList[i]}]");
        }
    }
}