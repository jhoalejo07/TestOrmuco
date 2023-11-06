using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.IsTrue(srt.Contains("null version"));
        }

        [TestMethod]
        public void TestVersions_Null_srt2()
        {
            string version1 = "1.0";
            string version2 = null;

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("null version"));
        }

        [TestMethod]
        public void TestVersions_Null_srt1_srt2()
        {
            string version1 = null;
            string version2 = null;

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("null version"));
        }

        [TestMethod]
        public void TestVersions_InvalidInput_Srt1()
        {
            string version1 = "1.0.";
            string version2 = "1.2";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("Invalid Input"));
        }

        [TestMethod]
        public void TestVersions_InvalidInput_Srt2()
        {
            string version1 = "1.2";
            string version2 = "1.0.";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("Invalid Input"));
        }

        [TestMethod]
        public void TestVersions_InvalidInput_Srt1_Null_srt2()
        {
            string version1 = "1.0.";
            string version2 = null;

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("null version"));
        }

        [TestMethod]
        public void TestVersions_InvalidInput_Srt2_Null_srt1()
        {
            string version1 = null;
            string version2 = "1.0.";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("null version"));
        }

        [TestMethod]
        public void TestVersions_InvalidInput_letter_Srt1()
        {
            string version1 = "1.w.5";
            string version2 = "1.0";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("Invalid Input"));
        }

        [TestMethod]
        public void TestVersions_InvalidInput_letter_Srt2()
        {
            string version1 = "1.0";
            string version2 = "1.w.5";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("Invalid Input"));
        }

        [TestMethod]
        public void TestVersions_InvalidInput_letter_Srt1_Null_srt2()
        {
            string version1 = "1.w.5";
            string version2 = null;

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("null version"));
        }

        [TestMethod]
        public void TestVersions_InvalidInput_letter_Srt2_Null_srt1()
        {
            string version1 = null;
            string version2 = "1.w.5";

            string srt = vs.BiggerVersion(version1, version2);
            Assert.IsTrue(srt.Contains("null version"));
        }

    }

    [TestClass]
     public class LRUCacheTests
        {
            LRU cache = new LRU(3);
            [TestMethod]
            public void TestLRUCache_AddItem()
            {
                cache.AddItem(1);
                CollectionAssert.AreEqual(new[] { 1 }, cache.GetItems());
            }
       

            [TestMethod]
            public void TestLRUCache_AddItem_MaxCapacity()
            {
                cache.AddItem(1);
                cache.AddItem(2);
                cache.AddItem(3);
                CollectionAssert.AreEqual(new[] {3, 2, 1 }, cache.GetItems());
            }

            [TestMethod]
            public void TestLRUCache_AddItem_ExistedNum()
            {
                cache.AddItem(5);
                cache.AddItem(1);
                cache.AddItem(3);
                cache.AddItem(5);
                CollectionAssert.AreEqual(new[] { 5, 3, 1 }, cache.GetItems());
            }

            [TestMethod]
            public void TestLRUCache_AddItem_Over_MaxCapacity()
            {
                cache.AddItem(1);
                cache.AddItem(2);
                cache.AddItem(3);
                cache.AddItem(4);
                CollectionAssert.AreEqual(new[] { 4, 3, 2 }, cache.GetItems());
            }
       

            [TestMethod]
            public void TestLRUCache_ExpireCacheOverLimit()
            {
                cache.AddItem(3);
                System.Threading.Thread.Sleep(6100); // Wait for items to expire
                cache.ExpireCache(TimeSpan.FromSeconds(6));
                Assert.AreEqual(0, cache.GetItems().Count);
            }

            [TestMethod]
            public void TestLRUCache_ExpireCacheLimit()
            {
                cache.AddItem(3);
                System.Threading.Thread.Sleep(6000); // Wait for items to expire
                cache.ExpireCache(TimeSpan.FromSeconds(6));
                Assert.AreEqual(0, cache.GetItems().Count);
            }

            [TestMethod]
            public void TestLRUCache_NO_ExpireCache()
            {
                cache.AddItem(3);
                cache.AddItem(2);
                cache.AddItem(1);
                System.Threading.Thread.Sleep(5000); // Wait for items to expire
                cache.ExpireCache(TimeSpan.FromSeconds(6));
               // Assert.AreNotEqual(0, cache.GetItems().Count);
                CollectionAssert.AreEqual(new[] { 1, 2, 3 }, cache.GetItems());
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

            Assert.IsTrue(result.Contains("There is a null line"));
        }

        [TestMethod]
        public void TestOverlap_NullLine2()
        {
            int[] line1 = { 3, 5 };
            int[] line2 = null;
            string result = overlap.CheckOverlap(line1, line2);

            Assert.IsTrue(result.Contains("There is a null line"));
        }


    }




}







