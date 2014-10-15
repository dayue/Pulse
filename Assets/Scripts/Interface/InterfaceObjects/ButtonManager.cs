using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour {

	static Dictionary<string, gButton> mButtonList = new Dictionary<string, gButton>();

    private bool mInitialised = false;

	void Awake()
	{
        if (mInitialised)
            return;

        mInitialised = true;

        DontDestroyOnLoad(this);
	}
	
	static public void enable(string iName)
	{
		mButtonList[iName].enable();
	}
	static public void disable(string iName)
	{
		mButtonList[iName].disable();
	}
	
	static public bool getEnabled(string iName)
	{
		return mButtonList[iName].getEnabled();		
	}
	
	static public gButton createButton(string iName, string iCaption, Rect iRectangle)
	{
        if (!mButtonList.ContainsKey(iName))
            mButtonList.Add(iName, new gButton(iName, iCaption, iRectangle));
        else
            Debug.Log("ButtonManager: Duplicate button name: " + iName);
		return mButtonList[iName];
	}
	static public gButton getButton(string iName)
	{
        if (mButtonList[iName] != null)
            return mButtonList[iName];
        else
        {
            Debug.Log("ButtonManager: Button not found : " + iName);
            return null;
        }
	}
	
	void OnGUI()
	{
		foreach(KeyValuePair<string, gButton> lBtn in mButtonList)
		{
			lBtn.Value.OnGUI();
		}
	}
}
