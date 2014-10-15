using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterPlayerController : MonoBehaviour
{
    //public MasterPawn mPawn;

    [HideInInspector]
    public PlayerInput mPlayerInput;

    [HideInInspector]
    public CameraControl mCameraControl;

	// Use this for initialization
	void Awake ()
    {
        mPlayerInput = GetComponent<PlayerInput>();
        mCameraControl = GetComponent<CameraControl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
