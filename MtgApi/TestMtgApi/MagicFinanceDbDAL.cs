using System;
using MySql.Data.MySqlClient;

namespace MtgApi
{
    public static class MagicFinanceDbDAL
    {
        
        public static void InsertCardDetails(int multiverse_id, int mtgo_id, int mtgo_foil_id, string name, 
                                      string usd, int cmc, string set_name, string type_line, string set, 
                                      string oracle_text, string mana_cost, string legalities, bool reserved,
                                      bool reprint, int collector_number, string rarity, string artist)
        {
            var myConnString = @"server = Austin; uid = root; pwd = default; database = magicfinancedb";
            MySqlConnection connection = new MySqlConnection(myConnString);

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

        }

        public static void InsertPriceDetails(int multiverse_id, string usd)
        {
            var myConnString = @"server = Austin; uid = root; pwd = default; database = magicfinancedb";
            MySqlConnection connection = new MySqlConnection(myConnString);

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


    }
}
