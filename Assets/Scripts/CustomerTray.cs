using UnityEngine;

public class CustomerTray : MonoBehaviour
{
    public Customer Customer { get; set; }
    bool orderPlaced = false;
    bool orderCompleted = false;

    // Start is called before the first frame update
    public void Start()
    {
        ClothStore.Instance.AddCustomerLounge(this);
    }

    // Update is called once per frame
    void Update()
    {   
       
    }

    public void PlaceOrder(Customer customer)
    {
        orderPlaced = true;
        orderCompleted = false;
        Customer = customer;
    }

    public Cloth GetCustomerOrder()
    {
        if (!orderPlaced || orderCompleted)
            return null;
        return Customer.Order;
    }

    public void OrderCompleted()
    {
        orderPlaced = false;
        orderCompleted = true;
        ClothStore.Instance.AddCustomerLounge(this);
        Customer.OrderCompleted();
        Debug.Log("Order Completed");
    }
}
