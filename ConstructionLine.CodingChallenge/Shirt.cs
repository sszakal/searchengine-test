﻿using System;

namespace ConstructionLine.CodingChallenge
{
    public class Shirt
    {
        public Guid Id { get; }
        
        public string Name { get; }

        public Size Size { get; }

        public Color Color { get; }

        public Shirt(Guid id, string name, Size size, Color color)
        {
            Id = id;
            Name = name;
            Size = size;
            Color = color;
        }
    }
}