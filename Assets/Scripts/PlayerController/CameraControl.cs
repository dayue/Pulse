using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float iSensitivityX = 2f;
    public float iSensitivityY = 2f;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    private float rotationY = 0F;

    public GameObject iGoCamera;
    public GameObject iGoPawn;

    public static bool mPaused = false;

    private MasterPlayerController mMasterPlayerController;

	// Use this for initialization
	void Awake ()
    {
        GetReferences();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mPaused)
            return;

        float l_yAxis = -mMasterPlayerController.mPlayerInput.getMouseDelta().y;
        if (l_yAxis > iSensitivityY) l_yAxis = iSensitivityY;
        if (l_yAxis < -iSensitivityY) l_yAxis = -iSensitivityY;

        Quaternion l_upRotation = Quaternion.EulerAngles(l_yAxis, 0f, 0f);

        Quaternion l_transformation = l_upRotation;
        iGoCamera.transform.rotation = Quaternion.Lerp(iGoCamera.transform.rotation, iGoCamera.transform.rotation * l_transformation, Time.deltaTime);	
	}

    private void GetReferences()
    {
        mMasterPlayerController = GetComponent<MasterPlayerController>();
    }
}
