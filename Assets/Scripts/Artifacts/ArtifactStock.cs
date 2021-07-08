using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactStock
{
    protected Dictionary<ArtifactType, int> stock;
    public int maxCount;

    public ArtifactStock() {
        stock = new Dictionary<ArtifactType, int>();
        maxCount = 100;
    }

    public List<ArtifactType> GetArtifactTypes() {
        return new List<ArtifactType>(stock.Keys);
    }

    public int GetCount(ArtifactType artifactType) {
        int count = 0;
        stock.TryGetValue(artifactType, out count);
        return count;
    }

    public void Add(ArtifactType artifactType, int count) {
        int currentCount = GetCount(artifactType);
        if (currentCount + count <= maxCount) {
            stock[artifactType] = currentCount + count;
        }
    }

    public void Remove(ArtifactType artifactType, int count) {
        int currentCount = GetCount(artifactType);
        if (currentCount >= count) {
            stock[artifactType] = currentCount - count;
        }
    }

    public void RemoveAll(ArtifactStock other) {
        foreach (KeyValuePair<ArtifactType, int> otherPair in other.stock) {
            ArtifactType artifactType = otherPair.Key;
            int otherCount = otherPair.Value;
            Remove(artifactType, otherCount);
        }
    }

    public bool Contains(ArtifactStock other) {
        foreach (KeyValuePair<ArtifactType, int> otherPair in other.stock) {
            ArtifactType artifactType = otherPair.Key;
            int otherCount = otherPair.Value;
            if (GetCount(artifactType) < otherCount) {
                return false;
            }
        }
        return true;
    }
}
