using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Days
{
    internal class Day05 : IDay
    {
        private string[] Input => Helper.GetDayAsStringArray(5);

        public Day05()
        {
        }

        public void Part01()
        {
            var crane = new Crane(this.Input);
            var output = crane.Part01();
            Console.WriteLine($"{this.GetType().Name} - Part 1: {output}");
        }

        public void Part02()
        {
            var crane = new Crane(this.Input);
            var output = crane.Part02();
            Console.WriteLine($"{this.GetType().Name} - Part 2: {output}");
        }

        class Crane {

            private System.Text.RegularExpressions.Regex MoveRegEx = new System.Text.RegularExpressions.Regex(@"move (\d+) from (\d+) to (\d+)");

            Dictionary<int, Stack<Crate>> Map = new();

            public string[] Instructions { get; set; }

            public Crane(string[] input)
            {
                var stackLabels = input.First(x => x.Trim()[0] == '1');
                foreach (var label in stackLabels.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    Map[int.Parse(label)] = new();
                }
                var rowIdx = Array.IndexOf(input, stackLabels);
                for (int i = rowIdx - 1; i >= 0; i--)
                {
                    var line = input[i];
                    for (int n = 0; n < this.Map.Keys.Count; n++)
                    {
                        var crateName = line.Substring((n) * 4, 3).Trim();
                        if (string.IsNullOrEmpty(crateName))
                        {
                            continue;
                        }
                        var crate = new Crate(crateName);
                        this.Map[n + 1].Push(crate);
                    }
                }
                this.Instructions = input.Skip(rowIdx + 1).ToArray();
            }

            public string Part01()
            {
                foreach (var item in Instructions)
                {
                    var match = this.MoveRegEx.Match(item);
                    var amount = int.Parse(match.Groups[1].Value);
                    var from = int.Parse(match.Groups[2].Value);
                    var to = int.Parse(match.Groups[3].Value);
                    for (int i = 0; i < amount; i++)
                    {
                        this.Map[to].Push(this.Map[from].Pop());
                    }
                }
                var sb = new StringBuilder();
                foreach (var kv in this.Map)
                {
                    sb.Append(kv.Value.First().Label);
                }
                return sb.ToString();
            }

            public string Part02()
            {
                foreach (var item in Instructions)
                {
                    var match = this.MoveRegEx.Match(item);
                    var amount = int.Parse(match.Groups[1].Value);
                    var from = int.Parse(match.Groups[2].Value);
                    var to = int.Parse(match.Groups[3].Value);
                    var taken = new List<Crate>();
                    for (int i = 0; i < amount; i++)
                    {
                        taken.Add(this.Map[from].Pop());
                    }
                    taken.Reverse();
                    foreach (var crate in taken)
                    {
                        this.Map[to].Push(crate);
                    }
                }
                var sb = new StringBuilder();
                foreach (var kv in this.Map)
                {
                    sb.Append(kv.Value.First().Label);
                }
                return sb.ToString();
            }


        }

        class Crate
        {
            public string Label { get; set; }
            public Crate(string input)
            {
                this.Label = input.Replace("[", string.Empty).Replace("]", string.Empty);
            }
        }
    }
}
