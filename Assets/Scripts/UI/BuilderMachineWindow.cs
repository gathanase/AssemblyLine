using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderMachineWindow : Window
{
    // public Dropdown typeField;
    // public Slider quantityField;
    // public List<ArtifactType> artifactTypes;
    private BuilderMachine builderMachine;

    public void Init(BuilderMachine builderMachine, GameController gameController) {
        Init(gameController);
        this.builderMachine = builderMachine;

        // artifactTypes = new List<ArtifactType>()
        //         { ArtifactType.IRON, ArtifactType.ALUMINUM, ArtifactType.GOLD, ArtifactType.COPPER, ArtifactType.DIAMOND };
        // typeField.options = artifactTypes.ConvertAll<Dropdown.OptionData>(type => new Dropdown.OptionData(type.ToString()));
        // typeField.SetValueWithoutNotify(artifactTypes.IndexOf(starterMachine.artifactType));
        // quantityField.SetValueWithoutNotify(starterMachine.quantity);
    }

    public void OnChange() {
        // starterMachine.artifactType = artifactTypes[typeField.value];
        // starterMachine.quantity = (int) quantityField.value;
    }
}