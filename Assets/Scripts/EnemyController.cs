using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float groundCheckDistance = 0.6f;
    [SerializeField] private LayerMask whatIsGround;
    private bool movingRight = true;

    [Header("Combat Settings")]
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;
    [SerializeField] private int damage = 1;
    private Rigidbody2D enemyRigidBody;
    private EnemySpawner spawner;
    private bool canChangeDirection = true;
    // Start is called before the first frame update
    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void Initialize(EnemySpawner spawnerReference){
        spawner = spawnerReference;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    //Watched tutorial, this function does this: link
    void Move(){
        Vector2 groundCheckPosition = movingRight ?
            new Vector2(transform.position.x + 0.5f, transform.position.y):
            new Vector2(transform.position.x - 0.5f, transform.position.y);

        bool isGrounded = Physics2D.Raycast(groundCheckPosition, Vector2.down, groundCheckDistance, whatIsGround);
        if(!isGrounded && canChangeDirection){
            movingRight =!movingRight;
            StartCoroutine(DelayDirectionChange());
        }
        //borrowed from naoise's class it does this...
        enemyRigidBody.velocity = movingRight ?
            new Vector2(moveSpeed, enemyRigidBody.velocity.y):
            new Vector2(-moveSpeed, enemyRigidBody.velocity.y);
    }

    IEnumerator DelayDirectionChange(){
        canChangeDirection = false;
        yield return new WaitForSeconds(0.5f);
        canChangeDirection = true;
    }

    public void TakeDamage(int damageAmount){
        currentHealth -= damageAmount;
        if(currentHealth<=0){
            Die();
        }
    }
    public void Die(){
        if(spawner != null){
            spawner.EnemyDied();
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        AdvancedPlayerMovement player = collision.gameObject.GetComponent<AdvancedPlayerMovement>();
        if (player != null){
            Debug.Log("Player took damage");
        }
    }
}
