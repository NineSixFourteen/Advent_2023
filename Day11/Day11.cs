
namespace Day11;
public class Day11 {

    public static long run(){
        List<List<char>> map = parseMap("../Day11/TextExamples/Messages.txt");
        (List<int> rowJumps, List<int> colJumps) = getJumpes(map);
        List<(long,long)> galaxyCoords = findGalaxys(map, rowJumps, colJumps);
        //galaxyCoords.ForEach(
            //item => Console.WriteLine("X - " + item.Item1 + " Y - " + item.Item2)
        //);
        return findSumOfAllPaths(galaxyCoords);
    }

    private static long findSumOfAllPaths(List<(long, long)> galaxyCoords){
        long total = 0;
        for(int i = 0; i < galaxyCoords.Count - 1;i++){
            for(int j = i + 1; j < galaxyCoords.Count;j++){
                total += Math.Abs(galaxyCoords[i].Item1 - galaxyCoords[j].Item1) + Math.Abs(galaxyCoords[i].Item2 - galaxyCoords[j].Item2); // Find differnce in positions = shortast path 
            }
        }
        return total;
    }

    private static List<(long, long)> findGalaxys(List<List<char>> map, List<int> rowJumps, List<int> colJumps){
        List<(long,long)> galaxyCoords = new ();
        int numberOfJumpsRow = 0;
        int numberOfJumpsCol;
        int multi = 1000000 - 1 ; // For part 1 value should be 1 or 2 - 1
        for(int i = 0; i < map.Count;i++){
            numberOfJumpsCol = 0;
            if(rowJumps.Contains(i)){
                numberOfJumpsRow++;
            }
            for(int j = 0; j < map[0].Count;j++){
                if(colJumps.Contains(j)){
                    numberOfJumpsCol++;
                }
                if(map[i][j] == '#'){
                    galaxyCoords.Add(( i + (numberOfJumpsRow * multi) , j + (numberOfJumpsCol * multi) ));
                }
            }
        }
        return galaxyCoords;
    }

    private static List<List<char>> parseMap(string v){
        List<List<char>> map = new List<List<char>>();
        try{
            StreamReader sr = new StreamReader(v);
            string? line = sr.ReadLine();
            while(line != null){
                map.Add(line.ToCharArray().ToList());
                line = sr.ReadLine();
            }
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return map;
    }

    private static (List<int>, List<int>) getJumpes(List<List<char>> map){
        //Check Rows
        List<int> rowJumps = new List<int>();
        for(int i = 0; i < map.Count;i++ ){
            var row = map[i];
            if(row.Where(elem => elem != '.').ToList().Count == 0){
                rowJumps.Add(i);
            }
        }
        //Check Columns
        List<int> colJumps = new List<int>();
        for(int i = 0; i < map[0].Count;i++ ){
            bool found = false;
            for(int j = 0; j < map.Count;j++){
                if(map[j][i] != '.'){
                    found = true;
                    break;
                }
            }
            if(!found) colJumps.Add(i);
        }
        return (rowJumps, colJumps);
    }
}
