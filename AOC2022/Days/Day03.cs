using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Days
{
    internal class Day03 : IDay
    {
        private string[] Input => Helper.GetDayAsStringArray(3);
        private char[] Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public Day03()
        {
        }

        public void Part01()
        {
            var totalPrioroty = 0;
            var rugsacks = Input.Select(x => x.ToCharArray()).Select(x => (x.Take(x.Length / 2).ToHashSet(), x.Skip(x.Length / 2).ToHashSet())).ToArray();
            foreach (var rugsack in rugsacks)
            {
                var overlaps = rugsack.Item1.Intersect(rugsack.Item2);
                var priority = overlaps.Select(c => Array.IndexOf(Alphabet, c) + 1).Sum();
                totalPrioroty += priority;
            }
            Console.WriteLine($"{this.GetType().Name} - Part 1: {totalPrioroty}");
        }

        public void Part02()
        {
            var totalPrioroty = 0;
            var rugsacks = Input.Select(x => x.ToCharArray()).ToArray();
            for (int i = 0; i < rugsacks.Length; i+=3)
            {
                var rugsack1 = rugsacks[i];
                var rugsack2 = rugsacks[i+1];
                var rugsack3 = rugsacks[i+2];

                var badge = rugsack1.Intersect(rugsack2).Intersect(rugsack3).First();
                var priority = Array.IndexOf(Alphabet, badge) + 1;
                totalPrioroty += priority;
            }
            Console.WriteLine($"{this.GetType().Name} - Part 2: {totalPrioroty}");
        }
    }
}
