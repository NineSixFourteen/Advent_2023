public class Parser{

    public static (List<long>,Finder) parse(string FileName){
        int place = 0;
        Finder finder = new Finder();
        List<long> seeds = new List<long>();
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
        long[] partNums = new long[]{long.Parse(parts[0]),long.Parse(parts[1]),long.Parse(parts[2])};
        (long,long) range = (partNums[1],partNums[1] + partNums[2] - 1);
        long diff = partNums[1] - partNums[0];
        return new Range(range, diff);
    }

    private static List<long> parseSeeds(string? line){
        if(line == null) line = ""; //To shut up warnings
        string[] parts = line.Split(":");
        string[] seedsS = parts[1].Trim().Split(" ");
        List<long> seeds = new List<long>();
        foreach(string seed in seedsS){
            seeds.Add(long.Parse(seed));
        }
        return seeds;
    }
}