using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
  public GameObject Enemys;
public Transform[] SpawnPoints;
public float STspwan;
float TBspwan; 



    void Start()
    {
           TBspwan = STspwan;
    }

    // Update is called once per frame
    void Update()
    {
          Spawnner();
    }

void Spawnner(){
   if(TBspwan <= 0 ){
      Vector3 spawnPosition = SpawnPoints[Random.Range(0,SpawnPoints.Length)].position; 
    Instantiate(Enemys, spawnPosition,Quaternion.identity);

    TBspwan = STspwan;
   }else{
     TBspwan -= Time.deltaTime;
   } 
}


}
