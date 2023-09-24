using AltV.Net.Data;

namespace Common.Enums;

public abstract class GlobalPosition
{
    public static readonly Position PlayerDiedSpawnPosition = new (56.53f, -295.140f, 47.39f);

    public static readonly Position PlayerLoginPosition = new(-1562.5055f, -579.6528f, 108.50769f);

    public static readonly Position NewPlayerSpawnPosition = new(0, 0, 72);
    
    public static readonly Position HospitalSpawnPosition = new(310.07f, -580.10f, 43.28f);
}