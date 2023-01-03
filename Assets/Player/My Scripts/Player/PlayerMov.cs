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
  

 
    if(Input.GetKeyDown(KeyCode.D)){
   
      Back();
      }
   else if(Input.GetKeyDown(KeyCode.A)){
   
      Forward();
      }
        if (Input.GetKey(KeyCode.Space)){
       {
       
       Jump();  

       }
      }
 
Idle();

}

void Idle(){
 
   if(transform.rotation.y > 0){
    anim.SetBool("Idle",true);
          Quaternion quaternion = Quaternion.Euler(0, 90, 0);
          transform.rotation = quaternion;
         }else if(transform.rotation.y < 45){
          anim.SetBool("Idle",true);
          Quaternion quaternion = Quaternion.Euler(0, -90, 0);
          transform.rotation = quaternion;
         }
         transform.Rotate(0, 0, 0);
}

 
public void Forward(){

  Quaternion quaternion = Quaternion.Euler(0, 90, 0);
        transform.rotation = quaternion; 
       rb.transform.Translate(Vector3.forward * 2 * speed );  
        anim.SetBool("Idle",false);
        anim.SetTrigger("Run");            
  }

public void Back(){
        Quaternion quaternion = Quaternion.Euler(0, -90, 0) ;
        transform.rotation = quaternion ;
       rb.transform.Translate(Vector3.forward * 2 * speed );  

        anim.SetBool("Idle",false);
          anim.SetTrigger("Run"); 
 }

public void Jump(){
 
    anim.SetBool("Run",false);
   anim.SetTrigger("Jump"); 
  	 isGrounded = false;
			rb.AddForce(new Vector2(0f, 6000f * speed));

       if(!isGrounded){   
  
         StartCoroutine(ResetJump());    
         }
       

          
    }

    
IEnumerator ResetJump(){
   
  yield return new WaitForSeconds(1f);
    transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
  isGrounded = true;
}


public void Stop(){
     anim.SetBool("Run",false);  
     anim.SetBool("Idle",true);
      
    }



 void OnTriggerEnter(Collider Col) {
    if(Col.gameObject.tag == "Enemy"){ 
     
        PlayerHud playerHud = GetComponent<PlayerHud>();
        playerHud.takeDamage(10);
        part.Play();
         Debug.Log("Health "+ playerHud.currentHealth );  
         Destroy(Col.gameObject);
        }

}


  



}//class

