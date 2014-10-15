using UnityEngine;
using System.Collections;

public class MasterPawn : MonoBehaviour
{
    public MasterPlayerController mMasterPlayerController;

    [HideInInspector]
    public MovementProperties mMovementProps;

    [HideInInspector]
    public PawnMovement mPawnMovement;

    [HideInInspector]
    public GameObject mCamera;

    [HideInInspector]
    public static AudioSource mAudioSource;

	// Use this for initialization
	void Awake ()
    {
        GetReferences();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void GetReferences()
    {
        mMovementProps = GetComponent<MovementProperties>();
        mPawnMovement = GetComponent<PawnMovement>();
        mCamera = GetComponentInChildren<Camera>().gameObject;
        mAudioSource = GetComponent<AudioSource>();
    }
}
