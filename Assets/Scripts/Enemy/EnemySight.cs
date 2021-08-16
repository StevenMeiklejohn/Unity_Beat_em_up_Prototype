using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
  public bool playerInSight;
  public bool playerOnRight;
  public float targetDistance;
  public GameObject target;

  GameObject player;
  Vector3 playerRelativePosition;
  GameObject frontTarget;
  GameObject backTarget;
  float frontTargetDistance;
  float backTargetDistance;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        frontTarget = GameObject.Find("EnemyFrontTarget");
        backTarget = GameObject.Find("EnemyBackTarget");

    }

    // Update is called once per frame
    void Update()
    {

      playerRelativePosition = player.transform.position - gameObject.transform.position;
      if(playerRelativePosition.x > 0){
        playerOnRight = true;
      }else if (playerRelativePosition.x < 0){
        playerOnRight = false;
      }

      frontTargetDistance = Vector3.Distance(frontTarget.transform.position, gameObject.transform.position);
      backTargetDistance = Vector3.Distance(backTarget.transform.position, gameObject.transform.position);
      if(frontTargetDistance < backTargetDistance){
        target = frontTarget;
      }else if(backTargetDistance < frontTargetDistance){
        target = backTarget;
      }
      targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
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
