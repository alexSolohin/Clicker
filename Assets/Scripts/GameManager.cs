using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameSaved gameSaved;
	public static float OffsetCamWidth = 1;
	
	private void Awake()
	{
		if (gameSaved.countLevelsOpen <= 0)
		{
			gameSaved.countLevelsOpen = 1;
		}
		if (gameSaved.speedsEnemies.Count == 0)
			gameSaved.CreateSpeedEnemies();
		if (gameSaved.timeToSpawn.Count == 0)
			gameSaved.CreateTimeToSpawn();
		if (gameSaved.priceToDie.Count == 0)
			gameSaved.CreatePriceToDie();	
	}
	
	private void Start()
	{
		EventManager.Instance.AddListener(EVENT_TYPE.CREATE_LVLS, CreateLvl);
	}

	private void CreateLvl(EVENT_TYPE eventType,
		Component sender,
		object Param = null)
	{
		List <Transform> list = (List<Transform>) Param;
		gameSaved.levelCount = list.Count;
	}
}
