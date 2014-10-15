using UnityEngine;
using System.Collections;

public class DistancePulser : MonoBehaviour {

    public float iPulseSpeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //SendOutPulse();
        //}
	}

    void SendOutPulse(Material iColor = null)
    {
        Object[] list = GameObject.FindGameObjectsWithTag("PulseReceiver");
        foreach (GameObject l_en in list)
        {
            ColourFade temp = l_en.GetComponent<ColourFade>();
            if (temp != null)
            {
                temp.PulseAfterTime(CalculateTimeToPulse(transform.position, l_en.transform.position), iColor);
            }            
        }
    }

    float CalculateTimeToPulse(Vector3 iPos1, Vector3 iPos2)
    {
        float l_distance = (iPos1 - iPos2).magnitude;
        return l_distance / iPulseSpeed;
    }
}
