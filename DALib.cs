using Terraria.ModLoader;
using System.IO;

namespace DALib;

public class DALib : Mod {
	public override void HandlePacket(BinaryReader reader, int whoAmI) {
		PacketHandler.HandlePacket(reader, whoAmI);
	}

	public class ServerCommands : ModCommand {
		public override CommandType Type => CommandType.Console;
		public override string Command => "dalib";
		public override string Description => this.Name;
		public override string Usage => Command + " setmasteradmin <authid>\n" + Command + " debug <yes/no>";
		public override void Action(CommandCaller caller, string input, string[] args) {
			switch (args[0].ToLower()) {
				case "setmasteradmin":
					if (args.Length > 1 && args[1] != null) {
						if (args[1].Length == Functions.StringFunctions.hashSize) {
							Config.Server.MasterAdminID = args[1];
							_Logger.Log("Master Admin Set");
						}
						else {
							_Logger.Log("Error: Invalid Authentication ID");
						}
					}
					else {
						_Logger.Log("Usage: " + Command + " " + args[0].ToLower() + " <authid>");
					}
					return;
				case "debug":
					if (args.Length > 1 && args[1] != null) {
						if (args[1] == "1" || args[1] == "true" || args[1] == "y" || args[1] == "yes" || args[1] == "on") {
							Config.Server.Debug = true;
							Config.Client.Debug = true;
							_Logger.Log("Debug Enabled");
						}
						else if (args[1] == "0" || args[1] == "false" || args[1] == "n" || args[1] == "no" || args[1] == "off") {
							Config.Server.Debug = false;
							Config.Client.Debug = false;
							_Logger.Log("Debug Disabled");
						}
						else {
							_Logger.Log("Error: Invalid Switch");
						}
					}
					else {
						_Logger.Log("Usage: " + Command + " " + args[0].ToLower() + " <yes/no>");
					}
					return;
			}
		}
	}
}