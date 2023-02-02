using Newtonsoft.Json;
using Steamworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;

using DALib.Extensions;
using static DALib.Functions.StringFunctions;

namespace DALib;

[Label("Client Config")]
public class ClientConfig : ModConfig {
	public override ConfigScope Mode => ConfigScope.ClientSide;
	public static ClientConfig Instance;

	[Header("Debug")]
	[Label("Show Debug Messages")]
	[Tooltip("Shows debug messages\nThis will affect all mods that use DALib's debug setting and methods")]
	[DefaultValue(false)]
	public bool Debug = false;

	[Header("Client Info")]
//	[Label("Client UUID")]
//	[Tooltip("Note: This UUID will not be the same between installations however you change it yourself in tModLoader's config.json under ClientUUID")]
//	[JsonIgnore]
	internal string ClientUUID => Main.netMode == 2 ? "" : Main.clientUUID;

//	[Label("Steam ID")]
//	[Tooltip("Note: This will only be valid if you are using a Steam version of tModLoader")]
//	[JsonIgnore]
	internal string SteamID => Main.netMode == 2 ? "" : SteamUser.GetSteamID().m_SteamID.ToString() ?? "";

	[Label("Authentication ID")]
	[Tooltip("This is used for authentication with DALib\nThis is tied to either your Steam ID or Client UUID")]
	public string AuthID => Main.netMode == 2 ? "" : !String.IsNullOrWhiteSpace(SteamID) ? MD5Hash(SteamID) : MD5Hash(ClientUUID);

	[Label("Is Admin")]
	[Tooltip("Are you a DALib Admin?\nIf so, you can edit any mod's Server Config that use DALib's Authentication")]
	[JsonIgnore]
	public string IsAdmin => Main.netMode == 2 ? "" : Main.netMode == 0 ? "Local" : ServerConfig.Instance.MasterAdminID == AuthID ? "Master" : ServerConfig.Instance.AdminIDs.ContainsCI(AuthID) ? "Yes" : "No";
}

[Label("Server Config")]
public class ServerConfig : ModConfig {
	public override ConfigScope Mode => ConfigScope.ServerSide;
	public static ServerConfig Instance;

	[Header("Debug")]
	[Label("Show Debug Messages")]
	[Tooltip("Shows debug messages in the server console\nThis will affect all mods that use DALib's debug setting and methods")]
	[DefaultValue(false)]
	public bool Debug = false;

	private string _MasterAdminID;
	[Header("Authentication")]
	[Label("Master Admin ID")]
	[Tooltip("The Authentication ID of the Master Admin")]
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

	[Label("Admin IDs")]
	[Tooltip("Add the Authentication IDs of players you want to be able to edit Server Configs for all mods that use DALib's authentication to this list\nPlayers can get their Authentication IDs from DALib's Client Info")]
	[DefaultListValue("")]
	public List<string> AdminIDs = new List<string>();

	/*[Header("Supported Authentication Modes")]
	[Label("DALib")]
	[Tooltip("Master Admin will not be affected")]
	[DefaultValue(true)]
	public bool DALibAuth => true;

	[Label("HERO's Mod")]
	[Tooltip("Imported as regular Admin and Master Admin\nNOT CURRENTLY IMPLEMENTED")]
	[DefaultValue(true)]
	public bool HEROsModAuth => false;*/

	public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		=> Auth.IsMasterAdmin(whoAmI, ref message);
}

public static class Config {
	public static ClientConfig Client => ClientConfig.Instance;
	public static ServerConfig Server => ServerConfig.Instance;
	public static bool Debug => Main.netMode == 2 ? ServerConfig.Instance.Debug : ClientConfig.Instance.Debug;
}

