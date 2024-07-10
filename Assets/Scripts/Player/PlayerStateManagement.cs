using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils;
using System;
public class PlayerStateManagement : StateManager<PlayerStateManagement.playerState>
{

    public enum playerState {
        IDLE,
        DIED,
        ALIVE,
        DAMAGE
    }

    void Awake()
    {
        currentState = states[playerState.ALIVE];
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
