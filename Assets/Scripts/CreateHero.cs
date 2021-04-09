using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateHero : MonoBehaviour
{
    public GameObject[] prefabsTI1;

    public GameObject[] prefabsTI2;

    public GameObject[] prefabsTI3;

    public GameObject[] prefabsTI4;

    public GameObject[] prefabsTI5;

    public GameObject[] prefabsTI6;
    
    private List<Transform> levelList;
    private GameSaved gameSaved;
    
    private void Start()
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
        CreateHeroAtTag();
    }

    private void CreateHeroAtTag()
    {
        for (int i = 0; i < gameSaved.countLevelsOpen; i++)
        {
            string tag = gameSaved.OnLevelHeroTag[0];
            GameObject hero = CompareTagHero(tag);
            if (!hero.Equals(null))
            {
                hero.transform.SetParent(levelList[i]);
                hero.transform.localPosition = new Vector3(-CameraControl.CamWidth() / 2 + GameManager.OffsetCamWidth, 0.5f, -2);
            }
        }
    }

    
    //сравниваем теги и героев, чтобы их инициализировать
    private GameObject CompareTagHero(string tag)
    {
        if (tag.Equals(prefabsTI1[0].tag))
        {
            return Instantiate(prefabsTI1[Random.Range(0, prefabsTI1.Length)]);
        }
        if (tag.Equals(prefabsTI2[0].tag))
        {
            return Instantiate(prefabsTI2[Random.Range(0, prefabsTI2.Length)]);
        }
        if (tag.Equals(prefabsTI3[0].tag))
        {
            return Instantiate(prefabsTI3[Random.Range(0, prefabsTI3.Length)]);
        }
        if (tag.Equals(prefabsTI4[0].tag))
        {
            return  Instantiate(prefabsTI4[Random.Range(0, prefabsTI4.Length)]);
        }
        if (tag.Equals(prefabsTI5[0].tag))
        {
            return Instantiate(prefabsTI5[Random.Range(0, prefabsTI5.Length)]);
        }
        if (tag.Equals(prefabsTI6[0].tag))
        {
            return Instantiate(prefabsTI6[Random.Range(0, prefabsTI6.Length)]);
        }

        return null;
    }
}
