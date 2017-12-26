using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MtgApi
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            var searchString = "";

            while (searchString != "quit")
            {
                Console.Write("enter set keycode or 'quit' to quit: ");
                searchString = Console.ReadLine();
                scryFall(searchString);
                // Need to wait here until the async method above completes.

            }

            //Console.ReadLine();
            //Console.ReadKey();
        }

        static async void scryFall(string searchString)
        {
            string page = "https://api.scryfall.com/cards/search?order=set&q=%2B%2Be%3A" + searchString;
            bool more_data = true;

            while (more_data)
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        JObject parsed = JObject.Parse(data);
                        var myConnString = @"server = Austin; uid = root; pwd = default; database = magicfinancedb";
                        MySqlConnection connection = new MySqlConnection(myConnString);

                        foreach (var obj in parsed["data"])
                        {
                            string name = (string)obj["name"];
                            string usd = (string)obj["usd"];
                            string set = (string)obj["set"];
                            string set_name = (string)obj["set_name"];
                            int multiverse_id = (int)obj["multiverse_ids"][0];
                            int mtgo_id = (int)obj["mtgo_id"];
                            int mtgo_foil_id = (int)obj["mtgo_foil_id"];
                            int cmc = (int)obj["cmc"];
                            string type_line = (string)obj["type_line"];
                            string oracle_text = (string)obj["oracle_text"];
                            string mana_cost = (string)obj["mana_cost"];
                            //string power = (string)obj["power"];
                            //string toughness = (string)obj["toughness"];
                            //string colors = (string)obj["colors"][0];
                            //string color_identity = (string)obj["color_identity"][0];
                            string legalities = (string)obj["legalities"]["standard"];
                            bool reserved = (bool)obj["reserved"];
                            bool reprint = (bool)obj["reprint"];
                            int collector_number = (int)obj["collector_number"];
                            string rarity = (string)obj["rarity"];
                            string artist = (string)obj["artist"];

                            Console.WriteLine("{2} - {0}: ${1}", name, usd, mana_cost);

                            MagicFinanceDbDAL.InsertCardDetails(multiverse_id,mtgo_id,mtgo_foil_id,name,usd,cmc,set_name,type_line,set,
                                                                oracle_text,mana_cost,legalities,reserved,reprint,collector_number,rarity,artist);
                            MagicFinanceDbDAL.InsertPriceDetails(multiverse_id,usd);

                        }

                        if (parsed["has_more"].ToString().ToLower() == "true")
                        {
                            page = parsed["next_page"].ToString();
                        }
                        else
                        {
                            more_data = false;
                            Console.WriteLine("done loading data");
                        }
                    }
                }
                Thread.Sleep(100);

            }

        }
    }
}