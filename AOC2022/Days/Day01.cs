using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Days
{
    internal class Day01 : IDay
    {
        private string Input => Helper.GetDay(1);
        private Elf[] Elfs { get; }

        public Day01() {
            this.Elfs = this.Input.Split("\n\n").Select(x => new Elf(x)).OrderByDescending(x => x.TotalCalories).ToArray();
        }

        public void Part01()
        {
            Console.WriteLine(Elfs[0].TotalCalories);
        }

        public void Part02()
        {
            Console.WriteLine(Elfs[0..3].Sum(x => x.TotalCalories));
        }

        private class Elf
        {
            int[] Calories { get; set; }

            public int TotalCalories => Calories.Sum();

            public Elf(string caloriesInput)
            {
                var strArray = caloriesInput.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Calories = strArray.Select(x => int.Parse(x)).ToArray();
            }

        }
    }
}
