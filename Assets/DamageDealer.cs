using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Enemy"))
		{
			collision.GetComponent<Enemy>().TakeDamage(gameObject.GetComponentInParent<Mine>().damageDealt);
		}
	}
}
