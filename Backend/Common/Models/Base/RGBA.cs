using AltV.Net.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Models.Base;

[Owned]
public class RGBA
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }
    
    public static implicit operator Rgba(RGBA rgba) => new (rgba.R, rgba.G, rgba.B, rgba.A);
}