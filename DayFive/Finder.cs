public class Finder{
    
    public List<List<Range>> ranges{get;set;}

    public Finder(){
        ranges = new List<List<Range>>();
    }

    public static long findNext(List<Range> ranges, long startingPoint){
        long diff = findDiffernce(ranges, startingPoint);
        return startingPoint - diff;
    }

    private static long findDiffernce(List<Range> ranges, long point){
        foreach(Range range in ranges){
            if(range.isInRange(point)){
                return range.getDiffernce();
            }
        }
        return 0;
    }

    

    public long findLocationFromSeed(long seed){
        long soil = findNext(this.ranges[0],seed);
        //Console.WriteLine("Soil " + soil);
        long fert = findNext(this.ranges[1],soil);
        //Console.WriteLine("fert " + fert);
        long water = findNext(this.ranges[2],fert);
        //Console.WriteLine("water " + water);
        long light = findNext(this.ranges[3],water);
        //Console.WriteLine("light " + light);
        long temp = findNext(this.ranges[4],light);
        //Console.WriteLine("temp " + temp);
        long hummid = findNext(this.ranges[5],temp);
        //Console.WriteLine("hummid " + hummid);
        long location = findNext(this.ranges[6],hummid);
        //Console.WriteLine("location " + location);
        return location;
    }

    public override string ToString(){
        String s = "";
        for(int i = 0; i < ranges.Count;i++){
            s += "Map " + i + "\n";
            foreach(Range r in ranges[i]){
                (long,long) range = r.getRange();
                s += " Range " + range.Item1 + " - " + range.Item2 + " Differne - " + r.getDiffernce() + "\n";
            }
        }
        return s;
    }

}

public class Range{
    (long,long) range;
    long differnce;

    public Range((long,long) range, long diff){
        this.range = range;
        this.differnce = diff;
    }

    public Boolean isInRange(long point){
        return point >= range.Item1 && point <= range.Item2;
    }

    public long getDiffernce(){
        return this.differnce;
    }

    public (long,long) getRange(){
        return this.range;
    }
}