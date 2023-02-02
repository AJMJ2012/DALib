using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DALib;

internal enum Requests : byte {
	ClientUUID,
	SteamID,
	AdminNotice,
}

public static class PacketHandler {
	static Mod mod => ModLoader.GetMod("DALib");
	public static void HandlePacket(BinaryReader reader, int whoAmI) {
		Requests request = (Requests)reader.ReadByte();
		if (request == Requests.SteamID) {
			MPlayer.ReceiveSteamID(ref reader, whoAmI);
		}
		else if (request == Requests.ClientUUID) {
			MPlayer.ReceiveClientUUID(ref reader, whoAmI);
		}
		else if (request == Requests.AdminNotice) {
			ReceiveAdminNotice(ref reader, whoAmI);
		}
	}

	internal static void SendAdminNotice(bool IsAmin, int SendTo) {
		if (Main.netMode == 2) {
			Logger.DebugLog("Sending admin notice to " + Main.player[SendTo].name);
			ModPacket netMessage = mod.GetPacket();
			netMessage.Write((byte)Requests.AdminNotice);
			netMessage.Write(IsAmin);
			netMessage.Send(SendTo);
		}
	}

	internal static void ReceiveAdminNotice(ref BinaryReader reader, int whoAmI) {
		if (Main.netMode == 1) {
			if (whoAmI == Main.myPlayer) {
				Player player = Main.player[Main.myPlayer];
				MPlayer PlayerInfo = player.GetModPlayer<MPlayer>();
				bool IsAdmin = reader.ReadBoolean();
				if (IsAdmin) {
					Main.NewText("You are a DALib admin on this server.", Colors.RarityGreen);
				}
				else {
					Main.NewText("You are no longer a DALib admin on this server.", Colors.RarityRed);
				}
			}
		}
	}
}

