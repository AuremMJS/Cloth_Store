using UnityEngine;

public class ClothRack : MonoBehaviour
{
    [SerializeField]
    private ClothType clothType = ClothType.Shirt;
    [SerializeField]
    private ClothColor clothColor = ClothColor.White;

    private Cloth cloth;

    // Start is called before the first frame update
    public void Start()
    {
        cloth = new Cloth(clothType, clothColor);
        UnlockRack();
    }

    public void UnlockRack()
    {
        ClothStore.Instance.AddClothToStore(cloth);
    }

    public Cloth GetCloth()
    {
        return cloth;
    }
}
