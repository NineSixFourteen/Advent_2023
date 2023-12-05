namespace DayThree;
public class DayThree {


    public static int run(){
        return doPart2();
    }

    private static int doPart1(){
        try{
            StreamReader sr = new StreamReader("../DayThree/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            List<char[]> lists = new List<char[]>();
            while(line != null){
                lists.Add(line.ToCharArray());
                line = sr.ReadLine();
            }
            return getTotal(lists);
        } catch(Exception e){
            Console.WriteLine(e);
            return 0;
        }
    }

    private static int doPart2(){
        try{
            StreamReader sr = new StreamReader("../DayThree/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            List<char[]> lists = new List<char[]>();
            while(line != null){
                lists.Add(line.ToCharArray());
                line = sr.ReadLine();
            }
            return getTotalGearRatio(lists);
        } catch(Exception e){
            Console.WriteLine(e);
            return 0;
        }
    }

    private static int getTotalGearRatio(List<char[]> lists){
    int total = 0;
        for(int i =0; i < lists.Count;i++){
            for(int l = 0; l < lists[i].Length;l++){
                if(lists[i][l] == '*'){
                    total += getGearRatio(lists,i,l);
                }
            }
        }
        return total;
    }

    private static int getGearRatio(List<char[]> lists, int i, int l){
        Boolean hasAbove = i > 0;
        Boolean hasBelow = i + 1 < lists.Count;
        Boolean hasLeft =  l > 0;
        Boolean hasRight = l + 1 < lists[i].Length;
        int totalFound = 0; 
        (int,int)[] positions = new (int, int)[]{(0,0),(0,0)};
        if(hasAbove){
            if(isDigit(lists[i-1][l])){
                if(totalFound < 2){
                    positions[totalFound] = (i-1,l);
                } 
                totalFound++;
            } else if(hasLeft && isDigit(lists[i-1][l-1])){
                if(totalFound < 2){
                    positions[totalFound] = (i-1,l-1);
                } 
                totalFound++;
            }
            if(hasRight && isDigit(lists[i-1][l+1]) && !isDigit(lists[i-1][l])){
                if(totalFound < 2){
                    positions[totalFound] = (i-1,l+1);
                } 
                totalFound++;
            }
        }
        if(hasLeft && isDigit(lists[i][l-1])){
            if(totalFound < 2){
                positions[totalFound] = (i,l-1);
                } 
                totalFound++;
        }
        if(hasRight && isDigit(lists[i][l+1])){
            if(totalFound < 2){
                positions[totalFound] = (i,l+1);
                } 
                totalFound++;
        }
        if(hasBelow){
            if(isDigit(lists[i+1][l])){
                if(totalFound < 2){
                    positions[totalFound] = (i+1,l);
                } 
                totalFound++;
            } else if(hasLeft && isDigit(lists[i + 1][l -1])){
                if(totalFound < 2){
                    positions[totalFound] = (i+1,l-1);
                } 
                totalFound++;
            }
            if(hasRight && isDigit(lists[i + 1][l + 1]) && !isDigit(lists[i+1][l])){
                if(totalFound < 2){
                    positions[totalFound] = (i+1,l+1);
                } 
                totalFound++;
            }
        }
        Console.WriteLine(totalFound);
        if(totalFound == 2){
            int firstNumber = getNumber(lists,positions[0]);
            int secondNumber = getNumber(lists,positions[1]);
            return firstNumber * secondNumber;
        }
        return 0;
    }

    private static int getTotal(List<char[]> lists){
        int total = 0;
        for(int i =0; i < lists.Count;i++){
            for(int l = 0; l < lists[i].Length;l++){
                if(isDigit(lists[i][l])){
                    if(hasSymbolAdjacent(lists,i,l)){
                        (int, int) number = grabNumber(lists[i],l);
                        total += number.Item1;
                        l = number.Item2;
                    }
                }
            }
        }
        return total;
    }

    private static bool hasSymbolAdjacent(List<char[]> lists,int i, int l){
        return hasAdjacent(isSymbol, lists, i, l, 1);
    }

    private static Boolean hasAdjacent(Func<Char,Boolean> checker, List<char[]> lists,int i, int l, int numberAdjacent){
        Boolean hasAbove = i > 0;
        Boolean hasBelow = i + 1 < lists.Count;
        Boolean hasLeft =  l > 0;
        Boolean hasRight = l + 1 < lists[i].Length;
        int totalFound = 0; 
        if(hasAbove){
            if(checker(lists[i-1][l])){
                if(totalFound + 1 == numberAdjacent) return true;
                else totalFound++;
            }
            if(hasLeft && checker(lists[i-1][l-1])){
                if(totalFound + 1 == numberAdjacent) return true;
                else totalFound++;
            }
            if(hasRight && checker(lists[i-1][l+1])){
                if(totalFound + 1 == numberAdjacent) return true;
                else totalFound++;
            }
        }
        if(hasLeft && checker(lists[i][l-1])){
                if(totalFound + 1 == numberAdjacent) return true;
                else totalFound++;
        }
        if(hasRight && checker(lists[i][l+1])){
                if(totalFound + 1 == numberAdjacent) return true;
                else totalFound++;
        }
        if(hasBelow){
            if(checker(lists[i+1][l])){
                if(totalFound + 1 == numberAdjacent) return true;
                else totalFound++;
            }
            if(hasLeft && checker(lists[i + 1][l -1])){
                if(totalFound + 1 == numberAdjacent) return true;
                else totalFound++;
            }
            if(hasRight && checker(lists[i + 1][l + 1])){
                if(totalFound + 1 == numberAdjacent) return true;
                else totalFound++;
            }
        }
        return false;
    }

    private static Boolean isSymbol(char c){
        return (c < 48 || c > 57) && c != '.'; 
    }


    private static Boolean isDigit(char c){
        return c > 47 && c < 58;
    }

    private static (int, int) grabNumber(char[] message, int place){
        string number = "" + message[place];
        int start = place;
        while(place - 1 >= 0 && isDigit(message[place - 1])){
            number = message[--place] + number;
        }
        place = start;
        while(message.Length > place + 1 && isDigit(message[place + 1])){
            number += message[++place];
        }
        return (int.Parse(number), place);
    }

    private static int getNumber(List<char[]> message, (int, int) value){
        char[] line = message[value.Item1];
        (int, int) val = grabNumber(line, value.Item2);
        return val.Item1;
    }
}
