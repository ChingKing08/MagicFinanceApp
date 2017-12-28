using System;
using System.Threading;
using System.Threading.Tasks;

namespace MtgApi
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            var searchString = "";

            ScryFallAPI.GetAllSets();
            Thread.Sleep(10000);
            Console.WriteLine("press enter to get cards for set");
            Console.ReadKey();

            while (searchString != "quit")
            {
                Console.Write("enter set keycode or 'quit' to quit: ");
                searchString = Console.ReadLine();
                if (searchString == "quit")
                {
                    return;
                }
                ScryFallAPI.GetCardsForSet(searchString);

                // Need to wait here until the async method above completes. This is a poor man's implementation of that.
                Thread.Sleep(10000);
            }
        }
    }
}