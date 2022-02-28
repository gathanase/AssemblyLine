using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SplitterMachineWindow : Window
{
    public Button closeButton;
    private Dictionary<Rotation, InputField> countFieldByRotation;
    private AbstractSplitterMachine splitterMachine;

    public void Init(AbstractSplitterMachine splitterMachine) {
        Init();
        this.splitterMachine = splitterMachine;
        this.countFieldByRotation = new Dictionary<Rotation, InputField>();
        InitPanel("RightPanel", Rotation.RIGHT);
        InitPanel("ForwardPanel", Rotation.NONE);
        InitPanel("LeftPanel", Rotation.LEFT);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    private void InitPanel(string panelName, Rotation rotation) {
        Transform panel = transform.Find("ContentPanel/" + panelName);
        panel.gameObject.SetActive(splitterMachine.GetRotations().Contains(rotation));
        Button lessButton = panel.Find("QuantityPanel/LessButton").GetComponent<Button>();
        Button moreButton = panel.Find("QuantityPanel/MoreButton").GetComponent<Button>();
        InputField inputField = panel.Find("QuantityPanel/QuantityField").GetComponent<InputField>();
        countFieldByRotation[rotation] = inputField;
        InitField(lessButton, moreButton, inputField, rotation);
    }

    private void InitField(Button lessButton, Button moreButton, InputField countField, Rotation rotation) {
        lessButton.onClick.RemoveAllListeners();
        lessButton.onClick.AddListener(() => AddCount(-1, rotation));
        moreButton.onClick.RemoveAllListeners();
        moreButton.onClick.AddListener(() => AddCount(1, rotation));
        countField.SetTextWithoutNotify(splitterMachine.countsByRotation[rotation].ToString());
        countField.onValueChanged.RemoveAllListeners();
        countField.onValueChanged.AddListener(value => {
            if (int.TryParse(value, out int count)) {
                SetCount(count, rotation);
            }
        });
    }

    private void SetCount(int count, Rotation rotation) {
        count = Mathf.Clamp(count, 1, 64);
        splitterMachine.countsByRotation[rotation] = count;
        countFieldByRotation[rotation].SetTextWithoutNotify(count.ToString());
    }

    private void AddCount(int delta, Rotation rotation) {
        int currentCount = splitterMachine.countsByRotation[rotation];
        SetCount(currentCount + delta, rotation);
    }

    public override void OnVerticalAxis(int vAxis)
    {
        if (vAxis > 0) {
            AddCount(1, Rotation.NONE);
        } else {
            foreach (Rotation rotation in splitterMachine.GetRotations()) {
                SetCount(1, rotation);
            }
        }
    }

    public override void OnHorizontalAxis(int hAxis)
    {
        Rotation rotation = hAxis > 0 ? Rotation.RIGHT : Rotation.LEFT;
        if (splitterMachine.GetRotations().Contains(rotation)) {
            AddCount(1, rotation);
        }
    }
}