using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextFieldManager : MonoBehaviour {

	static Dictionary<string, gTextField> mTextFieldList = new Dictionary<string, gTextField>();

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
		mTextFieldList[iName].enable();
	}
	static public void disable(string iName)
	{
		mTextFieldList[iName].disable();
	}
	
	static public bool getEnabled(string iName)
	{
		return mTextFieldList[iName].getEnabled();		
	}
	
	static public gTextField createTextField(string iName, string iCaption, Rect iRectangle, int iLength = 25)
	{
        if (!mTextFieldList.ContainsKey(iName))
            mTextFieldList.Add(iName, new gTextField(iName, iCaption, iRectangle, iLength));
        else
            Debug.Log("TextFieldManager: Duplicate textfield name: " + iName);
        return mTextFieldList[iName];
	}
	
	static public gTextField getTextField(string iName)
	{
        if (mTextFieldList[iName] != null)
            return mTextFieldList[iName];
        else
        {
            Debug.Log("TextFieldManager: Textfield not found : " + iName);
            return null;
        }
	}
	
	void OnGUI()
	{
		foreach(KeyValuePair<string, gTextField> lText in mTextFieldList)
		{
			lText.Value.OnGUI();
		}
	}
}
