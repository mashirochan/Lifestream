﻿using Dalamud.Memory;
using ECommons.Automation;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lifestream.Schedulers
{
    internal static unsafe class DCChange
    {
        internal static bool DCThrottle => EzThrottler.Throttle("DCOperation", 200);
        internal static bool DCRethrottle() => EzThrottler.Throttle("DCOperation", 200, true);

        internal static bool? TitleScreenClickStart()
        {
            if(Util.CanAutoLogin() && TryGetAddonByName<AtkUnitBase>("_TitleMenu", out var title) && IsAddonReady(title) && DCThrottle && EzThrottler.Throttle("TitleScreenClickStart"))
            {
                Callback.Fire(title, true, (int)1);
                return true;
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }

        internal static bool? OpenContextMenuForChara(string name)
        {
            if (TryGetAddonByName<AtkUnitBase>("_CharaSelectListMenu", out var addon) && IsAddonReady(addon))
            {
                if (Util.TryGetCharacterIndex(name, out var index) && DCThrottle && EzThrottler.Throttle("OpenContextMenuForChara"))
                {
                    Callback.Fire(addon, true, (int)17, (int)1, (int)index);
                    return true;
                }
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }

        internal static bool? SelectVisitAnotherDC()
        {
            if (TryGetAddonByName<AddonContextMenu>("ContextMenu", out var menu) && IsAddonReady(&menu->AtkUnitBase))
            {
                var addon = menu->AtkUnitBase;
                var list = addon.UldManager.NodeList[2];
                var item = list->GetAsAtkComponentNode()->Component->UldManager.NodeList[9];
                var textNode = item->GetAsAtkComponentNode()->Component->UldManager.NodeList[6];
                if (textNode->Alpha_2 == 255)
                {
                    var text = MemoryHelper.ReadSeString(&textNode->GetAsAtkTextNode()->NodeText).ExtractText();
                    if(text.EqualsAny("Visit Another Data Center") && DCThrottle && EzThrottler.Throttle("SelectVisitAnotherDC"))
                    {
                        Callback.Fire(&menu->AtkUnitBase, true, (int)0, (int)8, (int)0, new AtkValue() { Type = 0, Int = 0 }, new AtkValue() { Type = 0, Int = 0 });
                        return true;
                    }
                }
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }

        internal static bool? ConfirmDcVisitIntention()
        {
            if (TryGetAddonByName<AtkUnitBase>("LobbyDKTCheck", out var addon) && IsAddonReady(addon) && DCThrottle)
            {
                Callback.Fire(addon, true, 0);
                return true;
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }

        internal static bool? SelectTargetDataCenter(string name)
        {
            if (TryGetAddonByName<AtkUnitBase>("LobbyDKTWorldList", out var addon) && IsAddonReady(addon))
            {
                var list = addon->UldManager.NodeList[7]->GetAsAtkComponentNode();
                for (int i = 3; i < 3+4; i++)
                {
                    var t = list->Component->UldManager.NodeList[i]->GetAsAtkComponentNode()->Component->UldManager.NodeList[8]->GetAsAtkTextNode();
                    if (t->AtkResNode.Alpha_2 == 255)
                    {
                        var text = MemoryHelper.ReadSeString(&t->NodeText).ExtractText();
                        if (text == name && DCThrottle && EzThrottler.Throttle("SelectTargetDataCenter"))
                        {
                            P.Memory.ConstructEvent(addon, 1, 7, i - 2);
                            return true;
                        }
                    }
                }
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }

        internal static bool? SelectTargetWorld(string name)
        {
            if (TryGetAddonByName<AtkUnitBase>("LobbyDKTWorldList", out var addon) && IsAddonReady(addon))
            {
                var list = addon->UldManager.NodeList[6]->GetAsAtkComponentNode();
                for (int i = 3; i < 3+8; i++)
                {
                    var t = list->Component->UldManager.NodeList[i]->GetAsAtkComponentNode()->Component->UldManager.NodeList[8]->GetAsAtkTextNode();
                    if (t->AtkResNode.Alpha_2 == 255)
                    {
                        var text = MemoryHelper.ReadSeString(&t->NodeText).ExtractText();
                        if (text == name && DCThrottle && EzThrottler.Throttle("SelectTargetWorld"))
                        {
                            P.Memory.ConstructEvent(addon, 2, 6, i - 2);
                            return true;
                        }
                    }
                }
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }

        internal static bool? ConfirmDcVisit()
        {
            if (TryGetAddonByName<AtkUnitBase>("LobbyDKTWorldList", out var addon) && IsAddonReady(addon))
            {
                if (addon->UldManager.NodeList[5]->GetAsAtkComponentButton()->IsEnabled && DCThrottle && EzThrottler.Throttle("ConfirmDcVisit", 5000))
                {
                    Callback.Fire(addon, true, (int)4);
                    return true;
                }
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }

        internal static bool? ConfirmDcVisit2()
        {
            if (TryGetAddonByName<AtkUnitBase>("LobbyDKTCheckExec", out var addon) && IsAddonReady(addon))
            {
                if (addon->UldManager.NodeList[3]->GetAsAtkComponentButton()->IsEnabled && DCThrottle && EzThrottler.Throttle("ConfirmDcVisit", 5000))
                {
                    Callback.Fire(addon, true, (int)0);
                    return true;
                }
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }

        internal static bool? SelectOk()
        {
            if (TryGetAddonByName<AtkUnitBase>("SelectOk", out var addon) && IsAddonReady(addon))
            {
                if (DCThrottle && EzThrottler.Throttle("SelectOk", 500))
                {
                    Callback.Fire(addon, true, (int)0);
                    return true;
                }
            }
            else
            {
                DCRethrottle();
            }
            return false;
        }
    }
}