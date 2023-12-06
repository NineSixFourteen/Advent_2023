namespace DaySix;
public class DaySix{

    public static ulong run(){
        return doPart2();
    }

    public static int doPart1(){
        try{
            StreamReader sr = new StreamReader("../DaySix/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            if(line == null) line = "";//Remove Warnings I knew the input to I know there is a line
            int[] times = line.Split(":")[1]
                                .Trim().Split(" ")
                                .Where(x => x.Length > 0)
                                .Select(x => int.Parse(x))
                                .ToArray();
            line = sr.ReadLine();
            if(line == null) line = "";//Remove Warnings I knew the input to I know there is a line
            int[] records = line.Split(":")[1]
                                .Trim().Split(" ")
                                .Where(x => x.Length > 0)
                                .Select(x => int.Parse(x))
                                .ToArray();
            int total = 1;
            for(int i = 0; i < times.Length;i++ ){
                total *= findHowManyBeat(times[i],records[i]);
            }
            return total;
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return 0;
    }

    public static ulong doPart2(){
        try{
            StreamReader sr = new StreamReader("../DaySix/TextExamples/Messages2.txt");
            string? line = sr.ReadLine();
            if(line == null) line = "";//Remove Warnings I knew the input to I know there is a line
            ulong time = ulong.Parse(line.Split(":")[1]
                                .Trim());
            line = sr.ReadLine();
            if(line == null) line = "";//Remove Warnings I knew the input to I know there is a line
            ulong record = ulong.Parse(line.Split(":")[1]
                                .Trim());
            return findHowManyBeat(time,record);
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return 0;
    }

    private static ulong findHowManyBeat(ulong maxTime, ulong record){
        ulong total = 0;
        for(ulong i = 0; i < maxTime;i++){
            if(i * (maxTime - i) > record){

                total++;
            }
        }
        return total;
    }

    private static int findHowManyBeat(int maxTime, int record){
        int total = 0;
        for(int i = 0; i < maxTime;i++){
            if(i * (maxTime - i) > record){

                total++;
            }
        }
        Console.WriteLine(total);
        return total;
    }
}
