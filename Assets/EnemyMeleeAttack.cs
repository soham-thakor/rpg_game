using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackDamage;
    public float attackInterval;

    private float nextAttackTime = 0f;
	private float colliderRadius;
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
		if(Vector2.Distance(gameObject.transform.position, playerTarget.transform.position) <= colliderRadius)
		{
			if(!inRange)
			{
				nextAttackTime = Time.time + attackInterval;
			}
			inRange = true;
		}
		else
		{
			if(inRange)
			{
				inRange = false;
			}
			
		}
		pastInRange = inRange;

		if(TryDamage())
		{
			animator.SetTrigger("attack");
			playerTarget.GetComponent<PlayerController>().TakeDamage(attackDamage);
			nextAttackTime = Time.time + attackInterval;
		}

	}

	private bool TryDamage()
	{
		return pastInRange && Time.time >= nextAttackTime;
	}
}
