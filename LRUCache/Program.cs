using LRUCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/******************************************************************
* Jhohan Arias
* 02/11/2023
* Technical test Ormuco
*******************************************************************/


namespace LRUCache
{

    /**************************************************************************************************************************
    * Q3. Geo Distributed LRU (Least Recently Used) cache with time expiration.
    ***************************************************************************************************************************/


     class LRUCache
    {
        private List<int> itemsList;// list for items
        private HashSet<int> idList;  // List  for identifiers
        private Dictionary<int, DateTime> ExpirationList; // Dictionaty to store datatime of each item
        private int CACHE_SIZE;// Maximun number the items in the cache

        public LRUCache(int capacity)
        {
            itemsList = new List<int>(); 
            idList = new HashSet<int>(); 
            ExpirationList = new Dictionary<int, DateTime>(); 
            CACHE_SIZE = capacity; 
        }

        public void addItem(int page)
        {
            DateTime currentTime = DateTime.Now;// takes currrently timestamp to associate to the new item

            if (!idList.Contains(page)) // check if the item exists
            {
                if (itemsList.Count == CACHE_SIZE) // if reaches the maximun items then the last position it is removed
                {
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

            itemsList.Insert(0, page); // the item is insert in the first position of the list od contents
            idList.Add(page); //it is associate with and identifier
            ExpirationList[page] = currentTime; // it is store its datatime
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

            foreach (var itemToRemove in itemsToRemove)  // dete from the LRU's list the objects that have expiration time
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

        static void Main(string[] args)
        {
            LRUCache cache = new LRUCache(4); // instance the class with a capacity of 4 items
            cache.addItem(1); // enter the first item
            cache.Display(); // display the list
            cache.addItem(2);
            cache.Display();
            cache.addItem(3);
            cache.Display();
            Console.WriteLine("A existed value was entered");
            cache.addItem(1); // enter an existed value in the list that it doesn't expire
            cache.Display();
            cache.addItem(4);
            cache.Display();
            cache.addItem(5);
            cache.Display();
            cache.addItem(1);
            cache.Display();

            System.Threading.Thread.Sleep(6000); // waiting for 6 seconds to let expire the items
            // Expire cache items 
            cache.ExpireCache(TimeSpan.FromSeconds(6)); // if setup <= 6 the items are gone, but if I put > than 6 seconds the items remaind

            // Display the cache after expiration
            Console.WriteLine("Items after cache expiration");
            cache.Display();
            Console.ReadLine();
        }
    }
}
