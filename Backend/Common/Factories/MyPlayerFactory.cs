using AltV.Net;
using AltV.Net.Elements.Entities;
using Common.Models;

namespace Common.Factories;

public class MyPlayerFactory : IEntityFactory<IPlayer>
{
    public IPlayer Create(ICore core, IntPtr playerPointer, ushort id)
    {
        return new MyPlayer(core, playerPointer, id);
    }
}