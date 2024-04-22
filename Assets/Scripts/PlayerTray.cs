using System.Collections.Generic;
using UnityEngine;

public class PlayerTray : MonoBehaviour
{
    [SerializeField] private Transform clothParent;
    [SerializeField] private GameObject timer;

    PlayerController playerController;
    Dictionary<Cloth, Stack<GameObject>> clothMap;
    float lastClothCollectedTime;
    int totalCloth;

    // Start is called before the first frame update
    void Start()
    {
        clothMap = new Dictionary<Cloth, Stack<GameObject>>();
        playerController = GetComponent<PlayerController>();
        lastClothCollectedTime = 0;
        totalCloth = 0;
        timer.SetActive(false);
    }

    public virtual bool TakeFromContainer(Cloth cloth)
    {
        if(clothMap.ContainsKey(cloth) && clothMap[cloth].Count > 0)
        {
            GameObject clothGameObject = clothMap[cloth].Pop();
            Destroy(clothGameObject);
            totalCloth--;
            playerController.SetHoldingCloths(totalCloth > 0);
            return true;
        }

        return false;
    }

    // Taking the cloth into the player
    public virtual void PlaceIntoContainer(Cloth cloth)
    {
        if(!clothMap.ContainsKey(cloth))
        {
            clothMap.Add(cloth, new Stack<GameObject>());
        }
        GameObject newClothGameobject = Instantiate(AssetsLoader.Instance.GetModelForCloth(cloth));
        newClothGameobject.transform.parent = clothParent;
        newClothGameobject.transform.localPosition = Vector3.up * totalCloth * 0.1f;
        newClothGameobject.transform.forward = transform.forward;
        clothMap[cloth].Push(newClothGameobject);
        totalCloth++;
        playerController.SetHoldingCloths(true);
    }

    public void OnTriggerExit(Collider other)
    {
        lastClothCollectedTime = 0;    
        timer.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        ClothRack clothRack = other.GetComponent<ClothRack>();
        if(clothRack != null)
        {
            if (lastClothCollectedTime == 0)
            {
                lastClothCollectedTime = Time.time;
                timer.SetActive(true);
                timer.transform.forward = Vector3.forward;
            }
            if (Time.time - lastClothCollectedTime > 5.0f)
            {
                Cloth cloth = clothRack.GetCloth();
                PlaceIntoContainer(cloth);
                lastClothCollectedTime = Time.time;
                Debug.Log("Cloth collected");
                timer.SetActive(false);
                return;
            }
            else if(lastClothCollectedTime != 0.0f)
            {
                timer.SetActive(true);
                timer.transform.forward = Vector3.forward;
            }

        }

        CustomerTray customerTray = other.GetComponent<CustomerTray>();
        if(customerTray != null)
        {
            Cloth customerOrder = customerTray.GetCustomerOrder();
            if(customerOrder != null)
            {
                if(TakeFromContainer(customerOrder))
                {
                    customerTray.OrderCompleted();
                }
            }
        }
    }
}