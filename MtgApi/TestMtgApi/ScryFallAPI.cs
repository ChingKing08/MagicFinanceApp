using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace MtgApi
{
    public class ScryFallAPI
    {
        public ScryFallAPI()
        {
        }

        public static async void scryFall(string searchString)
        {
            //searching aginst just multiverseid can be done as below
            //https://api.scryfall.com/cards/multiverse/ + "multiverse_id"

            string page = "https://api.scryfall.com/cards/search?order=set&q=%2B%2Be%3A" + searchString;
            bool more_data = true;
            //List<Card> cardList;

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

                        foreach (var obj in parsed["data"])
                        {

                            Card card = JSONParser.ParseCard(obj);

                            Console.WriteLine("{2} - {0}: ${1}", card._name, card._usd, card._mana_cost);

                            MagicFinanceDbDAL.InsertCardDetails(card._multiverse_id, card._mtgo_id, card._mtgo_foil_id, card._name, card._usd, card._cmc, card._set_name, card._type_line, card._set,
                                                                card._oracle_text, card._mana_cost, card._legalities, card._reserved, card._reprint, card._collector_number, card._rarity, card._artist);
                            MagicFinanceDbDAL.InsertPriceDetails(card._multiverse_id, card._usd);

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

        public static async void GetAllSets()
        {
            //searching aginst just multiverseid can be done as below
            //https://api.scryfall.com/cards/multiverse/ + "multiverse_id"
            //all set details is available here, which can be used to traverse all cards for all/specific sets
            //https://api.scryfall.com/sets

            string page = "https://api.scryfall.com/sets";
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

                        foreach (var obj in parsed["data"])
                        {

                            Set set = JSONParser.ParseSet(obj);

                            Console.WriteLine("{0} - {1}: Block: {2}", set._code, set._name, set._block);

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
