using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour {

    private GameMasterScript gm;
    
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterScript>();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Coin"))
        {
            print(collider.gameObject);
            Destroy(collider.gameObject);
            gm.points += 1;
        }
    }
}
