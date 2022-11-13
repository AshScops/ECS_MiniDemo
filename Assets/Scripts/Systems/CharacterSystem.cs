using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class CharacterSystem : SystemBase
{
    protected override void OnCreate()
    {
        Entities.WithAll<CharacterComponent>().ForEach((ref CharacterComponent cc) =>
        {
            cc.health = 30;
            cc.score = 0;
            cc.characterState = CharacterState.alive;
        }).Run();
    }

    protected override void OnUpdate()
    {
        int h = -1;
        int s = -1;
        CharacterState cs = CharacterState.alive;

        Entities.WithAll<CharacterComponent>().ForEach((ref CharacterComponent cc) =>
        {
            if(cc.characterState == CharacterState.alive)
            {
                if(cc.health == 0)
                {
                    cc.characterState = CharacterState.dead;
                }
            }

            h = cc.health;
            s = cc.score;
            cs = cc.characterState;
        }).Run();

        GameManager.instance.UpdatePlayerState(h, s, cs);
    }

}
