using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace html_agility_pack_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            var youtube_name = "오마이걸 반하나 - 바나나 알러지 원숭이 [세로라이브] OH MY GIRL BANHANA - Banana allergy monkey";



            var html = @"http://search.mnet.com/search/index.asp?q=" + youtube_name;
            var url = Uri.EscapeUriString(@"http://search.api.mnet.com/search/totalweb?q=" + youtube_name + "&sort=r&callback=angular.callbacks._0");//인코딩?
            Console.WriteLine(url);
            string doc = "";
            //using (System.Net.WebClient client = new System.Net.WebClient()) // WebClient class inherits IDisposable
            //{

            //    doc = client.DownloadString(url);
            //}
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            doc = wc.DownloadString(url);
            wc.Dispose();//이게 해제인가
            //doc = doc.Substring(21);
            doc = doc.Substring(21, doc.Length - 1 - 21);

            JObject obj = JObject.Parse(doc);
            Console.WriteLine(obj["message"].ToString());

            if (obj["resultCode"].ToString()== "S0000" ||obj["message"].ToString()=="성공" )
            {
                string song_name = obj["data"]["songlist"][0]["songnm"].ToString();
                string album_mname = obj["data"]["songlist"][0]["albumnm"].ToString();
                string articst_name = obj["data"]["songlist"][0]["ARTIST_NMS"].ToString();

                Console.WriteLine("song_name : "+song_name);
                Console.WriteLine("album_mname : " + album_mname);
                Console.WriteLine("articst_name : " + articst_name);

            }
            else
            {
                Console.WriteLine("실패입니다.");
            }
            //obj["First"][""] 
            //Console.WriteLine(doc);

            //HtmlWeb web = new HtmlWeb();

            //var htmlDoc = web.Load(url);

            //var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            //Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);


            //Console.WriteLine(name.OuterHtml);
            //foreach(var oh in htmlDoc.DocumentNode.SelectNodes("//*[@id='content_v']//*[@class='search_result_line']//*[@class='MnetMusicList MnetMusicListSearch']//*[@class='MMLTable']"))
            //{
            //    Console.WriteLine(oh.OuterHtml);
            //}
            //Console.WriteLine(htmlDoc.Text);
            //Console.WriteLine(htmlDoc.DocumentNode.SelectSingleNode("//body").OuterHtml);

        }
    }
}
