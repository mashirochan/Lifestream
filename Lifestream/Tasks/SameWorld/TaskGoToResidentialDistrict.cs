﻿using ClickLib.Clicks;
using ECommons.Automation;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Lifestream.Schedulers;

namespace Lifestream.Tasks.SameWorld;
public unsafe static class TaskGoToResidentialDistrict
{
    public static void Enqueue(int ward)
    {
        if (ward < 1 || ward > 30) throw new ArgumentOutOfRangeException(nameof(ward));
        if (P.Config.WaitForScreen) P.TaskManager.Enqueue(Utils.WaitForScreen);
        P.TaskManager.Enqueue(WorldChange.TargetValidAetheryte);
        P.TaskManager.Enqueue(WorldChange.InteractWithTargetedAetheryte);
        P.TaskManager.Enqueue(() => Utils.TrySelectSpecificEntry(Lang.ResidentialDistrict, () => EzThrottler.Throttle("SelectResidentialDistrict")), $"TaskGoToResidentialDistrictSelect {Lang.ResidentialDistrict}");
        P.TaskManager.Enqueue(() => Utils.TrySelectSpecificEntry(Lang.GoToWard, () => EzThrottler.Throttle("SelectGoToWard")), $"TaskGoToResidentialDistrictSelect {Lang.GoToWard}");
        if(ward > 1) P.TaskManager.Enqueue(() => SelectWard(ward));
        P.TaskManager.Enqueue(GoToWard);
        P.TaskManager.Enqueue(ConfirmYesNoGoToWard);
    }

    public static bool ConfirmYesNoGoToWard()
    {
        var x = (AddonSelectYesno*)Utils.GetSpecificYesno(true, Lang.TravelTo);
        if (x != null)
        {
            if (x->YesButton->IsEnabled && EzThrottler.Throttle("ConfirmTravelTo"))
            {
                ClickSelectYesNo.Using((nint)x).Yes();
                return true;
            }
        }
        return false;
    }

    public static bool? SelectWard(int ward)
    {
        if(TryGetAddonByName<AtkUnitBase>("HousingSelectBlock", out var addon) && IsAddonReady(addon))
        {
            if (ward == 1)
            {
                return true;
            }
            else
            {
                if (EzThrottler.Throttle("HousingSelectBlockSelectWard"))
                {
                    Callback.Fire(addon, true, 1, ward - 1);
                    return true;
                }
            }
        }
        return false;
    }

    public static bool? GoToWard()
    {
        if (TryGetAddonByName<AtkUnitBase>("HousingSelectBlock", out var addon) && IsAddonReady(addon))
        {
            var button = addon->GetButtonNodeById(34);
            if (button->IsEnabled)
            {
                if (EzThrottler.Throttle("HousingSelectBlockConfirm"))
                {
                    button->ClickAddonButton(addon);
                    return true;
                }
            }
        }
        return false;
    }
}
