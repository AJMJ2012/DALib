using System.IO;
using Terraria.ModLoader;

namespace DALib;

public class DALib : Mod {
	public override void HandlePacket(BinaryReader reader, int whoAmI) {
		PacketHandler.HandlePacket(reader, whoAmI);
	}
}