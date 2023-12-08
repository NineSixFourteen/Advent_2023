namespace DayEight;
public class DayEight{

    public static long run(){
        return doPart2b();
    }

    public static int doPart1(){
        try{
            StreamReader sr = new StreamReader("../DayEight/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            char[] moves = line.ToCharArray();
            Dictionary<string,(string,string)> map = new Dictionary<string, (string, string)>();
            line = sr.ReadLine();
            line = sr.ReadLine();
            while(line != null){
                string[] parts = line.Split("=");
                string[] values = parts[1].Trim().Split(",");
                map.Add(parts[0].Trim(), (values[0].Trim().Substring(1), values[1].Substring(1,values[1].Length -2)));
                line = sr.ReadLine();
            }
            foreach(string key in map.Keys){
                Console.WriteLine("-- " + key + " - " + map[key]);
            }
            string current = "AAA";
            int moveCount = 0;
            int placeInSteps = 0;
            while(current != "ZZZ"){
                if(placeInSteps == moves.Length){
                    placeInSteps = 0 ;
                }
                current = makeMove(map[current], moves[placeInSteps++]);
                moveCount++;
            }
            return moveCount;
        } catch(Exception e){
            Console.WriteLine(e);
            return 0;
        }
    }


    public static uint doPart2(){
        try{
            StreamReader sr = new StreamReader("../DayEight/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            char[] moves = line.ToCharArray();
            Dictionary<string,(string,string)> map = new Dictionary<string, (string, string)>();
            line = sr.ReadLine();
            line = sr.ReadLine();
            while(line != null){
                string[] parts = line.Split("=");
                string[] values = parts[1].Trim().Split(",");
                map.Add(parts[0].Trim(), (values[0].Trim().Substring(1), values[1].Substring(1,values[1].Length -2)));
                line = sr.ReadLine();
            }
            List<string> startingNodes = findStartingNodes(map.Keys);
            uint lengthOfPath = findAllEndNodes(startingNodes, map);
            return lengthOfPath;
        } catch(Exception e){
            Console.WriteLine(e);
            return 0;
        }
    }

    private static uint findAllEndNodes(List<string> startingNodes, Dictionary<string, (string, string)> map){
        List<List<string>> paths = startingNodes.Select(x => new List<string>{x}).ToList();
        Boolean left = true;
        uint count = 0;
        while(!isDone(paths)){
            count++;
            if(left){
                paths = walkLeft(paths, map);
                left = false;
            } else {
                paths = walkright(paths, map);
                left = true;
            }
        }
        return count;
    }

    private static List<List<string>> walkright(List<List<string>> temp, Dictionary<string, (string, string)> map){
        return temp.Select(path => {
                    if(path[path.Count -1].EndsWith("Z")) return path;
                    var x = path.ToArray().ToList();
                    x.Add(map[path[path.Count -1]].Item2);
                    return x;
        }).ToList();
    }

    private static List<List<string>> walkLeft(List<List<string>> temp, Dictionary<string, (string, string)> map){
        var x = temp.Select(path => {
            if(path[path.Count -1].EndsWith("Z")) {Console.WriteLine("Yes"); return path;};
            var x = path.ToArray().ToList();
            x.Add(map[path[path.Count -1]].Item1);
            return x;
        }).ToList();
        return x;
    }

    private static int longestPath(List<List<string>> paths){
        int longestPath = 0;
        paths.ForEach(path => longestPath = Math.Max(longestPath, path.Count));
        return longestPath - 1;
    }

    private static bool isDone(List<List<string>> paths){
        foreach(List<string> path in paths){
            if(!path[path.Count - 1].EndsWith("Z")){
                return false;
            }
        }
        return true;
    }

    private static List<string> findStartingNodes(Dictionary<string, (string, string)>.KeyCollection keys){
        return keys.Where(x => x.EndsWith("A")).ToList();
    }

    private static string makeMove((string, string) value, char move){
        if(move == 'L'){
            return value.Item1;
        } else {
            return value.Item2;
        }
    }

    public static long doPart2b(){
            StreamReader sr = new StreamReader("../DayEight/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            char[] moves = line.ToCharArray();
            Dictionary<string,(string,string)> map = new Dictionary<string, (string, string)>();
            sr.ReadLine();
            line = sr.ReadLine();
            while(line != null){
                string[] parts = line.Split("=");
                string[] values = parts[1].Trim().Split(",");
                map.Add(parts[0].Trim(), (values[0].Trim().Substring(1), values[1].Substring(1,values[1].Length -2)));
                line = sr.ReadLine();
            }
            List<string> startingNodes = findStartingNodes(map.Keys);
            long lcm = 1;
            foreach (var node in startingNodes)
            {
                var n = node;
                var count = 0;
                while (!n.EndsWith("Z")){
                    n = moves[count++ % moves.Length] == 'L' ? map[n].Item1 : map[n].Item2;
                    }
                lcm = CalculateLeastCommonMultiple(lcm, count);
            }
            return lcm;
    }

    private static long CalculateLeastCommonMultiple(long a, long b)
    {
        var max = Math.Max(a, b);
        var min = Math.Min(a, b);
        for (long i = 1; i <= min; i++)
        {
            if (max * i % min == 0)
            {
                return i * max;
            }
        }

        return min;
    }

}
