using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Player : GameUnit
{
    private int coin = 0;
    [SerializeField] private LaneData laneData;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    private Transform[] laneTransforms;
    private float moveSpeed = 10f;
    [SerializeField] float jumpHeight = 2.5f;

    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] [Range(0 , 1)] private float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] GamePlay gamePlay;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeDistanceThreshold = 50.0f; 
    [SerializeField] private float fallForce = 10.0f;
    private bool isJumping = false;
    
    
    private Vector3 destination;
    private int currentLaneIndex = 0;
    private string currentAnim;


    float transformX;
    public int Coin { get => coin;}


    void Start()
    {
        laneTransforms = laneData.GetTransform();


    }

    void Update()
    {
        if(!GameManager.Instance.IsState(GameState.Gameplay))
            return;
        else if(gamePlay == null)
        {
            gamePlay = FindObjectOfType<GamePlay>();
            
        }
        else if(IsGrounded())
        {
    
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("jump");
        }
    

        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            float swipeDistanceX = Mathf.Abs(touchEndPos.x - touchStartPos.x);
            float swipeDistanceY = Mathf.Abs(touchEndPos.y - touchStartPos.y);

            if (swipeDistanceX > swipeDistanceThreshold || swipeDistanceY > swipeDistanceThreshold)
            {
                float swipeDirectionX = touchEndPos.x - touchStartPos.x;
                float swipeDirectionY = touchEndPos.y - touchStartPos.y;

                if (Mathf.Abs(swipeDirectionX) > Mathf.Abs(swipeDirectionY))
                {
                    if (swipeDirectionX > 0)
                    {
                        // Swipe phải
                        MoveRight();
                    }
                    else if (swipeDirectionX < 0)
                    {
                        // Swipe trái
                        MoveLeft();
                    }
                }
                else
                {
                    if (swipeDirectionY > 0)
                    {
                        // Swipe lên
                        Jump();
                    }
                    else if (swipeDirectionY < 0)
                    {
                        // Swipe xuống
                        Fall();
                    }
                }
            }
        }
        AutoMove();
        
    }

    private void AutoMove()
    {
        
        transformX = Mathf.Lerp(transform.position.x, destination.x, moveSpeed * Time.deltaTime); 
        transform.position = new Vector3(transformX, transform.position.y, transform.position.z);
    }


    private void Jump()
    {
        if(!IsGrounded())
            return;
        
        float jumpUpSpeed = Mathf.Sqrt(2* jumpHeight * Physics.gravity.magnitude);
        rb.AddForce(new Vector3(0f, jumpUpSpeed, 0f), ForceMode.VelocityChange);
        isJumping = true;
    }

    private void Fall()
    {
        if (isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, -fallForce, rb.velocity.z);
            isJumping = false;
        }
    }

    public void OnInit()
    {
        coin = 0;
        if(gamePlay != null)
        {
            gamePlay.SetCoin(0);
        }
        moveSpeed = LevelManager.Instance.EvnMoveSpeed;
        
        currentLaneIndex = 1;
        
        
    }


    private void MoveRight()
    {
        if(currentLaneIndex == 2)
        {
            return;
        }

        currentLaneIndex++;
        destination = laneTransforms[currentLaneIndex].position;

    }

    private void MoveLeft()
    {
        if(currentLaneIndex == 0)
        {
            return;
        }

        currentLaneIndex--;
        destination = laneTransforms[currentLaneIndex].position;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundCheckMask);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void ResetTransform()
    {
        destination = laneTransforms[1].position;
    }


    private void OnTriggerEnter(Collider other) 
    {
        
        if(other.CompareTag("DeathZone"))
        {
            LevelManager.Instance.GameOver();
            AudioManager.Instance.PlaySFX("hit");
            ResetTransform();
            return;
        }

        if(other.CompareTag("Coin"))
        {
            Coin coinComponent = Cache.GetCoin(other);
            if (coinComponent != null)
            {
                coinComponent.OnDespawn();
                coin++;
                gamePlay.SetCoin(coin);
                AudioManager.Instance.PlaySFX("coinPickUp");
            }
        } 

        if(other.CompareTag("SpeedUp"))
        {
            LevelManager.Instance.InscreaseEvnSpeed();
            other.gameObject.GetComponent<SpeedUp>().OnDespawn();
        }

    }




}
