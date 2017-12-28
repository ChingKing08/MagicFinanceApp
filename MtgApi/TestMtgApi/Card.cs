using System;
namespace MtgApi
{
    public class Card
    {
        public string _name;
        public string _usd;
        public string _set;
        public string _set_name;
        public int _multiverse_id;
        public int _mtgo_id;
        public int _mtgo_foil_id;
        public int _cmc;
        public string _type_line;
        public string _oracle_text;
        public string _mana_cost;
        public string _legalities;
        public bool _reserved;
        public bool _reprint;
        public int _collector_number;
        public string _rarity;
        public string _artist;

        public Card()
        {
            
        }
    }
}
