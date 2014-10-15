using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceAchievements : Interface {

    private Dictionary<string, AchievementProps> mAchievements = new Dictionary<string, AchievementProps>();
    private Dictionary<string, Vector3> mAchievementPositions = new Dictionary<string, Vector3>();

    private bool mChangeLevelToMenu = false;
    private bool mLevelLoaded = false;
    private float mVanityTimer = 0f;

    private AchievementProps mSelectedAchievement;
    private GameObject AchievementDescription;

    // Use this for initialisation
    public override void Initialise()
    {
        base.Initialise();
    }

    //Use this for drawing buttons
    public override void ButtonCreation()
    {
        createButton("AC_Return", "Return (O)", new Rect(Screen.width * 0.05f, Screen.height * 0.90f, Mathf.Clamp(Screen.width * 0.2f, 50f, 100f), Mathf.Clamp(Screen.height * 0.1f, 20f, 30f)));
    }

    // This is the main update
    public override void UpdateInterface()
    {
        base.UpdateInterface();

        mVanityTimer += Time.deltaTime;
        if (mVanityTimer > GameDefinitions.RecordTimes.Van)
        {
            AchievementManager.UnlockAchievement("Achievement_13");
            CheckAchievementUpdates();
        }

        if (GetButtonPress("AC_Return") || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            //InterfaceManager.ChangeInterface(InterfaceManager.Interfaces.MainMenu);
            GameManager.RequestLevelChange(GameDefinitions.SceneNames.Menu);
            mChangeLevelToMenu = true;
        }
        if (Application.loadedLevelName == GameDefinitions.SceneNames.Menu && mChangeLevelToMenu)
        {
            InterfaceManager.ChangeInterface(InterfaceManager.Interfaces.MainMenu);
            mChangeLevelToMenu = false;
        }
        if (Application.loadedLevelName == GameDefinitions.SceneNames.Achievements && !mLevelLoaded)
        {
            mLevelLoaded = true;
            EnterInterface();
        }
        UpdateAchievementPositions();
        CheckMousePress();
        UpdatePanel();
    }

    void UpdatePanel()
    {
        if (AchievementDescription == null)
            return;
        GUITexture l_text = AchievementDescription.GetComponent<GUITexture>();
        if (l_text == null)
            return;

        if (mSelectedAchievement != null)
        {
            l_text.enabled = true;
            l_text.texture = mSelectedAchievement.iDescription;
        }
        else
        {
            l_text.enabled = false;
        }
    }
    void CheckMousePress()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(Input.mousePosition.ToString());
            bool l_found = false;
            Vector3 l_position = Input.mousePosition;
            foreach (KeyValuePair<string, Vector3> l_en in mAchievementPositions)
            {
                if ((l_position - l_en.Value).magnitude < (Screen.width * 0.087f) / 2f)
                {
                    if (mAchievements[l_en.Key].mUnlocked)
                    {
                        mSelectedAchievement = mAchievements[l_en.Key];
                        l_found = true;
                    }
                }
            }
            if(!l_found)
                mSelectedAchievement = null;
        }
        
    }

    void UpdateAchievementPositions()
    {
        GameObject[] l_Alist = GameObject.FindGameObjectsWithTag("Achievement");
        foreach (GameObject l_en in l_Alist)
        {
            AchievementProps temp = l_en.GetComponent<AchievementProps>();
            if (temp != null)
            {
                mAchievementPositions[temp.iName] = new Vector3(l_en.transform.position.x * Screen.width, l_en.transform.position.y * Screen.height, 0f);
            }
        }
    }

    public virtual void EnterInterface()
    {//This should handle enabling all the buttons of the interface
        Debug.Log("Enter AchievementInterface");
        base.EnterInterface();
        mChangeLevelToMenu = false;
        CheckAchievementUpdates();
    }

    void CheckAchievementUpdates()
    {
        AchievementDescription = GameObject.FindGameObjectWithTag("AchievementPanel");
        GameObject[] l_Alist = GameObject.FindGameObjectsWithTag("Achievement");
        foreach (GameObject l_en in l_Alist)
        {
            AchievementProps temp = l_en.GetComponent<AchievementProps>();
            if(temp != null)
            {
                if (AchievementManager.GetAchievement(temp.iName).mUnlocked)
                    temp.Unlock();
                if(!mAchievements.ContainsKey(temp.iName))
                    mAchievements.Add(temp.iName, temp);
                //Debug.Log("Achievement: " + temp.iName);
            }
        }
    }

    public virtual void ExitInterface()
    {//this should handle disabling all the buttons of the interface
        base.ExitInterface();
    }
}
