using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static InterfaceManager mInterfaceManager;
    private static GUITexture mScreenFader;

    private static ButtonManager mButtonManager;
    private static TextManager mTextManager;
    private static TextFieldManager mTextFieldManager;
    private static AchievementManager mAchievementManager;

    private static bool mInitialised = false;
    private AudioSource mAudioSource;

    public float mFadeSpeed = 1.5f;
    public AudioClip iEnterLevel;
    public AudioClip iExitLevel;
    public AudioClip iButtonPress;
    private static bool mPlayButtonSound = false;
    private static string mNextLevel;
    private static bool mChangeLevel = false;
    private static bool mUnfadeLevel = false;
    private static float mExitTime = 5f;

    private bool mFadeToBlack = false;
    private bool mFadeToClear = false;

    private bool mAudioPlaying = false;

    public static bool mHardMode = false;

    void Awake()
    {
        if (mInitialised)
            return;

        DontDestroyOnLoad(this);

        mInitialised = true;

        mInterfaceManager = gameObject.AddComponent<InterfaceManager>();

        mButtonManager = gameObject.AddComponent<ButtonManager>();
        mTextManager = gameObject.AddComponent<TextManager>();
        mTextFieldManager = gameObject.AddComponent<TextFieldManager>();

        mScreenFader = GameObject.FindGameObjectWithTag("ScreenFader").GetComponent<GUITexture>();

        mAudioSource = gameObject.GetComponent<AudioSource>();

        mAchievementManager = gameObject.GetComponent<AchievementManager>();
        mAchievementManager.Initialise();

        Application.LoadLevel(GameDefinitions.SceneNames.Menu);
        mInterfaceManager.Initialise();
    }

    public static void PlayButtonSound()
    {
        mPlayButtonSound = true;
    }

    public static void RequestLevelChange(string iName)
    {
        mNextLevel = iName;
        mChangeLevel = true;
        PlayerInput.mEnabled = false;
        PawnMovement.mEnabled = false;
        InterfaceManager.mSafe = false;
    }
    void FadeToClear()
    {
        mScreenFader.color = Color.Lerp(mScreenFader.color, Color.clear, mFadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        mScreenFader.color = Color.Lerp(mScreenFader.color, Color.black, mFadeSpeed * Time.deltaTime);
    }

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}

        if (mPlayButtonSound)
        {
            mPlayButtonSound = false;
            AudioSource.PlayClipAtPoint(iButtonPress, transform.position);
        }

        if (mChangeLevel)
        {
            if (iEnterLevel != null && !mAudioPlaying)
            {
                mAudioPlaying = true;
                if(MasterPawn.mAudioSource == null)
                    mAudioSource.PlayOneShot(iEnterLevel);
                else
                    MasterPawn.mAudioSource.PlayOneShot(iEnterLevel);
            }
            FadeToBlack();
            if (mScreenFader.color.a > 0.95f)
            {
                mChangeLevel = false;
                Application.LoadLevel(mNextLevel);
                mNextLevel = "";
                mUnfadeLevel = true;
                mAudioPlaying = false;
                //mAudioSource.Stop();
                //if (MasterPawn.mAudioSource != null)
                    //MasterPawn.mAudioSource.Stop();
            }
        }
        if (mUnfadeLevel)
        {
            if (iExitLevel != null && !mAudioPlaying)
            {
                mAudioPlaying = true;
                //if (MasterPawn.mAudioSource == null)
                    mAudioSource.PlayOneShot(iExitLevel);
                //else
                    //MasterPawn.mAudioSource.PlayOneShot(iExitLevel);
            }
            FadeToClear();
            if (mScreenFader.color.a < 0.05f)
            {
                mUnfadeLevel = false;
                PlayerInput.mEnabled = true;
                PawnMovement.mEnabled = true;
                InterfaceInGame.mStopTimer = false;
                mAudioPlaying = false;
                mAudioSource.Stop();
                if (MasterPawn.mAudioSource != null)
                    MasterPawn.mAudioSource.Stop();
                InterfaceManager.mSafe = true;
            }
        }


        if (mFadeToBlack)
        {
            FadeToBlack();
            if (mScreenFader.color.a > 0.95f)
            {
                mFadeToBlack = false;
            }
        }
        else if (mFadeToClear)
        {
            FadeToClear();
            if (mScreenFader.color.a < 0.05f)
            {
                mFadeToClear = false;
            }
        }
	}
}
