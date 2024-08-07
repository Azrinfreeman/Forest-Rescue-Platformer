using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public PlayerControl control;

    void Awake()
    {
        instance = this;
        control = new PlayerControl();

        control.Enable();

        control.Land.Move.performed += ctx =>
        {
            input = ctx.ReadValue<float>();
        };
    }

    public Rigidbody2D playerRB;
    public Animator animator;

    public Transform attackIndicator;

    [Header("Jumping Mechanic")]
    public bool isGrounded;
    public bool jumpButtonPressed;
    public Transform feetBottom;
    public float jumpForce;
    public float groundCheckCircle;
    public LayerMask groundLayer;

    [Header("Speed Moving Mechanic")]
    public float speed;
    private float tempSpeed;
    public float input;

    [Header("Behavior")]
    public bool isAttacking;
    public bool isDead;

    public bool isWin;

    public bool isHurt;

    public bool left,
        right;

    public float waitingToShotTime;
    private float tempWaitingShotTime;

    [Header("ScreenTransform")]
    public Transform EndScreen;
    public Transform PauseScreen;
    public Transform ObjectiveScreen;

    public Transform EndLevelScreen;

    [Header("OutsideObject")]
    public Transform arrowPrefab;
    public Button AttackButton1;

    public Transform DoorButton;

    // Start is called before the first frame update
    void Start()
    {
        AttackButton1 = GameObject
            .Find("Canvas")
            .transform.GetChild(1)
            .transform.GetChild(4)
            .GetComponent<Button>();
        tempWaitingShotTime = waitingToShotTime;
        waitingToShotTime = 0;
        tempSpeed = speed;
        right = true;
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackIndicator = transform.GetChild(1).GetComponent<Transform>();
        feetBottom = transform.GetChild(0).GetComponent<Transform>();

        EndScreen = GameObject
            .Find("Canvas")
            .transform.GetChild(2)
            .transform.GetComponent<Transform>();
        PauseScreen = GameObject
            .Find("Canvas")
            .transform.GetChild(3)
            .transform.GetComponent<Transform>();
        ObjectiveScreen = GameObject
            .Find("Canvas")
            .transform.GetChild(4)
            .transform.GetComponent<Transform>();
        EndLevelScreen = GameObject
            .Find("Canvas")
            .transform.GetChild(5)
            .transform.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //capture player input
        //input = Input.GetAxisRaw("Horizontal");
        //flip character and play animation


        if (waitingToShotTime > 0)
        {
            waitingToShotTime -= Time.deltaTime;
        }

        if (waitingToShotTime <= 0)
        {
            if (AttackButton1 != null)
            {
                AttackButton1.interactable = true;
            }
        }
        if (!isDead || !isWin)
        {
            if (animator.GetFloat("isRun") < 0.1f)
            {
                /*
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    //Debug.Log("attacks");
                    attack();
                }
                */
            }
            if (!isAttacking && !isHurt)
            {
                //input = Input.GetAxisRaw("Horizontal");
                if (input > 0)
                {
                    //GetComponent<SpriteRenderer>().flipX = false;
                    TurnRight();
                }
                if (input < 0)
                {
                    //GetComponent<SpriteRenderer>().flipX = true;
                    TurnLeft();
                }
                /*
                else
                {
                    animator.SetBool("isRun", false);
                }
                */animator.SetFloat("isRun", Mathf.Abs(input));
                playerRB.velocity = new Vector2(input * speed, playerRB.velocity.y);
                jumping();
            }
            else { }
        }
        else if (isDead)
        {
            animator.SetBool("isDead", true);
        }
    }

    public void ShowEndScreen()
    {
        EndScreen.gameObject.SetActive(true);
    }

    public void ShowEndLevelScreen()
    {
        EndLevelScreen.gameObject.SetActive(true);
    }

    public void TurnRight()
    {
        if (!right)
        {
            transform.localScale = new Vector2(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );

            right = true;
            left = false;
        }
    }

    public void TurnLeft()
    {
        if (!left)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

            right = false;
            left = true;
        }
    }

    public void Move(float _input)
    {
        input = _input;
    }

    void stopMoving()
    {
        speed = 0f;
    }

    void continueMoving()
    {
        speed = tempSpeed;
    }

    public void attack()
    {
        stopMoving();
        StartCoroutine(toAttack());
    }

    public void doneAttack()
    {
        if (!AttackButton.instance.isAttackPressedDown)
        {
            Debug.Log("StopAttacking Mobile");
            continueMoving();
            StartCoroutine(doneAttacking());
        }
        else if (AttackButton.instance.isAttackPressedDown)
        {
            Debug.Log("AttacK2 Mobile");
            animator.SetTrigger("triggerAttack2");
            StartCoroutine(toAttack2());
        }
        /*


        if (!Input.GetKey(KeyCode.LeftControl))
        {
            //Debug.Log("StopAttacking");
            continueMoving();
            StartCoroutine(doneAttacking());
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            // Debug.Log("AttacK2");
            animator.SetTrigger("triggerAttack2");
            StartCoroutine(toAttack2());
        }

        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemyAttack1"))
        {
            StartCoroutine(getHurt());
        }

        if (other.gameObject.tag.Equals("enemyThrow1"))
        {
            StartCoroutine(getHurt());
        }
        if (other.gameObject.tag.Equals("Hostage"))
        {
            StartCoroutine(getHostage(other));
        }
        if (other.gameObject.tag.Equals("EndPoint"))
        {
            isWin = true;
            other.gameObject.SetActive(false);
            // control.Disable();
            ShowEndLevelScreen();
        }

        if (other.gameObject.tag.Equals("HealthCollect"))
        {
            HealthController.instance.getHealth(10);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag.Equals("schoolDoor"))
        {
            DoorButton
                .GetComponent<Button>()
                .onClick.AddListener(
                    () => other.GetComponent<DoorController>().TeleportPlayerToLocation()
                );
            transform.Find("DoorIndicator").gameObject.SetActive(true);
            DoorButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("schoolDoor"))
        {
            DoorButton.GetComponent<Button>().onClick.RemoveAllListeners();
            transform.Find("DoorIndicator").gameObject.SetActive(false);

            DoorButton.gameObject.SetActive(false);
        }
    }

    IEnumerator getHostage(Collider2D other)
    {
        HostageController.instance.RescueHostage();
        if (
            !other
                .transform.GetChild(other.transform.childCount - 1)
                .GetComponent<AudioSource>()
                .isPlaying
        )
        {
            other
                .transform.GetChild(other.transform.childCount - 1)
                .GetComponent<AudioSource>()
                .Play();
        }
        yield return new WaitForSeconds(.2f);
        Destroy(other.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            //Physics2D.IgnoreCollision(
            //     other.gameObject.GetComponent<Collider2D>(),
            //     GetComponent<Collider2D>()
            //  );
        }
    }

    IEnumerator getHurt()
    {
        // Debug.Log("get hit");
        animator.SetTrigger("isHurt");
        stopMoving();
        isHurt = true;
        HealthController.instance.getDamage(10);
        yield return new WaitForSeconds(1f);
        continueMoving();
        isHurt = false;
    }

    IEnumerator toAttack2()
    {
        if (!AttackButton.instance.isAttackPressedDown)
        {
            continueMoving();
            StartCoroutine(doneAttacking());
        }
        else if (AttackButton.instance.isAttackPressedDown)
        { //Debug.Log("AttacK3");
            StartCoroutine(toAttack3());
        }
        /*
if (!Input.GetKey(KeyCode.LeftControl))
        {
            continueMoving();
            StartCoroutine(doneAttacking());
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //Debug.Log("AttacK3");
            StartCoroutine(toAttack3());
        }

        */
        yield return null;
    }

    IEnumerator toAttack3()
    {
        animator.SetTrigger("triggerAttack3");
        StartCoroutine(doneAttacking());
        yield return new WaitForSeconds(0.5f);
        continueMoving();
    }

    IEnumerator toAttack()
    {
        animator.SetTrigger("triggerAttack");
        isAttacking = true;
        yield return null;
    }

    IEnumerator doneAttacking()
    {
        isAttacking = false;
        yield return null;
    }

    public void jumping()
    {
        isGrounded = Physics2D.OverlapCircle(feetBottom.position, groundCheckCircle, groundLayer);
        if (isGrounded == true)
        {
            /*

if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("triggerJump");
                playerRB.velocity = Vector2.up * jumpForce;
            }
            */

            if (jumpButtonPressed == true)
            {
                playerRB.velocity = Vector2.up * jumpForce;
            }
        }
    }

    public void jumpButtonTrue()
    {
        if (isGrounded)
        {
            jumpButtonPressed = true;
            animator.SetTrigger("triggerJump");
        }
    }

    public void jumpButtonFalse()
    {
        jumpButtonPressed = false;
    }

    //shot arrows
    public void ShotArrow()
    {
        if (input == 0 && waitingToShotTime <= 0)
        {
            AttackButton1.interactable = true;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("shotAnimation"))
            {
                stopMoving();
                animator.SetTrigger("triggerShot");
                waitingToShotTime = tempWaitingShotTime;
                AttackButton1.interactable = false;
            }
        }
        else
        {
            AttackButton1.interactable = false;
        }
    }

    IEnumerator shootingArrow()
    {
        //
        //yield return new WaitForSeconds(0.53f);
        Instantiate(arrowPrefab, attackIndicator.position, Quaternion.identity);
        continueMoving();
        yield return null;
    }

    void FixedUpdate() { }
}
