using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMov : MonoBehaviour
{  
  public Rigidbody rb;
  public float speed;
  bool isGrounded;

 public Animator anim;
 float jumptime = .2f;
 bool isJumping = false;
 [SerializeField]private ParticleSystem part; // Health Particles

 

void Update(){
    // Move forward
    if(Input.GetKey(KeyCode.D)){
        Back();
    }
    // Move backward
    else if(Input.GetKey(KeyCode.A)){
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
    Quaternion quaternion = Quaternion.Euler(0, 90, 0);
    transform.rotation = quaternion; 
    rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);  
    anim.SetBool("Idle",false);
    anim.SetTrigger("Run");            
}

public void Back(){
    Quaternion quaternion = Quaternion.Euler(0, -90, 0) ;
    transform.rotation = quaternion ;
    rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);  
    anim.SetBool("Idle",false);
    anim.SetTrigger("Run"); 
}

public void Jump(){
    anim.SetBool("Run",false);
    anim.SetTrigger("Jump"); 
    rb.AddForce(new Vector2(0f, 6000f * speed));
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