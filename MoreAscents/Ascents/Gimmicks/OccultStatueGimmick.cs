using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents;

public class OccultStatueGimmick : AscentGimmick
{
    public override string GetTitle() {
        return "Ascent 12";
    }
    
    public override string GetDescription() {
        return "No more Revive Statues.";
    }

    public override void RespawnChestExisted(Spawner chest) {
        if (MoreAscents.Plugin.ascent7Disabler.Value)
            return;
        chest.gameObject.SetActive(false);
    }
}