using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBuild : Build
{
    public int Energy;
    protected override void InitObject()
    {
        base.InitObject();
        Game.ResoursesManager.obj.Energy += Energy;
    }

    private void OnDestroy()
    {
        if(Init)
            Game.ResoursesManager.obj.Energy -= Energy;
    }
}
