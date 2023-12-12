
namespace DayFive;

public class Attempt2{

    public static long run(){
        return doPart2();
    }

    public static long doPart2(){
        (List<long>, Finder) items = Parser.parse("../DayFive/TextExamples/Messages.txt");
        List<RangeItem> seedRanges = new List<RangeItem>();
        for(int i = 0; i < items.Item1.Count;i += 2){
            seedRanges.Add(new RangeItem(items.Item1[i], items.Item1[i + 1]));
        }
        long lowest = long.MaxValue;
        foreach(RangeItem range in seedRanges){
            lowest = Math.Min(lowest, findLowest(range, items.Item2.ranges));
        }
        return lowest;
    }



    private static long findLowest(RangeItem range, List<List<Range>> ranges){
        List<RangeItem> allRanges = new List<RangeItem>{range};
       // Console.WriteLine("Ranges");
        //allRanges.ForEach(range => Console.WriteLine(range));
        for(int i = 0; i < 6;i++){
            allRanges = findNextRanges(allRanges, ranges[i]);
            //Console.WriteLine("Ranges");
            //allRanges.ForEach(range => Console.WriteLine(range));
        }
        long lowest = long.MaxValue; 
        allRanges.ForEach(range => {lowest = Math.Min(lowest, range.start);});
        return lowest;
    }

    private static List<RangeItem> findNextRanges(List<RangeItem> allRanges, List<Range> ranges){
        List<RangeItem> alRanges = new List<RangeItem>();
        ranges.Sort((a,b) => a.getRange().Item1.CompareTo(b.getRange().Item1));
        //ranges.ForEach(range => Console.WriteLine("range - " + range.getRange() + " diff - " + range.getDiffernce()));
        foreach(RangeItem item in allRanges){
            long currentPlace = item.start;
            int currentCheck = 0;
            while(currentCheck < ranges.Count){
                long nextStart = ranges[currentCheck].getRange().Item1;
                long nextDest =  ranges[currentCheck].getRange().Item2;
                if(currentPlace < nextStart){
                    if(item.dest < nextStart){
                        //Console.WriteLine("1 - " + currentPlace);
                        alRanges.Add(new RangeItem(currentPlace, item.dest + 1 - currentPlace));
                        break;
                    } else {
                        //Console.WriteLine("2 - " + currentPlace);
                        alRanges.Add(new RangeItem(currentPlace, nextStart - currentPlace));
                        currentPlace = nextStart;
                    }
                } else if(currentPlace >= nextStart && currentPlace <= nextDest) {
                    long diff = ranges[currentCheck].getDiffernce();
                    if(item.dest <= nextDest){
                        //Console.WriteLine("3 - " + currentPlace);
                        alRanges.Add(new RangeItem(currentPlace - diff,  item.range)); // -diff converts from a seed to soil etc
                        break;
                    } else {
                        //Console.WriteLine("4 - " + currentPlace);
                        alRanges.Add(new RangeItem(currentPlace - diff,  nextDest - item.start)); // -diff converts from a seed to soil etc
                        currentPlace = nextDest;
                        currentPlace++;
                    }
                } else {
                    currentCheck++;
                    if(currentCheck == ranges.Count){
                        //Console.WriteLine("5 - " + currentPlace);
                        alRanges.Add(item);
                    }
                }
            }
        }
        return alRanges;
    }

    public class RangeItem {
        public long start {get;set;}
        public long dest {get;set;}
        public long range {get;set;}
        public RangeItem(long start, long range){
            this.start = start;
            this.dest = start + range - 1;
            this.range = range;
        }
        public override string ToString(){
            return "Start - " + this.start + " dest - " + this.dest + " range - " + this.range;
        }
    }
}