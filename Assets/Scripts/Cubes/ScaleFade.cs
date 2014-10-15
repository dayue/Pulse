using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScaleFade : ColourFade
{
    public float RadiusShrinkSpeed = 2f;
    public float RadiusSize = 4f;

    void Update()
    {
        if (mAlpha > 0)
            mAlpha -= mSpeed * Time.deltaTime;
        else
            mAlpha = 0f;

        if (transform.localScale.magnitude > 1f)
            transform.localScale -= Vector3.one * RadiusShrinkSpeed * Time.deltaTime;

        PulseTimer();

        Color temp = renderer.material.color;
        temp.a = mAlpha;
        renderer.material.SetColor("_Color", temp);
    }

    public override void PulseTimer()
    {
        base.PulseTimer();
    }

    public override void LightMe(Material iColor)
    {
        transform.localScale = Vector3.one * RadiusSize;
        if (!mHardMode)
        {
            mAlpha = 1f;
            renderer.material = iColor;
        }
    }
}
