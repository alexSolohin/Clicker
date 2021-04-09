using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevels : MonoBehaviour
{
    public GameObject[] prefabsLevels;
    public GameObject visibleLvlPrefabs;


    private Vector3 _spawnPoint;
    private int _iterationCaves;

    private float camWidth;
    void Start()
    {
        camWidth = GameManager.Instance.camWidth;
        _spawnPoint = Vector3.zero;
        CreatePrefabs();
        
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
            GameManager.Instance.levelObjectsList.Add(cave);
            _iterationCaves++;
            _spawnPoint += new Vector3(0, 7.4f, 3);
        }    
    }

    private void SetPortalPosition(GameObject cave)
    {
        if (cave.transform.GetChild(1))
        {
            Vector3 pos = cave.transform.GetChild(1).transform.position;
            cave.transform.GetChild(1).transform.position = new Vector3(camWidth - 12f, pos.y, pos.z);
        }   
    }
    
    private void CreateVisiblePrefab(GameObject cave)
    {
        GameObject visible = Instantiate(visibleLvlPrefabs,
            _spawnPoint - new Vector3(0, -3, 4),
            Quaternion.identity);
        visible.transform.SetParent(cave.transform);
        
    }
}
