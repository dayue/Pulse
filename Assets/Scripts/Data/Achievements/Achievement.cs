using UnityEngine;
using System.Collections;

public class Achievement {

    public string iName;
    public bool mUnlocked = false;
    public int SpriteIndex = 0;

    public void Unlock()
    {
        mUnlocked = true;
    }
}
