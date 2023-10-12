using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializedField] private float moveSpeed = 5f;
    [SerializedField] private float groundCheckDistance = 0.6f;
    [SerializedField] private LayerMask whatIsGround;
    private bool movingRight = true;

    [Header("Combat Settings")]
    [SerializedField] private int maxHealth = 5;
    private int currentHealth;
    [SerializedField] private int damage = 1;
    private Rigidbody2D enemyRigidBody;
    //private EnemySpawner spawner;

    // Start is called before the first frame update
    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move(){
        Vector2 groundCheckPosition = movingRight ?
            new Vector2(moveSpeed, enemyRigidBody.velocity.y):
            new Vector2(-moveSpeed, enemyRigidBody.velocity.y):
    }
}
