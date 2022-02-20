using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SplitterMachineWindow : Window
{
    public Button closeButton;
    private Dictionary<int, InputField> countFieldByRotate;
    private AbstractSplitterMachine splitterMachine;

    public void Init(AbstractSplitterMachine splitterMachine) {
        Init();
        this.splitterMachine = splitterMachine;
        this.countFieldByRotate = new Dictionary<int, InputField>();
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
        countFieldByRotate[rotate] = inputField;
        InitField(lessButton, moreButton, inputField, rotate);
    }

    private void InitField(Button lessButton, Button moreButton, InputField countField, int rotate) {
        lessButton.onClick.RemoveAllListeners();
        lessButton.onClick.AddListener(() => AddCount(-1, rotate));
        moreButton.onClick.RemoveAllListeners();
        moreButton.onClick.AddListener(() => AddCount(1, rotate));
        countField.SetTextWithoutNotify(splitterMachine.countsByRotate[rotate].ToString());
        countField.onValueChanged.RemoveAllListeners();
        countField.onValueChanged.AddListener(value => {
            if (int.TryParse(value, out int count)) {
                SetCount(count, rotate);
            }
        });
    }

    private void SetCount(int count, int rotate) {
        count = Mathf.Clamp(count, 1, 64);
        splitterMachine.countsByRotate[rotate] = count;
        countFieldByRotate[rotate].SetTextWithoutNotify(count.ToString());
    }

    private void AddCount(int delta, int rotate) {
        int currentCount = splitterMachine.countsByRotate[rotate];
        SetCount(currentCount + delta, rotate);
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            foreach (int rotate in splitterMachine.GetRotates()) {
                SetCount(1, rotate);
            }
        }
    }

    public override void OnVerticalAxis(int vAxis)
    {
        if (vAxis > 0) {
            AddCount(1, 0);
        }
    }

    public override void OnHorizontalAxis(int hAxis)
    {
        int rotate = -hAxis;
        if (splitterMachine.GetRotates().Contains(rotate)) {
            AddCount(1, rotate);
        }
    }
}