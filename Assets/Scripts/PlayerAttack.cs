using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public AnimationClip attackAnimation;
    public Transform weaponTransform; // Reference to the weapon's transform
    public Vector3 originalWeaponPosition;
    private bool isAttacking = false;
    private int attacksRemaining = 2;

    private void Start()
    {
        
        originalWeaponPosition = weaponTransform.localPosition;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && attacksRemaining > 0)
        { // Attack if not currently attacking
            if (!isAttacking)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {


        animator.SetTrigger("AttackTrigger");
        isAttacking = true;
        attacksRemaining--;
        StartCoroutine(ResetAttackState(attackAnimation.length));
    }

        private IEnumerator ResetAttackState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        weaponTransform.localPosition = originalWeaponPosition;
    }
}

