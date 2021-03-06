using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterMachineWindow : Window
{
    public Button closeButton;
    public Dropdown typeField;
    public Button lessButton;
    public Button moreButton;
    public InputField quantityField;
    private List<ArtifactType> artifactTypes;
    private StarterMachine starterMachine;

    public void Init(StarterMachine starterMachine) {
        Init();
        this.starterMachine = starterMachine;

        artifactTypes = new List<ArtifactType>()
                { ArtifactType.ALUMINUM, ArtifactType.COPPER, ArtifactType.DIAMOND, ArtifactType.GOLD, ArtifactType.IRON };
        typeField.options = artifactTypes.ConvertAll<Dropdown.OptionData>(type => new Dropdown.OptionData(type.ToString(), gameDatabase.GetSprite(type)));
        typeField.options.Sort((a, b) => a.text.CompareTo(b.text));

        typeField.SetValueWithoutNotify(artifactTypes.IndexOf(starterMachine.artifactType));
        typeField.onValueChanged.RemoveAllListeners();
        typeField.onValueChanged.AddListener(ev => starterMachine.artifactType = artifactTypes[typeField.value]);
        lessButton.onClick.RemoveAllListeners();
        lessButton.onClick.AddListener(() => SetQuantity(starterMachine.quantity - 1));
        moreButton.onClick.RemoveAllListeners();
        moreButton.onClick.AddListener(() => SetQuantity(starterMachine.quantity + 1));
        quantityField.SetTextWithoutNotify(starterMachine.quantity.ToString());
        quantityField.onValueChanged.RemoveAllListeners();
        quantityField.onValueChanged.AddListener(value => {
            if (int.TryParse(value, out int quantity)) {
                SetQuantity(quantity);
            }
        });
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    private void SetQuantity(int quantity) {
        starterMachine.quantity = Mathf.Clamp(quantity, 0, 3);
        quantityField.SetTextWithoutNotify(starterMachine.quantity.ToString());
    }

    public override void OnVerticalAxis(int vAxis)
    {
        typeField.value -= vAxis;
    }

    public override void OnHorizontalAxis(int hAxis)
    {
        SetQuantity(starterMachine.quantity + hAxis);
    }
}