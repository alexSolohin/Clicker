using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSaved", menuName = "Scriptble/GameSaved", order = 1)]
public class GameSaved : ScriptableObject
{
	[Header("Скольео заработано денег")]
	public int allMoney;

	[Header("Сколько уровней открыто")] 
	public int countLevelsOpen;

	[Header("на каких уровнях какие герои по тегу")]
	public string[] OnLevelHeroTag;

	[Header("Массив со временем спавна enemy")]
	public float[] timeToSpawn;

	public void CreateTimeToSpawn()
	{
		if (countLevelsOpen > 0)
		{
			timeToSpawn = new float[countLevelsOpen];
			for (int i = 0; i < timeToSpawn.Length; i++)
			{
				timeToSpawn[i] = i;
			}
		}
	}
}
