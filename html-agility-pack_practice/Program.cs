using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace html_agility_pack_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            String callUrl = "http://lyrics.alsong.co.kr/alsongwebservice/service1.asmx";
            string title = "봄날";
            string artist = "방탄소년단";
            //String postData = "<?xml version=" + '\u0022' + "1.0" + '\u0022' + " encoding=" + '\u0022' + "UTF-8" + '\u0022' + "?>" + "<SOAP-ENV:Envelope  xmlns:SOAP-ENV=" + '\u0022' + "http://www.w3.org/2003/05/soap-envelope" + '\u0022' + " xmlns:SOAP-ENC=" + '\u0022' + "http://www.w3.org/2003/05/soap-encoding" + '\u0022' + " xmlns:xsi=" + '\u0022' + "http://www.w3.org/2001/XMLSchema-instance" + '\u0022' + " xmlns:xsd=" + '\u0022' + "http://www.w3.org/2001/XMLSchema" + '\u0022' + " xmlns:ns2=" + '\u0022' + "ALSongWebServer/Service1Soap" + '\u0022' + " xmlns:ns1=" + '\u0022' + "ALSongWebServer" + '\u0022' + " xmlns:ns3=" + '\u0022' + "ALSongWebServer/Service1Soap12" + '\u0022' + ">" + "<SOAP-ENV:Body>" + "<ns1:GetLyric5>" + "<ns1:stQuery>" + "<ns1:strChecksum>" + musicmd5 + "</ns1:strChecksum>" + "<ns1:strVersion>3.36</ns1:strVersion>" + "<ns1:strMACAddress>00ff667f9a08</ns1:strMACAddress>" + "<ns1:strIPAddress>xxx.xxx.xxx.xxx</ns1:strIPAddress>" + "</ns1:stQuery>" + "</ns1:GetLyric5>" + "</SOAP-ENV:Body>" + "</SOAP-ENV:Envelope>";
            String xml_string = "<?xml version=" + '\u0022' + "1.0" + '\u0022' + " encoding=" + '\u0022' + "UTF-8" + '\u0022' + "?><SOAP-ENV:Envelope xmlns:SOAP-ENV=" + '\u0022' + "http://www.w3.org/2003/05/soap-envelope" + '\u0022' + " xmlns:SOAP-ENC=" + '\u0022' + "http://www.w3.org/2003/05/soap-encoding" + '\u0022' + " xmlns:xsi=" + '\u0022' + "http://www.w3.org/2001/XMLSchema-instance" + '\u0022' + " xmlns:xsd=" + '\u0022' + "http://www.w3.org/2001/XMLSchema" + '\u0022' + " xmlns:ns2=" + '\u0022' + "ALSongWebServer/Service1Soap" + '\u0022' + " xmlns:ns1=" + '\u0022' + "ALSongWebServer" + '\u0022' + " xmlns:ns3=" + '\u0022' + "ALSongWebServer/Service1Soap12" + '\u0022' + "><SOAP-ENV:Body><ns1:GetResembleLyric2><ns1:stQuery><ns1:strTitle>" + title + "</ns1:strTitle><ns1:strArtistName>" + artist + "</ns1:strArtistName><ns1:nCurPage>0</ns1:nCurPage></ns1:stQuery></ns1:GetResembleLyric2></SOAP-ENV:Body></SOAP-ENV:Envelope>";

            //Console.WriteLine("{0}", xml_string);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(callUrl);
            // 인코딩 UTF-8
            byte[] sendData = UTF8Encoding.UTF8.GetBytes(xml_string);
            httpWebRequest.ContentType = "application/soap+xml; charset=UTF-8";
            httpWebRequest.UserAgent = "gSOAP/2.7";
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = sendData.Length;
            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(sendData, 0, sendData.Length);
            requestStream.Close();
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string respone = streamReader.ReadToEnd();
            Console.WriteLine(respone);
            //m_net();
            void m_net()
            {
                var youtube_name = "윤딴딴 Yun ddanddan 윤딴딴 10CM 폰서트 LIVE";



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


                if (obj["resultCode"].ToString() == "S0000" || obj["message"].ToString() == "성공")//맨뒤가 이러면 실패네
                {
                    if (obj["info"]["songcnt"].ToString() != "0")
                    {
                        string song_name = obj["data"]["songlist"][0]["songnm"].ToString();
                        string album_mname = obj["data"]["songlist"][0]["albumnm"].ToString();
                        string articst_name = obj["data"]["songlist"][0]["ARTIST_NMS"].ToString();

                        Console.WriteLine("song_name : " + song_name);
                        Console.WriteLine("album_mname : " + album_mname);
                        Console.WriteLine("articst_name : " + articst_name);

                    }
                    else
                    {
                        Console.WriteLine("0개 나옴\n검색결과 없음");
                    }
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
}
