using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    private Vector3 pCurrentMousePos = Vector3.zero;
    private Vector3 pPrevMousePos = Vector3.zero;
    private Vector3 pMousePosDelta = Vector3.zero;
    private Vector3 pKeyMovementInput = Vector3.zero;

    public static bool mEnabled = true;

    // Use this for initialization
    void Awake()
    {
    }

    public Vector3 getMouseDelta()
    {
        return pMousePosDelta;
    }

    public Vector3 getKeyMovementInput()
    {
        return pKeyMovementInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mEnabled)
            return;

        Vector3 l_screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 l_mouseBoundDistance = pCurrentMousePos - l_screenCenter;

        Screen.lockCursor = true;

        pMousePosDelta = new Vector3(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"), 0f);

        //Get Movement keys
        float l_keyX = Input.GetAxis("Horizontal"), l_keyZ = -Input.GetAxis("Vertical");
        //if (Input.GetKey(KeyCode.A))
        //    l_keyX -= 1f;
        //if (Input.GetKey(KeyCode.D))
        //    l_keyX += 1f;
        //if (Input.GetKey(KeyCode.W))
        //    l_keyZ += 1f;
        //if (Input.GetKey(KeyCode.S))
        //    l_keyZ -= 1f;

        pKeyMovementInput = new Vector3(l_keyX, 0f, l_keyZ);
    }

}
