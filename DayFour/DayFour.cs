namespace DayFour;
public class DayFour{


    public static int run(){
        return doPart2();
    }

    public static int doPart1(){
        int total = 0; 
        try{
            StreamReader sr = new StreamReader("../DayFour/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            while(line != null){
                (int[], int[]) value = parse(line);
                total += countPoints(value);
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return total;
    }

    public static int doPart2(){
        int total = 0;
        int lineNumber = 0;
        int[] numberOfCards = Enumerable.Range(0, 219).Select(x => 1).ToArray();
        try{
            StreamReader sr = new StreamReader("../DayFour/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            while(line != null){
                (int[], int[]) value = parse(line);
                int cards = countSame(value); 
                Console.WriteLine(numberOfCards[lineNumber]);
                for(int i = 1; i <= cards;i++){
                    if(lineNumber + 1 < numberOfCards.Length){
                        numberOfCards[lineNumber + i] += numberOfCards[lineNumber];
                    }
                }
                lineNumber++;
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return numberOfCards.Sum();
    }

    private static (int[],int[]) parse(string message){
        string[] parts = message.Split(":");
        string[] sides = parts[1].Split("|");
        string[] side1parts = sides[0].Split(" "); // Ignore first element in list since had leading space
        string[] side2parts = sides[1].Split(" ");
        return (toIntArray(side1parts), toIntArray(side2parts));
    }

    private static int[] toIntArray(string[] parts){
        List<int> numbers = new List<int>();
        for(int i = 0; i < parts.Length; i++){
            if(parts[i] != ""){
                numbers.Add(int.Parse(parts[i]));
            }
        }
        return numbers.ToArray();
    }

    private static int countSame((int[], int[]) value){
    int total = 0;
        foreach(int number in value.Item1 ){
            if(value.Item2.Contains(number)){
                total++;
            }
        }
        return total;
    }

    private static int countPoints((int[],int[]) value){
        int total = 0;
        foreach(int number in value.Item1 ){
            if(value.Item2.Contains(number)){
                if(total == 0) total++;
                else total *= 2;
            }
        }
        return total;
    }
}
