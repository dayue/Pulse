using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColourFade : MonoBehaviour {

    public class TimedPulse
    {
        public TimedPulse(float iTime)
        {
            mPulseTimer = iTime;
        }
        public float mPulseTimer;
        public Material mColor;
    }

    public float mSpeed = 1f;
    public Material mBaseColor;
    public bool mColourOverridable = true;
    
    [HideInInspector]
    public Material mCurrentColor;
    
    [HideInInspector]
    public float mAlpha = 0f;

    [HideInInspector]
    public List<TimedPulse> mPulseList = new List<TimedPulse>();

    public bool mHardMode = false;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (mAlpha > 0)
            mAlpha -= mSpeed * Time.deltaTime;
        else
            mAlpha = 0f;

        Color temp = renderer.material.color;
        temp.a = mAlpha;
        renderer.material.SetColor("_Color", temp);

        PulseTimer();
    }

    public virtual void PulseTimer()
    {
        List<TimedPulse> l_remove = new List<TimedPulse>();
        foreach(TimedPulse l_en in mPulseList)
        {
            l_en.mPulseTimer -= Time.deltaTime;
            if (l_en.mPulseTimer <= 0f)
            {
                Material t_color = mBaseColor;
                if (l_en.mColor != null && mColourOverridable)
                    t_color = l_en.mColor;
                LightMe(t_color);
                l_remove.Add(l_en);
            }
        }
        foreach (TimedPulse l_en in l_remove)
        {
            mPulseList.Remove(l_en);
        }
    }

    public virtual void LightMe(Material iColor)
    {
        if (!mHardMode)
        {
            mAlpha = 1f;
            renderer.material = iColor;
        }
    }

    public void PulseAfterTime(float iTime, Material iColor)
    {
        TimedPulse l_pulse = new TimedPulse(iTime);
        l_pulse.mColor = iColor;
        mPulseList.Add(l_pulse);
    }
}
