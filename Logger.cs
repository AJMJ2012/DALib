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

	public static void ErrorLog(string message, string modName = null) {
		if (modName == null) modName = Assembly.GetCallingAssembly().Name();
		message = "Error: "  + message;
		if (Main.netMode == 2) {
			Console.WriteLine(message.WithPrefix(modName));
		}
		else {
			Main.NewText(message.WithPrefix(modName));
		}
	}

	public static void ErrorLog(Exception e, string modName = null) {
		if (modName == null) modName = Assembly.GetCallingAssembly().Name();
		string message = e.ToString();
		if (Main.netMode == 2) {
			Console.WriteLine(message.WithPrefix(modName));
		}
		else {
			Main.NewText(message.WithPrefix(modName));
		}
	}

}


// Because using it internally wants to use log4net even with the DALib prefix.
internal static class _Logger {
	public static void Log(string message, string modName = null) => Logger.Log(message, modName);
	public static void DebugLog(string message, string modName = null) => Logger.DebugLog(message, modName);
}