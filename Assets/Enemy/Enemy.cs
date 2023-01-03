using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{



    public float speed = 3f;
    
 [SerializeField] private Transform target;
 [SerializeField]    private Rigidbody rb;

    void Start()
    {  
       
        rb = GetComponent<Rigidbody>();
        
      
    }
 
  public void FacePlayer(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        
    }
   
 
  void Update()
 {
      
     FacePlayer();
     movetowardPlayer();
       
       
 }
  public void movetowardPlayer(){
        rb.velocity = transform.forward * speed;
    }
 
 


  

   
    

   
    



}
