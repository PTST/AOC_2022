using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Days
{
    internal class Day08 : IDay
    {
        private string[] Input => Helper.GetDayAsStringArray(8);

        public Day08()
        {
        }

        public void Part01()
        {
            
            var forrest = new Forrest(this.Input);
            var VisibleCount = forrest.CountVisibleTrees();

            Console.WriteLine($"{this.GetType().Name} - Part 1: {VisibleCount}");
        }

        public void Part02()
        {
            var forrest = new Forrest(this.Input);
            forrest.CreateScenicMap();
            Console.WriteLine($"{this.GetType().Name} - Part 2: {forrest.ScenicMap.SelectMany(x => x).Max()}");
        }

        public class Forrest
        {
            public int[][] Map { get; set; }

            public bool[][] VisibilityMap { get; set; }

            public int[][] ScenicMap { get; set; }

            public int Width { get; set; }
            public int Height { get; set; }

            public Forrest(string[] input)
            {
                this.Map = input.Select(x => x.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
                this.Height = this.Map.Length;
                this.Width = this.Map[0].Length;
                this.VisibilityMap = new bool[this.Height][];
                this.ScenicMap = new int[this.Height][];

                for (int y = 0; y < this.Height; y++)
                {
                    this.VisibilityMap[y] = new bool[this.Width];
                    this.ScenicMap[y] = new int[this.Width];
                }
            }

            public void SaveToFile()
            {
                var sb = new StringBuilder();
                foreach (var row in VisibilityMap)
                {
                    var innerSb = new StringBuilder();
                    foreach (var col in row)
                    {
                        innerSb.Append(col ? 1: 0);
                    }
                    sb.AppendLine(innerSb.ToString());
                }
                File.WriteAllText("map.txt", sb.ToString());
            }

            public int CountVisibleTrees()
            {
                var counter = 0;
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        var visible = this.IsTreeVisible(x, y);
                        this.VisibilityMap[y][x] = visible;
                        if (visible)
                        {
                            counter++;
                        }
                    }
                }
                this.SaveToFile();
                return counter;
            }

            public bool IsTreeVisible(int x, int y)
            {
                if (x == 0 || x == this.Width - 1 || y == 0 || y == this.Height - 1)
                {
                    return true;
                }

                var treeHeight = this.Map[y][x];

                var treesAbove = Enumerable.Range(0, y).Select(p => this.Map[p][x]).ToArray();
                var treesBelov = Enumerable.Range(y + 1, this.Height - y - 1).Select(p => this.Map[p][x]).ToArray();

                var treesToTheLeft = Enumerable.Range(0, x).Select(p => this.Map[y][p]).ToArray();
                var treesToTheRight = Enumerable.Range(x + 1, this.Height - x - 1).Select(p => this.Map[y][p]).ToArray();
                var output = treesAbove.All(t => t < treeHeight) ||
                            treesBelov.All(t => t < treeHeight) ||
                            treesToTheLeft.All(t => t < treeHeight) ||
                            treesToTheRight.All(t => t < treeHeight);
                return output;

            }

            public void CreateScenicMap()
            {
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        var treesAbove = Enumerable.Range(0, y).Select(p => this.Map[p][x]).Reverse().ToArray();
                        var treesBelow = Enumerable.Range(y + 1, this.Height - y - 1).Select(p => this.Map[p][x]).ToArray();
                        var treesToTheLeft = Enumerable.Range(0, x).Select(p => this.Map[y][p]).Reverse().ToArray();
                        var treesToTheRight = Enumerable.Range(x + 1, this.Height - x - 1).Select(p => this.Map[y][p]).ToArray();

                        var left = treesToTheLeft.Length;
                        var right = treesToTheRight.Length;
                        var above = treesAbove.Length;
                        var below = treesBelow.Length;

                        var treeHeight = this.Map[y][x];

                        for (int i = 0; i < treesAbove.Length; i++)
                        {
                            var tree = treesAbove[i];
                            if (tree >= treeHeight)
                            {
                                above = i+1;
                                break;
                            }
                            if (treesAbove.Take(i).Any(t => t >= treeHeight))
                            {
                                above = i;
                                break;
                            }
                        }

                        for (int i = 0; i < treesBelow.Length; i++)
                        {
                            var tree = treesBelow[i];
                            if (tree >= treeHeight)
                            {
                                below = i + 1;
                                break;
                            }
                            if (treesBelow.Take(i).Any(t => t >= treeHeight))
                            {
                                below = i;
                                break;
                            }
                        }

                        for (int i = 0; i < treesToTheLeft.Length; i++)
                        {
                            var tree = treesToTheLeft[i];
                            if (tree >= treeHeight)
                            {
                                left = i + 1;
                                break;
                            }
                            if (treesToTheLeft.Take(i).Any(t => t >= treeHeight))
                            {
                                left = i;
                                break;
                            }
                        }

                        for (int i = 0; i < treesToTheRight.Length; i++)
                        {
                            var tree = treesToTheRight[i];
                            if (tree >= treeHeight)
                            {
                                right = i + 1;
                                break;
                            }
                            if (treesToTheRight.Take(i).Any(t => t >= treeHeight))
                            {
                                right = i;
                                break;
                            }
                        }

                        this.ScenicMap[y][x] = above * left * right * below;
                    }
                }
            }
        }
    }
}
