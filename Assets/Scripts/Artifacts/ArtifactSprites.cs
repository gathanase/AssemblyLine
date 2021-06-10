using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSprites : MonoBehaviour
{
    private Dictionary<ArtifactType, Sprite> spritesByType;
    public Sprite[] spritesArray;

    void Awake() {
        LoadSprites();
    }

    private void LoadSprites() {
        spritesByType = new Dictionary<ArtifactType, Sprite>();
        Register(ArtifactType.CIRCUIT, 0);
        Register(ArtifactType.ENGINE, 1);
        Register(ArtifactType.HEATER_PLATE, 2);
        Register(ArtifactType.COOLER_PLATE, 3);
        Register(ArtifactType.LIGHT_BULB, 4);
        Register(ArtifactType.CLOCK, 5);
        Register(ArtifactType.ANTENNA, 6);
        Register(ArtifactType.BATTERY, 7);
        Register(ArtifactType.PROCESSOR, 8);
        Register(ArtifactType.POWER_SUPPLY, 9);
        Register(ArtifactType.SERVER_RACK, 10);
        Register(ArtifactType.COMPUTER, 11);
        Register(ArtifactType.GENERATOR, 12);
        Register(ArtifactType.IRON, 13);
        Register(ArtifactType.ALUMINUM, 14);
        Register(ArtifactType.GOLD, 15);
        Register(ArtifactType.COPPER, 16);
        Register(ArtifactType.DIAMOND, 17);
        Register(ArtifactType.ADVANCED_ENGINE, 18);
        Register(ArtifactType.ELECTRIC_GENERATOR, 19);
        Register(ArtifactType.SUPER_COMPUTER, 20);
        Register(ArtifactType.ELECTRIC_ENGINE, 21);
        Register(ArtifactType.AI_PROCESSOR, 22);
        Register(ArtifactType.GRILL, 23);
        Register(ArtifactType.TOASTER, 24);
        Register(ArtifactType.AIR_CONDITIONER, 25);
        Register(ArtifactType.WASHING_MACHINE, 26);
        Register(ArtifactType.SOLAR_PANEL, 27);
        Register(ArtifactType.HEADPHONES, 28);
        Register(ArtifactType.DRILL, 29);
        Register(ArtifactType.SPEAKERS, 30);
        Register(ArtifactType.RADIO, 31);
        Register(ArtifactType.JACKHAMMER, 32);
        Register(ArtifactType.TV, 33);
        Register(ArtifactType.SMARTPHONE, 34);
        Register(ArtifactType.FRIDGE, 35);
        Register(ArtifactType.TABLET, 36);
        Register(ArtifactType.MICROWAVE, 37);
        Register(ArtifactType.RAILWAY, 38);
        Register(ArtifactType.SMARTWATCH, 39);
        Register(ArtifactType.WATER_HEATER, 40);
        Register(ArtifactType.IRON_PLATE, 41);
        Register(ArtifactType.ALUMINUM_PLATE, 42);
        Register(ArtifactType.COPPER_PLATE, 43);
        Register(ArtifactType.GOLD_PLATE, 44);
        Register(ArtifactType.DIAMOND_PLATE, 45);
        Register(ArtifactType.IRON_WIRE, 46);
        Register(ArtifactType.ALUMINUM_WIRE, 47);
        Register(ArtifactType.COPPER_WIRE, 48);
        Register(ArtifactType.GOLD_WIRE, 49);
        Register(ArtifactType.DIAMOND_WIRE, 50);
        Register(ArtifactType.IRON_GEAR, 51);
        Register(ArtifactType.ALUMINUM_GEAR, 52);
        Register(ArtifactType.COPPER_GEAR, 53);
        Register(ArtifactType.GOLD_GEAR, 54);
        Register(ArtifactType.DIAMOND_GEAR, 55);
        Register(ArtifactType.IRON_LIQUID, 56);
        Register(ArtifactType.ALUMINUM_LIQUID, 57);
        Register(ArtifactType.COPPER_LIQUID, 58);
        Register(ArtifactType.GOLD_LIQUID, 59);
        Register(ArtifactType.DIAMOND_LIQUID, 60);
        Register(ArtifactType.DRONE, 61);
        Register(ArtifactType.ELECTRIC_BOARD, 62);
        Register(ArtifactType.OVEN, 63);
        Register(ArtifactType.LAZER, 64);
        Register(ArtifactType.AI_ROBOT_BODY, 65);
        Register(ArtifactType.AI_ROBOT_HEAD, 66);
        Register(ArtifactType.AI_ROBOT, 67);
        Debug.LogFormat("Loaded {0} artifact sprites", spritesByType.Count);
    }

    private void Register(ArtifactType type, int spriteId)
    {
        spritesByType.Add(type, spritesArray[spriteId]);
    }

    public Sprite GetSprite(ArtifactType type)
    {
        spritesByType.TryGetValue(type, out Sprite sprite);
        return sprite;
    }
}
