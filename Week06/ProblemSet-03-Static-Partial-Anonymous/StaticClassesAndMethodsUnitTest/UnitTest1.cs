using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using StaticClassesAndMethodsLibrary;

namespace StaticClassesAndMethodsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IntersectWithoutSameElements()
        {
            List<int> nums1 = new List<int>() { 1, 2, 3, 4 };
            List<int> nums2 = new List<int>() { 5, 6, 7, 8, 9 };

            nums1 = nums1.Intersect(nums2);

            Assert.IsTrue(nums1.Count == 0);
        }

        [TestMethod]
        public void IntersectSomeSameElements()
        {
            List<int> nums1 = new List<int>() { 1, 5, 3, 8, 10, 11 };
            List<int> nums2 = new List<int>() { 5, 6, 7, 8, 11 };

            var intersected = new List<int>() { 5, 8, 11 };
            nums1 = nums1.Intersect(nums2);
            bool allIntersected = true;
            foreach (var item in nums1)
            {
                if (!intersected.Contains(item)) allIntersected = false;
            }
            foreach (var item in intersected)
            {
                if (!nums1.Contains(item)) allIntersected = false;
            }

            Assert.IsTrue(allIntersected);
        }

        [TestMethod]
        public void IntersectAllSameElements()
        {
            List<int> nums1 = new List<int>() { 1, 5, 3, 8, 10, 11 };
            List<int> nums2 = new List<int>() { 1, 5, 3, 8, 10, 11 };

            nums1 = nums1.Intersect(nums2);
            bool allIntersected = true;
            foreach (var item in nums1)
            {
                if (!nums2.Contains(item)) allIntersected = false;
            }
            foreach (var item in nums2)
            {
                if (!nums1.Contains(item)) allIntersected = false;
            }

            Assert.IsTrue(allIntersected);
        }

        [TestMethod]
        public void UnionAllTest()
        {
            List<int> nums1 = new List<int>() { 1, 5, 3, 8, 10, 11 };
            List<int> nums2 = new List<int>() { 5, 6, 7, 8, 11 };
            List<int> union = ArrayExtension.UnionAll(nums1, nums2);
            bool allIntersected = true;
            foreach (var item in union)
            {
                if (!(nums1.Contains(item) || nums2.Contains(item))) allIntersected = false;
            }
            Assert.IsTrue(allIntersected && union.Count == nums1.Count + nums2.Count);
        }

        [TestMethod]
        public void UnionTest()
        {
            List<int> nums1 = new List<int>() { 1, 5, 3, 8, 10, 11 };
            List<int> nums2 = new List<int>() { 5, 6, 7, 8, 11 };
            List<int> union = ArrayExtension.Union(nums1, nums2);
            bool allIntersected = true;
            foreach (var item in union)
            {
                if (!(nums1.Contains(item) || nums2.Contains(item))) allIntersected = false;
            }
            Assert.IsTrue(allIntersected && union.Count == nums1.Count + nums2.Count - nums1.Intersect(nums2).Count);
        }

        [TestMethod]
        public void JoinTest()
        {
            List<string> words = new List<string>() { "asd", "sd", "ewds", "afsdf", "asd" };
            string joined = ArrayExtension.Join(words);
            bool allWordsFound = true;
            int sumWordsLenght = 0;
            foreach (var word in words)
            {
                if (!words.Contains(word)) allWordsFound = false;
                sumWordsLenght += word.Length;
            }
            Assert.IsTrue(allWordsFound && sumWordsLenght + words.Count - 1 == joined.Length);
        }
    }
}
