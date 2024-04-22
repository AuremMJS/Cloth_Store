using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private Transform followCamera;

    private Rigidbody mRigidbody;
    private bool isHoldingCloths;

    public ManMovementState ManMovementState
    {
        private set; get;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        ManMovementState = ManMovementState.Idle;
    }

    // Update is called once per frame
    private void Update()
    {
        followCamera.position = Vector3.up * 3.99f + transform.position;
    }

    private void FixedUpdate()
    {
        ProcessState();
    }

    private void ProcessState()
    {
        switch (ManMovementState)
        {
            case ManMovementState.Idle:
                Idle();
                break;
            case ManMovementState.WalkOrRun:
                WalkOrRun(); 
                break;
        }
    }

    private void Idle()
    {
        if (InputController.Instance.GetPlayerVelocity().magnitude > 0)
        {
            ManMovementState = ManMovementState.WalkOrRun;
        }
        else
        {
            mRigidbody.velocity = Vector3.zero;
        }
    }

    private void WalkOrRun()
    {
        Vector3 velocity = InputController.Instance.GetPlayerVelocity();
        if (velocity.magnitude > 0.1f)
        {
            ManMovementState = ManMovementState.WalkOrRun;
            mRigidbody.velocity = velocity * speedMultiplier;
            transform.forward = velocity;
        }
        else
            ManMovementState = ManMovementState.Idle;
    }

    public void SetHoldingCloths(bool holdingCloths)
    {
        isHoldingCloths = holdingCloths;
    }

    public bool IsHoldingCloths()
    {
        return isHoldingCloths;
    }
}