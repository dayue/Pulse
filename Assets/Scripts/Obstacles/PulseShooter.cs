using UnityEngine;
using System.Collections;

public class PulseShooter : MonoBehaviour {

    public GameObject iPulsorPrefab;
    public float iOffset = 0f;
    public float iRateOfFire = 5f;
    public float iSpeed = 10f;
    public float iLifeSpan = 0f;
    public Vector3 iDirection = Vector3.left;

    private float mFireTimer = 0f;

	// Use this for initialization
	void Start ()
    {
        mFireTimer = iOffset;
	}
	
	// Update is called once per frame
	void Update ()
    {
        mFireTimer -= Time.deltaTime;
        if (mFireTimer <= 0f)
        {
            mFireTimer = iRateOfFire;
            GameObject l_projectile = (GameObject)GameObject.Instantiate(iPulsorPrefab, transform.position, Quaternion.identity);
            l_projectile.rigidbody.velocity = iDirection.normalized * iSpeed;
            if (iLifeSpan > 0f)
            {
                DeathTimer l_deathTimer = l_projectile.AddComponent<DeathTimer>();
                l_deathTimer.SetLifeSpan(iLifeSpan);
            }
        }
	}
}
