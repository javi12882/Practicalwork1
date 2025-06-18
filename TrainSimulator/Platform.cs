public enum PlatformStatus // The different status of the platform
{
    Free,
    Occupied
}

public class Platform  
{
    private string id;
    private PlatformStatus Status;
    private Train? CurrentTrain;
    private int DockingTime;
    private int CurrentTicks;

    public Platform(string id)
    {
        this.id = id;
        DockingTime = 2;
        Status = PlatformStatus.Free;
        this.CurrentTrain = null;
    }

    public PlatformStatus getStatus()
    {
        return this.Status;
    }

    public string getID()
    {
        return this.id;
    }

    public Train? getTrain()
    {
        return this.CurrentTrain;
    }

    public int getCurrentTicks()
    {
        return this.CurrentTicks;
    }

    //It assigns an aircraft to land on this platform.
    public void RequestPlatform(Train s)
    {
        if(Status == PlatformStatus.Free)
        {
            Status = PlatformStatus.Occupied;
            CurrentTrain = s;
            this.CurrentTicks = this.DockingTime;
            this.CurrentTrain.setStatus(EStatus.Docking);
        }
    }

    //It frees the platform once the traion has docked.
    public void ReleasePlatform()
    {
        if (Status == PlatformStatus.Occupied && CurrentTrain != null)
        {
            Status = PlatformStatus.Free;
            CurrentTrain.setStatus(EStatus.Docked);
            CurrentTrain = null;
            this.CurrentTicks = 0;
        }
    }

    public int updatePlatform() // It updates the Platform status after releasing it
    {
        if(this.CurrentTrain != null)
        {
            if(this.CurrentTrain.getStatus() == EStatus.Docking)
            {
                if (this.CurrentTicks > 0)
                {
                    this.CurrentTicks--;
                }
            }
        }

        return this.CurrentTicks;
    }

    public void ShowInfo() // Show the Runway Status cases
    {
        Console.Write(id);
        switch(Status)
        {
            case PlatformStatus.Free:
                Console.WriteLine(" Status: Free");
                break;
            case PlatformStatus.Occupied:
                Console.WriteLine(" Status: Occupied");
                if(this.CurrentTrain != null) this.CurrentTrain.ShowInfo();
                break;
        }
    }
}