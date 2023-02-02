using Terraria;

using DALib.Extensions;

namespace DALib;

public static class Auth {
	public static bool IsMasterAdmin(int whoAmI, ref string message) {
		if (IsMasterAdmin(whoAmI)) {
			message = "Server Config updated";
			return true;
		}
		else {
			message = "You are not a DALib Master Admin";
			return false;
		}
	}

	public static bool IsMasterAdmin(int whoAmI) 
		=> Config.Server.MasterAdminID == Main.player[whoAmI].GetModPlayer<MPlayer>().AuthID;

	public static bool IsAdmin(int whoAmI, ref string message) {
		if (IsAdmin(whoAmI)) {
			message = "Server Config updated";
			return true;
		}
		else {
			message = "You are not a DALib Admin";
			return false;
		}
	}

	public static bool IsAdmin(int whoAmI)
		=> IsMasterAdmin(whoAmI) || Config.Server.AdminIDs.ContainsCI(Main.player[whoAmI].GetModPlayer<MPlayer>().AuthID);
}

