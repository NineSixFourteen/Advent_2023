namespace Day12;

public class Day12{

    public static int run(){
        return doPart1();
    }

    public static int doPart1(){
        int total = 0;
        List<(string, List<int>)> lines = getLines2("../Day12/TextExamples/Test1.txt");
        foreach((string line, List<int> nums) in lines){
            int permutations = findPermutations(line, nums).Count();
            Console.WriteLine("Message - " + line);
            Console.Write("Map - ");
            nums.ForEach(num => Console.Write(num + ", "));
            Console.WriteLine();
            Console.WriteLine("Number of permutations - " + permutations);
            total += permutations;
        }
        return total;
    }

    private static List<(string, List<int>)> getLines2(string v){
        List<(string, List<int>)> map = new ();
        try{
            StreamReader sr = new StreamReader(v);
            string? line = sr.ReadLine();
            while(line != null){
                string[] parts = line.Split(" ");
                string message = multX5(parts[0]);
                List<int> longMap = multX5(parts[1].Split(",").Select(x => int.Parse(x)).ToList());
                map.Add((message,longMap));
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return map;
    }

    private static List<int> multX5(List<int> list){
        List<int> newList = new();
        for(int i = 0; i < 5;i++){
            foreach(int num in list){
                newList.Add(num);
            }
        }
        return newList;
    }
    private static string multX5(string s ){
        string message = "";
        for(int i = 0; i < 5;i++){
            foreach(char c in s){
                message += c;
            }
            message += "?";
        }
        return message;
    }

    private static List<string> findPermutations(string line, List<int> map){
        List<string> completePermutations = new List<string>();
        Queue<string> steps = new Queue<string>(){};
        steps.Enqueue(line);
        while(steps.Count != 0){
            string next = steps.Dequeue();
            List<string> nextSteps = step(next);
            foreach(string step in nextSteps){
                if(isValid(step, map)){
                    if(!step.ToCharArray().Where(c => c == '?').Any() && isSame(map, mapF(step))){
                        completePermutations.Add(step);
                    } else {
                        steps.Enqueue(step);
                    }
                }
            }
        }
        return completePermutations;
    }

    private static bool isSame(List<int> x, List<int> y){
        if(x.Count != y.Count) return false;
        for(int i =0 ; i < x.Count;i++){
            if(x[i] != y[i]) return false;
        }
        return true;
    }

    private static List<string> step(string next){
        List<string> steps = new List<string>();
        for(int i = 0; i < next.Length;i++){
            if(next[i] == '?'){
                char[] Move1 = next.ToCharArray();
                char[] Move2 = next.ToCharArray();
                Move1[i] = '.';
                Move2[i] = '#';
                steps.Add(dumbestFunctionEverMadeByBrainThickAF(Move1));
                steps.Add(dumbestFunctionEverMadeByBrainThickAF(Move2));
                return steps;
            }
        }
        return steps;
    }

    private static string dumbestFunctionEverMadeByBrainThickAF(char[] mes){
        string s = "";
        foreach(char c in mes){
            s += c;
        }
        return s;
    }

    private static bool isValid(string step, List<int> map){
        List<int> pMap = mapF(step);
        return isPossibleMatch(pMap, map);
    }

    private static bool isPossibleMatch(List<int> pMap, List<int> map){
        if(pMap.Count > map.Count) return false;
        if(pMap.Count == 0) return true;
        for(int i = 0; i < pMap.Count -1;i++){
            if(pMap[i] != map[i]){
                return false;
            }
        }
        return pMap[pMap.Count - 1] <= map[pMap.Count -1];
    }

    private static List<int> mapF(string step){
        List<int> map = new List<int>();
        bool looking = false;
        int count = 0;
        foreach(char c in step){
            if(c == '?'){
                break;
            }
            if(!looking && c == '#'){
                looking = true;
                count = 1;
            } else if(looking && c == '#'){
                count++;
            } else if(looking && c != '#'){
                map.Add(count);
                count = 0;
                looking = false;
            }
        }
        if(looking) map.Add(count); 
        return map;
    }

    private static List<(string, List<int>)> getLines(string v){
        List<(string, List<int>)> map = new ();
        try{
            StreamReader sr = new StreamReader(v);
            string? line = sr.ReadLine();
            while(line != null){
                string[] parts = line.Split(" ");
                map.Add( (parts[0], parts[1].Split(",").Select(x => int.Parse(x)).ToList()) );
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return map;
    }
}
