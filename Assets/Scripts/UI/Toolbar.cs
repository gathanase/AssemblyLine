using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Toolbar : MonoBehaviour
{
    public Button exitButton;
    public Button saveButton;
    public Button loadButton;
    public Button infoButton;
    public Button buildButton;
    public Button deleteButton;
    public Button rotateButton;
    public Button moveButton;
    public GameController gameController;
    public Cursor cursor;
    private BuildWindow buildWindow;

    void Awake() {
        buildWindow = FindObjectOfType<BuildWindow>(true);

        AddClickListener(exitButton, () => Application.Quit());
        AddClickListener(saveButton, () => gameController.SaveGame());
        AddClickListener(loadButton, () => gameController.LoadGame());
        AddClickListener(infoButton, () => cursor.Info());
        AddClickListener(buildButton, () => buildWindow.Init());
        AddClickListener(deleteButton, () => cursor.Delete());
        AddClickListener(rotateButton, () => cursor.Rotate());
        AddClickListener(moveButton, () => gameController.SetTool(ToolType.MOVE));
    }

    private void AddClickListener(Button button, UnityAction action) {
        button.onClick.AddListener(() => action.Invoke());
    }
}
