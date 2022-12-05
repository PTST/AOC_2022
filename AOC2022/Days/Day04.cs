using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Days
{
    internal class Day04 : IDay
    {
        private string[] Input => Helper.GetDayAsStringArray(4);
        private List<(HashSet<int>, HashSet<int>)> Ranges { get; set; } = new();

        public Day04()
        {
            foreach (var item in this.Input)
            {
                var rnges = item.Split(",");
                var rng1 = rnges[0].Split("-").Select(x => int.Parse(x)).ToArray();
                var rng2 = rnges[1].Split("-").Select(x => int.Parse(x)).ToArray();
                this.Ranges.Add((Enumerable.Range(rng1[0], rng1[1] - rng1[0]+1).ToHashSet(), Enumerable.Range(rng2[0], rng2[1] - rng2[0]+1).ToHashSet()));
            }
        }

        public void Part01()
        {
            var subsets = this.Ranges.Count(x => x.Item1.IsSubsetOf(x.Item2) || x.Item2.IsSubsetOf(x.Item1));
            Console.WriteLine($"{this.GetType().Name} - Part 1: {subsets}");
        }

        public void Part02()
        {
            var subsets = this.Ranges.Count(x => x.Item1.Any(y => x.Item2.Contains(y)) || x.Item2.Any(y => x.Item1.Contains(y)));
            Console.WriteLine($"{this.GetType().Name} - Part 2: {subsets}");
        }
    }
}
