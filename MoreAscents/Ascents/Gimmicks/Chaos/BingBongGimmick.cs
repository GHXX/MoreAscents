using System.Collections.Generic;
using MoreAscents.Patches;
using UnityEngine;
using Zorro.Core;

namespace MoreAscents;

public class BingBongGimmick : AscentGimmick {
    public override string GetDescription() {
        return "Bing Bong calls upon an evil force if not given attention.";
    }
    
    public override string GetTitle() {
        return "Chaos 4";
    }

    public override void Update() {
        if (BingBongPatches.Update.BingBongUnheldFor >= 15) {
            foreach (Character character in Character.AllCharacters) {
                if (!character.IsLocal) {
                    continue;
                }
                if (character.data.passedOutOnTheBeach > 0) {
                    BingBongPatches.Update.BingBongUnheldFor = 0;
                    break;
                }
                character.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Crab,
                    0.05f * Time.deltaTime, false);
            }
        }
    }
}