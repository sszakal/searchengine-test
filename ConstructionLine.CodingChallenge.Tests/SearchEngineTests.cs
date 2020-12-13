﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void ShouldFilterByOneColorOnly()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }
        
        
        [Test]
        public void ShouldFilterByOneColorAndOneSize()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small },
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void ShouldFilterByMultiColorAndMultiSize()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "White - Large", Size.Large, Color.White),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Blue, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium },
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void ShouldReturnNoResultsIfNoShirtsAvailable()
        {
            var shirts = new List<Shirt>();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions();

            var results = searchEngine.Search(searchOptions);

            Assert.AreEqual(0, results.Shirts.Count);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void ShouldReturnAllResultsWhenNoFilter()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "White - Large", Size.Large, Color.White),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions();

            var results = searchEngine.Search(searchOptions);

            Assert.AreEqual(shirts.Count, results.Shirts.Count);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }
        
        [Test]
        public void ShouldThrowExceptionWhenNullOptionsArgument()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "White - Large", Size.Large, Color.White),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                searchEngine.Search(null);
            });
        }
    }
}
