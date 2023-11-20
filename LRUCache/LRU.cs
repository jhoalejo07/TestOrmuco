using LRUCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


/******************************************************************
* Jhohan Arias
* 02/11/2023
* Technical test Ormuco
*******************************************************************/


namespace LRUCache
{

    /**************************************************************************************************************************
    * Q3. Geo Distributed LRU (Least Recently Used) cache with time expiration.
    * 
    * To be honest, at this point, I developed this solution from my point of view as a beginner. My knowledge as a programmer could be 
    * improved in dealing with network failures or crashes, real-time replication, and consistency across regions. For that reason, 
    * I came up with a solution pretty simple with an example of the LRU (Least Recently Used) concept of adding time expiration in a 
    * standalone environment. I want to put it under your consideration.
    * 
    ***************************************************************************************************************************/


    public class LRU
    {
        private List<int> itemsList;// list for items
        private HashSet<int> idList;  // List  for identifiers
        private Dictionary<int, DateTime> ExpirationList; // Dictionary to store the DateTime of each item
        private int CACHE_SIZE;// constant for Maximun number the items in the cache
        private Timer expirationTimer; // add a timer to trigger the ExpireCacheTimerCallback method periodically (NEW)
        private int EXPIRATION_TIME; // constant to setup the time expiration (NEW)

        public LRU(int capacity, int experation)
        {
            itemsList = new List<int>(); 
            idList = new HashSet<int>(); 
            ExpirationList = new Dictionary<int, DateTime>(); 
            CACHE_SIZE = capacity;
            EXPIRATION_TIME = experation;

            // Set up a timer to trigger cache expiration check every EXPIRATION_TIME seconds (parameter) (NEW)
            expirationTimer = new Timer(ExpireCacheTimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(EXPIRATION_TIME));
        }

        public void AddItem(int page)
        {
            DateTime currentTime = DateTime.Now;// takes currrently timestamp to associate to the new item

            if (!idList.Contains(page)) // check if the item doesn´t exist
            {
                if (itemsList.Count > CACHE_SIZE-1) // if the number is not in the list, check it reaches the maximun items 
                {
                    //if it reaches the maximum, the last item is removed
                    int last = itemsList[itemsList.Count - 1];
                    itemsList.RemoveAt(itemsList.Count - 1);
                    idList.Remove(last);
                    ExpirationList.Remove(last);
                }
            }
            else
            {
                itemsList.Remove(page); // if the item exists then it is removed 
            }
            
            itemsList.Insert(0, page); // the new number it add it on the top 
            idList.Add(page); //it is associate with and identifier
            ExpirationList[page] = currentTime; // it is store its datatime
        }


        private void ExpireCacheTimerCallback(object state)
        {
            // Expire cache items 
            ExpireCache(TimeSpan.FromSeconds(EXPIRATION_TIME)); // Adjust the expiration time as needed
            Console.WriteLine("Items after cache expiration");
            Display();
        }


        // Remove expired items from the cache
        public void ExpireCache(TimeSpan expirationTime) 
        {
            DateTime currentTime = DateTime.Now; // takes currrently timestamp to compare with the object's timestamp
            List<int> itemsToRemove = new List<int>();// list to keep expired  items

            foreach (var item in ExpirationList)  // check one by one the item if they have an expiration greater than it was set up
            {
                if (currentTime - item.Value > expirationTime)
                {
                    itemsToRemove.Add(item.Key);
                }
            }

            foreach (var itemToRemove in itemsToRemove)  // delete from the LRU's list the objects that have expiration time
            {
                idList.Remove(itemToRemove);
                itemsList.Remove(itemToRemove);
                ExpirationList.Remove(itemToRemove);
            }
        }

        public void Display() // to show the items from the list
        {
            foreach (int page in itemsList)
            {
                Console.Write(page + " ");
            
            }
            Console.WriteLine("\n");
        }

        public List<int> GetItems()
        {
            return itemsList.ToList(); // Create a new list to avoid exposing the internal list directly
        }

        static void Main(string[] args)
        {
            LRU cache = new LRU(3,1); // instance the class with a capacity of 3 items with 1 second of expiration time
            cache.AddItem(1); // enter the first item
            cache.Display(); // display the list
            cache.AddItem(2);
            cache.Display();
            cache.AddItem(3);
            cache.Display();
            cache.AddItem(1); // enter an existing value in the list that it doesn't expire
            cache.Display();
            cache.AddItem(4);
            cache.Display();
            cache.AddItem(5);
            cache.Display();
            cache.AddItem(1);
            cache.Display();

            System.Threading.Thread.Sleep(2000); // waiting for 2 seconds to let expire the items
            cache.AddItem(5); // add new items to see them expire
            cache.Display();
            cache.AddItem(1);
            cache.Display();

            Console.ReadLine();
        }
    }
}
