using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ManAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator manAnimator;

    private PlayerController playerController;
    private Dictionary<ManMovementState, string> stateTriggerMap;
    
    private const string VELOCITY_PARAM = "Velocity";
    private const string IS_HOLDING_CLOTHS_PARAM = "IsHoldingCloths";

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerController = GetComponent<PlayerController>();
        stateTriggerMap = new Dictionary<ManMovementState, string>();
        stateTriggerMap.Add(ManMovementState.Idle, "Idle");
        stateTriggerMap.Add(ManMovementState.WalkOrRun, "WalkRun");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch(playerController.ManMovementState)
        {
            case ManMovementState.Idle:
                Idle();
                break;
            case ManMovementState.WalkOrRun:
                WalkRun();
                break;
        }

        manAnimator.SetBool(IS_HOLDING_CLOTHS_PARAM, playerController.IsHoldingCloths());
    }

    protected virtual float GetPlayerVelocityMagnitude()
    {
        return InputController.Instance.GetPlayerVelocity().magnitude;
    }

    protected void Idle()
    {
        manAnimator.ResetTrigger(stateTriggerMap[ManMovementState.WalkOrRun]);
        manAnimator.SetTrigger(stateTriggerMap[ManMovementState.Idle]);
    }

    protected void WalkRun()
    {
        manAnimator.ResetTrigger(stateTriggerMap[ManMovementState.Idle]);
        manAnimator.SetTrigger(stateTriggerMap[ManMovementState.WalkOrRun]);
        manAnimator.SetFloat(VELOCITY_PARAM, Mathf.Abs(GetPlayerVelocityMagnitude()));
    }
}
