using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /* public Animator animator;
    public AnimationClip attackAnimation;
    */
    public Transform weaponTransform; // Reference to the weapon's transform
    // public Vector3 originalWeaponPosition;
    public Vector3 weaponPosition;
    private bool isAttacking = false;
    public float weaponSpeed = 5;
    public GameObject playerCamera;
    public GameObject weapon;
    public Vector3 playerPosition; // this is really just the player's hand
    Vector3 lerpStart, lerpFinish;

    // private int attacksRemaining = 2;

    private void Start()
    {
        isAttacking = false;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        weapon = GameObject.FindGameObjectWithTag("Pipe");
        // originalWeaponPosition = weapon.transform.localPosition;

        lerpStart = weapon.transform.position;
        lerpFinish = weapon.transform.position;
    }
    private void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("PlayerHand").transform.position;
        
        if (Input.GetKeyDown(KeyCode.X)) {
            lerpStart = playerPosition;
            lerpFinish = playerPosition;
        }

        if (Input.GetMouseButtonDown(0)) //&& attacksRemaining > 0)
        { // Attack if not currently attacking
            if (!isAttacking)
            {
                Debug.Log("Clicked Mouse");
                weaponPosition = weapon.transform.position;
                Attack();
            }
        }

        if (!isAttacking) {
            lerpFinish = playerPosition;
        }
        transform.Translate(lerpStart, lerpFinish, weaponSpeed * Time.deltaTime);
        // Vector3.Lerp(lerpStart, lerpFinish, weaponSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        // animator.SetTrigger("AttackTrigger");
        Debug.Log("Attacking!");
        isAttacking = true;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit, float.PositiveInfinity))
        {
            Debug.Log(raycastHit.transform);
            lerpStart = transform.position;
            lerpFinish = raycastHit.transform.position;
            // Vector3.Lerp(weaponPosition, raycastHit.transform.position, weaponSpeed * Time.deltaTime);
        }
        // attacksRemaining--;
        // StartCoroutine(ResetAttackState(attackAnimation.length));
    }

    private void OnTriggerEnter() {
        lerpStart = transform.position;
        lerpFinish = playerPosition;
        // weaponPosition = weapon.transform.position;
        // transform.position = Vector3.Lerp(weaponPosition, playerPosition, weaponSpeed * Time.deltaTime);
        isAttacking = false;
    }

    private IEnumerator ResetAttackState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        // weaponTransform.localPosition = originalWeaponPosition;
    }
}

