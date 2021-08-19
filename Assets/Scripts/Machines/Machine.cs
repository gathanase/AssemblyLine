using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public abstract class Machine : MonoBehaviour
{
    public Vector2Int position;
    public Direction direction;
    protected static GameController gameController = null;
    protected static GameDatabase gameDatabase = null;
    private FactoryFloor factoryFloor = null;

    public class Save {
        public string type;
        public int x, y;
        public string direction;

        public Machine Load(FactoryFloor floor, GameDatabase gameDatabase) {
            MachineType type = MachineTypeExtensions.Parse(this.type);
            Machine machine = Instantiate(gameDatabase.GetModel(type));
            machine.Init(this, floor, gameDatabase);
            return machine;
        }
    }

    public virtual Save ToSave() {
        Save save = new Save();
        WriteSave(save);
        return save;
    }

    public virtual Save WriteSave(Save save) {
        save.type = GetMachineType().ToString();
        save.x = position.x;
        save.y = position.y;
        save.direction = direction.ToString();
        return save;
    }
    
    public virtual void Init(Save save, FactoryFloor floor, GameDatabase gameDatabase) {
        Vector2Int position = new Vector2Int(save.x, save.y);
        Direction direction = DirectionExtensions.Parse(save.direction);
        Init(floor, position, direction);
    }

    void Awake() {
        if (gameController == null) {
            gameController = FindObjectOfType<GameController>();
        }
        if (gameDatabase == null) {
            gameDatabase = FindObjectOfType<GameDatabase>();
        }
    }

    public MachineDatabase.MachineInfo GetInfo() {
        return gameDatabase.GetInfo(GetMachineType());
    }

    public void Init(FactoryFloor factoryFloor, Vector2Int position, Direction direction)
    {
        transform.position = new Vector3(position.x, position.y, 0);
        transform.rotation = Quaternion.Euler(0, 0, ((int)direction) * 90);
        this.position = position;
        this.direction = direction;
        this.factoryFloor = factoryFloor;
    }

    public abstract MachineType GetMachineType();
    public abstract Window CreateInfoWindow();
    public abstract void Feed(Artifact artifact);
    public abstract void OnTick();

    public void Rotate(int count) {
        direction = direction.Rotate(count);
        transform.rotation = Quaternion.Euler(0, 0, ((int)direction) * 90);
    }

    protected void Add(ArtifactType type, Direction direction) {
        Artifact artifact = Instantiate(gameDatabase.GetModel(type));
        artifact.Init(position, direction, type);
        factoryFloor.Add(artifact);
    }

    protected void Remove(Artifact artifact) {
        factoryFloor.Remove(artifact);
    }
}