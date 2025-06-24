﻿using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MoreAscents;

public class SkeletonGimmick : AscentGimmick
{
    public override string GetDescription() {
        return "Passing out instantly turns you into a skeleton.";
    }

    public override string GetTitle() {
        return "Chaos 1";
    }

    public override void CharacterPassedOut(Character character) {
        if (!character.IsLocal) {
            return;
        }
        MethodInfo info = character.GetType().GetMethod("DieInstantly",BindingFlags.Instance | BindingFlags.NonPublic);
        info.Invoke(character,[]);
    }
}