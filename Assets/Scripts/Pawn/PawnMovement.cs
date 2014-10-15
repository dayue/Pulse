using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PawnMovement : MonoBehaviour {

    public float iSensitivityX = 2f;

    public float iForwardSpeed = 1f;
    public float iStrafeSpeed = 1f;

    public Vector3 iRespawn = new Vector3(0f, 3f, 0f);
    public float iDeathLevel = -20f;
    public GameObject iGoCamera;

    public Material iMatGrounded;
    public Material iMatNotGrounded;

    private MasterPawn mMasterPawn;
    private MasterPlayerController mMasterPlayerController;

    private Vector3 mHitStart;
    private Vector3 mHitEnd;

    private Vector3 mGroundContact = Vector3.zero;
    private Vector3 mGroundNormal = Vector3.zero;
    private Vector3 mGroundLastContact = Vector3.zero;
    private Vector3 mGroundLastNormal = Vector3.zero;
    private bool mGrounded = false;

    private float timeInAir = 0f;

    private MovementProperties mMoveProps;
    [HideInInspector]
    public static bool mEnabled = true;
    
    [HideInInspector]
    public int numOfTouchingColliders = 0;

    [HideInInspector]
    public List<Collider> mListOfColliders;

	// Use this for initialization
	void Awake ()
    {
        mMasterPawn = GetComponent<MasterPawn>();
        mMoveProps = GetComponent<MovementProperties>();
	}

    void Start()
    {
        mMasterPlayerController = mMasterPawn.mMasterPlayerController;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!mEnabled)
            return;

        if (transform.position.y < iDeathLevel)
        {
            transform.position = iRespawn;// new Vector3(0f, 5f, 0f);
            transform.rotation = Quaternion.identity;
            InterfaceInGame.mNumDeaths++;
        }
        PawnRotation();

        Vector3 l_velocity = rigidbody.velocity;
        l_velocity = ApplyInputVelocityChange(l_velocity);
        l_velocity = ApplyGravityAndJumping(l_velocity);
        //l_velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = l_velocity;

        CheckGroundedThorough();

        if (mMoveProps.j_jumping)
        {
            //renderer.material = iMatNotGrounded;
        }
        else
        {
            //renderer.material = iMatGrounded;
        }

        if (mGrounded)
        {
            timeInAir = 0f;
        }
        else
        {
            timeInAir += Time.deltaTime;
        }
        if(!mMoveProps.j_jumpReleased)
            mMoveProps.j_jumpReleased = mMoveProps.j_releaseJumpButton();

        if (mGrounded && !IsGroundedTest())
        {
            mGrounded = false;
            //Debug.Log("Left Ground: " + mMoveProps.j_jumpTimer.ToString());
        }
        else if (!mGrounded && IsGroundedTest() && mMoveProps.j_jumpTimer > 0.2f)
        {
            mGrounded = true;
            mMoveProps.j_jumping = false;
            Debug.Log("Landed");
        }
        if (mMoveProps.j_jumpTimer < 0.2f)
        {
            mMoveProps.j_jumpTimer += Time.deltaTime;
        }
        mGroundLastNormal = mGroundNormal;
        mMoveProps.lastHitPoint = mMoveProps.hitPoint;

        Debug.DrawLine(mHitStart, mHitEnd, Color.black);
	}

    void RemoveDeadColliders()
    {
        List<Collider> l_remove = new List<Collider>();
        foreach (Collider l_en in mListOfColliders)
        {
            if (l_en == null)
                l_remove.Add(l_en);
        }
        foreach (Collider l_rem in l_remove)
        {
            mListOfColliders.Remove(l_rem);
            Debug.Log("Remove Collider");
        }
        numOfTouchingColliders = mListOfColliders.Count;
    }
    void CheckGroundedThorough()
    {
        RemoveDeadColliders();
        if (numOfTouchingColliders <= 0)
        {
            mGroundNormal = Vector3.zero;
            mGrounded = false;
            //Debug.Log("In Air");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!mListOfColliders.Contains(collision.collider))
        {
            mListOfColliders.Add(collision.collider);
            ++numOfTouchingColliders;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (mListOfColliders.Contains(collision.collider))
        {
            mListOfColliders.Remove(collision.collider);
            --numOfTouchingColliders;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        ContactPoint l_contactPoint = collision.contacts[0];
        if (l_contactPoint.normal.y > 0.01f && (l_contactPoint.point - transform.position).y < 0f)
        {
            if ((mMoveProps.hitPoint - mMoveProps.lastHitPoint).sqrMagnitude > 0.001f || mGroundLastNormal == Vector3.zero)
            {
                mGroundNormal = l_contactPoint.normal;
            }
            else
            {
                mGroundNormal = mGroundLastNormal;
            }
            mHitStart = l_contactPoint.point;
            mHitEnd = l_contactPoint.point + l_contactPoint.normal * 5f;
            mMoveProps.hitPoint = l_contactPoint.point;
            mGroundContact = l_contactPoint.point;
        }
    }

    private void PawnRotation()
    {
        //CHANGE THIS MOUSE MOVEMENT
        float l_xAxis = mMasterPlayerController.mPlayerInput.getMouseDelta().x; //SWAP FOR MOBILE
        //l_xAxis = 0f;
        //l_xAxis = mMasterPlayerController.mOnScreenRightAnalog.GetTouchLeftMouse().x;
        if (l_xAxis > iSensitivityX) l_xAxis = iSensitivityX;
        if (l_xAxis < -iSensitivityX) l_xAxis = -iSensitivityX;

        Quaternion l_rightRotation = Quaternion.EulerAngles(0f, l_xAxis*0.05f, 0f);

        Quaternion l_transformation = l_rightRotation;
        gameObject.transform.rotation = gameObject.transform.rotation * l_transformation;
    }

    private Vector3 ApplyInputVelocityChange(Vector3 iVelocity)
    {
        Vector3 l_desiredVelocity;
        l_desiredVelocity = GetDesiredHorizontalDirection();

        if (mGrounded)
        {
            l_desiredVelocity = AdjustGroundVelocityToNormal(l_desiredVelocity, mGroundNormal);
        }
        else
        {
            iVelocity.y = 0;
        }

        float maxVelocityChange = GetMaxAcceleration(mGrounded) * Time.deltaTime;
        Vector3 velocityChangeVector = (l_desiredVelocity - iVelocity);
        if (velocityChangeVector.magnitude > maxVelocityChange)
        {
            velocityChangeVector = velocityChangeVector.normalized * maxVelocityChange;
        }

        if (mGrounded)
        {
            iVelocity += velocityChangeVector;
            iVelocity.y = Mathf.Min(iVelocity.y, 0);
        }
        return iVelocity;
    }

    private Vector3 ApplyGravityAndJumping(Vector3 iVelocity)
    {
        if (!mMoveProps.j_pressJumpButton())
        {
            mMoveProps.j_lastButtonDownTime = -100f;
        }
        if (mMoveProps.j_pressJumpButton() && mMoveProps.j_lastButtonDownTime < 0f)
            mMoveProps.j_lastButtonDownTime = Time.time;

        if (mGrounded)
        {
            iVelocity.y = Mathf.Min(0, iVelocity.y) - mMoveProps.Gravity * Time.deltaTime;
        }
        else
        {
            iVelocity.y = iVelocity.y + mMoveProps.Gravity * timeInAir * 2f;//Time.deltaTime;

            //if (mMoveProps.j_holdingJumpButton() && mMoveProps.j_jumping)// && timeInAir < mMoveProps.j_extraHeight)
            {
                if(mMoveProps.j_jumping)//if (!mMoveProps.j_jumpReleased)
                {
                    //Debug.Log("Hold Jump");
                    iVelocity.y += Mathf.Clamp((mMoveProps.j_extraHeight - timeInAir), 0f, mMoveProps.j_extraHeight);
                }
            }
        }
        if (mGrounded)
        {
            if (mMoveProps.j_pressJumpButton() && (Time.time - mMoveProps.j_lastButtonDownTime < 0.2f))
            {
                Debug.Log("Jump");
                mGrounded = false;
                mMoveProps.j_jumping = true;
                mMoveProps.j_lastButtonDownTime = -100f;
                mMoveProps.j_jumpTimer = 0f;
                mMoveProps.j_jumpReleased = false;
                if (TooSteep())
                    mMoveProps.j_jumpDirection = Vector3.Slerp(Vector3.up, mGroundNormal, mMoveProps.j_steepPerpAmount);
                else
                    mMoveProps.j_jumpDirection = Vector3.Slerp(Vector3.up, mGroundNormal, mMoveProps.j_perpAmount);

                iVelocity.y = 0f;
                iVelocity += Vector3.up * CalculateJumpVerticalSpeed(mMoveProps.j_baseHeight);
            }
        }
        return iVelocity;
    }

    Vector3 GetDesiredHorizontalDirection()
    {
        Vector3 l_moveKeys = mMasterPlayerController.mPlayerInput.getKeyMovementInput();
        if (l_moveKeys == Vector3.zero)
        {
            return Vector3.zero;
        }
        Vector3 l_desiredDirection = transform.forward * l_moveKeys.z + transform.right * l_moveKeys.x;
        float l_maxSpeed = mMoveProps.MaxSpeed;
        if (!mGrounded)
        {
            l_maxSpeed = 0f;
        }
        return l_desiredDirection.normalized * l_maxSpeed;
    }

    Vector3 AdjustGroundVelocityToNormal(Vector3 iVelocity, Vector3 iGroundNormal)
    {
        Vector3 l_sideways = Vector3.Cross(Vector3.up, iVelocity);
        //Debug line for adjusted direction
        Debug.DrawLine(transform.position, transform.position + Vector3.Cross(l_sideways, iGroundNormal).normalized * 2f, Color.red);
        return Vector3.Cross(l_sideways, iGroundNormal).normalized * iVelocity.magnitude;
    }

    private float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        return Mathf.Sqrt(2f * targetJumpHeight * (-mMoveProps.Gravity));
    }

    private float GetMaxAcceleration(bool iGrounded)
    {
        if (iGrounded)
            return mMoveProps.maxGroundAcceleration;
        else
            return mMoveProps.maxAirAcceleration;
    }

    private bool IsGroundedTest()
    {
        if (numOfTouchingColliders <= 0)
        {
            return false;
        }
        return (mGroundNormal.y > 0.01f);
    }

    private bool TooSteep()
    {
        return (mGroundNormal.y <= Mathf.Cos(mMoveProps.SteepLimit * Mathf.Deg2Rad));
    }
}