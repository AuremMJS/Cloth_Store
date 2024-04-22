using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer orderSprite;
    [SerializeField]
    private GameObject completedTickGO;

    NavMeshAgent mAgent;
    Rigidbody mRigidbody;
    public Cloth Order { get; set; }

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        mAgent = GetComponent<NavMeshAgent>();
        mRigidbody = GetComponent<Rigidbody>();
        completedTickGO.SetActive(false);
    }

    void Update()
    {
        if(mAgent != null && Vector3.Distance(mAgent.transform.position, mAgent.destination) < 0.5f) 
        {
            mAgent.velocity = Vector3.zero;
            mRigidbody.velocity = Vector3.zero;
            mAgent.isStopped = true;
        }
    }

    public void CreateOrder() 
    {
        CustomerTray customerLounge = ClothStore.Instance.GetCustomerLounge();
        if (customerLounge == null )
        {
            return;
        }
        mAgent.SetDestination(customerLounge.transform.position);
        // Random logic to generate the order
        Order = ClothStore.Instance.GetRandomCloth();
        orderSprite.sprite = AssetsLoader.Instance.GetSpriteForCloth(Order);
        completedTickGO.SetActive(false);
    }

    public void OrderCompleted()
    {
        Order = null;
        mAgent.SetDestination(startPosition);
        mAgent.isStopped = false;
        completedTickGO.SetActive(true);
    }
    
    // Checking if the order is correct
    public bool IsOrderCorrect(Cloth _cloth)
    {
        return Order.Type == _cloth.Type && Order.Color == _cloth.Color;
    }

    public void OnTriggerEnter(Collider other)
    {
        CustomerTray customerTray = other.GetComponent<CustomerTray>();
        if (customerTray != null)
        {
            customerTray.PlaceOrder(this);
        }
    }
    public void OnTriggerStay(Collider other) 
    { 
        if(other.CompareTag("OrderCreator") && Order == null)
        {
            CreateOrder();
        }
    }
}
