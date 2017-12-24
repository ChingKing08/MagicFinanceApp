CREATE TABLE `cardprice` (
  `multiverse_id` int(11) NOT NULL,
  `usd` float DEFAULT NULL,
  `price_date` datetime NOT NULL,
  PRIMARY KEY (`multiverse_id`, `price_date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;