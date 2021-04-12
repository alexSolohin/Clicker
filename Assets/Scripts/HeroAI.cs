using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : MonoBehaviour
{

    private Animator animator;
    void Start()
    {
        // EventManager.Instance.AddListener(EVENT_TYPE.DIE_ENEMY, Attack);
        EventManager.Instance.AddListener(EVENT_TYPE.STOP_RUN_ANIM, StopRunAnim);
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        
    }

    private void Attack(EVENT_TYPE eventType,
        Component sender,
        object Param = null)
    {
        Vector3 target = (Vector3) Param;
        if (Vector3.Distance(transform.position, target) <= 2.5f)
        {
            int rand = Random.Range(0, 7);
            animator.SetInteger("RandomAttack", rand);
            animator.SetBool("Attack", true);  
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
    
    private void StopRunAnim(EVENT_TYPE eventType,
        Component sender,
        object Param = null)
    {
        animator.SetBool("Walk", false);
    }
}
