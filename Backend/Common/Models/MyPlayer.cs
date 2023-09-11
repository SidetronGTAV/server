using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using Common.Dto.UserStuff;

namespace Common.Models;

public class MyPlayer : AsyncPlayer, IAsyncConvertible<MyPlayer>
{
    public int AccountId { get; set; }

    public long AccountDiscordId { get; set; }

    public bool isLoggin { get; set; }

    public int isInCharacterId { get; set; } = 0;

    public int MaxCharacters { get; set; }

    public List<CharacterSmallDto> Characters { get; set; }

    public MyPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
    {
    }

    public new MyPlayer ToAsync(IAsyncContext _) => this;
}