using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHud : MonoBehaviour
{
 


 public int maxHealth;
 public int currentHealth;
 public HealthBar healthBar;

 private void Start(){
    
    Time.timeScale = 1f;   
    currentHealth = maxHealth;
    healthBar.SetMaxHealth(maxHealth);
    }

void Update(){   
   

    if(currentHealth <= 0)
    {  
        Time.timeScale = 0f;
    }
}

public void takeDamage(int damage){

  StartCoroutine(Damage(damage));
  
}

IEnumerator Damage(int damage){
  
      yield return new WaitForSeconds(0.5f);
      
      currentHealth -= damage;
      healthBar.SetHealth(currentHealth);
      // CameraShake();
    
 }


}


