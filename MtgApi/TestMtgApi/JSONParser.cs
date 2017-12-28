using System;
using Newtonsoft.Json.Linq;

namespace MtgApi
{
    public class JSONParser
    {
        public JSONParser()
        {
        }

        public static Card ParseCard(JToken cardData)
        {
            Card card = new Card();

            card._name = (string)cardData["name"];
            card._usd = (string)cardData["usd"];
            //card._set = (string)cardData["set"];
            //card._set_name = (string)cardData["set_name"];

            JToken token = cardData["multiverse_ids"];
            if (token != null && token.ToString() != "[]")
            {
                card._multiverse_id = (int)cardData["multiverse_ids"][0];
            }

            //card._multiverse_id = (int)cardData["multiverse_ids"][0];
            //card._mtgo_id = (int)cardData["mtgo_id"];
            //card._mtgo_foil_id = (int)cardData["mtgo_foil_id"];
            //card._cmc = (int)cardData["cmc"];
            //card._type_line = (string)cardData["type_line"];
            //card._oracle_text = (string)cardData["oracle_text"];
            //card._mana_cost = (string)cardData["mana_cost"];
            //string power = (string)cardData["power"];
            //string toughness = (string)cardData["toughness"];
            //string colors = (string)cardData["colors"][0];
            //string color_identity = (string)cardData["color_identity"][0];
            //card._legalities = (string)cardData["legalities"]["standard"];
            //card._reserved = (bool)cardData["reserved"];
            //card._reprint = (bool)cardData["reprint"];
            //int collector_number = (int)cardData["collector_number"];
            //string rarity = (string)cardData["rarity"];
            //string artist = (string)cardData["artist"];

            return card;
        }

        public static Set ParseSet(JToken setData)
        {
            Set set = new Set();

            set._name = (string)setData["name"];
            set._block = (string)setData["block"];
            set._code = (string)setData["code"];

            return set;
        }
    }
}
