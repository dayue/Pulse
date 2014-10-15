using UnityEngine;
using System.Collections;

public class AchievementProps : MonoBehaviour {

    public string iName;
    public Texture iDescription;
    public bool mUnlocked = false;
    private GUITexture mImage;

    public void Unlock()
    {
        mUnlocked = true;
        mImage.enabled = true;
    }

	// Use this for initialization
	void Awake () {
        mImage = GetComponent<GUITexture>();
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
