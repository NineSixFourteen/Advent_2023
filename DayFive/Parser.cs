public class Parser{

    public static (List<uint>,Finder) parse(string FileName){
        int place = 0;
        Finder finder = new Finder();
        List<uint> seeds = new List<uint>();
        try{
            StreamReader sr = new StreamReader(FileName);
            string? line = sr.ReadLine();
            seeds = parseSeeds(line);
            line = sr.ReadLine();
            line = sr.ReadLine();
            line = sr.ReadLine();
            List<Range> ranges = new List<Range>();
            while(line != null){
                if(line.Length != 0){
                    ranges.Add(parseRange(line));
                }else {
                    finder.ranges.Add(ranges);
                    place++;
                    ranges = new List<Range>();
                    line = sr.ReadLine();
                }
                line = sr.ReadLine();
            }
            finder.ranges.Add(ranges);
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return(seeds, finder);
    }

    private static Range parseRange(string line){
        string[] parts = line.Split(" ");
        uint[] partNums = new uint[]{uint.Parse(parts[0]),uint.Parse(parts[1]),uint.Parse(parts[2])};
        (uint,uint) range = (partNums[1],partNums[1] + partNums[2] - 1);
        uint diff = partNums[1] - partNums[0];
        return new Range(range, diff);
    }

    private static List<uint> parseSeeds(string? line){
        if(line == null) line = ""; //To shut up warnings
        string[] parts = line.Split(":");
        string[] seedsS = parts[1].Trim().Split(" ");
        List<uint> seeds = new List<uint>();
        foreach(string seed in seedsS){
            seeds.Add(uint.Parse(seed));
        }
        return seeds;
    }
}