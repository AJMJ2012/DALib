using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DALib;

public static class UI
{
	static string LastLine = "";
	static string LastLinePercent = "";

	static readonly Assembly A = typeof(ModLoader).Assembly;
	static readonly FieldInfo Interface_LoadMods_Field = A.GetType("Terraria.ModLoader.UI.Interface").GetField("loadMods", BindingFlags.NonPublic | BindingFlags.Static);
	static readonly FieldInfo UILoadMods_DisplayText_Field = A.GetType("Terraria.ModLoader.UI.UILoadMods").GetField("DisplayText", BindingFlags.Public | BindingFlags.Instance);
	static readonly PropertyInfo UILoadMods_Progress_Property = A.GetType("Terraria.ModLoader.UI.UILoadMods").GetProperty("Progress", BindingFlags.Public | BindingFlags.Instance);
	static readonly PropertyInfo UILoadMods_SubProgressText_Property = A.GetType("Terraria.ModLoader.UI.UILoadMods").GetProperty("SubProgressText", BindingFlags.Public | BindingFlags.Instance);
	static readonly object Interface_LoadMods_Object = Interface_LoadMods_Field.GetValue(null);

	public static void SetLoadingMessageDisplayText(string title)
	{
		if (Main.netMode == NetmodeID.Server)
		{
			string Line = title;
			if (Line != LastLine)
			{
				Console.WriteLine(Line);
				LastLine = Line;
			}
		}
		else
		{
			UILoadMods_DisplayText_Field.SetValue(Interface_LoadMods_Object, title);
		}
	}

	public static void SetLoadingMessageProgress(float progress)
	{
		if (Main.netMode == NetmodeID.Server && progress >= 0)
		{
			string LinePercent = LastLine + " " + (int)progress + "%";
			if (LinePercent != LastLinePercent)
			{
				Console.WriteLine(LinePercent);
				LastLinePercent = LinePercent;
			}
		}
		else
		{
			UILoadMods_Progress_Property.SetValue(Interface_LoadMods_Object, progress);
		}
	}

	public static void SetLoadingMessageSubProgressText(string submessage)
	{
		if (Main.netMode != NetmodeID.Server)
		{
			UILoadMods_SubProgressText_Property.SetValue(Interface_LoadMods_Object, submessage);
		}
	}

	public static void SetLoadingMessage(string title, float progress = -1, string submessage = "")
	{
		if (Main.netMode == NetmodeID.Server && progress >= 0)
		{
			string Line = title + " " + (int)progress + "%";
			if (Line != LastLine)
			{
				Console.WriteLine(Line);
				LastLine = Line;
			}
		}
		else
		{
			SetLoadingMessageDisplayText(title);
			SetLoadingMessageProgress(progress);
			SetLoadingMessageSubProgressText(submessage);
		}
	}
}

