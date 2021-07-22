using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GameController : MonoBehaviour
{
    public GameDatabase gameDatabase;
    public GameState gameState;
    public Text moneyText;

    void Awake() {
        gameState = new GameState(this);
        SetTool(ToolType.INFO);
    }

    void Start()
    {
        AddMoney(10000);
        InvokeRepeating("OnTick", 1, 1);
    }

    public void SetTool(ToolType toolType) {
        foreach (var gameTool in gameDatabase.GetTools()) {
            if (gameTool.GetToolType() == toolType) {
                gameTool.OnActivate();
                gameTool.gameObject.SetActive(true);
            } else {
                gameTool.gameObject.SetActive(false);
            }
        }
    }

    public FactoryFloor GetFactoryFloor() {
        return gameState.factoryFloor;
    }

    public void ZDestroy(GameObject go) {
        Destroy(go);
    }

    void OnTick() {
        gameState.OnTick();
    }

    public void AddMoney(long amount) {
        gameState.money += amount;
        RefreshMoney();
    }

    public void RemoveMoney(long amount) {
        gameState.money -= amount;
        RefreshMoney();
    }

    private void RefreshMoney() {
        moneyText.text = gameState.money.ToString("C0");
    }

    public void SaveGame() {
        Debug.Log("Save");
        string path = Application.persistentDataPath + "/toto.save";
        string json = JsonConvert.SerializeObject(gameState.ToSave());
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    public void LoadGame() {
        Debug.Log("Load");
        // https://stackoverflow.com/questions/29528648/json-net-serialization-of-type-with-polymorphic-child-object
        string path = Application.persistentDataPath + "/toto.save";
        string json = File.ReadAllText(path);
        Debug.Log(json);
        GameState.Save save = JsonConvert.DeserializeObject<GameState.Save>(json);
        gameState.FromSave(save);
        RefreshMoney();
    }
}
