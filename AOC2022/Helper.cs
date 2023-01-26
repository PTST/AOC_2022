using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022
{
    internal static class Helper
    {
        public static string GetDay(int day, int year = 2022)
        {
            
            if (!Directory.Exists("Inputs"))
            {
                Directory.CreateDirectory("Inputs");
            }
            var filepath = Path.Combine("Inputs", $"{day}.txt");
            if (File.Exists(filepath))
            {
                return File.ReadAllText(filepath);
            }

            var date = new DateTime(year, 12, day, 6, 0, 0);
            if (DateTime.Now < date)
            {
                Console.WriteLine($"Puzzle not yet available for {date:dd}");
                return string.Empty;
            }
            var AOC_TOKEN = Environment.GetEnvironmentVariable("AOC_TOKEN");
            if (string.IsNullOrEmpty(AOC_TOKEN))
            {
                throw new Exception("No token found");
            }
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://adventofcode.com/{year}/day/{day}/input"),
                Headers ={
                    { "User-Agent", "personal downloader for patrick%40steffensen.io" },
                    { "Cookie", $"session={AOC_TOKEN}" },
                },
            };
            using (var response = client.SendAsync(request).Result)
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                File.WriteAllText(filepath, body);
                return body;
            }
        }

        public static string[] GetDayAsStringArray(int day, char[]? splitOn = null)
        {
            splitOn ??= Environment.NewLine.ToCharArray();
            var input = GetDay(day);
            var data = input.Split(splitOn, StringSplitOptions.RemoveEmptyEntries);
            return data;
        }

        public static long[] GetDayAsLongArray(int day, char[]? splitOn = null)
        {
            return GetDayAsStringArray(day, splitOn).Select(x => long.Parse(x)).ToArray();
        }

        public static int[] GetDayAsIntArray(int day, char[]? splitOn = null)
        {
            return GetDayAsStringArray(day, splitOn).Select(x => int.Parse(x)).ToArray();
        }
    }
}
