using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator animator;
    public Vector3 playerMoveDirection;
    public float playerMaxHealth;
    public float playerHealth;
    public int experience;
    public int currentLevel;
    public int maxLevel;
    public List<int> playerLevels;



    public float attackTime;
    public float attackCooldown;
    public GameObject attackObject;
    private bool isImmune;
    [SerializeField] private float immunityDuration;
    [SerializeField] private float immunityTimer;

    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        playerHealth = playerMaxHealth;
        UiController.Instance.UpdateHealthSlider();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector2(inputX, inputY).normalized;

        animator.SetFloat("MoveX", playerMoveDirection.x);
        animator.SetFloat("MoveY", playerMoveDirection.y);

        if (playerMoveDirection == Vector3.zero)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        attackTime += Time.deltaTime;
        if (attackTime >= attackCooldown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
            }
        }

        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        } else
        {
            isImmune = false;
        }

    }

    //updates once every 2ms
    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerMoveDirection.x * moveSpeed,
          playerMoveDirection.y * moveSpeed);
    }

    public void TakeDamage(float damage)
    {
        if(!isImmune){
            isImmune = true;
            immunityTimer = immunityDuration;
            playerHealth -= damage;
            UiController.Instance.UpdateHealthSlider();
            if (playerHealth <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
        }
    }
    
    public void Attack()
    {
        Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f);
        Instantiate(attackObject, transform.position + spawnOffset, transform.rotation);
    }

    public void GetExperience(int expToGet)
    {
        experience += expToGet;
    }

}
