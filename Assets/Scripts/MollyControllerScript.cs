using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MollyControllerScript : MonoBehaviour
{

  public float walkMovementSpeed;
  public float runMovementSpeed;
  public float attackMovementSpeed;
  public float xMin, xMax, zMin, zMax;
  private float movementSpeed;
  private bool facingRight;
  private Rigidbody rigidBody;
  public bool tookDamage;
  public bool knockedDown;
  public float stunTime;
  Animator animator;
  Stats otherStats;


  // Animation state Machine
  AnimatorStateInfo currentStateInfo;
  static int currentState;
  static int idleState = Animator.StringToHash("Base Layer.Idle");
  static int walkState = Animator.StringToHash("Base Layer.Walk");
  static int runState = Animator.StringToHash("Base Layer.Run");
  static int jumpState = Animator.StringToHash("Base Layer.Jump");
  static int takeDamageState = Animator.StringToHash("Base Layer.TakeDamage");
  static int attack1State = Animator.StringToHash("Base Layer.Attack1");
  static int attack2State = Animator.StringToHash("Base Layer.Attack2");
  static int attack3State = Animator.StringToHash("Base Layer.Attack3");
  static int blockState = Animator.StringToHash("Base Layer.Block");
  static int FallState = Animator.StringToHash("Base Layer.Fall");





  // Run variables
  // Half a second before reset.
  float buttonCooler = 0.5f;
  int buttonCount = 0;

  // Hit boxes and attacking
  public GameObject attack1Box, attack2Box, attack3Box;
  SpriteRenderer currentSprite;
  public Sprite attack1SpriteHitFrame, attack2SpriteHitFrame, attack3SpriteHitFrame;
  // When projectile is instantiated in throw animation.
  // public Sprite projectileSpriteThrowFrame
  public bool isBlocking;
  public float knockedDownTime;
  public bool canMove;
  public float knockBackForce;





    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        walkMovementSpeed = 1;
        runMovementSpeed = 3;
        attackMovementSpeed = 0.1f;
        movementSpeed = walkMovementSpeed;
        canMove = true;
        otherStats = GetComponent<Stats>();

    }


    void Update()
    {
      currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
      currentState = currentStateInfo.fullPathHash;
      // if(currentState == idleState){
      //   Debug.Log("Idle State");
      // }
      // if(currentState == walkState){
      //   Debug.Log("Walk State");
      // }
      // if(currentState == runState){
      //   Debug.Log("Run State");
      // }
      // if(currentState == jumpState){
      //   Debug.Log("Jump State");
      // }
      // if(currentState == takeDamageState){
      //   Debug.Log("Take Damage State");
      // }
      // if(currentState == attack1State){
      //   Debug.Log("Attack1 State");
      // }
      // if(currentState == attack2State){
      //   Debug.Log("Attack2 State");
      // }
      // if(currentState == attack3State){
      //   Debug.Log("Attack3k State");
      // }
      // if(currentState == blockState){
      //   Debug.Log("Block State");
      // }
      // if(currentState == FallState){
      //   Debug.Log("Fall State");
      // }
      // Control speed based on commands (can't attack while walking etc)
      if(currentState == idleState || currentState == walkState){
        movementSpeed = walkMovementSpeed;
      }else if(currentState == runState){
        movementSpeed = runMovementSpeed;
      }else{
        movementSpeed = attackMovementSpeed;
      }

    }

    void FixedUpdate(){
      // Get inputs
      float moveHorizontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical");
      Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

      // --Movement--------------------------------
      // Check if double tap (run)
      if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)){
        if(buttonCooler > 0 && buttonCount == 1){
          movementSpeed = runMovementSpeed;
          // Vector3 runMovement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          rigidBody.velocity = movement * movementSpeed;
          rigidBody.position = new Vector3(
            Mathf.Clamp(rigidBody.position.x, xMin, xMax),
            transform.position.y,
            Mathf.Clamp(rigidBody.position.z, zMin, zMax)
          );
          animator.SetFloat("Speed", rigidBody.velocity.sqrMagnitude);
          animator.SetBool("isRunning", true);
        }else{
         buttonCooler = 0.5f ;
         buttonCount += 1 ;
         animator.SetBool("isRunning", false);
         movementSpeed = walkMovementSpeed;
       }
      }

      if ( buttonCooler > 0 ){
        buttonCooler -= 1 * Time.deltaTime ;
      }else{
        buttonCount = 0 ;
        animator.SetBool("isRunning", false);
      }

      // Apply movement (and prevent going off screen)
      // If not running

      rigidBody.velocity = movement * movementSpeed;
      rigidBody.position = new Vector3(
        Mathf.Clamp(rigidBody.position.x, xMin, xMax),
        transform.position.y,
        Mathf.Clamp(rigidBody.position.z, zMin, zMax)
      );

      // Flip sprite if walking left
      if(moveHorizontal < 0 && !facingRight && canMove == true){
        Flip();
      }else if(moveHorizontal > 0 && facingRight && canMove == true){
          Flip();
      }
      // Convert the vector 3 of the sprite to a float and set it to the value of Animation parameter 'Speed'
      // When this paramater goes above 1, our Animator transition triggers and switches to the walk animatioon.
      animator.SetFloat("Speed", rigidBody.velocity.sqrMagnitude);
      // --Movement-end-------------------------------



      // --Combo Attack--------------------------------
      if(Input.GetKey(KeyCode.O)){
        animator.SetBool("Attack", true);
      }else{
        animator.SetBool("Attack", false);
      }
      if(attack1SpriteHitFrame == currentSprite.sprite){
        attack1Box.gameObject.SetActive(true);
      }else{
        attack1Box.gameObject.SetActive(false);
      }
      if(attack2SpriteHitFrame == currentSprite.sprite){
        attack2Box.gameObject.SetActive(true);
      }else{
        attack2Box.gameObject.SetActive(false);
      }
      if(attack3SpriteHitFrame == currentSprite.sprite){
        attack3Box.gameObject.SetActive(true);
      }else{
        attack3Box.gameObject.SetActive(false);
      }
      // Blocking
      if(Input.GetKey(KeyCode.I)){
        animator.SetBool("Block", true);
        isBlocking = true;
      }else{
        animator.SetBool("Block", false);
        isBlocking = false;
      }

      // Is Hit Test
      if(tookDamage == true && knockedDown == false){
        StartCoroutine(TookDamage());
      }
      // if(Input.GetKeyDown(KeyCode.Q)){
      //   animator.SetBool("isHit", true);
      // }else{
      //   animator.SetBool("isHit", false);
      // }

      // Knock Down TestPlatform
      if(otherStats.health <= 0){
        StartCoroutine(KnockedDown());
      }

      // Projectile
      // Have an If statement that checks for right mouse click (or key)
      // animator.SetBool
      // Do a check when the Projectile throw frame == the projectile throw frame in the sprite renderer.
      // Instantiate projectile prefab. (Projectile has a mover script attached).
      // Projectile is destroyed when leaving the screen.




    }
    // Flip the sprite.
    void Flip(){
      facingRight = !facingRight;
      Vector3 thisScale = transform.localScale;
      thisScale.x *= -1;
      transform.localScale = thisScale;
    }

    IEnumerator KnockedDown(){
      animator.Play("Fall");
      animator.SetBool("KnockedDown", true);
      canMove = false;
      if(facingRight == false){
        rigidBody.AddForce(transform.right * (-1 * knockBackForce));
      }else if(facingRight == true){
        rigidBody.AddForce(transform.right * knockBackForce);
      }
      // yield return new WaitForSeconds(knockedDownTime);
      // animator.SetBool("KnockedDown", false);
      // canMove = true;
      // knockedDown = false;
      // animator.Play("Idle");

    }

    IEnumerator TookDamage(){
      animator.Play("Hurt");
      animator.SetBool("isHit", true);
      canMove = false;
      yield return new WaitForSeconds(stunTime);
      canMove = true;
      tookDamage = false;

    }
}
