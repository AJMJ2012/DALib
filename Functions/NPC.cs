using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DALib.Functions;

public static class NPCFunctions {
#region NPCID
	static Dictionary<int, string> npcTypeIDCache = new();

	static string GetVanillaNPCName(int type) {
		string typeID = type.ToString();
		if (!npcTypeIDCache.TryGetValue(type, out typeID)) {
			typeID = FieldResolver.FindConstantName<short>(typeof(NPCID), (short)type) ?? typeID;
			npcTypeIDCache.TryAdd(type, typeID);
		}
		return typeID;
	}

	static string GetNPCID(int type)
		=> String.Format("{0}.{1}", GetNPCMod(type), GetNPCName(type));

	static string GetNPCMod(int type) 
		=> NPCLoader.GetNPC(type)?.Mod.Name ?? "Terraria";

	static string GetNPCName(int type)
		=> NPCLoader.GetNPC(type)?.Name ?? GetVanillaNPCName(type);

	public static string GetNPCID(this NPC npc)
		=> GetNPCID(npc.type);

	public static string GetNPCName(this NPC npc)
		=> GetNPCName(npc.type);

	public static string GetNPCMod(this NPC npc)
		=> GetNPCMod(npc.type);
#endregion
#region NPCAIStyleID
	static Dictionary<int, string> npcAIStyleIDCache = new();
	static string GetNPCAIStyleID(int aiStyle) {
		string aiStyleID = aiStyle.ToString();
		if (!npcAIStyleIDCache.TryGetValue(aiStyle, out aiStyleID)) {
			aiStyleID = FieldResolver.FindConstantName<short>(typeof(NPCAIStyleID), (short)aiStyle) ?? aiStyleID;
			npcAIStyleIDCache.TryAdd(aiStyle, aiStyleID);
		}
		return String.IsNullOrEmpty(aiStyleID) ? aiStyle.ToString() : aiStyleID;
	}

	public static string GetNPCAIStyleID(this NPC npc)
		=> GetNPCAIStyleID(npc.aiStyle);
#endregion
}
