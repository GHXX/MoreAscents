using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents;

public class AscentGimmick
{
    public virtual string GetTitle()
    {
        return "";
    }

    public virtual string GetDescription()
    {
        return "";
    }
    
    public virtual void OnCharacterFall(Character character) { }
    public virtual void RespawnChestExisted(Spawner chest) {}
    public virtual void SpawnerSpawnItems(Spawner spawner,ref List<Transform> spawnSpots) { }

    public virtual void OnGrabbedCharacter(Character thisCharacter) { }

    public virtual float AfflictionMultiplier(CharacterAfflictions afflictions, CharacterAfflictions.STATUSTYPE statusType,float amount) {
        return 0f;
    }

    public AscentStruct _ascentData;
    public bool active;
}