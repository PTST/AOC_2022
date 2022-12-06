using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Days
{
    internal class Day06 : IDay
    {
        private string Input => Helper.GetDay(6);
        private Device device = new Device();
        public Day06()
        {
        }

        public void Part01()
        {
            var marker = this.device.FindStartOfPackageMarker(this.Input.ToCharArray());
            Console.WriteLine($"{this.GetType().Name} - Part 1: {marker}");
        }

        public void Part02()
        {
            var marker = this.device.FindStartOfMessageMarker(this.Input.ToCharArray());
            Console.WriteLine($"{this.GetType().Name} - Part 2: {marker}");
        }

        class Device{
            public int FindStartOfPackageMarker(char[] input)
            {
                for (int i = 3; i < input.Length; i++)
                {
                    var chars = input[(i - 3)..(i+1)];
                    if (chars.Distinct().Count() == 4)
                    {
                        return i + 1;
                    }
                }
                throw new Exception("No marker found");
            }

            public int FindStartOfMessageMarker(char[] input)
            {
                for (int i = 13; i < input.Length; i++)
                {
                    var chars = input[(i - 13)..(i + 1)];
                    if (chars.Distinct().Count() == 14)
                    {
                        return i + 1;
                    }
                }
                throw new Exception("No marker found");
            }
        }
    }
}
