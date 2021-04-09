using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class LogicWithEnemies : MonoBehaviour
{
    public List<Enemies> enemies;
    public bool useJob;

    private float _targetX;
    
    private void Start()
    {
        enemies = new List<Enemies>();
        _targetX = -CameraControl.CamWidth() / 2 + GameManager.OffsetCamWidth;
        EventManager.Instance.AddListener(EVENT_TYPE.CREATE_ENEMY, AddEnemyToList);
    }

    private void Update()
    {
        if (useJob)
        {
            NativeArray<float> speedArr = new NativeArray<float>(enemies.Count, Allocator.TempJob);
            TransformAccessArray transformAccessArray = new TransformAccessArray(enemies.Count);
        
            for (int i = 0; i < enemies.Count; i++)
            {
                speedArr[i] = enemies[i].speed;
                transformAccessArray.Add(enemies[i].transform);
            }
            
            UpdateTransform job = new UpdateTransform
            {
                deltaTime = Time.deltaTime,
                speedArray = speedArr,
                targetX = _targetX,
            };
            
            JobHandle jobHandle = job.Schedule(transformAccessArray);
            jobHandle.Complete();

            speedArr.Dispose();
            transformAccessArray.Dispose();
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].transform.position -= new Vector3(enemies[i].speed * Time.deltaTime, 0, 0);
            }
        }
        
    }

    private void AddEnemyToList(EVENT_TYPE eventType,
        Component sender,
        object param = null)
    {
        enemies.Add((Enemies)param);
    }

    public struct UpdateTransform : IJobParallelForTransform
    {
        [ReadOnly] public float deltaTime;

        [ReadOnly] public float targetX;

        public NativeArray<float> speedArray;

        public void Execute(int index, TransformAccess transform)
        {
            if (transform.position.x >= targetX)
                transform.position -= new Vector3(speedArray[index] * deltaTime, 0, 0);
        }
    }
}


