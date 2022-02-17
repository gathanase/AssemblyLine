using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GameController : MonoBehaviour
{
    public GameDatabase gameDatabase;
    public GameState gameState;
    public Text moneyText;
    public float gameSpeed;  // 0: pause, 1: normal

    void Awake() {
        gameState = new GameState(this);
        gameSpeed = 0.5f;
        SetTool(ToolType.INFO);
    }

    void Start()
    {
        AddMoney(10000);
        InvokeRepeating("OnTick", 1/gameSpeed, 1/gameSpeed);
    }

    public void SetTool(ToolType toolType) {
        foreach (var gameTool in gameDatabase.GetTools()) {
            if (gameTool.GetToolType() == toolType) {
                gameTool.OnActivate();
                gameTool.SetActive(true);
            } else {
                gameTool.SetActive(false);
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
        string path = Application.persistentDataPath + "/toto.save";
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        string json = JsonConvert.SerializeObject(gameState.ToSave(), settings);
        Debug.Log("Saving to file " + path);
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    public void LoadGame() {
        string path = Application.persistentDataPath + "/toto.save";
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        string json = File.ReadAllText(path);
        Debug.Log("Loading file " + path);
        Debug.Log(json);
        GameState.Save save = JsonConvert.DeserializeObject<GameState.Save>(json, settings);
        gameState.FromSave(save);
        RefreshMoney();
    }
}
