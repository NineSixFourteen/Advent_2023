namespace DayTwo;
public class DayTwo{

    public static int run(){
        return doPart2();
    }

    private static int doPart1(){
        int total = 0; 
        try{
            StreamReader sr = new StreamReader("../DayTwo/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            while(line != null){
                Game temp = new Game(line);
                total += temp.getValue(new int[]{12,13,14});
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return total;
    }

        private static int doPart2(){
        int total = 0; 
        int i =0;
        try{
            StreamReader sr = new StreamReader("../DayTwo/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
   
            while(line != null){
                Game temp = new Game(line);
                total += temp.getMinimumCube();
                i++;
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        Console.WriteLine(i);
        return total;
    }

}
