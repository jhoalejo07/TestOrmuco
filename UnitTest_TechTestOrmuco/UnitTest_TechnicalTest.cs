﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LRUCache;
using VersionComparer;
using CheckOverlap;
/******************************************************************
* Jhohan Arias
* 06/11/2023
* Technical test Ormuco
*******************************************************************/

namespace UnitTest_TechTestOrmuco
{


    [TestClass]
    public class CheckVersionsTests
    {
        CheckVersions vs = new CheckVersions();
        [TestMethod]
        public void TestVersions_GreaterVersion()
        {

            string version1 = "2.5";
            string version2 = "2.0.5";
            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("is greater than"));
        }

        [TestMethod]
        public void TestVersions_LowerVersion()
        {
            string version1 = "1.1";
            string version2 = "1.1.3";
            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("is lower than"));
        }

        [TestMethod]
        public void TestVersions_EqualVersions()
        {
            string version1 = "1.0.5";
            string version2 = "1.0.5";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("are equal"));
        }


        [TestMethod]
        public void TestVersions_Null_srt1()
        {
            string version1 = null;
            string version2 = "1.0";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Value cannot be null.", srt);
        }

        [TestMethod]
        public void TestVersions_Null_srt2()
        {
            string version1 = "1.0";
            string version2 = null;

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Value cannot be null.", srt);
        }

        [TestMethod]
        public void TestVersions_Null_srt1_srt2()
        {
            string version1 = null;
            string version2 = null;

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Value cannot be null.", srt);
        }

        [TestMethod]
        public void TestVersions_InvalidInput_Srt1()
        {
            string version1 = "1.0.";
            string version2 = "1.0";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Input string was not in a correct format.", srt);
        }

        [TestMethod]
        public void TestVersions_InvalidInput_Srt2()
        {
            string version1 = "1.0.5";
            string version2 = "1.0.";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Input string was not in a correct format.", srt);
        }

        [TestMethod]
        public void TestVersions_InvalidInput_Srt1_Null_srt2()
        {
            string version1 = "1.0.";
            string version2 = null;

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Value cannot be null.", srt);
        }

        [TestMethod]
        public void TestVersions_InvalidInput_Srt2_Null_srt1()
        {
            string version1 = null;
            string version2 = "1.0.";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Value cannot be null.", srt);
        }

        [TestMethod]
        public void TestVersions_letter_Srt1()
        {
            string version1 = "1.w.5";
            string version2 = "1.0";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("is greater than"));

        }

        [TestMethod]
        public void TestVersions_letter_Srt2()
        {
            string version1 = "1.0";
            string version2 = "1.w.5";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("is lower than"));
        }

        [TestMethod]
        public void TestVersions_letter_Srt1_Null_srt2()
        {
            string version1 = "1.w.5";
            string version2 = null;

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Value cannot be null.", srt);
        }

        [TestMethod]
        public void TestVersions_letter_Srt2_Null_srt1()
        {
            string version1 = null;
            string version2 = "1.w.5";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.AreEqual("Value cannot be null.", srt);
        }

    }

    [TestClass]
     public class LRUCacheTests
        {
            LRU cache = new LRU(3,2);
            [TestMethod]
            public void TestLRUCache_AddItem()
            {
                cache.AddItem(1);
                CollectionAssert.AreEqual(new[] { 1 }, cache.GetItems()); // compare if the item was store
            }
       

            [TestMethod]
            public void TestLRUCache_AddItem_MaxCapacity()
            {
                cache.AddItem(1);
                cache.AddItem(2);
                cache.AddItem(3);
                CollectionAssert.AreEqual(new[] {3, 2, 1 }, cache.GetItems());// The maximun was define in 3 item, it should let enter three items
            }

            [TestMethod]
            public void TestLRUCache_AddItem_ExistedNum()
            {
                cache.AddItem(5);
                cache.AddItem(1);
                cache.AddItem(3);
                cache.AddItem(5);
                CollectionAssert.AreEqual(new[] { 5, 3, 1 }, cache.GetItems());//if an item exist it should be put in the beginning
        }

            [TestMethod]
            public void TestLRUCache_AddItem_Over_MaxCapacity()
            {
                cache.AddItem(1);
                cache.AddItem(2);
                cache.AddItem(3);
                cache.AddItem(4);
                CollectionAssert.AreEqual(new[] { 4, 3, 2 }, cache.GetItems()); //if it reaches the maximum it should remove the last item and place the new one in the beginning
            }
       

            [TestMethod]
            public void TestLRUCache_ExpireCacheOverLimit()
            {
                cache.AddItem(3);
                System.Threading.Thread.Sleep(2100); // Wait 2.1 secs for items expire
              // cache.ExpireCache(TimeSpan.FromSeconds(6));
                Assert.AreEqual(0, cache.GetItems().Count);// After that time, there shouldn't be items
            }

            [TestMethod]
            public void TestLRUCache_ExpireCacheLimit()
            {
                cache.AddItem(3);
                System.Threading.Thread.Sleep(2000); // Wait 2 secs for items expire, in the expire limit it shouldn´t remove the item
               // cache.ExpireCache(TimeSpan.FromSeconds(6));
                Assert.AreEqual(1, cache.GetItems().Count); // in exactly two seconds the element should still be there
            }

            [TestMethod]
            public void TestLRUCache_NO_ExpireCache()
            {
                cache.AddItem(3);
                cache.AddItem(2);
                cache.AddItem(1);
                System.Threading.Thread.Sleep(1800); // In 1.8 second the items won't expire
               // cache.ExpireCache(TimeSpan.FromSeconds(6));
                CollectionAssert.AreEqual(new[] { 1, 2, 3 }, cache.GetItems());
            }

            [TestMethod]
            public void TestLRUCache_ExpireCache()
            {
                cache.AddItem(3);
                cache.AddItem(2);
                cache.AddItem(1);
                System.Threading.Thread.Sleep(2100); // In 2.1 second the items should expire
                CollectionAssert.AreNotEqual(new[] { 1, 2, 3 }, cache.GetItems());
            }
    }

    [TestClass]
    public class OverlapTests
    {
        Overlap overlap = new Overlap();
       
        [TestMethod]
        public void TestOverlap_OverlapCase()
        {
            int[] line1 = { 3, 5 };
            int[] line2 = { 2, 6 };
   
            string result = overlap.CheckOverlap(line1, line2);

            Assert.IsTrue(result.Contains("overlaps"));
        }

        [TestMethod]
        public void TestOverlap_NoOverlapCase()
        {
            int[] line3 = { 1, 5 };
            int[] line4 = { -3, 0 };
            
            string result = overlap.CheckOverlap(line3, line4);

            Assert.IsTrue(result.Contains("does NOT overlap"));
        }

        [TestMethod]
        public void TestOverlap_NullLine1()
        {
            int[] line1 = null;
            int[] line2 = { 2, 6 };
            string result = overlap.CheckOverlap(line1, line2);

            Assert.IsTrue(result.Contains("One line is null"));
        }


    [TestMethod]
        public void TestOverlap_NullLine2()
        {
            int[] line1 = { 3, 5 };
            int[] line2 = null;
            string result = overlap.CheckOverlap(line1, line2);

            Assert.IsTrue(result.Contains("One line is null"));
        }

        [TestMethod]
        public void TestOverlap_NoAccurateLenght()
        {
            int[] line2 = { 3, 5, 8 };
            int[] line1 = { 2, int.MaxValue };
            string result = overlap.CheckOverlap(line1, line2);

            Assert.IsTrue(result.Contains("has no accurate length"));
        }

    }




}







