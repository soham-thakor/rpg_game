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
	public Animator animator;

	private void Start()
	{
		colliderRadius = GetComponent<CircleCollider2D>().radius;
		playerTarget = GameObject.FindGameObjectWithTag("Player");
	}


	private void Update()
	{
		if (Vector2.Distance(gameObject.transform.position, playerTarget.transform.position) <= colliderRadius)
		{
			if (!inRange)
			{
				nextAttackTime = Time.time + initialAttackInterval;
			}
			inRange = true;
			if (TryDamage())
			{
				if (!this.transform.parent.name.Contains("GOBLIN"))
				{
					animator.SetTrigger("attack");
				}
				playerTarget.GetComponent<PlayerController>().TakeDamage(attackDamage);
				nextAttackTime = Time.time + subsequentAttackInterval;
				//tracking initial attack, The first attack comes out faster to prevent people from walking right past the enemies
				if (initial) { initial = false; }
			}
		}
		else if (inRange)
		{
			inRange = false;
			initial = true;
		}
		pastInRange = inRange;



	}

	private bool TryDamage()
	{
		string debug = pastInRange.ToString() + "|" + Time.time.ToString() + "|" + nextAttackTime.ToString();
		Debug.Log(debug);
		return pastInRange && Time.time >= nextAttackTime;
	}
}
