using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemies : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    
    private List<Transform> levelList;
    private GameSaved gameSaved;

    private int _iterator = 0;

    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.CREATE_LVLS, SetLevelList);
        gameSaved = GameObject.Find("GameManager").GetComponent<GameManager>().gameSaved;
        if (gameSaved.Equals(null))
            print("Don't find GameManager");       
    }
    
    private void SetLevelList(EVENT_TYPE eventType,
        Component sender,
        object Param = null)
    {
        levelList = (List<Transform>) Param;
        StartCoroutine(InitilizadeCoroutine());
    }

    IEnumerator InitilizadeCoroutine()
    {
        while (true)
        {
            InitialEnemy();
            yield return new WaitForSeconds(1f);
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
            Enemies eny = new Enemies
            {
                transform = enemy.transform,
                speed = Random.Range(1, 5),
                priceToDie = 100,
            };
            EventManager.Instance.PostNotification(EVENT_TYPE.CREATE_ENEMY, this, eny);
            _iterator++;
        }
    }
}

public class Enemies
{
    public Transform transform;
    public float speed;
    public int priceToDie;
    
    
}
