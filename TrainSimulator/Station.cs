public class Station
{
    private List<Platform> Platforms;

    private List<Train> Trains;

    public Station()
    {
        this.Platforms = new List<Platform>();
        this.Trains = new List<Train>();
    }

    public void setPlatform(Platform p)
    {
        this.Platforms.Add(p);
    }

    public void ShowStatus()
    {
        foreach (Platform p in this.Platforms)
        {
            Console.Write($"Platform {p.getID()}. Status: ");
            if (p.getStatus() == PlatformStatus.Occupied)
            {
                Console.Write("Busy. ");
                Train? currentTrain = p.getTrain();

                if (currentTrain != null)
                {
                    Console.Write($"Train: {currentTrain.getID()}. Remaining ticks: {p.getCurrentTicks()}\n");
                }
            }
            else
            {
                Console.Write("Free\n");
            }
        }
    }

    //This method is needed to stop the automatic simulation when all the trains are docked
    public List<Train> GetTrainList()
    {
        return this.Trains;
    }

    //To show the status if the trains each tick
    public void ShowTrainStatus()
    {
        Console.WriteLine();
        Console.WriteLine("Trains Status:");

        foreach (Train train in this.Trains)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Train ID: {train.getID()}");
            Console.WriteLine($"Status: {train.getStatus()}");
            Console.WriteLine($"Arrival Time: {train.getArrivalTime()} minutes");
            Console.WriteLine($"Type: {train.getType()}");
        }
    }

    //The method for the manual simulation (third option in the menu)
    public void AdvanceTick()
    {
        // Update Platforms first
        foreach (Platform p in this.Platforms)
        {
            if (p.updatePlatform() == 0 && p.getTrain() != null)
            {
                p.ReleasePlatform();
            }
        }

        // Update Trains
        foreach (Train r in this.Trains)
        {
            // Calculates the arrival time lef
            int time = r.getArrivalTime() - 15;

            // The arrival time is updated only if the train is in EnRoute or Waiting.
            if (r.getStatus() == EStatus.EnRoute || r.getStatus() == EStatus.Waiting)
            {
                r.setArrivalTime(time);

                // We confirm that there is no negative arrival time
                if (r.getArrivalTime() < 0)
                    r.setArrivalTime(0);
            }

            // It changes the status of the train depending on the arrival time
            if (r.getArrivalTime() == 0 && r.getStatus() == EStatus.EnRoute)
            {
                r.setStatus(EStatus.Waiting);
            }
            else if (r.getStatus() == EStatus.Waiting)
            {
                Platform? freePlatform = SearchPlatform();

                if (freePlatform != null)
                {
                    freePlatform.RequestPlatform(r); // It assigns a platform and changes to Docking
                }
                else
                {
                    Console.WriteLine($"[INFO] No free platform available for train {r.getID()}. Still waiting...");
                }
            }

        }
    }


    //This method is for search platforms that are free due to arrive the trains
    private Platform? SearchPlatform()
    {
        foreach (Platform r in this.Platforms)
        {
            if (r != null)
            {
                if (r.getStatus() == PlatformStatus.Free)
                {
                    return r;
                }
            }
        }

        return null;
    }

    private int ReadInt(string message)
    {
        int result;
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out result))
                return result;
            Console.WriteLine("Por favor, introduce un número entero válido.");
        }
    }

    //This is the constructor of the trains that are created to save all the values into it.
    private void GetDataByUser(ref string id, ref int ArrivalTime, ref string Type)
    {
        //Pedir usuario datos tren
        Console.Write("ID: ");
        id = Console.ReadLine() ?? string.Empty;
        ArrivalTime = ReadInt("Arrival Time: ");
        Console.Write("Type: ");
        Type = Console.ReadLine() ?? string.Empty;
    }

    //This method is for the creation of the aircrafts manually
    public void AddTrain()
    {
        bool secondRound = false;

        do
        {
            Console.WriteLine();
            Console.WriteLine("1. Passenger Train");
            Console.WriteLine("2. Freight Train");
            Console.Write("Select a type aircraft: ");

            if(int.TryParse(Console.ReadLine(), out int option))
            {
                if(option < 1 || option > 2)
                {
                    if (secondRound)
                    {
                        throw new ArgumentException("Opción fuera de rango");
                    }
                    secondRound = true;
                }
                else
                {
                    string id = "";
                    int ArrivalTime = 0;
                    string Type = "";
                    EStatus status = EStatus.EnRoute;

                    GetDataByUser(ref id, ref ArrivalTime, ref Type);
                    Train? r = null;

                    switch (option)
                    {
                        case 1:
                            int numberOfCarriages = ReadInt("Number of Carriages");
                            int capacity = ReadInt("Capacity:");

                            //Creas objeto
                            r = new PassengerTrain(id, ArrivalTime, Type, status, numberOfCarriages, capacity);
                            break;
                        case 2:
                            int MaxWeight = ReadInt("Max Weight: ");
                            Console.Write("Freight Type: ");
                            string FreightType = Console.ReadLine() ?? string.Empty; 

                            r = new FreightTrain(id, ArrivalTime, Type, status, MaxWeight, FreightType);
                            break;
                    }

                    if(r != null)
                    {
                        this.Trains.Add(r);
                    }
                }
            }
            else
            {
                if(secondRound)
                {
                    throw new ArgumentOutOfRangeException("Caracter no válido");
                }
                secondRound = true;
            }
        } while(secondRound);   
    }

    //This method is for the first option of the menu
    public void LoadTrainFromFile()
    {
        StreamReader? sr = null;

        //With the try and catches we ensure that the file is correct
        try
        {
            Console.Write("Input a file path: ");
            string? path = Console.ReadLine();
            string separator = ",";

            if(path == null)
            {
                throw new ArgumentException("No file indicated");
            }

            sr = File.OpenText(path);
            string? line = "";

            while((line = sr.ReadLine()) != null)
            {
                LoadLine(separator, line);
            }
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine("ERROR: Unfound file");
        }
        catch(ArgumentException)
        {
            Console.WriteLine("ERROR: Unfound file");
        }
        finally
        {
            if(sr != null) sr.Close();
        }
    }

    //This method is for the second one in the menu. This add the train created manually to the list of trains to simule later.
    public void AddTrainManually(Train train)
    {
        this.Trains.Add(train);
    }


    private void LoadLine(string separator, string line)
    {
        string[] values = line.Split(separator);
        string id = values[0];

        string statusString = values[1];
        EStatus status;

        switch (statusString)
        {
            case "EnRoute":
                status = EStatus.EnRoute;
                break;
            case "Waiting":
                status = EStatus.Waiting;
                break;
            case "Docking":
                status = EStatus.Docking;
                break;
            case "Docked":
                status = EStatus.Docked;
                break;
            default:
                throw new ArgumentException("Status invalid");
        }

        int arrivalTime = int.Parse(values[2]);
        string type = values[3];


        Train r;

        switch (type)
        {
            case "Passenger":
                int numberOfCarriages = int.Parse(values[4]);
                int capacity = int.Parse(values[5]);

                r = new PassengerTrain(id, arrivalTime, type, EStatus.EnRoute, numberOfCarriages, capacity);
                break;

            case "Freight":
                int maxWeight = int.Parse(values[4]);
                string freightType = values[5];

                r = new FreightTrain(id, arrivalTime, type, EStatus.EnRoute, maxWeight, freightType);
                break;
                
            default:
                throw new ArgumentException("Type of train invalid");
        }

        this.Trains.Add(r);
    }
}

