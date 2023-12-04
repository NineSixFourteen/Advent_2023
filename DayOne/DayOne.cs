
namespace DayOne;
public class Runner{

    public static int run(){
        return getTotalPart2();
    }

    private static int getTotalPart1(){
        int total = 0; 
        try{
            StreamReader sr = new StreamReader("../DayOne/TextExamples/Test4.txt");
            string? line = sr.ReadLine();
            while(line != null){
                total += Convert(line);
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return total;
    }

    private static int Convert(string line){
        char first = ' ';
        char last = ' ';  
        Char[] li = line.ToCharArray();
        int length = li.Length;
        for(int i = 0; i < length; i++){
            if(li[i] > 47 && li[i] < 58){
                first = li[i];
                break;
            }
        }
        for(int i = length - 1; i >= 0; i--){
            if(li[i] > 47 && li[i] < 58){
                last = li[i];
                break;
            }
        }
        return Int32.Parse("" + first + last);
    }

    // Part 2 

    private static int getTotalPart2(){
        int total = 0; 
        try{
            StreamReader sr = new StreamReader("../DayOne/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            while(line != null){
                
                total += ConvertPart2(line);
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return total;
    }

    private static int ConvertPart2(string line){
        Console.WriteLine(line);
        int first = 0;
        int last = 0;
        Char[] li = line.ToCharArray();
        int length = li.Length;
        for(int i = 0; i < length; i++){
            int value = IsDigit(line, i);
            if(value != -1){
                first = value;
                break;
            }
        }
        for(int i = length - 1; i >= 0; i--){
            int value = IsDigit(line, i);
            if(value != -1){
                last = value;
                break;
            }
        }
        return int.Parse("" + first + "" + last);
    }

    private static int IsDigit(string line, int place){
        char firstDigit = line[place];
        if(firstDigit > 47 && firstDigit < 58){
            return firstDigit - 48;
        }
        switch(firstDigit){
            case 'o':
                if(line.Length > place + 2){
                    if(line.Substring(place, 3) == "one"){
                        return 1;
                    }
                }
                return -1;
            case 't':
                if(line.Length > place + 1){
                    if(line[place + 1] == 'w'){
                        if(line.Length > place + 2 && line.Substring(place, 3) == "two") return 2;
                    }
                    if(line[place + 1] == 'h'){
                        if(line.Length > place + 4 && line.Substring(place, 5) == "three") return 3;
                    }
                }
                return -1;
            case 'f':
                if(line.Length > place + 1){
                    if(line[place + 1] == 'o'){
                        if(line.Length > place + 3 && line.Substring(place, 4) == "four") return 4;
                    }
                    if(line[place + 1] == 'i'){
                        if(line.Length > place + 3 && line.Substring(place, 4) == "five") return 5;
                    }
                }
                return -1;
            case 's':
                if(line.Length > place + 1){
                    if(line[place + 1] == 'i'){
                        if(line.Length > place + 2 && line.Substring(place, 3) == "six") return 6;
                    }
                    if(line[place + 1] == 'e'){
                        if(line.Length > place + 4 && line.Substring(place, 5) == "seven") return 7;
                    }
                }
                return -1;
            case 'e':
                if(line.Length > place + 4 && line.Substring(place, 5) == "eight") return 8;
                return -1;
            case 'n':
                if(line.Length > place + 3 && line.Substring(place, 4) == "nine") return 9;
                return -1;
            default:
                return -1;
        }
    }
}
