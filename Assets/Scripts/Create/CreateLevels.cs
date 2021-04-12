using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevels : MonoBehaviour
{
    public GameObject[] prefabsLevels;
    public GameObject visibleLvlPrefabs;

    [Header("Сколько раз вывести все пещеры")]
    public int iterator;

    public List<Transform> levelsList;
    
    private Vector3 _spawnPoint;
    private int _iterationCaves;
    private GameSaved gameSaved;

    void Start()
    {
        gameSaved = GameObject.Find("GameManager").GetComponent<GameManager>().gameSaved;
        if (gameSaved.Equals(null))
            print("Don't find GameManager");
        
        levelsList = new List<Transform>();
        _spawnPoint = Vector3.zero;
        for (int i = 0; i < iterator; i++)
        {
            CreatePrefabs();
        }
        OpenLevels();
        SetStartGameDataTag();
        EventManager.Instance.PostNotification(EVENT_TYPE.CREATE_LVLS, this, levelsList);
    }

    public int GetSizeCave()
    {
        return _iterationCaves;
    }
    
    private void CreatePrefabs()
    {
        for (int i = 0; i < prefabsLevels.Length; i++)
        {
            GameObject cave = Instantiate(prefabsLevels[i],
                _spawnPoint,
                Quaternion.identity) as GameObject;
            CreateVisiblePrefab(cave);
            StaticBatchingUtility.Combine(cave);
            SetPortalPosition(cave);
            cave.name = _iterationCaves.ToString();
            levelsList.Add(cave.transform);
            _iterationCaves++;
            _spawnPoint += new Vector3(0, 7.4f, 3);
        }    
    }

    private void SetStartGameDataTag()
    {
        if (gameSaved.OnLevelHeroTag.Length.Equals(0))
        {
            gameSaved.OnLevelHeroTag = new string[_iterationCaves];
            for (int i = 0; i < _iterationCaves; i++)
            {
                gameSaved.OnLevelHeroTag[i] = "Knight";
            }
        }
    }
    
    private void SetPortalPosition(GameObject cave)
    {
        if (cave.transform.GetChild(1))
        {
            Vector3 pos = cave.transform.GetChild(1).transform.position;
            cave.transform.GetChild(1).transform.position = new Vector3(CameraControl.CamWidth() / 2 - GameManager.OffsetCamWidth, pos.y, pos.z);
        }   
    }
    
    private void CreateVisiblePrefab(GameObject cave)
    {
        GameObject visible = Instantiate(visibleLvlPrefabs,
            _spawnPoint - new Vector3(0, -3, 4),
            Quaternion.identity);
        visible.name = "Visible";
        visible.transform.SetParent(cave.transform);
        
    }
    
    private void OpenLevels()
    {
        for (int i = 0; i < gameSaved.countLevelsOpen; i++)
        {
            Transform level = levelsList[i];
            if (level.GetChild(2))
            {
                level.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
