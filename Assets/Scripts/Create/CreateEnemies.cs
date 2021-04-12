using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateEnemies : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    
    private List<Transform> levelList;
    private GameSaved gameSaved;

    private int _iterator = 0;
    private List<Enemy> enemiesAtLevel;
    private List<float> time;

    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.CREATE_LVLS, SetLevelList);
        gameSaved = GameObject.Find("GameManager").GetComponent<GameManager>().gameSaved;
        if (gameSaved.Equals(null))
            print("Don't find GameManager");
        enemiesAtLevel = new List<Enemy>();
        time = new List<float>();
    }

    private void Update()
    {
        InitEnemyAtTime();
    }

    private void SetLevelList(EVENT_TYPE eventType,
        Component sender,
        object Param = null)
    {
        levelList = (List<Transform>) Param;
        InitialEnemy();
    }
    
    /// <summary>
    /// create enimy at time timeToSpawn
    /// </summary>
    private void InitEnemyAtTime()
    {
        for (int i = 0; i < enemiesAtLevel.Count; i++)
        {
            if (Time.time - time[i] >= enemiesAtLevel[i].timeToSpawn && !enemiesAtLevel[i].enemyObject.Equals(null))
            {
                GameObject enemy = Instantiate(enemiesAtLevel[i].enemyObject);
                enemy.transform.SetParent(enemiesAtLevel[i].transform.parent);
                enemy.transform.localPosition = new Vector3(CameraControl.CamWidth() / 2 - GameManager.OffsetCamWidth, 0.5f, -2);
                Enemy eny = new Enemy
                {
                    enemyObject = enemy,
                    transform = enemy.transform,
                    speed = gameSaved.speedsEnemies[i],
                    priceToDie = gameSaved.priceToDie[i],
                    timeToSpawn = enemiesAtLevel[i].timeToSpawn,
                    iterator = enemiesAtLevel[i].iterator
                };
                EventManager.Instance.PostNotification(EVENT_TYPE.CREATE_ENEMY, this, eny);
                time[i] = Time.time;
            }
        }
    }
    
    private void InitialEnemy()
    {
        for (int i = 0; i < gameSaved.countLevelsOpen; i++)
        {
            if (enemiesPrefabs.Length.Equals(_iterator))
                _iterator = 0;
            GameObject enemy = Instantiate(enemiesPrefabs[_iterator]);
            enemy.transform.SetParent(levelList[i]);
            enemy.transform.localPosition = new Vector3(CameraControl.CamWidth() / 2 - GameManager.OffsetCamWidth, 0.5f, -2);
            SetMainEnemiesData(enemiesPrefabs[_iterator], i);
            Enemy newEnemy = new Enemy
            {
                enemyObject = enemy,
                transform = enemy.transform,
                speed = gameSaved.speedsEnemies[i],
                priceToDie = gameSaved.priceToDie[i],
                timeToSpawn = gameSaved.timeToSpawn[i],
                iterator = i
            };
            EventManager.Instance.PostNotification(EVENT_TYPE.CREATE_ENEMY, this, newEnemy);
            _iterator++;
        }
    }

    private void SetMainEnemiesData(GameObject prefabs, int i)
    {
        GameObject enemy = Instantiate(prefabs);
        enemy.transform.SetParent(levelList[i]);
        enemy.transform.localPosition = new Vector3(CameraControl.CamWidth() / 2 - GameManager.OffsetCamWidth + 5f, 0.5f, -2);
        Enemy eny = new Enemy
        {
            enemyObject = enemy,
            transform = enemy.transform,
            speed = gameSaved.speedsEnemies[i],
            priceToDie = gameSaved.priceToDie[i],
            timeToSpawn = gameSaved.timeToSpawn[i],
            iterator = i,
        };
        enemiesAtLevel.Add(eny);
        time.Add(Time.time);
    }
}

public class Enemy
{
    public GameObject enemyObject;
    public Transform transform;
    public float speed;
    public int priceToDie;
    public float timeToSpawn;
    public int iterator;
}
