using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSaved", menuName = "Scriptble/GameSaved", order = 1)]
public class GameSaved : ScriptableObject
{
	[Header("Колличество уровней")]
	public int levelCount;
	
	[Header("Скольео заработано денег")]
	public int allMoney;

	[Header("Сколько уровней открыто")] 
	public int countLevelsOpen;

	[Header("на каких уровнях какие герои по тегу")]
	public string[] OnLevelHeroTag;

	[Header("Массив со скорость движения enemy")]
	public List<float> speedsEnemies;
	
	[Header("Массив со временем спавна enemy")]
	public List<float> timeToSpawn;

	[Header("Массив с ценной за убийство enemy")]
	public List<int> priceToDie;
	
	public void CreateSpeedEnemies()
	{
		speedsEnemies = new List<float>();
		float speed = 1f;
		for (int i = 0; i < countLevelsOpen; i++)
		{
			speedsEnemies.Add(speed);
			speed += 1f;
		}
	}
	public void CreateTimeToSpawn()
	{
		timeToSpawn = new List<float>();
		float time = 0.5f;
		for (int i = 0; i < countLevelsOpen; i++)
		{
			timeToSpawn.Add(time);
			time += 0.7f;
		}
	}

	public void CreatePriceToDie()
	{
		priceToDie = new List<int>();
		int price = 100;
		for (int i = 0; i < countLevelsOpen; i++)
		{
			priceToDie.Add(price);
			price += 100;
		}
	}
}
