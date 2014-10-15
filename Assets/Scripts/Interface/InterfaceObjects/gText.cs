using UnityEngine;
using System.Collections;

public class gText {

	public string mCaption;
	public string mName;
	public bool mEnabled;
	public Rect mRectangle;
    public GUIStyle mStyle;
	
	public gText(string iName, string iCaption, Rect iRectangle)
	{
		mName = iName;
		mEnabled = true;
		mCaption = iCaption;
		mRectangle = iRectangle;
        mStyle = new GUIStyle();
	}
	
	public void setCaption(string iCaption)
	{
		mCaption = iCaption; 
	}
    public void setAlignment(TextAnchor iAnchor)
    {
        mStyle.alignment = iAnchor;
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
	
	public void OnGUI()
	{
		if(mEnabled)
		{
			GUI.Label(mRectangle, mCaption, mStyle);
		}
	}
}
