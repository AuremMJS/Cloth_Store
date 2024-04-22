using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct ClothAssetsSOEntry
{
    [SerializeField]
    public ClothType clothType;
    [SerializeField]
    public ClothColor clothColor;
    [SerializeField]
    public Sprite sprite;
    [SerializeField]
    public GameObject clothModel;
}

// Scriptable object to store the assetes for each cloth
[CreateAssetMenu(fileName = "ClothAssetssSO", menuName = "ScriptableObjects/ClothAssetsSO", order = 1)]
public class ClothAssetsSO : ScriptableObject
{
    [SerializeField]
    public ClothAssetsSOEntry[] cloths;

    public Sprite GetSpriteForCloth(Cloth cloth)
    {
        ClothAssetsSOEntry clothAssetSOEntry = cloths.FirstOrDefault((clothAssetSO) => { return clothAssetSO.clothType == cloth.Type && clothAssetSO.clothColor == cloth.Color;});
        return clothAssetSOEntry.sprite;
    }

    public GameObject GetModelForCloth(Cloth cloth)
    {
        ClothAssetsSOEntry clothAssetSOEntry = cloths.FirstOrDefault((clothAssetSO) => { return clothAssetSO.clothType == cloth.Type && clothAssetSO.clothColor == cloth.Color; });
        return clothAssetSOEntry.clothModel;
    }
}
