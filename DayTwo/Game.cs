namespace DayTwo;

class Game {

    // When creating a "Set" of info the order of cubes will always be stored in Red, Green, Blue Cubes

    private int gameID;
    private List<int[]> sets;
    private int[] highestAmounts;
    private int[] lowestAmounts;

    public Game(int gameID, List<int[]> sets, int[] highestAmounts, int[] lowestAmounts){
        this.gameID = gameID;
        this.sets = sets;
        this.highestAmounts = highestAmounts;
        this.lowestAmounts = lowestAmounts;
    }

    public Game(string message){
        string[] parts = message.Split(":");
        this.gameID = int.Parse(parts[0].Split(" ")[1]);
        string[] setss = parts[1].Split(";"); 
        this.highestAmounts = new int[]{0,0,0};
        this.lowestAmounts  = new int[]{1000,1000,1000,1000};
        this.sets = new List<int[]>();
        foreach( string se in setss){
            string temp = se.Substring(1);
            List<int> set = new() { 0,0,0};
            string[] setparts = temp.Split(",");
            foreach(string value in setparts){
                string temp1;
                if(value[0] == ' '){
                    temp1 = value.Substring(1);
                } else {
                    temp1 = value;
                }
                string[] split = temp1.Split(" ");
                int val = 0;
                switch(split[1]){
                    case "red":
                        val = int.Parse(split[0]);
                        this.highestAmounts[0] = Math.Max(val, this.highestAmounts[0]);
                        this.lowestAmounts[0] = Math.Min(val, this.lowestAmounts[0]);
                        set[0] = val;
                        break;
                    case "blue":
                        val = int.Parse(split[0]);
                        this.highestAmounts[2] = Math.Max(val, this.highestAmounts[2]);
                        this.lowestAmounts[2] = Math.Min(val, this.lowestAmounts[2]) ;
                        set[2] = val;
                        break;
                    case "green":
                        val = int.Parse(split[0]);
                        this.highestAmounts[1] = Math.Max(val, this.highestAmounts[1]);
                        this.lowestAmounts[1] = Math.Min(val, this.lowestAmounts[1]) ;
                        set[1] = val;
                        break;
                }
            }
        this.sets.Add(set.ToArray());
        }
    }

    public int getMinimumCube(){
        return this.highestAmounts[0] * this.highestAmounts[1] * this.highestAmounts[2];
    }


    public int getGameID(){
        return gameID;
    }

    public int getValue(int[] bagInformation){
        if(isPossible(bagInformation)){
            return gameID;
        }
        return 0;
    }


    public int getScore(int[] info){
        for(int i = 0; i < 3; i++){
            if(this.highestAmounts[i] < info[i]){
                return 0;
            }
        }
        return gameID;
    }

    public Boolean isPossible(int[] bagInformation){
        foreach(int[] set in sets){
            if(!setPossible(set, bagInformation)){
                return false;
            }
        }
        return true;        
    }

    private bool setPossible(int[] set, int[] bagInformation){
        for(int i = 0;i < 3; i++){
            if(set[i] > bagInformation[i]){
                return false;
            }
        }
        return true;
    }

    public override string ToString(){
        String s = "";
        s += ("Game: " + this.gameID) + '\n';
        s += ("Highest Amounts" + '\n');
        s += ("Red : " + this.highestAmounts[0]);
        s += (" Green : " + this.highestAmounts[1]); 
        s += (" Blue : " + this.highestAmounts[2]) + '\n';
        s += ("Lowest Amounts" + '\n');
        s += ("Red : " + this.lowestAmounts[0]);
        s += (" Green : " + this.lowestAmounts[1]); 
        s += (" Blue : " + this.lowestAmounts[2]) + '\n';
        for(int i = 0; i < this.sets.Count; i++){
            s += ("Set " + i) + '\n';
            s += ("Red : " + this.sets[i][0]) + '\n';
            s += ("Green : " + this.sets[i][1]) + '\n';
            s += ("Blue : " + this.sets[i][2]) + '\n';
        }
        return s;
    }
}