/*
 * Jacob Vanderwill
 * created: 10/16/24
 * last edited: 11/12/24
 * Make a script that will make an enemy pace or chase the target if they are close enough
 */

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Chasetarget : MonoBehaviour
{
    public GameObject target;

    [Tooltip("If left un-changed it will be set to current pos when started")]
    public Vector3 startPos;

    private Vector3 targetPos;

    public Vector3[] wayPoints = new Vector3[1];
    [Tooltip("Speed to pace at")]
    public int paceSpeed = 20;
    [Tooltip("Speed to chase the player at")]
    public int chaseSpeed = 50;
    private int currentSpeed = 30;
    [Tooltip("Distance BEFORE the player is seen to start chasing")]
    public float TriggerDistanceBeforeSeen = 5;
    [Tooltip("Distance AFTER the player is seen to start chasing")]
    public float TriggerDistanceAfterSeen = 10;
    [Tooltip("Jump trigger distance")]
    public float jumpDist = 3;
    [Tooltip("Jump Strength")]
    public float jumpForce = 20;

    // layermasks
    [Tooltip("Whatever the ai can NOT see through")]
    public LayerMask WallLayer;
    [Tooltip("what will kill the ai (for example the Water)")]
    public LayerMask DeathLayer;
    [Tooltip("The layer that is the ground")]
    public LayerMask GroundLayer;

    private float jumpWaitTime = 0.1f; // (Cooldown)
    private float jumpWaitTimer;

    private Vector3 checkPosLeft;
    private Vector3 checkPosRight;

    private quaternion lockedRotation;

    private float closeEnough = 0.3f;
    public float TriggerDist = 1;
    private Vector2 lastPos;
    public Vector3 lastKnownTargetPos;
    public bool isGrounded;
    
    public bool chasing = false;
    private bool returningToPace;
    private Vector3 jump;
    private int currentWaypoint = 0;
    private Rigidbody2D myRB;

    public bool ShowGizmos = true;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        jump = new Vector3(0.0f, 2.0f, 0.0f);

        lastPos = myRB.position;
        lastKnownTargetPos = myRB.position;
        jumpWaitTimer = 0;

        checkPosLeft = new Vector3(transform.position.x - 1, transform.position.y - 2, transform.position.z);
        checkPosRight = new Vector3(transform.position.x + 1, transform.position.y - 2, transform.position.z);

        lockedRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = target.transform.position;

        transform.rotation = lockedRotation;

        // in case the enemy somehow gets it's z value changed it is reset back to the proper place
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);

        checkGroundStatus();
    }

    private void FixedUpdate()
    {
        paceOrChase();
    }

    void paceOrChase()
    {
        // if ai is stuck allow it to jump to attempt to get unstuck
        if (jumpWaitTimer < jumpWaitTime && wayPoints != null)
        {
            if (lastPos == myRB.position)
            {
                if (Vector3.Distance(transform.position, targetPos) > 1)
                    checkJump();
            }
            lastPos = myRB.position;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                            (targetPos - transform.position).normalized,
                                            Vector3.Distance(transform.position, targetPos),
                                            WallLayer);

        // chase
        Vector3 totTarget = targetPos - transform.position;
        if (hit.collider == null && totTarget.sqrMagnitude < TriggerDist * TriggerDist)
        {
            chase(totTarget);
            chasing = true;
            lastKnownTargetPos = targetPos;
            TriggerDist = TriggerDistanceAfterSeen;
        }
        // goto last known pos
        else if (chasing)
        {
            goToLastKnownTargetPos();
            Vector3 lastKnownDirection = lastKnownTargetPos - transform.position;
            if (Mathf.Abs(myRB.position.x - lastKnownTargetPos.x) <= closeEnough)
            {
                chasing = false;
                returningToPace = true;
            }
        }
        // if the ai is returning to pace let it maintane its faster speed
        else if (returningToPace)
        {
            currentSpeed = chaseSpeed;
            TriggerDist = TriggerDistanceBeforeSeen;
            myRB.AddForce((wayPoints[currentWaypoint] - transform.position).normalized * currentSpeed);
            if ((wayPoints[currentWaypoint] - transform.position).sqrMagnitude < closeEnough * closeEnough)
            {
                returningToPace = false;
            }
        }
        // waypoints
        else if (wayPoints != null)
        {
            TriggerDist = TriggerDistanceBeforeSeen;
            pace();
        }
        // check if jump is needed
        jumpIfNeeded();
    }
    void chase(Vector3 totTarget)
    {
        currentSpeed = chaseSpeed;
        // if target is close enough and higher, jump
        if (targetPos.y - 1 > myRB.position.y && totTarget.sqrMagnitude < jumpDist * jumpDist)
        {
            checkJump();
        }
        totTarget.y = 0;
        totTarget.z = 0;
        // add force towards the target
        myRB.AddForce(totTarget.normalized * currentSpeed);
    }
    void goToLastKnownTargetPos()
    {
        currentSpeed = chaseSpeed;
        Vector3 lastKnownDirection = lastKnownTargetPos - transform.position;
        if (lastKnownTargetPos.y - 0.1 > myRB.position.y && lastKnownDirection.sqrMagnitude < jumpDist * jumpDist)
        {
            checkJump();
        }
        lastKnownDirection.z = 0;
        lastKnownDirection.y = 0;
        myRB.AddForce(lastKnownDirection.normalized * currentSpeed);
    }
    void jumpIfNeeded()
    {
        checkPosLeft = new Vector3(transform.position.x - 0.8f, transform.position.y - 2, transform.position.z);
        checkPosRight = new Vector3(transform.position.x + 0.8f, transform.position.y - 2, transform.position.z);

        RaycastHit2D JumpCheckLeft = Physics2D.Raycast(transform.position,
                                                      (checkPosLeft - transform.position).normalized,
                                                      Vector3.Distance(transform.position, checkPosLeft),
                                                      DeathLayer);
        RaycastHit2D JumpCheckRight = Physics2D.Raycast(transform.position, 
                                                       (checkPosRight - transform.position).normalized,
                                                       Vector3.Distance(transform.position, checkPosRight),
                                                       DeathLayer);
        // make the ai go to the edge of the cliff before jumping
        RaycastHit2D JumpCheckLeftHitWallQ = Physics2D.Raycast(transform.position,
                                            (checkPosLeft - transform.position).normalized,
                                            Vector3.Distance(transform.position, checkPosLeft),
                                            WallLayer);
        RaycastHit2D JumpCheckRightHitWallQ = Physics2D.Raycast(transform.position,
                                                       (checkPosRight - transform.position).normalized,
                                                       Vector3.Distance(transform.position, checkPosRight),
                                                       WallLayer);

        if (JumpCheckLeft.collider != null && JumpCheckLeftHitWallQ.collider == null)
        {
            // dont let the ai jump if it isn't headed for the fall it is detecting
            if (myRB.velocity.x < 0)
                checkJump();
        }
        if (JumpCheckRight.collider != null && JumpCheckRightHitWallQ.collider == null)
        {
            if (myRB.velocity.x > 0)
                checkJump();
        }
    }    
    void pace()
    {
        if (wayPoints != null)
        {
            // ensure that the ai can make jumps by allowing it to travel faster in the air
            if (isGrounded)
            currentSpeed = paceSpeed;
        else
            currentSpeed = chaseSpeed;
            // check if we are close enough to current waypoint
            Vector3 toWaypoint = wayPoints[currentWaypoint] - transform.position;
            // neutralize the third demension
            toWaypoint.z = 0;

            if (toWaypoint.sqrMagnitude < closeEnough * closeEnough)
                {
                // move to next waypoint
                currentWaypoint++;
                if (currentWaypoint >= wayPoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            else
            {
                // follow the waypoint
                myRB.AddForce(toWaypoint.normalized * currentSpeed);
            }
        }
    }
    void checkGroundStatus()
    {
        Vector3 groundCheckPos = new Vector3(transform.position.x,
                                                  transform.position.y - 0.7f,
                                                  transform.position.z) -
                                                  transform.position;

        RaycastHit2D onGround = Physics2D.Raycast(transform.position,
                                                 groundCheckPos.normalized,
                                                  0.7f,
                                                  GroundLayer);
        if (onGround.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void checkJump()
    {
        if (isGrounded)
        {
            Debug.Log("Enemy: Jump", myRB);
            myRB.AddForce(jumpForce * jump, ForceMode2D.Impulse);
            isGrounded = false;
        }
        else
        {
            Debug.Log("Enemy: Not Jumping", myRB);
        }
    }
    private void OnDrawGizmos()
    {
        if (ShowGizmos)
        {
            // draw wayPoints
            if (wayPoints != null)
            {
                foreach (Vector3 waypoint in wayPoints)
                {
                    if (waypoint == wayPoints[currentWaypoint])
                    {
                        Gizmos.color = Color.green;
                    }
                    else
                        Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(waypoint, closeEnough);
                }
            }

            // draw trigger distance
            if (chasing)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, TriggerDistanceBeforeSeen);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, TriggerDistanceAfterSeen);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, TriggerDistanceBeforeSeen);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, TriggerDistanceAfterSeen);
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, jumpDist);

            // draw jump checkers
            checkPosLeft = new Vector3(transform.position.x - 0.8f, transform.position.y - 2, transform.position.z);
            checkPosRight = new Vector3(transform.position.x + 0.8f, transform.position.y - 2, transform.position.z);

            RaycastHit2D JumpCheckLeft = Physics2D.Raycast(transform.position,
                                                (checkPosLeft - transform.position).normalized,
                                                Vector3.Distance(transform.position, checkPosLeft),
                                                DeathLayer);
            RaycastHit2D JumpCheckRight = Physics2D.Raycast(transform.position,
                                                           (checkPosRight - transform.position).normalized,
                                                           Vector3.Distance(transform.position, checkPosRight),
                                                           DeathLayer);
            // draw left
            if (JumpCheckLeft.collider != null)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawLine(transform.position, checkPosLeft);
            // draw right
            if (JumpCheckRight.collider != null)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawLine(transform.position, checkPosRight);
            // draw ground status
            Vector3 groundCheckPos = new Vector3(transform.position.x,
                                                      transform.position.y - 0.7f,
                                                      transform.position.z);

            RaycastHit2D onGround = Physics2D.Raycast(transform.position,
                                                     (groundCheckPos).normalized,
                                                      Vector3.Distance(transform.position, groundCheckPos),
                                                      GroundLayer);
            if (onGround.collider != null)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawWireSphere(groundCheckPos, 0.1f);

            // draw visibility
            if (target == null) return; // if there is no target then don't show any of the chasing stuff

            Vector2 direction = (targetPos - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, TriggerDist, WallLayer);

            // Debug.Log("Raycast Direction: " + direction);
            // Debug.Log("Raycast Distance: " + Vector3.Distance(transform.position, targetPos));
            // Debug.Log("Hit: " + (hit.collider != null ? hit.collider.name : "None"));

            if (hit.collider == null && Vector3.Distance(transform.position, targetPos) <= TriggerDist)
            {
                // draw line to target
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, targetPos);
                Gizmos.DrawWireSphere(targetPos, 0.3f);

                Gizmos.color = Color.red;
            }
            else
            {
                // draw line to target
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, targetPos);
                Gizmos.DrawWireSphere(targetPos, 0.3f);

                // draw line to last known pos
                if (chasing)
                    Gizmos.color = Color.green;
            }
            // draw line to last known pos
            Gizmos.DrawLine(transform.position, lastKnownTargetPos);
            Gizmos.DrawWireSphere(lastKnownTargetPos, 0.3f);
        }
    }
}
