using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    
 public Transform player;
    public float speed ;
    public int damage;
    private PlayerHud playerHud;
    public ParticleSystem part; // Damage Particles
    private Rigidbody rb;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHud = player.GetComponent<PlayerHud>();
          player = GameObject.Find("Player").transform;
    }

    void Update()
    { if(player != null)
        FacePlayer();
        if(IsGrounded()){
              rb.useGravity = false;
            MoveTowardPlayer();
        }   
      else{
        rb.useGravity = true;
      }
    }


    public void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void MoveTowardPlayer()
    {
        rb.velocity = transform.forward * speed;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 1f)
        {
            playerHud.takeDamage(damage);
            part.Play();
            Debug.Log("Player Health: " + playerHud.currentHealth);
            Destroy(gameObject);
        }
    }

   bool IsGrounded()
    {
           return Physics.CheckSphere(transform.position - new Vector3(0, player.position.y, 0), 1f, groundLayer);
    }









}//class