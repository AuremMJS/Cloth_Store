using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Loading the assets
public class AssetsLoader : MonoBehaviour
{
    public static AssetsLoader Instance;

    [SerializeField]
    private ClothAssetsSO clothAssetsSO;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Find the correct sprite and load it
    public Sprite GetSpriteForCloth(Cloth _cloth)
    {
        return clothAssetsSO.GetSpriteForCloth(_cloth);
    }

    public GameObject GetModelForCloth(Cloth _cloth)
    {
        return clothAssetsSO.GetModelForCloth(_cloth);
    }
}
