using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class LogicWithEnemies : MonoBehaviour
{
    private List<Enemy> enemies;
    
    private GameSaved gameSaved;
    private float _targetX;

    private Enemy[] enemyAtLevel;
    
    private void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.CREATE_ENEMY, AddEnemyToList);
        enemies = new List<Enemy>();
        _targetX = -CameraControl.CamWidth() / 2 + GameManager.OffsetCamWidth;
        gameSaved = GameObject.Find("GameManager").GetComponent<GameManager>().gameSaved;
        enemyAtLevel = new Enemy[gameSaved.countLevelsOpen];
    }

    private void Update()
    {
        JobWorkToTransform();
    }
    
    
    /// <summary>
    /// create a job for transform position all enimies with speed
    /// </summary>
    private void JobWorkToTransform()
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
    
    private void AddEnemyToList(EVENT_TYPE eventType,
        Component sender,
        object param = null)
    {
        enemies.Add((Enemy)param);
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].transform.Equals(null))
            {
                enemies.RemoveAt(i);
            }
        }
    }

    [BurstCompile]
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


