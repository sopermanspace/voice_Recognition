using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{  
  public Rigidbody rb;
  public float speed;
  bool isGrounded;

 public Animator anim;
//  float jumptime = .2f;
//  bool isJumping = false;
 [SerializeField]private ParticleSystem part; // Health Particles

void Update(){
    anim.SetBool("Run",true);
    // Move forward
    if(Input.GetKeyDown(KeyCode.A)){
        Back();
    }
    // Move backward
    else if(Input.GetKeyDown(KeyCode.D)){
        Forward();
    }
    // Jump
    if (Input.GetKey(KeyCode.Space) && isGrounded){
        Jump();
    }
    // Stop
    else if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)){
        Stop();
    }
}

public void Forward(){
     anim.SetBool("Idle",false);
    Quaternion quaternion = Quaternion.Euler(0, 90, 0);
    transform.rotation = quaternion; 
    rb.transform.Translate(Vector3.forward * speed );  
    anim.SetBool("Run",true);            
}

public void Back(){  
    // if Command is "Back" or "Turn" 
    anim.SetBool("Idle",false);
    Quaternion quaternion = Quaternion.Euler(0, -90, 0) ;
    transform.rotation = quaternion ;
    rb.transform.Translate(Vector3.forward * speed );  
    anim.SetBool("Run",true); 
}

public void Jump(){
    anim.SetBool("Run",false);
    anim.SetTrigger("Jump"); 
    rb.AddForce(new Vector2(0f, 9000f * speed));
    isGrounded = false;
}

public void Stop(){
    anim.SetBool("Run",false);  
    anim.SetBool("Idle",true);
}

public void OnCollisionEnter(Collision collision){
    if(collision.gameObject.CompareTag("Ground")){
        isGrounded = true;
    }
}

public void OnTriggerEnter(Collider Col) {
    if(Col.gameObject.CompareTag("Enemy")){ 
        PlayerHud playerHud = GetComponent<PlayerHud>();
        playerHud.takeDamage(10);
        part.Play();
        Debug.Log("Health "+ playerHud.currentHealth );  
        Destroy(Col.gameObject);
    }
}



}//class