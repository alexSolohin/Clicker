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
	}

	private void Start()
	{
		
	}
	
}
