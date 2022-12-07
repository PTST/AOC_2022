namespace AOC2022.Days
{
    internal class Day07 : IDay
    {
        private string[] Input => Helper.GetDayAsStringArray(7);

        public Day07()
        {
           
        }

        public void Part01()
        {
            var rootFolder = new Dir("/", null);
            ProcessCommands(Input, rootFolder);
            var allDirs = GetAllDirs(rootFolder);
            var allUnder100000 = allDirs.Where(x => x.Size <= 100000).ToArray();
            Console.WriteLine($"{this.GetType().Name} - Part 1: {allUnder100000.Sum(x => x.Size)}");
        }

        public void Part02()
        {
            var rootFolder = new Dir("/", null);
            ProcessCommands(Input, rootFolder);
            var allDirs = GetAllDirs(rootFolder);
            var spaceUsed = rootFolder.Size;
            var spaceAvailable = 70000000 - spaceUsed;
            var needed = 30000000 - spaceAvailable;
            var allOverneeded = allDirs.Where(x => x.Size >= needed).ToArray();
            Console.WriteLine($"{this.GetType().Name} - Part 2: {allOverneeded.Min(x => x.Size)}");
        }

        public void ProcessCommands(string[] commands, Dir root)
        {
            Dir currentDir = root;
            var latestCommand = "";
            foreach (var command in commands)
            {
                if (command.StartsWith("$"))
                {
                    if (command.Contains("ls"))
                    {
                        latestCommand = "ls";
                    }
                    else
                    {
                        latestCommand = "cd";
                        var cdArg = command.Split(" ").Last();
                        if (cdArg == "..")
                        {
                            var parent = currentDir.ParentDirectory; 
                            if (parent == null)
                            {
                                parent = currentDir;
                            }
                            currentDir = parent; 
                        }
                        else
                        {
                            if (cdArg == "/")
                            {
                                currentDir = root;
                                continue;
                            }
                            var newDir = currentDir.SubDirectories.FirstOrDefault(x => x.Name == cdArg);
                            if (newDir == null)
                            {
                                newDir = new Dir(cdArg, currentDir);
                                currentDir.SubDirectories.Add(newDir);
                            }
                            currentDir = newDir;
                        }
                    }
                    continue;
                }

                var splitted = command.Split(" ");
                if (splitted[0] == "dir")
                {
                    currentDir.SubDirectories.Add(new Dir(splitted[1], currentDir));
                }
                else
                {
                    currentDir.Files.Add(new File(long.Parse(splitted[0]), splitted[1]));
                }
            }
        }

        public List<Dir> GetAllDirs(Dir root)
        {
            var output = new List<Dir>() { root };
            foreach (var dir in root.SubDirectories)
            {
                output.Add(dir);
                output.AddRange(GetAllDirs(dir));
            }
            return output.ToHashSet().ToList();
        }

        public class Dir
        {
            public Dir? ParentDirectory { get; set; }
            public List<Dir> SubDirectories { get; set; } = new();
            public List<File> Files { get; set; } = new();
            public string Name { get; set; }

            public long Size => this.Files.Sum(x => x.Size)+this.SubDirectories.Sum(x => x.Size);
            public Dir(string name, Dir? parent)
            {
                this.Name = name;
                this.ParentDirectory = parent;
            }

            public override string ToString()
            {
                return $"{this.Name} (dir)";
            }
        }

        public class File
        {
            public long Size { get; set; }
            public string Name { get; set; }

            public File(long size, string name)
            {
                this.Size = size;
                this.Name = name;
            }

            public override string ToString()
            {
                return $"{this.Name} (file, size={this.Size})";
            }
        }
    }
}