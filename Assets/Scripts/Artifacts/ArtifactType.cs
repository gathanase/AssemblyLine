using System;

public enum ArtifactType
{
    IRON, ALUMINUM, GOLD, COPPER, DIAMOND,
    IRON_PLATE, ALUMINUM_PLATE, COPPER_PLATE, GOLD_PLATE, DIAMOND_PLATE,
    IRON_WIRE, ALUMINUM_WIRE, COPPER_WIRE, GOLD_WIRE, DIAMOND_WIRE,
    IRON_GEAR, ALUMINUM_GEAR, COPPER_GEAR, GOLD_GEAR, DIAMOND_GEAR,
    IRON_LIQUID, ALUMINUM_LIQUID, COPPER_LIQUID, GOLD_LIQUID, DIAMOND_LIQUID,
    HEATER_PLATE, COOLER_PLATE,
    LIGHT_BULB, CLOCK, ANTENNA, BATTERY, POWER_SUPPLY,
    CIRCUIT, ELECTRIC_BOARD, PROCESSOR, COMPUTER, SERVER_RACK, SUPER_COMPUTER, AI_PROCESSOR,
    ENGINE, ELECTRIC_ENGINE, ADVANCED_ENGINE,
    GENERATOR, ELECTRIC_GENERATOR,
    AIR_CONDITIONER, SOLAR_PANEL,
    WASHING_MACHINE, GRILL, TOASTER, FRIDGE, MICROWAVE, WATER_HEATER, OVEN,
    HEADPHONES, SPEAKERS, RADIO,
    DRILL, JACKHAMMER, RAILWAY,
    TV, SMARTPHONE, TABLET, SMARTWATCH, DRONE, LAZER,
    AI_ROBOT_BODY, AI_ROBOT_HEAD, AI_ROBOT
}

public static class ArtifactTypeExtensions
{
    public static ArtifactType Parse(string name)
    {
        return (ArtifactType) Enum.Parse(typeof(ArtifactType), name, true);
    }
}