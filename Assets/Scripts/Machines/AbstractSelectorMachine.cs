using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSelectorMachine : Machine
{
    public ArtifactType leftType;
    public ArtifactType rightType;

    public AbstractSelectorMachine() {
        leftType = ArtifactType.AI_ROBOT;
        rightType = ArtifactType.AI_ROBOT;
    }

    public new class Save : Machine.Save {
        public string leftType;
        public string rightType;
    }

    public override Machine.Save ToSave()
    {
        Save save = new Save();
        base.WriteSave(save);
        save.leftType = leftType.ToString();
        save.rightType = rightType.ToString();
        return save;
    }

    public override void Init(Machine.Save _save, FactoryFloor floor, GameDatabase gameDatabase)
    {
        Save save = (Save) _save;
        base.Init(save, floor, gameDatabase);
        leftType = ArtifactTypeExtensions.Parse(save.leftType);
        rightType = ArtifactTypeExtensions.Parse(save.rightType);
    }

    public override Window CreateInfoWindow() {
        SelectorMachineWindow infoWindow = FindObjectOfType<SelectorMachineWindow>(true);
        infoWindow.Init(this);
        return infoWindow;
    }

    public abstract bool isRightEnabled();
    public abstract bool isLeftEnabled();

    public override void Feed(Artifact artifact) {
        if (artifact.direction == this.direction) {
            // artifact comes from behind
            int rotate = 0;
            if (isLeftEnabled() && artifact.type == leftType) {
                rotate = 1;
            } else if (isRightEnabled() && artifact.type == rightType) {
                rotate = -1;
            }
            artifact.direction = this.direction.Rotate(rotate);
        } else {
            Remove(artifact);
        }
    }

    public override void OnTick() {
    }
}