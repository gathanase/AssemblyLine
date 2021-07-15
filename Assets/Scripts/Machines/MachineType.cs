using System;

public enum MachineType
{
    STARTER,       
    SELLER,
    CRAFTER,
    CUTTER,
    FURNACE,
    HYDRAULIC_PRESS,
    ROLLER,
    WIRE_DRAWER,
    SPLITTER,
    RIGHT_SPLITTER,
    LEFT_SPLITTER
}

public static class MachineTypeExtensions
{
    public static MachineType Parse(string name)
    {
        return (MachineType) Enum.Parse(typeof(MachineType), name, true);
    }
}
