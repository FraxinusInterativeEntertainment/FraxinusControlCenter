using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        AppFacade.instance.startup();
    }

    void Update()
    {
        
    }
}
