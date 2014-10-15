using UnityEngine;
using System.Collections;

public class gTextField {

	public string mCaption;
	public string mName;
	public bool mEnabled;
	public Rect mRectangle;
	public int mLength;
	
	public gTextField(string iName, string iCaption, Rect iRectangle, int iLength)
	{
		mName = iName;
		mEnabled = true;
		mCaption = iCaption;
		mRectangle = iRectangle;
		mLength = iLength;
	}
	
	public void setCaption(string iCaption)
	{
		mCaption = iCaption; 
	}
	public string getCaption()
	{
		return mCaption;
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
			mCaption = GUI.TextField(mRectangle, mCaption, mLength);
		}
	}
}
