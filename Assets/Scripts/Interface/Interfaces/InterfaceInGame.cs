using UnityEngine;
using System.Collections;

public class InterfaceInGame : Interface {

    public static string mCurrenLevelName = GameDefinitions.SceneNames.Menu;
    public static float mLevelTimer = 0f;
    public static float mGameTimer = 0f;
    public static bool mEndOfLevel = false;
    public static bool mStopTimer = true;
    public static int mNumDeaths = 0;
    private bool mPaused = false;

    // Use this for initialisation
    public override void Initialise()
    {
        base.Initialise();
    }

    //Use this for drawing buttons
    public override void ButtonCreation()
    {
        createButton("Timer", mLevelTimer.ToString("###.#"), new Rect(Screen.width * 0.05f, Screen.height * 0.05f, Mathf.Clamp(Screen.width * 0.2f, 50f, 100f), Mathf.Clamp(Screen.height * 0.1f, 20f, 30f)));

        if (mPaused)
        {
            createButton("InGameMainMenu", "Main Menu (Select)", new Rect(Screen.width * 0.4f, Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.05f));
            createButton("InGameQuit", "Quit Game (L + R)", new Rect(Screen.width * 0.4f, Screen.height * 0.56f, Screen.width * 0.2f, Screen.height * 0.05f));
        }
    }

    // This is the main update
    public override void UpdateInterface()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            mPaused = !mPaused;
            createButton("InGameMainMenu", "Main Menu (Select)", new Rect(Screen.width * 0.4f, Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.05f));
            createButton("InGameQuit", "Quit Game (L + R)", new Rect(Screen.width * 0.4f, Screen.height * 0.56f, Screen.width * 0.2f, Screen.height * 0.05f));
            PlayerInput.mEnabled = !mPaused;
            mButtons["InGameMainMenu"].mEnabled = mPaused;
            mButtons["InGameQuit"].mEnabled = mPaused;
            //InterfaceManager.ChangeInterface(InterfaceManager.Interfaces.MainMenu);
            Screen.lockCursor = !mPaused;
        }
        if (mPaused)
        {
            if (GetButtonPress("InGameMainMenu") || Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                mButtons["InGameMainMenu"].mEnabled = false;
                mButtons["InGameQuit"].mEnabled = false;
                InterfaceManager.ChangeInterface(InterfaceManager.Interfaces.MainMenu);
                GameManager.RequestLevelChange(GameDefinitions.SceneNames.Menu);
            }
            if (GetButtonPress("InGameQuit") || (Input.GetKey(KeyCode.JoystickButton4) && Input.GetKey(KeyCode.JoystickButton5)))
                Application.Quit();
        }
        else
        {
            base.UpdateInterface();
            if (!mStopTimer)
                mLevelTimer += Time.deltaTime;

            if (Application.loadedLevelName != mCurrenLevelName)
            {
                //Debug.Log(Application.loadedLevelName);
                mGameTimer += mLevelTimer;
                mLevelTimer = 0f;
                mCurrenLevelName = Application.loadedLevelName;
                mEndOfLevel = true;
            }
            if (Application.loadedLevelName == GameDefinitions.SceneNames.Menu && mEndOfLevel)
            {
                if (mGameTimer < GameDefinitions.RecordTimes.SGG)
                {
                    AchievementManager.UnlockAchievement("Achievement_12");
                }
                mGameTimer = 0f;
                InterfaceManager.ChangeInterface(InterfaceManager.Interfaces.MainMenu);
                Screen.lockCursor = false;
            }
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    mLevelTimer = 0f;
            //}
            mEndOfLevel = false;
            //Debug.Log(mLevelTimer.ToString());
            AchievementChecking();
        }
    }

    void AchievementChecking()
    {
        if (Application.loadedLevelName == GameDefinitions.SceneNames.S01)
        {
            if (mNumDeaths > 0)
                AchievementManager.UnlockAchievement("Achievement_02");
        }
    }

    //Unchaged
    public override void EnterInterface()
    {//This should handle enabling all the buttons of the interface
        base.EnterInterface();
        mPaused = false;
        if (mButtons.ContainsKey("InGameMainMenu"))
        {
            mButtons["InGameMainMenu"].mEnabled = false;
            mButtons["InGameQuit"].mEnabled = false;
        }
        mLevelTimer = 0f;
        mGameTimer = 0f;
        mNumDeaths = 0;
    }

    public override void ExitInterface()
    {//this should handle disabling all the buttons of the interface
        base.ExitInterface();
    }
}
