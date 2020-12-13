using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }
        
        public SearchResults Search(SearchOptions options)
        {
            if (options == null)
                throw new ArgumentNullException($"'{nameof(options)}' cannot be null");
            
            var sizeFilter = options.Sizes?.Select(s => s.Id).ToHashSet() ?? new HashSet<Guid>();
            var colorFilter = options.Colors?.Select(c => c.Id).ToHashSet() ?? new HashSet<Guid>();
            
            var filteredShirts = _shirts.AsParallel()
                .Where(shirt => (!sizeFilter.Any() || sizeFilter.Contains(shirt.Size.Id))
                    && (!colorFilter.Any() || colorFilter.Contains(shirt.Color.Id)))
                .ToList();

            var colorReport = Task.Run(() => Report(filteredShirts, s => s.Color, Color.All)
                .Select(x => new ColorCount { Color = x.Item1, Count = x.Item2 }).ToList());
            var sizeReport = Task.Run(() => Report(filteredShirts, s => s.Size, Size.All)
                .Select(x => new SizeCount { Size = x.Item1, Count = x.Item2 }).ToList());

            Task.WaitAll(colorReport, sizeReport);
            
            return new SearchResults
            {
                Shirts = filteredShirts,
                ColorCounts = colorReport.Result,
                SizeCounts = sizeReport.Result
            };
        }

        private IEnumerable<(TM, int)> Report<T, TM>(IEnumerable<T> items, Func<T, TM> member, IEnumerable<TM> allPossibleValues)
        {
            var groupByMember = items
                .GroupBy(member)
                .ToDictionary(g => g.Key, g => g.Count());
            var report = groupByMember
                .Select(g => (g.Key, g.Value))
                .ToList();
            allPossibleValues.Where(x => !groupByMember.ContainsKey(x))
                .ToList()
                .ForEach(x => report.Add((x, 0)));

            return report;
        }
    }
}