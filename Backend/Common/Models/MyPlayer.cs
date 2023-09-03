using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;

namespace Common.Models;

public class MyPlayer : AsyncPlayer, IAsyncConvertible<MyPlayer>
{
    public int AccountId { get; set; }
    
    public long AccountDiscordId { get; set; }
    
    public bool isLoggin { get; set; }
    
    public bool isInCharacter { get; set; }
    
    public MyPlayer(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
    {
    }

    public new MyPlayer ToAsync(IAsyncContext _) => this;
}