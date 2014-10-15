using UnityEngine;
using System.Collections;

public class TimedPulser : MonoBehaviour {

    public float iOffset = 0f;
    public float iPulseSpeed = 50f;
    public float iRateOfPulse = 2f;
    public float iDistance = 0f;
    public Material iColor = null;
    private float mPulseTimer = 0f;

    public AudioClip mPulseSound;
    private AudioSource mAudioSource;

	// Use this for initialization
	void Start ()
    {
        mPulseTimer = iOffset;
        if (mPulseSound != null)
        {
            mAudioSource = GetComponent<AudioSource>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        mPulseTimer -= Time.deltaTime;
        if (mPulseTimer <= 0f)
        {
            mPulseTimer = iRateOfPulse;
            SendOutPulse();
            if (mPulseSound != null)
                mAudioSource.PlayOneShot(mPulseSound);
        }
	}

    void SendOutPulse()
    {
        Object[] list = GameObject.FindGameObjectsWithTag("PulseReceiver");
        foreach (GameObject l_en in list)
        {
            ColourFade temp = l_en.GetComponent<ColourFade>();
            if (temp != null)
            {
                float l_time = CalculateTimeToPulse(transform.position, l_en.transform.position);
                if(l_time != -1f)
                    temp.PulseAfterTime(l_time, iColor);
            }            
        }
    }

    float CalculateTimeToPulse(Vector3 iPos1, Vector3 iPos2)
    {
        float l_distance = (iPos1 - iPos2).magnitude;
        if (l_distance > iDistance && iDistance != 0f)
            return -1f;
        return l_distance / iPulseSpeed;
    }
}
