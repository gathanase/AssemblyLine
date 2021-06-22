using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterMachineWindow : Window
{
    public Dropdown typeField;
    public Slider quantityField;
    public Button closeButton;
    private List<ArtifactType> artifactTypes;
    private StarterMachine starterMachine;

    public void Init(StarterMachine starterMachine) {
        Init();
        this.starterMachine = starterMachine;

        artifactTypes = new List<ArtifactType>()
                { ArtifactType.IRON, ArtifactType.ALUMINUM, ArtifactType.GOLD, ArtifactType.COPPER, ArtifactType.DIAMOND };
        typeField.options = artifactTypes.ConvertAll<Dropdown.OptionData>(type => new Dropdown.OptionData(type.ToString(), artifactSprites.GetSprite(type)));
        typeField.SetValueWithoutNotify(artifactTypes.IndexOf(starterMachine.artifactType));
        quantityField.SetValueWithoutNotify(starterMachine.quantity);
        typeField.onValueChanged.RemoveAllListeners();
        typeField.onValueChanged.AddListener(ev => starterMachine.artifactType = artifactTypes[typeField.value]);
        quantityField.onValueChanged.RemoveAllListeners();
        quantityField.onValueChanged.AddListener(ev => starterMachine.quantity = (int) quantityField.value);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }
}