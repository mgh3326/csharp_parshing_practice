using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
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

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);


            //Console.WriteLine(name.OuterHtml);
            //foreach(var oh in htmlDoc.DocumentNode.SelectNodes("//*[@id='content_v']//*[@class='search_result_line']//*[@class='MnetMusicList MnetMusicListSearch']//*[@class='MMLTable']"))
            //{
            //    Console.WriteLine(oh.OuterHtml);
            //}
            Console.WriteLine(htmlDoc.DocumentNode.OuterHtml);
            //Console.WriteLine(htmlDoc.DocumentNode.SelectSingleNode("//body").OuterHtml);

        }
    }
}
