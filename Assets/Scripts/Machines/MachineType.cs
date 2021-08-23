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
    RIGHT_SPLITTER,
    LEFT_SPLITTER,
    BI_SPLITTER,
    TRI_SPLITTER,
    RIGHT_SELECTOR,
    LEFT_SELECTOR,
    BI_SELECTOR
}

public static class MachineTypeExtensions
{
    public static MachineType Parse(string name)
    {
        return (MachineType) Enum.Parse(typeof(MachineType), name, true);
    }
}
