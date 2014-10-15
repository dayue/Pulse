using UnityEngine;
using System.Collections;

public class gButton {

	public string mCaption;
	public string mName;
	public bool mPress;
	public bool mEnabled;
	public Rect mRectangle;
    public GUIStyle mStyle;
	
	public gButton(string iName, string iCaption, Rect iRectangle)
	{
		mName = iName;
		mEnabled = true;
		mCaption = iCaption;
		mRectangle = iRectangle;
		mPress = false;
        mStyle = new GUIStyle();
	}
	public void setCaption(string iCaption)
	{
		mCaption = iCaption; 
	}
	public void setRectangle(Rect iRectangle)
	{
		mRectangle = iRectangle;
	}
	public void enable()
	{
		mEnabled = true;
	}

	public void disable()
	{
		mEnabled = false;
	}
	
	public bool getEnabled()
	{
		return mEnabled;
	}
	
	public bool getPressed()
	{
        if (mEnabled)
        {
            if (mPress == true)
                GameManager.PlayButtonSound();
            return mPress;
        }
        else
        {
            return false;
        }
	}
	
	public void OnGUI()
	{
		if(mEnabled)
		{
			if(GUI.Button(mRectangle, mCaption))
			{
				mPress = true;
			}
			else	
			{
				mPress = false;
			}
		}
		else
		{
			mPress = false;
		}
	}
}
