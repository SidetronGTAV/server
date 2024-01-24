using Common.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Common.Models.Entity;

[Owned]
public class VehicleData
{
    public string AppearanceData { get; set; }
    
    public uint BodyHealth { get; set; }
    
    public string DamageData { get; set; }
    
    public byte DashboardColor { get; set; }
    
    public string HealthData { get; set; }
    
    public int EngineHealth { get; set; }
    
    public uint CounterMeasureCount { get; set; }
    
    public bool CustomTires { get; set; }
    
    public byte DirtLevel { get; set; }
    
    public bool DriftMode { get; set; }
    
    public byte HeadlightColor { get; set; }
    
    public bool HybridExtraActive { get; set; }
    
    public byte HybridExtraState { get; set; }
    
    public byte InteriorColor { get; set; }
    
    public bool IsRoofClosed { get; set; }
    
    public byte LightsMultiplier { get; set; }
    
    public byte LockState { get; set; }
    
    public uint NumberplateIndex { get; set; }
    
    public string NumberplateText { get; set; }
    
    public byte PearlColor { get; set; }
    
    public int PetrolTankHealth { get; set; }
    
    public RGBA PrimaryColorRgb { get; set; }
    
    public RGBA SecondaryColorRgb { get; set; }
    
    public byte RearWheel { get; set; }
    
    public byte RoofLivery { get; set; }
    
    public byte ScriptMaxSpeed { get; set; }
    
    public byte SpecialDarkness { get; set; }
    
    public byte WheelColor { get; set; }
    
    public byte WindowTint { get; set; }
    
    public short[] Mods { get; set; }
    
    public byte Livery { get; set; }
}