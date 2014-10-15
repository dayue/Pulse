using UnityEngine;
using System.Collections;

public class HardModeInitiate : MonoBehaviour {

    public Material mMat;
    private ColourFade mFade;
    private bool mInit = false;

	// Use this for initialization
	void Awake ()
    {
        mFade = GetComponent<ColourFade>();
        if (mFade != null)
        {
            mInit = true;
        }
        else if(GameManager.mHardMode)
        {
            renderer.material = mMat;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!mInit)
        {
            Destroy(this);
            return;
        }

        if (GameManager.mHardMode)
        {
            mFade.mBaseColor = mMat;
            mFade.mHardMode = true;
        }

        Destroy(this);
	}
}
