using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackDamage;
    public float initialAttackInterval;
	public float subsequentAttackInterval;

    private float nextAttackTime = 0f;
	private float colliderRadius;
	private bool initial = true;
	private bool inRange = false;
	private bool pastInRange = false;
	private GameObject playerTarget;

	private void Start()
	{
		colliderRadius = GetComponent<CircleCollider2D>().radius;
		playerTarget = GameObject.FindGameObjectWithTag("Player");
	}


	private void Update()
	{
		if(Vector2.Distance(gameObject.transform.position, playerTarget.transform.position) <= colliderRadius)
		{
			if(!inRange)
			{
				nextAttackTime = Time.time + initialAttackInterval;
			}
			inRange = true;
		}
		else if(inRange)
		{
			inRange = false;
			initial = true;
		}
		pastInRange = inRange;

		if(TryDamage())
		{
			playerTarget.GetComponent<PlayerController>().TakeDamage(attackDamage);
			nextAttackTime = Time.time + subsequentAttackInterval;
			Debug.Log(nextAttackTime);
			//tracking initial attack, The first attack comes out faster to prevent people from walking right past the enemies
			if (initial) { initial = false; }
		}

	}

	private bool TryDamage()
	{
		return pastInRange && Time.time >= nextAttackTime;
	}
}
