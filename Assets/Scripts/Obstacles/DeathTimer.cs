using UnityEngine;
using System.Collections;

public class DeathTimer : MonoBehaviour {

    private float mLifeSpan = 0f;

    public void SetLifeSpan(float iLifeSpan)
    {
        mLifeSpan = iLifeSpan;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        mLifeSpan -= Time.deltaTime;
        if (mLifeSpan <= 0f)
            Destroy(gameObject);
	}
}
