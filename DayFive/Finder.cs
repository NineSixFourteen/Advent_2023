public class Finder{
    
    public List<List<Range>> ranges = new List<List<Range>>();
    public Finder(){}

    public static uint findNext(List<Range> ranges, uint startingPoint){
        uint diff = findDiffernce(ranges, startingPoint);
        return startingPoint - diff;
    }

    private static uint findDiffernce(List<Range> ranges, uint point){
        foreach(Range range in ranges){
            if(range.isInRange(point)){
                return range.getDiffernce();
            }
        }
        return 0;
    }

    public uint findLocationFromSeed(uint seed){
        uint soil = findNext(this.ranges[0],seed);
        //Console.WriteLine("Soil " + soil);
        uint fert = findNext(this.ranges[1],soil);
        //Console.WriteLine("fert " + fert);
        uint water = findNext(this.ranges[2],fert);
        //Console.WriteLine("water " + water);
        uint light = findNext(this.ranges[3],water);
        //Console.WriteLine("light " + light);
        uint temp = findNext(this.ranges[4],light);
        //Console.WriteLine("temp " + temp);
        uint hummid = findNext(this.ranges[5],temp);
        //Console.WriteLine("hummid " + hummid);
        uint location = findNext(this.ranges[6],hummid);
        //Console.WriteLine("location " + location);
        return location;
    }

    public override string ToString(){
        String s = "";
        for(int i = 0; i < ranges.Count;i++){
            s += "Map " + i + "\n";
            foreach(Range r in ranges[i]){
                (uint,uint) range = r.getRange();
                s += " Range " + range.Item1 + " - " + range.Item2 + " Differne - " + r.getDiffernce() + "\n";
            }
        }
        return s;
    }

}

public class Range{
    (uint,uint) range;
    uint differnce;

    public Range((uint,uint) range, uint diff){
        this.range = range;
        this.differnce = diff;
    }

    public Boolean isInRange(uint point){
        return point >= range.Item1 && point <= range.Item2;
    }

    public uint getDiffernce(){
        return this.differnce;
    }

    public (uint,uint) getRange(){
        return this.range;
    }
}