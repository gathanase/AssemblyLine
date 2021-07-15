using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SplitterMachineWindow : Window
{
    public Button closeButton;
    private AbstractSplitterMachine splitterMachine;

    public void Init(AbstractSplitterMachine splitterMachine) {
        Init();
        this.splitterMachine = splitterMachine;
        InitPanel("RightPanel", -1);
        InitPanel("ForwardPanel", 0);
        InitPanel("LeftPanel", 1);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    private void InitPanel(string panelName, int rotate) {
        Transform panel = transform.Find("ContentPanel/" + panelName);
        panel.gameObject.SetActive(splitterMachine.GetRotates().Contains(rotate));
        Button lessButton = panel.Find("QuantityPanel/LessButton").GetComponent<Button>();
        Button moreButton = panel.Find("QuantityPanel/MoreButton").GetComponent<Button>();
        InputField inputField = panel.Find("QuantityPanel/QuantityField").GetComponent<InputField>();
        InitField(lessButton, moreButton, inputField, rotate);
    }

    private void InitField(Button lessButton, Button moreButton, InputField countField, int rotate) {
        lessButton.onClick.RemoveAllListeners();
        lessButton.onClick.AddListener(() => SetCount(countField, splitterMachine.countsByRotate[rotate] - 1, rotate));
        moreButton.onClick.RemoveAllListeners();
        moreButton.onClick.AddListener(() => SetCount(countField, splitterMachine.countsByRotate[rotate] + 1, rotate));
        countField.SetTextWithoutNotify(splitterMachine.countsByRotate[rotate].ToString());
        countField.onValueChanged.RemoveAllListeners();
        countField.onValueChanged.AddListener(value => {
            if (int.TryParse(value, out int count)) {
                SetCount(countField, count, rotate);
            }
        });
    }

    private void SetCount(InputField countField, int count, int rotate) {
        count = Mathf.Clamp(count, 1, 64);
        splitterMachine.countsByRotate[rotate] = count;
        countField.SetTextWithoutNotify(count.ToString());
    }
}