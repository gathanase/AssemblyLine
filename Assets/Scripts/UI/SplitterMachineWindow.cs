using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitterMachineWindow : Window
{
    public Button closeButton;
    public Button lessRightButton;
    public Button moreRightButton;
    public InputField countRightField;
    public Button lessForwardButton;
    public Button moreForwardButton;
    public InputField countForwardField;
    public Button lessLeftButton;
    public Button moreLeftButton;
    public InputField countLeftField;
    private SplitterMachine splitterMachine;

    public void Init(SplitterMachine splitterMachine) {
        Init();
        this.splitterMachine = splitterMachine;
        InitField(lessRightButton, moreRightButton, countRightField, -1);
        InitField(lessForwardButton, moreForwardButton, countForwardField, 0);
        InitField(lessLeftButton, moreLeftButton, countLeftField, 1);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    private void InitField(Button lessButton, Button moreButton, InputField countField, int rotate) {
        int k_rotate = rotate;  // use a local variable
        lessButton.onClick.RemoveAllListeners();
        lessButton.onClick.AddListener(() => SetCount(splitterMachine.countsByRotate[k_rotate] - 1, k_rotate));
        moreButton.onClick.RemoveAllListeners();
        moreButton.onClick.AddListener(() => SetCount(splitterMachine.countsByRotate[k_rotate] + 1, k_rotate));
        countField.SetTextWithoutNotify(splitterMachine.countsByRotate[k_rotate].ToString());
        countField.onValueChanged.RemoveAllListeners();
        countField.onValueChanged.AddListener(value => {
            if (int.TryParse(value, out int count)) {
                SetCount(count, k_rotate);
            }
        });
    }

    private void SetCount(int count, int rotate) {
        count = Mathf.Clamp(count, 1, 64);
        splitterMachine.countsByRotate[rotate] = count;
        switch (rotate) {
            case -1: countRightField.SetTextWithoutNotify(count.ToString()); break;
            case 0: countForwardField.SetTextWithoutNotify(count.ToString()); break;
            case 1: countLeftField.SetTextWithoutNotify(count.ToString()); break;
        }
    }
}