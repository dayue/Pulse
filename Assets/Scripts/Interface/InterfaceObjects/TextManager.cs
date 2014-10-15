using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextManager : MonoBehaviour {

	static Dictionary<string, gText> mTextList = new Dictionary<string, gText>();

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
		mTextList[iName].enable();
	}
	static public void disable(string iName)
	{
		mTextList[iName].disable();
	}
	
	static public bool getEnabled(string iName)
	{
		return mTextList[iName].getEnabled();		
	}
	
	static public gText createText(string iName, string iCaption, Rect iRectangle)
	{
        if (!mTextList.ContainsKey(iName))
            mTextList.Add(iName, new gText(iName, iCaption, iRectangle));
        else
            Debug.Log("TextManager: Duplicate text name: " + iName);
		return mTextList[iName];
	}
	
	static public gText getText(string iName)
	{
        if (mTextList[iName] != null)
            return mTextList[iName];
        else
        {
            Debug.Log("TextManager: Text not found : " + iName);
            return null;
        }
	}
	
	void OnGUI()
	{
		foreach(KeyValuePair<string, gText> lText in mTextList)
		{
			lText.Value.OnGUI();
		}
	}
}
