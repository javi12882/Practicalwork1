public enum EStatus
{
    EnRoute,
    Waiting,
    Docking,
    Docked
}


public abstract class Train
{
    private string ID;
    private EStatus Status;
    
    private int ArrivalTime; // In minutes
    
    private string Type;

    
    public Train(string id, int ArrivalTime, string Type, EStatus status)
    {
        this.Status = status;
        this.ID = id;
        this.ArrivalTime = ArrivalTime;
        this.Type = Type;
    }

    public string getID()
    {
        return this.ID;
    }
    public int getArrivalTime()
    {
        return this.ArrivalTime;
    }
    public string getType()
    {
        return this.Type;
    }

    public void setType(string type)
    {
        this.Type = type;
    }

    public void setID(string d)
    {
        this.ID = d;
    }
    public void setArrivalTime(int d)
    {
        if(d < 0)
        {
            d = 0;
        }

        this.ArrivalTime = d;
    }

    public void setStatus(EStatus e)
    {
        this.Status = e;
    }

    public EStatus getStatus()
    {
        return this.Status;
    }

    public abstract void ShowInfo();
}

public class PassengerTrain  : Train //Passenger Train subclass added by inheritance 
{
    private int NumberOfCarriages; //Specific attribute of the Passenger Train
    private int Capacity; //Specific attribute of the Passenger Train

    public PassengerTrain(string id, int ArrivalTime, string Type, EStatus status, int numberOfCarriages, int Capacity) : base(id, ArrivalTime, Type, status)
    {
        this.NumberOfCarriages = numberOfCarriages;
        this.Capacity = Capacity;
    }

    public int GetNumberOfCarriages()
    {
        return this.NumberOfCarriages;
    }

    public int GetCapacity()
    {
        return this.Capacity;
    }

    public override void ShowInfo() //We call the ShowInfo method in order to show the info about the Passenger Train
    {
        Console.WriteLine($"Passenger Train ID: {getID()}");
        Console.WriteLine($"Passengers: {NumberOfCarriages}");
        Console.WriteLine($"Capacity: {Capacity}");
        Console.WriteLine($"Passenger Train arrival time: {getArrivalTime()}");
        Console.WriteLine($"Passenger Train Status: {getStatus()}");
    }
}

public class FreightTrain  : Train //Freight Train subclass added by inheritance
{
    private int MaxWeight ; //Specific attribute of the Freight Train
    private string FreightType; //Specific attribute of the Freight Train

    public FreightTrain(string id, int ArrivalTime, string Type, EStatus status, int MaxWeight, string FreightType) : base(id, ArrivalTime, Type, status)
    {
        this.MaxWeight = MaxWeight;
        this.FreightType = FreightType;
    }

    public int GetMaxWeight()
    {
        return this.MaxWeight;
    }

    public string GetFreightType()
    {
        return this.FreightType;
    }

    public override void ShowInfo() //We call the ShowInfo method in order to show info about the Freight Train
    {
        Console.WriteLine($"Freight Train ID: {getID()}");
        Console.WriteLine($"Maximum weight of the Freight Train: {MaxWeight}");
        Console.WriteLine($"Type of Freight Train: {FreightType}");
        Console.WriteLine($"Freight Train arrival time: {getArrivalTime()}");
        Console.WriteLine($"Freight Train Status: {getStatus()}");
    }

}