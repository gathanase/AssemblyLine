using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ArtifactDatabase : MonoBehaviour
{
    private Dictionary<ArtifactType, ArtifactInfo> artifacts;

    public class ArtifactInfo {
        public ArtifactType type;
        public string name;
        public int cost;
    }

    [System.Serializable]
    private class ArtifactJson {
        public string type;
        public string name;
        public int cost;
    }

    [System.Serializable]
    private class ArtifactsJson {
        public List<ArtifactJson> artifacts;
    }

    void Awake() {
        TextAsset file = Resources.Load<TextAsset>("Artifacts");
        ArtifactsJson json = JsonUtility.FromJson<ArtifactsJson>(file.text);
        artifacts = json.artifacts.ConvertAll(ToModel).ToDictionary(model => model.type);
        Debug.LogFormat("Loaded {0} artifacts", artifacts.Count);
    }

    private ArtifactInfo ToModel(ArtifactJson json) {
        ArtifactInfo model = new ArtifactInfo();
        model.type = ArtifactTypeExtensions.Parse(json.type);
        model.name = json.name;
        model.cost = json.cost;
        return model;
    }

    public ArtifactInfo GetInfo(ArtifactType type) {
        return artifacts[type];
    }
}
