namespace Day15;
public class Day15{

    public static int run(){
        return doPart1();
    }

    public static int doPart1(){
        try{
            StreamReader sr = new StreamReader("../Day15/TextExamples/Input.txt");
            string? line = sr.ReadLine();
            string[] parts = line.Split(",");
            int total = 0;
            foreach(string part in parts){
                Console.WriteLine("Word - " + part);
                Console.WriteLine("HASH V - " + HASH(part));
                total += HASH(part); 
            }
            return total;
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return 0;
    }

    private static int HASH(string part){
        int current = 0;
        foreach(char c in part){
            current += c;
            current *= 17;
            current %= 256;
        }
        return current;
    }
}
