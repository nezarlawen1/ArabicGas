using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGroundEnemyDummyAI : EnemyAI
{
    private MeleeAttack _meleeAttackRef;
    private bool _attacking;

    private void Awake()
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
        //if (AnimatorRef != null) AnimatorRef.SetFloat("Speed", Agent.velocity.magnitude);

        //if (_attacking)
        //{
        //    if ((int)(_meleeAttackRef.ComboState) == 1)
        //    {
        //        AnimatorRef.SetBool("PunchMirror", false);
        //    }
        //    else if ((int)(_meleeAttackRef.ComboState) == 2)
        //    {
        //        AnimatorRef.SetBool("PunchMirror", true);
        //    }
        //    AnimatorRef.SetTrigger("Punch");
        //    _attacking = false;
        //}
    }
}
