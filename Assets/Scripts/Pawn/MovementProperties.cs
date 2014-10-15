using UnityEngine;
using System.Collections;

public class MovementProperties : MonoBehaviour
{
    public float SteepLimit = 15f;
    public float MaxSpeed = 100f;
    public float Gravity = -9.8f;
    public float j_baseHeight = 5f;
    public float j_extraHeight = 7.5f;

    public float maxGroundAcceleration = 100f;
    public float maxAirAcceleration = 100f;

    [HideInInspector]
    public Vector3 hitPoint = Vector3.zero;
    [HideInInspector]
    public Vector3 lastHitPoint = Vector3.zero;
    [HideInInspector]
    public Vector3 velocity = Vector3.zero;

    [HideInInspector]
    public bool j_jumping = false;
    [HideInInspector]
    public bool j_jumpReleased = true;
    [HideInInspector]
    public float j_lastButtonDownTime = 0f;
    [HideInInspector]
    public Vector3 j_jumpDirection = Vector3.zero;
    [HideInInspector]
    public float j_steepPerpAmount = 1f;
    [HideInInspector]
    public float j_perpAmount = 2f;
    [HideInInspector]
    public float j_jumpTimer = 0f;

    public bool j_holdingJumpButton()
    {
        //return Input.GetKey(KeyCode.Space);
        return Input.GetKey(KeyCode.JoystickButton0);
    }
    public bool j_pressJumpButton()
    {
        //return Input.GetKeyDown(KeyCode.Space);
        return Input.GetKeyDown(KeyCode.JoystickButton0);
    }
    public bool j_releaseJumpButton()
    {
        //return Input.GetKeyUp(KeyCode.Space);
        return Input.GetKeyUp(KeyCode.JoystickButton0);
    }
}
