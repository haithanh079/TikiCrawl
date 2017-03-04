using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinTrinhLibrary;
using System.Text.RegularExpressions;


//Mo ta crawl:
//    + Thu Thap danh sach 100 sach ban chay nhat tren tiki (Co the sua doi de crawl toan bo sach cua tiki)
//    + Top 100 bao gom 4 page
//    + 1 page 25 san pham
//    + Ghi file txt trong disk D:\

namespace Lab1crawl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start...");
            //GET : HTTPS://tiki.vn
            TinTrinhLibrary.WebClient client = new TinTrinhLibrary.WebClient();
            int stt = 0;
            StreamWriter myFile = new StreamWriter(@"D:\Top100BookTiki.txt");
            //Vong lap so page cua top 100
            for (int i = 1; i<=4; i++)
            {
                string link = "https://tiki.vn/bestsellers/sach-truyen-tieng-viet/c316?p=" + i;
                string html = client.Get(link, "https://tiki.vn", "");

                //Regex data
                MatchCollection author = Regex.Matches(html, "data-brand=\"(.*?)\" data-category");
                MatchCollection name = Regex.Matches(html, "data-title=\"(.*?)\" data-brand");
                MatchCollection price = Regex.Matches(html, "data-price=\"(.*?)\" data-title");
                MatchCollection description = Regex.Matches(html, "description\">(.*?)<a");

                // 1 page bao gom 25 sp
                for (int j=0; j<25; j++)
                {
                    stt++;
                    Console.WriteLine("STT : {0}", stt);
                    Console.WriteLine("Ten Sach : {0}", name[j].Groups[1].Value);
                    Console.WriteLine("Ten Tac Gia : {0}", author[j].Groups[1].Value);
                    Console.WriteLine("Gia Sach : {0}", price[j].Groups[1].Value," VND");
                    Console.WriteLine("  ");
                    //Output to log
                    myFile.WriteLine("STT : {0}", stt);
                    myFile.WriteLine("Ten Sach : {0}", name[j].Groups[1].Value);
                    myFile.WriteLine("Ten Tac Gia : {0}", author[j].Groups[1].Value);
                    myFile.WriteLine("Gia Sach : {0}", price[j].Groups[1].Value, " VND");
                    myFile.WriteLine("  ");
                };
            }
            myFile.Close();
            Console.Read();
        }
    }
}
