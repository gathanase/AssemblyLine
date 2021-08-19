using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SelectorMachineWindow : Window
{
    public Button closeButton;
    private AbstractSelectorMachine selectorMachine;
    private static List<ArtifactType> artifactTypes = Enum.GetValues(typeof(ArtifactType)).Cast<ArtifactType>().ToList();

    public void Init(AbstractSelectorMachine selectorMachine) {
        Init();
        this.selectorMachine = selectorMachine;

        Transform leftPanel = transform.Find("ContentPanel/LeftPanel");
        leftPanel.gameObject.SetActive(selectorMachine.isLeftEnabled());
        Dropdown leftField = leftPanel.Find("LeftDropdown").GetComponent<Dropdown>();
        leftField.options = artifactTypes.ConvertAll<Dropdown.OptionData>(type => 
            new Dropdown.OptionData(type.ToString(), gameDatabase.GetSprite(type))
        );
        leftField.SetValueWithoutNotify(artifactTypes.IndexOf(selectorMachine.leftType));
        leftField.onValueChanged.RemoveAllListeners();
        leftField.onValueChanged.AddListener(ev => selectorMachine.leftType = artifactTypes[leftField.value]);

        Transform rightPanel = transform.Find("ContentPanel/RightPanel");
        rightPanel.gameObject.SetActive(selectorMachine.isRightEnabled());
        Dropdown rightField = rightPanel.Find("RightDropdown").GetComponent<Dropdown>();
        rightField.options = artifactTypes.ConvertAll<Dropdown.OptionData>(type => 
            new Dropdown.OptionData(type.ToString(), gameDatabase.GetSprite(type))
        );
        rightField.SetValueWithoutNotify(artifactTypes.IndexOf(selectorMachine.rightType));
        rightField.onValueChanged.RemoveAllListeners();
        rightField.onValueChanged.AddListener(ev => selectorMachine.rightType = artifactTypes[rightField.value]);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }
}
