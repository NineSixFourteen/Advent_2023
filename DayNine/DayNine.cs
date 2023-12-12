namespace DayNine;
public class DayNine{

    public static long run() {
        return doPart2();
    }

    public static long doPart1(){
        long total = 0;
        try{
            StreamReader sr = new StreamReader("../DayNine/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            while(line != null){
                List<long> numbers = line.Split(" ").Select(num => long.Parse(num)).ToList();
                numbers.Add(0);
                total += nextValue(numbers);
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return total;
    }

    public static long doPart2(){
                long total = 0;
        try{
            StreamReader sr = new StreamReader("../DayNine/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            while(line != null){
                line = "0 " + line;
                List<long> numbers = line.Split(" ").Select(num => long.Parse(num)).ToList();
                total += lastValue(numbers);
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return total;
    }
    

    private static long lastValue(List<long> numbers){
        List<List<long>> longs = buildDiffTable2(numbers); 
        fillPlaceHolder2(longs);
        return longs[0][0];
    }

    private static List<List<long>> buildDiffTable2(List<long> numbers){
        List<List<long>> diffTable = new List<List<long>>{numbers};
        bool run  = true;
        while(run){
            List<long> temp = diffTable[diffTable.Count - 1];
            List<long> nextRow = new List<long>{0};
            for(int i = 1; i < temp.Count - 1; i++){
                nextRow.Add(temp[i + 1] - temp[i]);
            }
            diffTable.Add(nextRow);
            if(nextRow.Where(val => val != 0).ToList().Count == 0){
                run = false;
            }
        }
        return diffTable;
    }
    private static void fillPlaceHolder2(List<List<long>> longs){
        for(int i = longs.Count - 2; i >= 0;i--){
            longs[i][0] = longs[i][1] -  longs[i + 1][0];
        }
    }

    private static long nextValue(List<long> numbers){
        List<List<long>> longs = buildDiffTable(numbers); //TODO rename struct 
        fillPlaceHolder(longs);
        return longs[0][longs[0].Count - 1];
    }

    private static void fillPlaceHolder(List<List<long>> longs){
        for(int i = longs.Count - 2; i >= 0;i--){
            longs[i][longs[i].Count - 1] = longs[i][longs[i].Count - 2] +  longs[i + 1][longs[i + 1].Count - 1];
        }
    }

    private static List<List<long>> buildDiffTable(List<long> numbers){
        List<List<long>> diffTable = new List<List<long>>{numbers};
        bool run  = true;
        while(run){
            List<long> temp = diffTable[diffTable.Count - 1];
            List<long> nextRow = new List<long>();
            for(int i = 0; i < temp.Count - 2; i++){
                nextRow.Add(temp[i + 1] - temp[i]);
            }
            nextRow.Add(0);
            diffTable.Add(nextRow);
            if(nextRow.Where(val => val != 0).ToList().Count == 0){
                run = false;
            }
        }
        return diffTable;
    }
}
