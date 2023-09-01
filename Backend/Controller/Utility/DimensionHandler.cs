namespace Controller.Utility;

public class DimensionHandler
{
    private static List<uint> _dimensions = new();
    public static readonly int DefaultDimension = 0;
    
    
    public static int GetPrivateDimension()
    {
        var dimension = 1;
        while (_dimensions.Contains((uint)dimension))
        {
            dimension++;
        }

        _dimensions.Add((uint)dimension);
        return dimension;
    }
    
    public static void RemovePrivateDimension(int dimension)
    {
        _dimensions.Remove((uint)dimension);
    }
}