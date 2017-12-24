﻿using System;
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

                        foreach (var obj in parsed["data"])
                        {
                            string name = (string)obj["name"];
                            string price = (string)obj["usd"];

                            Console.WriteLine("{0}: ${1}", name, price);
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

                var myConnString = @"server = 192.168.1.140; uid = root; pwd = default; database = magicfinancedb";
                MySqlConnection connection = new MySqlConnection(myConnString);

                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM TestTable;", connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("{0}: {1}",
                            reader.GetInt32(0),
                            reader.GetValue(1));
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

        }
    }
}