using Terraria;
using Terraria.ID;

namespace DALib.Checks;

public static class NPCChecks {
	public static bool IsBoss(this NPC npc) {
		if (npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || npc.GetBossHeadTextureIndex() >= 0) { return true; }
		switch (npc.type) {
			case NPCID.EaterofWorldsHead:
			case NPCID.GolemHead:
			case NPCID.QueenBee:
				return true;
		}
		return false;
	}

	public static bool IsBossPiece(this NPC npc) {
		if (IsChild(npc) && (Main.npc[npc.realLife].boss || NPCID.Sets.ShouldBeCountedAsBoss[Main.npc[npc.realLife].type] || Main.npc[npc.realLife].GetBossHeadTextureIndex() >= 0)) { return true; }
		switch (npc.type) {
			case NPCID.Creeper:
			case NPCID.EaterofWorldsBody:
			case NPCID.EaterofWorldsTail:
			case NPCID.Golem:
			case NPCID.GolemFistLeft:
			case NPCID.GolemFistRight:
			case NPCID.MartianSaucer:
			case NPCID.MartianSaucerCannon:
			case NPCID.MartianSaucerTurret:
			case NPCID.MoonLordCore:
			case NPCID.MoonLordHand:
			case NPCID.MoonLordHead:
			case NPCID.MoonLordFreeEye:
			case NPCID.PlanterasHook:
			case NPCID.PlanterasTentacle:
			case NPCID.PrimeCannon:
			case NPCID.PrimeLaser:
			case NPCID.PrimeSaw:
			case NPCID.PrimeVice:
			case NPCID.SkeletronHand:
			case NPCID.SkeletronHead:
			case NPCID.TheDestroyerBody:
			case NPCID.TheDestroyerTail:
			case NPCID.TheHungry:
			case NPCID.TheHungryII:
			case NPCID.WallofFlesh:
			case NPCID.WallofFleshEye:
			case NPCID.Probe:
				return true;
		}
		return false;
	}

	public static bool IsChild(this NPC npc)
		=> (npc.realLife >= 0 && npc.realLife != npc.whoAmI);

	public static bool IsChildOfBoss(this NPC npc)
		=> (npc.realLife >= 0 && npc.realLife != npc.whoAmI && IsBoss(Main.npc[npc.realLife]));

	public static bool IsChildOfBossPiece(this NPC npc)
		=> (npc.realLife >= 0 && npc.realLife != npc.whoAmI && IsBossPiece(Main.npc[npc.realLife]));

	public static bool IsBossOrPieceOrChildOf(this NPC npc)
		=> IsBoss(npc) || IsBossPiece(npc) || IsChildOfBoss(npc) || IsChildOfBossPiece(npc);
}
