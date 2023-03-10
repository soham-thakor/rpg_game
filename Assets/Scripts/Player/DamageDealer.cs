using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

	void OnEnable(){
		Debug.Log("DAMAGE DEALER ON");
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("dealt damage");
		if(collision.CompareTag("Enemy"))
		{
			
			collision.GetComponent<Enemy>().TakeDamage(gameObject.GetComponentInParent<Mine>().damageDealt);
		}
	}
}
