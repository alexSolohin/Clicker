using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LogicWithHeroes : MonoBehaviour
{
	[SerializeField]
	private GameObject bloodParticle;
	private List<GameObject> heroes;
	
	private void Start()
	{
		EventManager.Instance.AddListener(EVENT_TYPE.CREATE_HEROES, SetListHeroes);
		EventManager.Instance.AddListener(EVENT_TYPE.DIE_ENEMY, Attack);
	}
	
	private void Attack(EVENT_TYPE eventType,
		Component sender,
		object Param = null)
	{
		Vector3 target = (Vector3) Param;
		for (int i = 0; i < heroes.Count; i++)
		{
			Animator animator = heroes[i].GetComponent<Animator>();
			if (Vector3.Distance(heroes[i].transform.position, target) <= 2.5f)
			{
				int rand = Random.Range(0, 7);
				animator.SetInteger("RandomAttack", rand);
				animator.SetBool("Attack", true);
				GameObject blood = Instantiate(bloodParticle, target  + new Vector3(0, 1f, 0), Quaternion.identity);
				Destroy(blood, 1f);
			}
			else
			{
				animator.SetBool("Attack", false);
			}
		}
		
	}

	private void SetListHeroes(EVENT_TYPE eventType,
		Component sender,
		object Param = null)
	{
		heroes = (List<GameObject>) Param;
	}

	private void Update()
	{
		// for (int i = 0; i < heroes.Count; i++)
		// {
		// 	if (heroes[i].)
		// }
	}
}

