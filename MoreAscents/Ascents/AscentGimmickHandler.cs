using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace MoreAscents;

public class AscentGimmickHandler
{
    public static List<AscentGimmick> gimmicks = new();

    public static void MarkGimmickAsActive(AscentData.AscentInstanceData instanceData) {
        int activeOrder = 0;
        foreach (AscentGimmick gimmick in gimmicks) {
            if (gimmick._ascentData.data == instanceData) {
                activeOrder = gimmick._ascentData.order;
                break;
            }
        }
        foreach (AscentGimmick gimmick in gimmicks) {
            if (gimmick._ascentData.order <= activeOrder) {
                gimmick.active = true;
                Plugin.Logger.LogWarning($"gimmick {gimmick.GetType().Name} is now active.");
            }
        }
    }
    
    public static void DisableGimmicks() {
        foreach (AscentGimmick gimmick in gimmicks) {
            gimmick.active = false;
        }
    }

    public static AscentGimmick GetGimmick<T>() where T : AscentGimmick
    {
        foreach (AscentGimmick gimmick in gimmicks)
        {
            if (gimmick.GetType() == typeof(T))
                return gimmick;
        }
        return null;
    }
    
    public static bool IsGimmickActive<T>() where T : AscentGimmick
    {
        foreach (AscentGimmick gimmick in gimmicks)
        {
            if (gimmick.GetType() == typeof(T))
                return gimmick.active;
        }
        return false;
    }
    
    public static void RegisterAscent<T>(List<AscentData.AscentInstanceData> ascents) where T : AscentGimmick
    {
        AscentGimmick gimmick = Activator.CreateInstance<T>();
        
        AscentData.AscentInstanceData newData = new();
        newData.title = gimmick.GetTitle();
        if (newData.title == "") {
            newData.title = $"Ascent {(ascents.Count - 2) + 1}";
        }
        
        newData.description = gimmick.GetDescription();
        newData.titleReward = "A pat on the back";
        ascents.Add(newData);

        gimmick._ascentData = new AscentStruct() {
            data = newData,
            order = ascents.Count,
        };
        gimmicks.Add(gimmick);
    }
}