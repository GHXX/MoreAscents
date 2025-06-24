using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents;

public class OccultStatueGimmick : AscentGimmick
{
    public override string GetDescription() {
        return "No more Revive Statues.";
    }

    public override void RespawnChestExisted(Spawner chest) {
        chest.gameObject.SetActive(false);
    }
}