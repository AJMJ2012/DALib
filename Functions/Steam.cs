using Steamworks;

namespace DALib.Functions;

internal static class Steam
{
    // Hopefully this works
    public static ulong? SteamID => (ulong?)SteamUser.GetSteamID().m_SteamID ?? (ulong)0;
}