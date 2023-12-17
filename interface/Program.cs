using DayOne;

class Interface {
    static void Main(string[] args) {
        Console.WriteLine("Hello, What day of advent would you like to run");
        int input = getInput();
        switch(input){
            case 1:
                Console.WriteLine(Runner.run());
                break;
            case 2:
                Console.WriteLine(DayTwo.DayTwo.run()); 
                break;
            case 3: 
                Console.WriteLine(DayThree.DayThree.run());
                break;
            case 4:
                Console.WriteLine(DayFour.DayFour.run());
                break;
            case 5:
                Console.WriteLine(DayFive.Attempt2.run());
                break;
            case 6: 
                Console.WriteLine(DaySix.DaySix.run());
                break;
            case 7: 
                Console.WriteLine(DaySeven.DaySeven.run());
                break;
            case 8: 
                Console.WriteLine(DayEight.DayEight.run());
                break;
            case 9: 
                Console.WriteLine(DayNine.DayNine.run());
                break;
            case 10:
                Console.WriteLine(Day10.Day10.run());
                break;
            case 11:
                Console.WriteLine(Day11.Day11.run());
                break;
            case 12:
                Console.WriteLine(Day12.Day12.run());
                break;
            default:
                Console.WriteLine("Sorry, I have not done that day of advent yet :)");
                break;
        }
    }

    private static int getInput(){  
        while(true){
            string? input = Console.ReadLine();
            if(input == null){
                Console.WriteLine("No value has been entered please enter a day of advent(1-25)");
                continue;
            }
            int number;
            try {
                number = Int32.Parse(input);
            } catch(Exception){
                Console.WriteLine("Value entered is not a number please enter a day of advent(1-25)");
                continue;
            }
            if(number < 1 || number > 25){
                Console.WriteLine("Number entered is not a a day of advent, please enter a day of advent(1-25)");
                continue;
            }
            return number;
        } 
    }
}
