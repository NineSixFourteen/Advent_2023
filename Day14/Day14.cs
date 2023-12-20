namespace Day14;
public class Day14{

    public static int run(){
        return doPart1();
    }

    public static int doPart1(){
        List<List<char>> map = new ();
        try{
            StreamReader sr = new StreamReader("../Day14/TextExamples/Input.txt");
            string? line = sr.ReadLine();
            while(line != null){
                map.Add(line.ToCharArray().ToList());
                line = sr.ReadLine();
            }
            doCycle(map);
            List<List<char>> lookFor = new List<List<char>>();
            map.ForEach( row => {
                char[] temp = row.ToArray().Clone() as char[];
                lookFor.Add(temp.ToList());
            });
            doCycle(map);
            int stepCounter = 1;
            while(!compareMaps(lookFor,map)){
                doCycle(map);
                stepCounter++;
            }
            Console.WriteLine("Steps - " + stepCounter);
            int remainingCycles = 1000000000 % stepCounter;
            for(int i = 0; i < remainingCycles;i++){
                doCycle(map);
            }
            return CalculateScore(map);
        } catch(Exception e){
            Console.WriteLine(e);
            return 0;
        }
    }

    private static bool compareMaps(List<List<char>> lookFor, List<List<char>> map){
        for(int i = 0; i < lookFor.Count;i++){
            for(int j = 0; j < lookFor[0].Count;j++){
                if(lookFor[i][j] != map[i][j]) return false;
            }
        }
        return true;
    }

    private static void doCycle(List<List<char>> map){
        rollUp(map);
        rollLeft(map);
        rollDown(map);
        rollRight(map);
    }

    private static void rollUp(List<List<char>> map){
        for(int i = 0; i < map[0].Count;i++){
                roll(map,i); 
        }
    }
    private static void rollDown(List<List<char>> map){
        for(int i = 0; i < map[0].Count;i++){
                rollDo(map,i); 
        }
    }

    private static void rollLeft(List<List<char>> map){
        for(int i = 0; i < map.Count;i++){
            rollLe(map,i);   
        }
    }

    private static void rollLe(List<List<char>> map, int row){
        int stopper = 0;
        for(int i = 0; i < map[row].Count;i++){
            switch(map[row][i]){
                case '#':
                    stopper = i + 1;
                    break;
                case 'O':
                    if(i != stopper){
                        map[row][stopper] = map[row][i];
                        map[row][i] = '.';
                    }
                    stopper++;
                    break;
                case '.':
                    break;
                default:
                return;
            }
        }
    }

    private static void rollRight(List<List<char>> map){
        for(int i = 0; i < map.Count;i++){
            rollRi(map,i);   
        }
    }

    private static void rollRi(List<List<char>> map, int row){
        int stopper = map[row].Count -1 ;
        for(int i = map[row].Count -1; i >= 0;i--){
            switch(map[row][i]){
                case '#':
                    stopper = i -1;
                    break;
                case 'O':
                    if(i != stopper){
                        map[row][stopper] = map[row][i];
                        map[row][i] = '.';
                    }
                    stopper--;
                    break;
                case '.':
                    break;
                default:
                return;
            }
        }
    }

    private static void rollDo(List<List<char>> map, int row){
        int stopper = map.Count - 1;
        for(int i = map.Count -1; i >= 0;i--){
            switch(map[i][row]){
                case '#':
                    stopper = i - 1;
                    break;
                case 'O':
                    if(i != stopper){
                        map[stopper][row] = map[i][row];
                        map[i][row] = '.';
                    }
                    stopper--;
                    break;
                case '.':
                    break;
                default:
                return;
            }
        }
    }
 
    private static int CalculateScore(List<List<char>> mapRolled){
        int total = 0;
        for(int i = 0; i < mapRolled.Count;i++){
            total += mapRolled[i].Where(x =>  x== 'O').ToList().Count * (mapRolled.Count - i);
        }
        return total;
    }

    private static void roll(List<List<char>> map, int row){
        int stopper = 0;
        for(int i = 0; i < map.Count;i++){
            switch(map[i][row]){
                case '#':
                    stopper = i + 1;
                    break;
                case 'O':
                    if(i != stopper){
                        map[stopper][row] = map[i][row];
                        map[i][row] = '.';
                    }
                    stopper++;
                    break;
                case '.':
                    break;
                default:
                return;
            }
        }
    }
}
