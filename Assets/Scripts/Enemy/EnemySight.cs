using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
  public bool playerInSight;
  public GameObject player;
  public GameObject target;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
      target = player;
    }

    void OnTriggerStay(Collider other){
      if(other.gameObject == player){
        playerInSight = true;
      }

    }

    void OnTriggerExit(Collider other){
      if(other.gameObject == player){
        playerInSight = false;
      }
    }
}
