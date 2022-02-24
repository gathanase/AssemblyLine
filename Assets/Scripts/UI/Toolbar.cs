using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Toolbar : MonoBehaviour
{
    public Toggle exitButton;
    public Toggle saveButton;
    public Toggle loadButton;
    public Toggle infoButton;
    public Toggle buildButton;
    public Toggle deleteButton;
    public Toggle rotateButton;
    public Toggle moveButton;
    public GameController gameController;
    private BuildWindow buildWindow;

    void Awake() {
        buildWindow = FindObjectOfType<BuildWindow>(true);

        AddClickListener(exitButton, () => Application.Quit());
        AddClickListener(saveButton, () => gameController.SaveGame());
        AddClickListener(loadButton, () => gameController.LoadGame());
        AddClickListener(infoButton, () => gameController.SetTool(ToolType.INFO));
        AddClickListener(buildButton, () => buildWindow.Init());
        AddClickListener(deleteButton, () => gameController.SetTool(ToolType.DELETE));
        AddClickListener(rotateButton, () => gameController.SetTool(ToolType.ROTATE));
        AddClickListener(moveButton, () => gameController.SetTool(ToolType.MOVE));
    }

    private void AddClickListener(Toggle button, UnityAction action) {
        button.onValueChanged.AddListener(value => { if (value) action.Invoke(); });
    }
}
