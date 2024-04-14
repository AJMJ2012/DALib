using Newtonsoft.Json;
using Steamworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

using DALib.Extensions;
using static DALib.Functions.StringFunctions;

namespace DALib;

public class ClientConfig : ModConfig {
	public override ConfigScope Mode => ConfigScope.ClientSide;
	public static ClientConfig Instance;

	[Header("Debug")]
	[DefaultValue(false)]
	public bool Debug = false;

	[Header("DebugTooltips")]
	[DefaultValue(false)]
	public bool TooltipDebug = false;

	[Header("ClientInfo")]
	[JsonIgnore]
	internal string ClientUUID => Main.netMode == 2 ? "" : Main.clientUUID;

	[JsonIgnore]
	internal string SteamID => Main.netMode == 2 ? "" : SteamUser.GetSteamID().m_SteamID.ToString() ?? "";

	[JsonIgnore]
	public string AuthID => Main.netMode == 2 ? "" : !String.IsNullOrWhiteSpace(SteamID) ? HashString(SteamID) : HashString(ClientUUID);

	[JsonIgnore]
	public string IsAdmin => /*Main.netMode == 2 ? "" : Main.netMode == 0 ? "Local" : */ServerConfig.Instance.MasterAdminID == AuthID ? "Master" : ServerConfig.Instance.AdminIDs.ContainsCI(AuthID) ? "Yes" : "No";
}

public class ServerConfig : ModConfig {
	public override ConfigScope Mode => ConfigScope.ServerSide;
	public static ServerConfig Instance;

	[Header("Debug")]
	[DefaultValue(false)]
	public bool Debug = false;
/*
	private string _MasterAdminID;
	[Header("Authentication")]
	[BackgroundColor(255, 150, 150)]
	[ReloadRequired]
	public string MasterAdminID {
		get {
			// Auto apply Auth ID if Steam ID is detected
			return !String.IsNullOrWhiteSpace(_MasterAdminID) ? _MasterAdminID : !String.IsNullOrWhiteSpace(Config.Client.SteamID) ? Config.Client.AuthID : "";
		}
		set {
			_MasterAdminID = value;
		}
	}
*/
	[Header("Authentication")]
	[BackgroundColor(255, 150, 150)]
	[ReloadRequired]
	public string MasterAdminID;

	[DefaultListValue("")]
	public List<string> AdminIDs = new List<string>();

	/*[Header("SupportedAuthenticationModes")]
	[DefaultValue(true)]
	public bool DALibAuth => true;

	[DefaultValue(true)]
	public bool HEROsModAuth => false;*/

	public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
		=> Auth.IsMasterAdmin(whoAmI, ref message);
}

public static class Config {
	public static ClientConfig Client => ClientConfig.Instance;
	public static ServerConfig Server => ServerConfig.Instance;
	public static bool Debug => Main.netMode == 2 ? ServerConfig.Instance.Debug : ClientConfig.Instance.Debug;
}

