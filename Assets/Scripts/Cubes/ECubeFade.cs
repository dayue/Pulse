using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class ECubeFade : ColourFade {

    public float SphereColliderRadius = 2f;
    public float RadiusShrinkSpeed = 2f;
    private SphereCollider mCollider;

    void Awake()
    {
        mCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (mAlpha > 0)
            mAlpha -= mSpeed * Time.deltaTime;
        else
            mAlpha = 0f;

        if(mCollider.radius > 0f)
            mCollider.radius -= RadiusShrinkSpeed * Time.deltaTime;

        PulseTimer();

        Color temp = renderer.material.color;
        temp.a = mAlpha;
        renderer.material.SetColor("_Color", temp);
    }

    public override void PulseTimer()
    {
        List<TimedPulse> l_remove = new List<TimedPulse>();
        foreach (TimedPulse l_en in mPulseList)
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

    public override void LightMe(Material iColor)
    {
        mCollider.radius = SphereColliderRadius;
        if (!mHardMode)
        {
            mAlpha = 1f;
            renderer.material = iColor;
        }
    }
}
