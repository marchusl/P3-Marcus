using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public float health = 30f;
    public float speed;
    public float myDamange;
    private PlayerUI instance;
    private WaveScript reference;
    private float standardDamage;

    void Start()
    {
        target = GameObject.Find("EnemyGoal").transform; 
        rb = GetComponent<Rigidbody>();
        myDamange = 5f;
        health = 50f;
        instance = GameObject.Find("PlayerHolder").GetComponent<PlayerUI>();
        reference = GameObject.Find("GameManager").GetComponent<WaveScript>();
        standardDamage = 15f;
        speed = reference.enemySpeed;
    }
    void Update()
    {
        Movement();
    }
    public void TakeDamage(float amount)
    {
        Debug.Log("bullet was succesful");
        health -= amount;
        if (health <= 0)
        {
            reference.enemyCount--;
            instance.currentScore++;
            DeathChecker();
        }
    }
    void DeathChecker()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void Movement()
    {
        transform.LookAt(target);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            reference.enemyCount--;
            instance.TakeDamage(myDamange);
            Destroy(gameObject);

        }
        if (other.gameObject.tag == "Bullet")
        {
            reference.enemyCount--;
            instance.currentScore++;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Deathzone")
        {
            reference.enemyCount--;
            Destroy(gameObject);
        }
    }
}
