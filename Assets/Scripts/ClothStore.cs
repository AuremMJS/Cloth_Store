using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic container
public class ClothStore : MonoBehaviour
{
    public static ClothStore Instance;

    List<Cloth> unlockedClothRacks;
    Stack<CustomerTray> customerLounges;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        unlockedClothRacks = new List<Cloth>();
        customerLounges = new Stack<CustomerTray>();
    }

    public virtual void Start()
    {
    }

    public void AddClothToStore(Cloth cloth)
    {
        unlockedClothRacks.Add(cloth);
    }

    public void AddCustomerLounge(CustomerTray customerLounge)
    {
        customerLounges.Push(customerLounge);
    }

    public CustomerTray GetCustomerLounge()
    {
        if(customerLounges.Count == 0) return null;
        return customerLounges.Pop();
    }

    public Cloth GetRandomCloth()
    {
        if(unlockedClothRacks == null || unlockedClothRacks.Count <= 0)
            return null;
        int rndClothIndex = UnityEngine.Random.Range(0, unlockedClothRacks.Count);
        return unlockedClothRacks[rndClothIndex];
    }
}
