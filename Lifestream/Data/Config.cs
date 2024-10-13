﻿using ECommons.Configuration;
using Lifestream.Enums;
using Lifestream.Tasks.Shortcuts;

namespace Lifestream.Data;

public class Config : IEzConfig
{
    public bool Enable = true;
    internal bool AllowClosingESC2 = false;
    public int ButtonWidth = 10;
    public int[] ButtonWidthArray = null;
    public int ButtonHeightAetheryte = 1;
    public int ButtonHeightWorld = 5;
    public bool FixedPosition = false;
    public Vector2 Offset = Vector2.Zero;
    public bool UseMapTeleport = true;
    public bool HideAddon = true;
    public HashSet<string> HideAddonList = [.. Utils.DefaultAddons];
    public BasePositionHorizontal PosHorizontal = BasePositionHorizontal.Middle;
    public BasePositionVertical PosVertical = BasePositionVertical.Middle;
    public bool ShowAethernet = true;
    public bool ShowWorldVisit = true;
    public HashSet<uint> Favorites = [];
    public HashSet<uint> Hidden = [];
    public Dictionary<uint, string> Renames = [];
    public WorldChangeAetheryte WorldChangeAetheryte = WorldChangeAetheryte.Uldah;
    public bool Firmament = true;
    public bool WalkToAetheryte = true;
    public bool LeavePartyBeforeWorldChange = true;
    public bool AllowDcTransfer = true;
    public bool LeavePartyBeforeLogout = true;
    public bool TeleportToGatewayBeforeLogout = true;
    public bool NoProgressBar = false;
    public Dictionary<string, int> ServiceAccounts = [];
    public bool DCReturnToGateway = false;
    public bool WorldVisitTPToAethernet = false;
    public string WorldVisitTPTarget = "";
    public bool WorldVisitTPOnlyCmd = true;
    public bool UseAutoRetainerAccounts = true;
    public bool SlowTeleport = false;
    public int SlowTeleportThrottle = 0;
    public bool WaitForScreenReady = true;
    public bool ShowWards = true;
    internal bool ShowPlots = false;
    public List<AddressBookFolder> AddressBookFolders = [];
    public bool AddressNoPathing = false;
    public bool AddressApartmentNoEntry = false;
    public bool SingleBookMode = false;
    public List<MultiPath> MultiPathes = [];
    public string GameVersion = "";
    public Dictionary<uint, int> PublicInstances = [];
    public bool ShowInstanceSwitcher = true;
    public bool InstanceSwitcherRepeat = true;
    public int InstanceButtonHeight = 10;
    public bool UseSprintPeloton = true;
    public bool EnableFlydownInstance = true;
    public bool DisplayChatTeleport = false;
    public bool DisplayPopupNotifications = true;
    public List<HousePathData> HousePathDatas = [];
    public bool EnterMyApartment = true;
    public HouseEnterMode HouseEnterMode = HouseEnterMode.None;
    public bool UseReturn = true;
    public uint PreferredInn = 0;
    public List<AutoPropertyData> PropertyPrio = [new(true, TaskPropertyShortcut.PropertyType.Home), new(true, TaskPropertyShortcut.PropertyType.FC), new(true, TaskPropertyShortcut.PropertyType.Apartment), new(true, TaskPropertyShortcut.PropertyType.Inn)];
    public bool EnableDvcRetry = false;
    public int MaxDcvRetries = 3000;
    public bool DcvUseAlternativeWorld = true;
    public int DcvRetryInterval = 30;
    public bool RetryWorldVisit = false;
    public int RetryWorldVisitInterval = 5;
    public List<CustomAlias> CustomAliases = [];
    public bool UseGuestWorldTravel = false;
    public bool AllowDCTravelFromCharaSelect = true;
    public List<TravelBanInfo> TravelBans = [];
    public bool TerminateSelfPartyFinder = false;
}
