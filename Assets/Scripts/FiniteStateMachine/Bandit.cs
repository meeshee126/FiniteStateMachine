using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Entity
{
    Machine brain = new Machine();

    void Start()
    {
        //Add states to Bandit
        brain.AddState(new IChase(this));
        brain.AddState(new IWarning(this));
        brain.AddState(new IWander(this));
    }

    void Update()
    {   
        //Call states
        brain.Update();
    }
}
