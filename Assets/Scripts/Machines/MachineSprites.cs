using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSprites : MonoBehaviour
{
    private Dictionary<MachineType, Sprite> spritesByType;
    public Sprite[] spritesArray;

    void Awake() {
        LoadSprites();
    }

    private void LoadSprites() {
        spritesByType = new Dictionary<MachineType, Sprite>();
        Register(MachineType.CRAFTER, 0);
        Register(MachineType.CUTTER, 1);
        Register(MachineType.FURNACE, 2);
        Register(MachineType.LEFT_SELECTOR, 3);
        Register(MachineType.LEFT_SPLITTER, 4);
        Register(MachineType.RIGHT_SELECTOR, 5);
        Register(MachineType.RIGHT_SPLITTER, 6);
        Register(MachineType.ROLLER, 7);
        Register(MachineType.MULTI_SELECTOR, 8);
        Register(MachineType.SPLITTER, 9);
        Register(MachineType.HYDRAULIC_PRESS, 11);
        Register(MachineType.SELLER, 16);
        Register(MachineType.STARTER, 17);
        Register(MachineType.WIRE_DRAWER, 20);
        Debug.LogFormat("Loaded {0} machine sprites", spritesByType.Count);
    }

    private void Register(MachineType type, int spriteId)
    {
        spritesByType.Add(type, spritesArray[spriteId]);
    }

    public Sprite GetSprite(MachineType type)
    {
        spritesByType.TryGetValue(type, out Sprite sprite);
        return sprite;
    }
}
