using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {

    public enum Follow
    {
        Loop = 0,
        RestartLoop,
        BackAndForth,
    }

    public Follow mFollow;
    public List<GameObject> mPath = new List<GameObject>();
    public float mSpeed = 10f;

    private GameObject mCurrentTarget;
    private bool mReversing = false;
    private bool mStarted = false;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!mStarted && mPath.Count > 1)
        {
            mStarted = true;
            mCurrentTarget = mPath[0];
        }

        if (mStarted)
        {
            if(CheckCloseness(0.5f))
                GetNextTarget();
            MoveInDirection();
        }
	}

    bool CheckCloseness(float iDistance)
    {
        float l_distance = (transform.position - mCurrentTarget.transform.position).magnitude;
        if (l_distance < iDistance)
            return true;
        else
            return false;
    }

    void GetNextTarget()
    {
        if (mFollow == Follow.Loop)
            GetNextTargetLoop();
        else if (mFollow == Follow.RestartLoop)
            GetNextTargetRestartLoop();
        else if (mFollow == Follow.BackAndForth)
            GetNextTargetBackForth();
    }

    void GetNextTargetBackForth()
    {
        int l_index = mPath.IndexOf(mCurrentTarget);
        if (l_index == mPath.Count - 1)// && mReversing == false)
        {//Current node is the last one
            mReversing = true;    
        }
        else if (l_index == 0)// && mReversing == true)
        {//current node is the first one
            mReversing = false;
        }
        if (mReversing)
        {
            l_index--;
        }
        else if (!mReversing)
        {
            l_index++;
        }
        mCurrentTarget = mPath[l_index];
    }

    void GetNextTargetLoop()
    {
        int l_index = mPath.IndexOf(mCurrentTarget);
        if (l_index == mPath.Count - 1)
        {//current node is the last one
            l_index = 0;
        }
        else
        {
            l_index++;
        }
        mCurrentTarget = mPath[l_index];
    }

    void GetNextTargetRestartLoop()
    {
        int l_index = mPath.IndexOf(mCurrentTarget);
        if (l_index == mPath.Count - 1)
        {//current node is the last one
            l_index = 1;
            transform.position = mPath[0].transform.position;
        }
        else
        {
            l_index++;
        }
        mCurrentTarget = mPath[l_index];
    }

    void MoveInDirection()
    {
        Vector3 l_direction = (mCurrentTarget.transform.position - transform.position).normalized * mSpeed;
        transform.position = Vector3.Lerp(transform.position, transform.position + l_direction, Time.deltaTime);
    }
}
