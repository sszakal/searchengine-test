using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge.Tests.SampleData
{
    public class SampleDataBuilder
    {
        private readonly int _numberOfShirts;
        private readonly Size[] _sizes;
        private readonly Color[] _colors;

        private readonly Random _random = new Random();

        
        public SampleDataBuilder(int numberOfShirts)
        {
            _numberOfShirts = numberOfShirts;
            _sizes = Size.All.ToArray();
            _colors = Color.All.ToArray();
        }

        public List<Shirt> CreateShirts()
        {
            return Enumerable.Range(0, _numberOfShirts)
                .Select(i => new Shirt(Guid.NewGuid(), $"Shirt {i}", GetRandomSize(), GetRandomColor()))
                .ToList();
        }

       
        private Size GetRandomSize()
        {
            var index = _random.Next(0, _sizes.Length);
            return _sizes[index];
        }


        private Color GetRandomColor()
        {
            var index = _random.Next(0, _colors.Length);
            return _colors.ElementAt(index);
        }
    }
}