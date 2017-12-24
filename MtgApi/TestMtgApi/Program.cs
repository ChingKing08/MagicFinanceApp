using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;
using MySql.Data.MySqlClient;

namespace MtgApi
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Write("set keycode: ");
            var searchString = Console.ReadLine();
            scryFall(searchString);
            Console.ReadLine();
            Console.ReadKey();
        }

        static async void scryFall(string searchString)
        {
            //string page = "https://api.scryfall.com/cards";
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

                            connection.Open();
                            try
                            {
                                var SqlCommandString = "INSERT INTO Card (`multiverse_id`, `name`, `set`, `set_name`, `usd`, `mtgo_id`, `mtgo_foil_id`, `cmc`, `type_line`, `oracle_text`, `mana_cost`, `legalities`, `reserved`, `reprint`, `collector_number`, `rarity`, `artist`) " +
                                    "VALUES (@multiverse_id, @name, @set, @set_name, @usd, @mtgo_id, @mtgo_foil_id, @cmc, @type_line, @oracle_text, @mana_cost, @legalities, @reserved, @reprint, @collector_number, @rarity, @artist);";
                                MySqlCommand cmd = new MySqlCommand(SqlCommandString, connection);
                                cmd.Parameters.AddWithValue("@multiverse_id", multiverse_id);
                                cmd.Parameters.AddWithValue("@mtgo_id", mtgo_id);
                                cmd.Parameters.AddWithValue("@mtgo_foil_id", mtgo_foil_id);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Parameters.AddWithValue("@usd", usd);
                                cmd.Parameters.AddWithValue("@cmc", cmc);
                                cmd.Parameters.AddWithValue("@set_name", set_name);
                                cmd.Parameters.AddWithValue("@type_line", type_line);
                                cmd.Parameters.AddWithValue("@set", set);
                                cmd.Parameters.AddWithValue("@oracle_text", oracle_text);
                                cmd.Parameters.AddWithValue("@mana_cost", mana_cost);
                                //cmd.Parameters.AddWithValue("@power", power);
                                //cmd.Parameters.AddWithValue("@toughness", toughness);
                                //cmd.Parameters.AddWithValue("@colors", colors);
                                //cmd.Parameters.AddWithValue("@color_identity", color_identity);
                                cmd.Parameters.AddWithValue("@legalities", legalities);
                                cmd.Parameters.AddWithValue("@reserved", reserved);
                                cmd.Parameters.AddWithValue("@reprint", reprint);
                                cmd.Parameters.AddWithValue("@collector_number", collector_number);
                                cmd.Parameters.AddWithValue("@rarity", rarity);
                                cmd.Parameters.AddWithValue("@artist", artist);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            connection.Close();


                            connection.Open();
                            try
                            {
                                var SqlCommandString = "INSERT INTO CardPrice (`multiverse_id`, `usd`, `price_date`) " +
                                    "VALUES (@multiverse_id, @usd, @price_date);";
                                MySqlCommand cmd = new MySqlCommand(SqlCommandString, connection);
                                cmd.Parameters.AddWithValue("@multiverse_id", multiverse_id);
                                cmd.Parameters.AddWithValue("@usd", usd);
                                cmd.Parameters.AddWithValue("@price_date", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            connection.Close();

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

                //var myConnString = @"server = 192.168.1.140; uid = root; pwd = default; database = magicfinancedb";
                //MySqlConnection connection = new MySqlConnection(myConnString);

                //try
                //{
                //    MySqlCommand cmd = new MySqlCommand("SELECT * FROM TestTable;", connection);
                //    connection.Open();
                //    cmd.ExecuteNonQuery();
                //    MySqlDataReader reader = cmd.ExecuteReader();

                //    while (reader.Read())
                //    {
                //        Console.WriteLine("{0}: {1}",
                //            reader.GetInt32(0),
                //            reader.GetValue(1));
                //    }
                //    connection.Close();
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);
                //}
            }

        }
    }
}