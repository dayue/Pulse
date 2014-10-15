using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interface {

    public Dictionary<string, gButton> mButtons = new Dictionary<string,gButton>();
    public Dictionary<string, gText> mTexts = new Dictionary<string,gText>();
    public Dictionary<string, gTextField> mTextFields = new Dictionary<string,gTextField>();

    // Use this for initialisation
	public virtual void Initialise()
    {
        ButtonCreation();
	}

    public virtual void ButtonCreation()
    {

    }

    public virtual void EnterInterface()
    {//This should handle enabling all the buttons of the interface
        foreach (KeyValuePair<string, gButton> l_btn in mButtons)
        {
            l_btn.Value.enable();
        }

        foreach (KeyValuePair<string, gText> l_txt in mTexts)
        {
            l_txt.Value.enable();
        }

        foreach (KeyValuePair<string, gTextField> l_txtf in mTextFields)
        {
            l_txtf.Value.enable();
        }
    }

    public virtual void ExitInterface()
    {//this should handle disabling all the buttons of the interface
        foreach (KeyValuePair<string, gButton> l_btn in mButtons)
        {
            l_btn.Value.disable();
        }

        foreach (KeyValuePair<string, gText> l_txt in mTexts)
        {
            l_txt.Value.disable();
        }

        foreach (KeyValuePair<string, gTextField> l_txtf in mTextFields)
        {
            l_txtf.Value.disable();
        }
    }
	
	public virtual void UpdateInterface()
    {
        ButtonCreation();
	}

    public void createButton(string iName, string iCaption, Rect iRect)
    {
        if (!mButtons.ContainsKey(iName))
        {
            mButtons.Add(iName, ButtonManager.createButton(iName, iCaption, iRect));
            mButtons[iName].disable();
        }
        else
        {
            mButtons[iName].setCaption(iCaption);
            mButtons[iName].setRectangle(iRect);
        }
    }

    public void createText(string iName, string iCaption, Rect iRect)
    {
        if (!mTexts.ContainsKey(iName))
        {
            mTexts.Add(iName, TextManager.createText(iName, iCaption, iRect));
            mTexts[iName].disable();
        }
        else
        {
            mTexts[iName].setCaption(iCaption);
            mTexts[iName].setRectangle(iRect);
        }
    }

    public void createTextField(string iName, string iCaption, Rect iRect, int iLength = 25)
    {
        if (!mTextFields.ContainsKey(iName))
        {
            mTextFields.Add(iName, TextFieldManager.createTextField(iName, iCaption, iRect, iLength));
            mTextFields[iName].disable();
        }
        else
        {
            mTextFields[iName].setCaption(iCaption);
            mTextFields[iName].setRectangle(iRect);
        }
    }

    public bool GetButtonPress(string iName)
    {
        if (!mButtons.ContainsKey(iName))
            return false;
        return mButtons[iName].getPressed();
    }
}
