using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        AppFacade.instance.startup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
