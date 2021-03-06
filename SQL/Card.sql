CREATE TABLE `card` (
  `multiverse_id` int(11) NOT NULL,
  `mtgo_id` int(11) DEFAULT NULL,
  `mtgo_foil_id` int(11) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `cmc` int(11) DEFAULT NULL,
  `type_line` varchar(255) DEFAULT NULL,
  `mana_cost` varchar(255) DEFAULT NULL,
  `power` varchar(255) DEFAULT NULL,
  `toughness` varchar(255) DEFAULT NULL,
  `colors` varchar(255) DEFAULT NULL,
  `color_identity` varchar(255) DEFAULT NULL,
  `legalities` varchar(255) DEFAULT NULL,
  `reserved` bool DEFAULT NULL,
  `reprint` bool DEFAULT NULL,
  `collector_number` int(11) DEFAULT NULL,
  `rarity` varchar(255) DEFAULT NULL,
  `artist` varchar(255) DEFAULT NULL,
  `oracle_text` varchar(2000) DEFAULT NULL,
  `set` varchar(255) DEFAULT NULL,
  `set_name` varchar(255) DEFAULT NULL,
  `usd` float DEFAULT NULL,
  PRIMARY KEY (`multiverse_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;