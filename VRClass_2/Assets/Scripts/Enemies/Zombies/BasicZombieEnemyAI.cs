using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieEnemyAI : EnemyAI
{
    private MeleeAttack _meleeAttackRef;
    private bool _attacking;

    private void Start()
    {
        _meleeAttackRef = GetComponent<MeleeAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        RunEnemy();
    }

    public override void AttackPatternController()
    {
        _meleeAttackRef.DoAttack();
        _attacking = true;
    }

    public override void AnimationHandler()
    {
        if (AnimatorRef != null) AnimatorRef.SetFloat("Speed", Agent.velocity.normalized.magnitude);

        if (_attacking)
        {
            if ((int)(_meleeAttackRef.ComboState) == 1)
            {
                AnimatorRef.SetBool("Mirrored", false);
            }
            else if ((int)(_meleeAttackRef.ComboState) == 2)
            {
                AnimatorRef.SetBool("Mirrored", true);
            }
            AnimatorRef.SetTrigger("Attacking");
            _attacking = false;
        }
    }
}
