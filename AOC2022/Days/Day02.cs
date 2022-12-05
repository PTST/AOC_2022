using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Days
{
    internal class Day02 : IDay
    {
        private string[] Input => Helper.GetDayAsStringArray(2);
        List<(RockPaperScissor, RockPaperScissor)> RockPaperScissorsPart1 { get; set; }
        List<(RockPaperScissor, RockPaperScissor)> RockPaperScissorsPart2 { get; set; }


        public Day02()
        {
            this.RockPaperScissorsPart1 = Input.Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)).Select(x => (Convert(x[1]), Convert(x[0]))).ToList();
            this.RockPaperScissorsPart2 = Input.Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)).Select(x => Convert2(x[0], x[1])).ToList();

        }

        public void Part01()
        {
            Console.WriteLine($"{this.GetType().Name} - Part 1: {RockPaperScissorsPart1.Sum(x => this.Fight(x.Item1, x.Item2))}");
        }

        public void Part02()
        {
            Console.WriteLine($"{this.GetType().Name} - Part 2: {RockPaperScissorsPart2.Sum(x => this.Fight(x.Item2, x.Item1))}");
        }

        private int Fight(RockPaperScissor me, RockPaperScissor them)
        {
            if (me == them)
            {
                return 3 + (int)me;
            }
            if (me == RockPaperScissor.Rock && them == RockPaperScissor.Scissor)
            {
                return 6 + (int)me;
            }
            if (me == RockPaperScissor.Paper && them == RockPaperScissor.Rock)
            {
                return 6 + (int)me;
            }
            if (me == RockPaperScissor.Scissor && them == RockPaperScissor.Paper)
            {
                return 6 + (int)me;
            }
            return 0 + (int)me;
        }

        private RockPaperScissor Convert(string x)
        {
            switch (x.ToLower())
            {
                case "a":
                case "x":
                    return RockPaperScissor.Rock;
                case "b":
                case "y":
                    return RockPaperScissor.Paper;
                case "c":
                case "z":
                    return RockPaperScissor.Scissor;
                default:
                    throw new Exception("Unknown type");
            }
        }


        private (RockPaperScissor, RockPaperScissor) Convert2(string them, string type)
        {
            RockPaperScissor themSelected;
            switch (them.ToLower())
            {
                case "a":
                    themSelected = RockPaperScissor.Rock;
                    break;
                case "b":
                    themSelected = RockPaperScissor.Paper;
                    break;
                case "c":
                    themSelected = RockPaperScissor.Scissor;
                    break;
                default:
                    throw new Exception("Unknown type");
            }
            if (type == "Y")
            {
                return (themSelected, themSelected);
            }
            if (themSelected == RockPaperScissor.Rock)
            {
                if (type == "X")
                {
                    return (themSelected, RockPaperScissor.Scissor);
                }
                else
                {
                    return (themSelected, RockPaperScissor.Paper);
                }
            }

            if (themSelected == RockPaperScissor.Paper)
            {
                if (type == "X")
                {
                    return (themSelected, RockPaperScissor.Rock);
                }
                else
                {
                    return (themSelected, RockPaperScissor.Scissor);
                }
            }

            
                if (type == "X")
                {
                    return (themSelected, RockPaperScissor.Paper);
                }
                else
                {
                    return (themSelected, RockPaperScissor.Rock);
                }
            
        }


        enum RockPaperScissor
        {
            Rock = 1,
            Paper = 2,
            Scissor = 3
        }
    }
}
