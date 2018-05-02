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
            var html = @"http://html-agility-pack.net/";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
            Console.WriteLine(htmlDoc.DocumentNode.SelectSingleNode("//body").OuterHtml);

        }
    }
}
