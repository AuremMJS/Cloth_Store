using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Singleton instance
    public static InputController Instance { get; private set; }

    [SerializeField]
    private float Sensitivity=10.0f ;

    Vector2 startPosition;
    bool swipeInProgress;

    Vector3 velocity;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        swipeInProgress = false;
        velocity = Vector3.zero;
    }

    void Update()
    {
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            if (!swipeInProgress)
            {
                startPosition = Input.touches[0].position;
                swipeInProgress = true;
            }
            else
            {
                Vector2 touchDiff = Input.touches[0].position - startPosition;
                touchDiff.Normalize();
                velocity = new Vector3(touchDiff.x, 0, touchDiff.y) * Sensitivity;
            }
        }
        else
        {
            swipeInProgress=false;
            velocity = Vector3.zero;
        }
#elif UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            if(!swipeInProgress)
            {
                startPosition = Input.mousePosition;
                swipeInProgress= true;
            }
            else
            {
                Vector2 touchDiff = Input.mousePosition - startPosition;
                touchDiff.Normalize();
                velocity = new Vector3(touchDiff.x, 0, touchDiff.y) * Sensitivity;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            swipeInProgress = false;
            velocity = Vector3.zero;
        }
#endif
    }

    // Fetching the player velocity
    public Vector3 GetPlayerVelocity()
    {
        return velocity;
    }
}
