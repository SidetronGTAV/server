namespace Controller.Utility;

public abstract class DimensionHandler
{
    public const int DefaultDimension = 0;
    private static readonly List<uint> Dimensions = new();


    public static int GetPrivateDimension()
    {
        var dimension = 1;
        while (Dimensions.Contains((uint)dimension)) dimension++;

        Dimensions.Add((uint)dimension);
        return dimension;
    }

    public static void RemovePrivateDimension(int dimension)
    {
        Dimensions.Remove((uint)dimension);
    }
}