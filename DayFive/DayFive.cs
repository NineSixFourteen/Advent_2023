
namespace DayFive;
public class DayFive {

    public static uint run(){
        return doPart1();
    }

    public static uint doPart1(){
        (List<uint>, Finder) items = Parser.parse("../DayFive/TextExamples/Messages.txt");
        return getAllseeds(items);
  
    }

    private static uint getAllseeds((List<uint>, Finder) items){
        uint smallest = uint.MaxValue;
        for(int i = 0; i < items.Item1.Count;i += 2){
            for(uint l = items.Item1[i]; l < items.Item1[i] + items.Item1[i + 1];l++){
                uint location = items.Item2.findLocationFromSeed(l);
                if(location < smallest){
                    smallest = location;
                }
            }
        }
        return smallest;
    }
}
