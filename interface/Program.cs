using System.Numerics;
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
