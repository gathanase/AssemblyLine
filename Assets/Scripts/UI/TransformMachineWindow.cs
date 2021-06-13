using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformMachineWindow : Window
{
    // public Dropdown typeField;
    // public Slider quantityField;
    // public List<ArtifactType> artifactTypes;
    private TransformMachine transformMachine;

    public void Init(TransformMachine transformMachine, GameController gameController) {
        Init(gameController);
        this.transformMachine = transformMachine;

        // artifactTypes = new List<ArtifactType>()
        //         { ArtifactType.IRON, ArtifactType.ALUMINUM, ArtifactType.GOLD, ArtifactType.COPPER, ArtifactType.DIAMOND };
        // typeField.options = artifactTypes.ConvertAll<Dropdown.OptionData>(type => new Dropdown.OptionData(type.ToString()));
        // typeField.SetValueWithoutNotify(artifactTypes.IndexOf(starterMachine.artifactType));
        // quantityField.SetValueWithoutNotify(starterMachine.quantity);
    }
}