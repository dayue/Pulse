using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

    public float iRotationSpeed = 5f;
    public Vector3 iDirection = new Vector3(0f, 0f, 0f);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Quaternion l_rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
        Quaternion l_rotation = Quaternion.Euler(iDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * l_rotation, iRotationSpeed * Time.deltaTime);	
	}
}
