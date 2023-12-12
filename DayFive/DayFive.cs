
namespace DayFive;
public class DayFive {

    public static long run(){
        return doPart1();
    }

    public static long doPart1(){
        (List<long>, Finder) items = Parser.parse("../DayFive/TextExamples/Messages.txt");
        return getAllseeds(items);
  
    }

    private static long getAllseeds((List<long>, Finder) items){
        long smallest = long.MaxValue;
        for(int i = 0; i < items.Item1.Count;i += 2){
            for(long l = items.Item1[i]; l < items.Item1[i] + items.Item1[i + 1];l++){
                long location = items.Item2.findLocationFromSeed(l);
                if(location < smallest){
                    smallest = location;
                }
            }
        }
        return smallest;
    }
}
