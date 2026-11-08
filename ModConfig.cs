using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;

using DALib.Extensions;
using DALib.Functions;
using static DALib.Functions.StringFunctions;

namespace DALib;

public class ClientConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ClientSide;
	public static ClientConfig Instance;

	[Header("Debug")]
	[DefaultValue(false)]
	public bool Debug = false;

	[Header("DebugTooltips")]
	[DefaultValue(false)]
	public bool TooltipDebug = false;

	[Header("ClientInfo")]
	public bool _;
	internal string ClientUUID => Main.netMode == NetmodeID.Server ? "" : Main.clientUUID;

	internal string SteamID => Main.netMode == NetmodeID.Server ? "" : Steam.SteamID?.ToString() ?? "";

	public string AuthID => Main.netMode == NetmodeID.Server ? "" : !String.IsNullOrWhiteSpace(SteamID) ? HashString(SteamID) : HashString(ClientUUID);

	public string IsAdmin => Main.netMode == NetmodeID.Server ? "" : Main.netMode == NetmodeID.SinglePlayer ? "Local" : ServerConfig.Instance != null ? (ServerConfig.Instance.MasterAdminID == AuthID ? "Master" : ServerConfig.Instance.AdminIDs.ContainsCI(AuthID) ? "Yes" : "No") : "Unknown";
}

public class ServerConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;
	public static ServerConfig Instance;

	[Header("Debug")]
	[DefaultValue(false)]
	public bool Debug = false;

	[Header("Authentication")]
	[BackgroundColor(255, 150, 150)]
	public string MasterAdminID;

	[DefaultListValue("")]
	public List<string> AdminIDs = [];

	public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
		=> Auth.IsMasterAdmin(whoAmI, ref message);
}

public static class Config
{
	public static ClientConfig Client => ClientConfig.Instance;
	public static ServerConfig Server => ServerConfig.Instance;
	public static bool Debug => Main.netMode == NetmodeID.Server ? ServerConfig.Instance.Debug : ClientConfig.Instance.Debug;
}

