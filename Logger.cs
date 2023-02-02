using Microsoft.Xna.Framework;
using System;
using System.Reflection;
using Terraria;
using Terraria.Localization;

using DALib.Extensions;
using static DALib.Functions.ModFunctions;

namespace DALib;

public static class Logger {
	public static void DebugLog(string message, string modName = null) {
		if (!Config.Debug) { return; }
		if (modName == null) modName = Assembly.GetCallingAssembly().Name();
		if (Main.netMode == 2) {
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("[Debug] ["+modName+"] " + message);
			Console.ResetColor();
		}
		else {
			Main.NewText("[Debug] ["+modName+"] " + message, Color.Gray);
		}
	}

	public static void Log(string message, string modName = null) {
		if (modName == null) modName = Assembly.GetCallingAssembly().Name();
		if (Main.netMode == 2) {
			Console.WriteLine(message.WithPrefix(modName));
		}
		else {
			Main.NewText(message.WithPrefix(modName));
		}
	}
}

