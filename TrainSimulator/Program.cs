using System;
using System.Buffers;
using System.Data;

public class Program
{
    static Station station = new Station();

    //The menu
    public static void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Welcome to the station simulator");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("");

        Console.WriteLine("Choose an option: ");
        Console.WriteLine("");
        Console.WriteLine("1. Load train from file");
        Console.WriteLine("2. Load a train manually");
        Console.WriteLine("3. Start simulation (manual)");
        Console.WriteLine("4. Start simulation (automatic)");
        Console.WriteLine("5. Exit");
        Console.WriteLine("");
    }

    //The selection to load a file (.csv) with flights 
    public static void LoadFiles()
    {
        try
        {
            station.LoadTrainFromFile();
            Console.WriteLine("Files succesfully loaded from the file");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR!!!! {ex.Message}");
        }
    }
    
    //This is the selection of load manually 
    public static void LoadTrain()
    {
        Console.WriteLine("Loading your trains manually");
        Console.WriteLine("Select your train");

        int finalElection = SubMenu(); 
        
        Console.Write("ID: ");
        string id = Console.ReadLine() ?? "";
        
        Console.Write("ArrivalTime (minutes): ");
        int arrivalTime = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Type (passenger, freight): ");
        string type = Console.ReadLine() ?? "";
        
        Train? train = null;
        EStatus status = EStatus.EnRoute;

        if (finalElection == 1)
        {
            Console.Write("Number Of Carriages : ");
            int NumberOfCarriages  = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Capacity  : ");
            int Capacity  = int.Parse(Console.ReadLine() ?? "0");

            train = new PassengerTrain(id, arrivalTime, type, EStatus.EnRoute, NumberOfCarriages, Capacity);
        }
        else if (finalElection == 2)
        {
            Console.Write("Maximum Weight (kg): ");
            int maxWeight = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Freight Type: ");
            string FreightTypeype = Console.ReadLine() ?? "";
            
            train = new FreightTrain(id, arrivalTime, type, EStatus.EnRoute, maxWeight, FreightTypeype);
        }

        if (train != null)
        {
            station.AddTrainManually(train);
            Console.WriteLine("Aircraft added successfully!");
        }

        
    }                    

    public static void StartSimManual()
    {
        Console.Clear();
        Console.WriteLine("Starting simulation (manual)... ");
        Console.WriteLine("Press ENTER to advance one tick, or type 'exit' to finish.");

        string? input = "";

        do
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Current Station Status:");
            station.ShowStatus();
            station.ShowTrainStatus(); 
            Console.WriteLine("------------------------------------");

            Console.WriteLine();
            Console.Write("Press ENTER to advance tick, or type 'exit': ");
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                station.AdvanceTick(); 
            }

        } while (input != "exit");

        Console.WriteLine("Manual simulation finalized.");
    }

    public static void StartSimAuto() // Method for the automatic simulation
    {
        Console.Clear();
        Console.WriteLine("Starting simulation (automatic)... ");

        bool simulationRunning = true;
        int tickCount = 0;

        while (simulationRunning)
        {
            Console.Clear();
            Console.WriteLine($"Tick #{tickCount + 1}");

            station.AdvanceTick(); 

            Console.WriteLine("------------------------------------"); 
            Console.WriteLine("Current station Status:");
            station.ShowStatus(); //To show the platforms status
            station.ShowTrainStatus(); //To show the train status
            Console.WriteLine("------------------------------------");

            //It delays 2.5s the ticks
            Thread.Sleep(2500);

            //See if all aircrafts are on ground
            simulationRunning = !AllTrainsDocked();

            tickCount++;
        }

        Console.WriteLine("All trains are docked. Simulation finished!");
    }

    //Checks if all the trains are docked
    public static bool AllTrainsDocked()
    {
        foreach (Train train in station.GetTrainList())
        {
            if (train.getStatus() != EStatus.Docked)
            {
                return false;
            }
        }
        return true;
    }

    //The submenu to create trains manually
    public static int SubMenu()
    {
        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("1. Passenger Train");
        Console.WriteLine("2. Freight Train");
        Console.WriteLine("-------------------");

        string? input = Console.ReadLine();
        Console.WriteLine("");
        int election;
        bool rightNumber = int.TryParse(input, out election); 
        int finalElection = election;

        while (!rightNumber || finalElection < 1 || finalElection > 2)
        {
            Console.Clear();
            Console.WriteLine("-------------------");
            Console.WriteLine("1. Passenger Train");
            Console.WriteLine("2. Freight Train");
            Console.WriteLine("-------------------");
            Console.WriteLine("");
            Console.WriteLine("ERROR. Please insert a valid number: ");
            input = Console.ReadLine();
            Console.WriteLine("");
            rightNumber = int.TryParse(input, out finalElection);
        }

        return finalElection;
    }


    //Main
    public static void Main()
    {
        Console.WriteLine("Number of platsform to create");
        int number = int.Parse(Console.ReadLine() ?? "0");

        for (int i = 0; i < number; i++)
        {
            station.setPlatform(new Platform($"Platform {i}"));  
        }

        int finalElection = 0;

        do
        {
            // Print the menu
            PrintMenu();

            // Input reading
            string? input = Console.ReadLine();
            Console.WriteLine("");

            bool rightNumber = int.TryParse(input, out finalElection);

            if (!rightNumber || finalElection < 1 || finalElection > 5)
            {
                Console.WriteLine("Invalid input. Please select a number between 1 and 5.");
                continue;
            }

            switch (finalElection)
            {
                case 1:
                    LoadFiles();
                    break;
                case 2:
                    LoadTrain();
                    break;
                case 3:
                    StartSimManual();
                    break;
                case 4:
                    StartSimAuto();
                    break;
                case 5:
                    Console.WriteLine("Exiting the program...");
                    break;
            }

        } while (finalElection != 5);
    }
}