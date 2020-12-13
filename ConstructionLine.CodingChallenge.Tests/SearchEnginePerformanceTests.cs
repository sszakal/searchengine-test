using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConstructionLine.CodingChallenge.Tests.SampleData;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private SearchEngine _searchEngine;

        [SetUp]
        public void Setup()
        {
            var dataBuilder = new SampleDataBuilder(50000);

            _shirts = dataBuilder.CreateShirts();

            _searchEngine = new SearchEngine(_shirts);
        }
        
        [Test]
        public void NoFilter()
        {
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions();

            var results = _searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            Assert.AreEqual(results.Shirts.Count, _shirts.Count);
        }

        [Test]
        public void SearchByColor()
        {
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = _searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            AssertResults(results.Shirts, options);
            AssertSizeCounts(results.Shirts, options, results.SizeCounts);
            AssertColorCounts(results.Shirts, options, results.ColorCounts);
        }
        
        [Test]
        public void SearchBySize()
        {
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Sizes = new List<Size> { Size.Large }
            };

            var results = _searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            AssertResults(results.Shirts, options);
            AssertSizeCounts(results.Shirts, options, results.SizeCounts);
            AssertColorCounts(results.Shirts, options, results.ColorCounts);
        }
        
        [Test]
        public void SearchByColorAndSize()
        {
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.White, Color.Yellow },
                Sizes = new List<Size> { Size.Small, Size.Large },
            };

            var results = _searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            AssertResults(results.Shirts, options);
            AssertSizeCounts(results.Shirts, options, results.SizeCounts);
            AssertColorCounts(results.Shirts, options, results.ColorCounts);
        }
    }
}
