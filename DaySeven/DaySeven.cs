using System.Linq;

namespace DaySeven;
public class DaySeven{

    public static int run(){
        return doPart1();
    }

    public static int doPart1(){
        try{
            StreamReader sr = new StreamReader("../DaySeven/TextExamples/Messages.txt");
            string? line = sr.ReadLine();
            Dictionary<String,int> values = new Dictionary<string, int>();
            while(line != null){
                (string, int) parsed = parse(line);
                values.Add(parsed.Item1, parsed.Item2);
                line = sr.ReadLine();
            }
            List<string> hands = values.Keys.ToList();
            hands.Sort(compareHands);
            hands.Reverse();
            int score = 0; 
            for(int i = 0; i < hands.Count;i++){
                score += (i + 1) * values[hands[i]];
            }
            return score;
        } catch(Exception e){
            Console.WriteLine(e);
        }
        return 0;
    }

    private static (string,int) parse(string line){
        String[] parts = line.Split(" ");
        return (parts[0],int.Parse(parts[1]));
    }

    private static int compareHands(string hand1, string hand2){
        int hand1Score = findScore(hand1);
        int hand2Score = findScore(hand2);
        if(hand1Score < hand2Score){
            return 1;
        } else if( hand1Score == hand2Score){
            return highestHand(hand1,hand2);
        } else {
            return -1;
        }
    }

    private static int highestHand(string hand1, string hand2){
        List<int> hand1I = toIntArray(hand1);
        List<int> hand2I = toIntArray(hand2);
        for(int i = 0; i < hand1.Length;i++){
            if(hand1I[i] < hand2I[i]){
                return 1;
            } else if(hand1I[i] != hand2I[i]){
                return -1;
            }
        }
        return 0;
    }

    private static List<int> toIntArray(String hand){
        return hand.ToCharArray().ToList()
        .Select(c => {
            switch(c){
                case 'A':
                    return 14;
                case 'K':
                    return 13;
                case 'Q':
                    return 12;
                case 'J':
                    return 1;
                case 'T':
                    return 10;
                default:
                    return c - 0x30;
        }}).ToList();
    }

    /* Part 1 
    
        private static List<int> toIntArray(String hand){
        return hand.ToCharArray().ToList()
        .Select(c => {
            switch(c){
                case 'A':
                    return 14;
                case 'K':
                    return 13;
                case 'Q':
                    return 12;
                case 'J':
                    return 11;
                case 'T':
                    return 10;
                default:
                    return c - 0x30;
        }}).ToList();
    }
    
    private static int findScore(string hand){
        List<char> chars = hand.ToList();
        chars.Sort();
        int most = 0;
        int cur = 1;
        List<int> repeats = new List<int>();
        char current = chars[0];
        for(int i = 1; i < chars.Count;i++){
            if(chars[i] == current){
                cur++;
            } else {
                most = Math.Max(most, cur);
                repeats.Add(cur);
                current = chars[i];
                cur = 1;
            }
        }
        most = Math.Max(most, cur);
        repeats.Add(cur);
        if(most == 1){
            return 0;
        }
        if(most == 2){
            if(repeats.Count == 3){
                return 2;
            } 
            return 1;    
        }
        if(most == 3){
            if(repeats.Count == 2){
                return 4;
            } 
            return 3;
        } else {
            return most + 1;
        }
    }


     */

    private static int findScore(string hand){
        List<char> chars = hand.ToList();
        int joker = 0;
        chars.ForEach(x => {
            if(x == 'J'){
                joker++;
            }
        });
        chars = chars.Where(x => x != 'J').ToList();
        Console.WriteLine();
        if(chars.Count < 2){
           return 6;
        }
        chars.Sort();
        int most = 0;
        int cur = 1;
        List<int> repeats = new List<int>();
        char current = chars[0];
        for(int i = 1; i < chars.Count;i++){
            if(chars[i] == current){
                cur++;
            } else {
                most = Math.Max(most, cur);
                repeats.Add(cur);
                current = chars[i];
                cur = 1;
            }
        }
        most = Math.Max(most, cur);
        repeats.Add(cur);
        most += joker;
        if(most == 1){
            return 0;
        }
        if(most == 2){
            if(repeats.Count == 3){
                return 2;
            } 
            return 1;    
        }
        if(most == 3){
            if(repeats.Count == 2){
                return 4;
            } 
            return 3;
        } else {
            return most + 1;
        }
    }
}
