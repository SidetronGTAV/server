using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using Common.Dto.UserStuff;
using Common.Enums;

namespace Common.Models;

public class MyPlayer : AsyncPlayer, IAsyncConvertible<MyPlayer>
{
    public int AccountId { get; set; }

    public long AccountDiscordId { get; set; }

    public bool IsLoggin { get; set; }

    public int IsInCharacterId { get; set; } = 0;

    public bool IsCharacterDead { get; set; }

    public DateTime? AtCharacterDied { get; set; }

    public int MaxCharacters { get; set; }

    public SupportLevel SupportLevel { get; set; }

    public List<CharacterSmallDto> Characters { get; set; }

    public MyPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
    {
    }

    public new MyPlayer ToAsync(IAsyncContext _) => this;
}