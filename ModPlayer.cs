using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using static DALib.Functions.StringFunctions;

namespace DALib;

public class MPlayer : ModPlayer
{
	static Mod mod => ModLoader.GetMod("DALib");
	internal string ClientUUID;
	internal string SteamID;
	internal string AuthID;

	public override void Initialize()
	{
		ClientUUID = Config.Client.ClientUUID;
		SteamID = Config.Client.SteamID;
		AuthID = Config.Client.AuthID;
	}

	public override void OnEnterWorld()
	{
		if (Player.whoAmI != Main.myPlayer) { return; }
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			if (!String.IsNullOrWhiteSpace(SteamID))
			{
				SendSteamID(SteamID);
			}
			SendClientUUID(ClientUUID);
		}
	}

	internal static void SendClientUUID(string ClientUUID)
	{
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			Logger.DebugLog("Sending ClientUUID " + ClientUUID + " to server");
			ModPacket netMessage = mod.GetPacket();
			netMessage.Write((byte)Requests.ClientUUID);
			netMessage.Write(ClientUUID);
			netMessage.Send();
		}
	}

	internal static void ReceiveClientUUID(ref BinaryReader reader, int whoAmI)
	{
		if (Main.netMode == NetmodeID.Server)
		{
			Player player = Main.player[whoAmI];
			MPlayer PlayerInfo = player.GetModPlayer<MPlayer>();
			string ClientUUID = reader.ReadString();
			Logger.DebugLog("Received ClientUUID " + ClientUUID + " from " + player.name);
			PlayerInfo.ClientUUID = ClientUUID;
			if (String.IsNullOrWhiteSpace(PlayerInfo.SteamID))
			{ // Only if there is no SteamID
				PlayerInfo.AuthID = HashString(ClientUUID);
			}
		}
	}

	internal static void SendSteamID(string SteamID)
	{
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			if (!String.IsNullOrWhiteSpace(SteamID))
			{
				Logger.DebugLog("Sending SteamID " + SteamID + " to server");
				ModPacket netMessage = mod.GetPacket();
				netMessage.Write((byte)Requests.SteamID);
				netMessage.Write(SteamID);
				netMessage.Send();
			}
		}
	}

	internal static void ReceiveSteamID(ref BinaryReader reader, int whoAmI)
	{
		if (Main.netMode == NetmodeID.Server)
		{
			Player player = Main.player[whoAmI];
			MPlayer PlayerInfo = player.GetModPlayer<MPlayer>();
			string SteamID = reader.ReadString();
			Logger.DebugLog("Received SteamID " + SteamID + " from " + player.name);
			PlayerInfo.SteamID = SteamID;
			if (!String.IsNullOrWhiteSpace(PlayerInfo.SteamID))
			{ // Just in case an invalid string was sent
				PlayerInfo.AuthID = HashString(SteamID);
			}
		}
	}
}

