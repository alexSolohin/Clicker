using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Animator animator;

    private bool isDie;
    void Start()
    {
        isDie = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((transform.position.x <= -CameraControl.CamWidth() / 2 + GameManager.OffsetCamWidth + 2.5f) && !isDie)
        {
            isDie = true;
            EventManager.Instance.PostNotification(EVENT_TYPE.DIE_ENEMY, this, transform.position);
            animator.SetBool("Die", true);
            Destroy(gameObject, 1.5f);
        }
    }
}
