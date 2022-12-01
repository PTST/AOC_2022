using AOC2022.Days;
using System.Net.Http.Headers;

namespace AOC2022
{
    internal class Program
    {
        static IDay[] Days = new IDay[] { new Day01(), new Day02(), new Day03(), new Day04(), new Day05(), new Day06(), new Day07(), new Day08(), new Day09(), new Day10(), new Day11(), new Day12(), new Day13(), new Day14(), new Day15(), new Day16(), new Day17(), new Day18(), new Day19(), new Day20(), new Day21(), new Day22(), new Day23(), new Day24(), new Day25() };

        static void Main(string[] args)
        {
            var todaysDay = DateTime.Now.Day;
            Days[todaysDay - 1].Part01();
            Days[todaysDay - 1].Part02();
        }
    }
}