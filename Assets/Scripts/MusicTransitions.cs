using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransitions : MonoBehaviour {

    private static MusicTransitions instance;
	void Awake()
    {
        if(instance)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else
        {
            Destroy(gameObject);
        }
    }
}
