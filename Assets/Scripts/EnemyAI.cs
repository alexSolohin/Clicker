using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -CameraControl.CamWidth() / 2 + GameManager.OffsetCamWidth)
        {
            Destroy(gameObject);
        }
    }
}
