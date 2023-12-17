
using System.Collections;

namespace Day10;
public class Day10 {

    public static int run(){
        return doPart2();
    }

    private static int doPart1(){
        try{
            StreamReader sr = new StreamReader("../Day10/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            List<List<Directions>> map = new List<List<Directions>>();
            while(line != null){
                List<Directions> parsedLine = parse(line);
                map.Add(parsedLine);
                line = sr.ReadLine();
            }
            return solvePart1(map);
        } catch(Exception e){
            Console.WriteLine(e);
            return 0;
        }
    }

    private static int solvePart1(List<List<Directions>> map){
        (int,int) startPoint = (0,0);
        for(int i = 0; i < map.Count;i++){
            for(int j = 0; j < map[i].Count;j++){
                if(map[i][j].isStart()){
                    startPoint = (i,j);
                    i = map.Count;
                    break;
                }
            }
        }
        int moves = findLoop(map, startPoint);
        return moves / 2; 
    }

    private static int doPart2(){
        List<List<char>> map = new List<List<char>>();
        try{
            StreamReader sr = new StreamReader("../Day10/TextExamples/Test1.txt");
            string? line = sr.ReadLine();
            while(line != null){
                map.Add(line.ToCharArray().ToList());
                line = sr.ReadLine();
            }
            return solvePart2(map);
        } catch(Exception e){
            Console.WriteLine(e);
            return 0;
        }
    }

    private static int solvePart2(List<List<char>> map){
        int total = 0;
        List<(int,int)> knownCords = new();
        for(int i = 0; i < map.Count;i++){
            for(int l = 0; l <map[i].Count;l++){
                if(map[i][l] == '.' && !knownCords.Contains((i,l))){
                    List<(int,int)> coordinates = findAllCords(map,i,l);
                    knownCords.AddRange(coordinates);
                    if(isClosed(map,coordinates)){
                        total += coordinates.Count;
                    }
                }
            }
        }
        return total;
    }

    private static List<(int,int)> findAllCords(List<List<char>> map, int x, int y){
        List<(int,int)> rer = new List<(int, int)>{(x,y)};
        Queue<(int,int)> coords = new Queue<(int, int)>{};
        coords.Enqueue((x,y));
        while(coords.Count > 0){
            (int,int) temp = coords.Dequeue();
            if(temp.Item1 + 1 < map.Count && map[temp.Item1 + 1][temp.Item2] == '.'){
                rer.Add((temp.Item1 + 1, temp.Item2));
                coords.Enqueue((temp.Item1 + 1, temp.Item2));
            }
            if(temp.Item2 + 1 < map[temp.Item1].Count && map[temp.Item1][temp.Item2 + 1] == '.'){
                rer.Add((temp.Item1, temp.Item2 + 1));
                coords.Enqueue((temp.Item1, temp.Item2 + 1));
            }
        }
        return rer;
    }

    private static bool isClosed(List<List<char>> map, List<(int, int)> coordinates){
        foreach((int,int) coor in coordinates){
            if(!checkLeft(map, coor)){
                return false;
            }
            if(!checkRight(map, coor)){
                return false;
            }
            if(!checkUp(map, coor)){
                return false;
            }
            if(!checkDown(map, coor)){
                return false;
            }
        }
        return true;
    }

    private static bool checkLeft(List<List<char>> map, (int, int) coor){
        int x = coor.Item1;
        int y = coor.Item2;
        if(y < 0){
            return false;
        }
        switch(map[x][y]){
            case '.':
            case '|':
            case '7':
            case 'J':
                return true;
            default:
                return false;
        }
    }

    private static bool checkRight(List<List<char>> map, (int, int) coor){
        int x = coor.Item1;
        int y = coor.Item2 + 1;
        if(y >= map[x].Count){
            return false;
        }
        switch(map[x][y]){
            case '.':
            case '|':
            case 'L':
            case 'F':
                return true;
            default:
                return false;
        }
    }

    private static bool checkUp(List<List<char>> map, (int, int) coor){
        int x = coor.Item1 - 1;
        int y = coor.Item2;
        if(x < 0 ){
            return false;
        }
        switch(map[x][y]){
            case '.':
            case '-':
            case 'L':
            case 'J':
                return true;
            default:
                return false;
        }
    }

    private static bool checkDown(List<List<char>> map, (int, int) coor){
        int x = coor.Item1 + 1;
        int y = coor.Item2;
        if(x >= map.Count){
            return false;
        }
        switch(map[x][y]){
            case '.':
            case '-':
            case 'F':
            case '7':
                return true;
            default:
                return false;
        }
    }

    private static int findLoop(List<List<Directions>> map, (int, int) startPoint){
        Queue<List<Direction>> paths = new ();
        findStarts(map, startPoint).ForEach(paths.Enqueue);
        while(paths.Count != 0){
            (int, bool) loop = finishLoop(map, paths.Dequeue(), startPoint.Item1, startPoint.Item2);
            if(loop.Item2){
                return loop.Item1;
            }
        }
        return 0;
    }

    private static (int, bool) finishLoop(List<List<Directions>> map, List<Direction> directions, int x, int y){
        Direction prevMove = directions[0];
        int moves = 1;
        switch(prevMove){
            case Direction.Left:
                y -= 1;
                break;
            case Direction.Right:
                y += 1;
                break;
            case Direction.Up:
                x -= 1;
                break;
            case Direction.Down:
                x += 1;
                break;
        }
        while(true){
            Console.WriteLine("Moves - " + moves);
            Console.WriteLine("Move - " + prevMove);
            Direction move = map[x][y].move(prevMove);
            switch(move){
                case Direction.Left:
                y -= 1;
                break;
            case Direction.Right:
                y += 1;
                break;
            case Direction.Up:
                x -= 1;
                break;
            case Direction.Down:
                x += 1;
                break;
            case Direction.Start:
                return (moves, true);
            default:
                return (0, false);
            }
            moves++;
            prevMove = move;

        }
    }

    private static List<List<Direction>> findStarts(List<List<Directions>> map, (int, int) startPoint){
        List<List<Direction>> starts = new List<List<Direction>>();
        int x = startPoint.Item1;
        int y = startPoint.Item2;
        if( x - 1 > 0){
            Direction movem = map[x - 1][y].move(Direction.Up);
            if(movem != Direction.None){
                starts.Add(new List<Direction>{Direction.Up});
            }
        }
        if( x + 1 < map.Count){
            Direction movem = map[x + 1][y].move(Direction.Right);
            if(movem != Direction.None){
                starts.Add(new List<Direction>{Direction.Right});
            }
        } 
        if( y - 1 > 0){
            Direction movem = map[x ][y - 1].move(Direction.Left);
            if(movem != Direction.None){
                starts.Add(new List<Direction>{Direction.Left});
            }
        }  
        if( y + 1 < map[x].Count()){
            Direction movem = map[x][y + 1].move(Direction.Right);
            if(movem != Direction.None){
                starts.Add(new List<Direction>{Direction.Right});
            }
        }  
        return starts;
    }

    private static List<Directions> parse(string line){
        List<Directions> parsedLine = new List<Directions>();
        foreach(char c in line){
            parsedLine.Add(parseSymbol(c));
        }
        return parsedLine;
    }

    private static Directions parseSymbol(char c){
        switch(c){
            default: 
            case '.':
                return new Directions((Direction.None, Direction.None));
            case 'F':
                return new Directions((Direction.Up, Direction.Right));
            case 'L':
                return new Directions((Direction.Down, Direction.Right));
            case '7':
                return new Directions((Direction.Up, Direction.Left));
            case 'J':
                return new Directions((Direction.Down, Direction.Left));
            case '|':
                 return new Directions((Direction.Down, Direction.Down));
            case '-':
                return new Directions((Direction.Left, Direction.Left));
            case 'S':
                return new Directions((Direction.Start, Direction.Start));
            
                
        }
    }
}

public class Directions{

    (Direction, Direction) paths;

    public Directions((Direction, Direction) paths){
        this.paths = paths;
    }

    public Direction move(Direction dir){
        if(paths.Item1 == Direction.Start){
            return Direction.Start;
        }
        if(dir == paths.Item1){
            return paths.Item2;
        }
        if(dir == reverse(paths.Item2)){
            return reverse(paths.Item1);
        }
        return Direction.None;
    }

    public bool isStart(){
        return paths == (Direction.Start, Direction.Start);
    }

    private Direction reverse(Direction item2){
        switch(item2){
            case Direction.Left:    
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            default: 
                return Direction.None;
        }
    }

    public override string ToString(){
        return "Through " + paths.Item1 + " to " + paths.Item2;
        
    }
}

public enum Direction {
    Left, 
    Right, 
    Up, 
    Down,
    Start,
    None,
}
