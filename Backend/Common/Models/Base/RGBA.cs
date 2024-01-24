using AltV.Net.Data;

namespace Common.Models.Base;

public class RGBA
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }
    
    public static implicit operator Rgba(RGBA rgba) => new (rgba.R, rgba.G, rgba.B, rgba.A);
}