using AltV.Net;
using AltV.Net.Elements.Entities;

namespace Common.Models;

public class MyPlayer : Player
{
    public string UserName { get; set; }
    
    public MyPlayer(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
    {
    }
}