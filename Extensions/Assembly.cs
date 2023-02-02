using System.Reflection;
using System.Text.RegularExpressions;

namespace DALib.Extensions;

public static class AssemblyExtensions {
	public static string Name(this Assembly assembly)
		=> new Regex("_[0-9]+$").Replace(assembly.GetName().Name, "", 1);
}
