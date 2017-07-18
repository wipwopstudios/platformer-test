using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour {

	public GameObject coin;
	public Transform[] coinSpawns;

	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	// Spawn a coin at a random location
	void Spawn () {
		for (int i = 0; i < coinSpawns.Length; i++)
		{
			int coinFlip = Random.Range(0, 2);
			if (coinFlip > 0)
			{
				Instantiate(coin, coinSpawns[i].position, Quaternion.identity);
			}
		}
	}
}
