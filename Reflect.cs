using System;
using System.Linq;
using System.Reflection;
using Terraria;

namespace DALib;

public static class Reflect {
	// For Static, src must be a System.RuntimeType or System.Type of the object
	// For Non-Static, src must be an instance of the object
	public static T GetValue<T>(object src, string fieldName) {
		try {
			if (src.GetType().ToString() == "System.RuntimeType" || src.GetType().ToString() == "System.Type") {
				Type type = (Type)src;
				FieldInfo info = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
				if (info != null) {
					return (T) info.GetValue(null);
				}
			}
			else {
				FieldInfo info = src.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				if (info != null) {
					return (T) info.GetValue(src);
				}
			}
		} catch {}
		return default(T);
	}

	public static void SetValue(object src, string fieldName, object newValue) {
		try {
			if (src.GetType().ToString() == "System.RuntimeType" || src.GetType().ToString() == "System.Type") {
				Type type = (Type)src;
				FieldInfo info = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
				if (info != null) {
					info.SetValue(null, newValue);
				}
			}
			else {
				FieldInfo info = src.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				if (info != null) {
					info.SetValue(src, newValue);
				}
			}
		} catch {}
	}

	public static T GetPropValue<T>(object src, string propName) {
		try {
			if (src.GetType().ToString() == "System.RuntimeType" || src.GetType().ToString() == "System.Type") {
				Type type = (Type)src;
				PropertyInfo info = type.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
				if (info != null) {
					return (T) info.GetValue(null);
				}
			}
			else {
				PropertyInfo info = src.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				if (info != null) {
					return (T) info.GetValue(src);
				}
			}
		} catch {}
		return default(T);
	}

	public static void SetPropValue(object src, string propName, object newValue) {
		try {
			if (src.GetType().ToString() == "System.RuntimeType" || src.GetType().ToString() == "System.Type") {
				Type type = (Type)src;
				PropertyInfo info = type.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
				if (info != null) {
					info.SetValue(null, newValue);
				}
			}
			else {
				PropertyInfo info = src.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				if (info != null) {
					info.SetValue(src, newValue);
				}
			}
		} catch {}
	}

	public static T Invoke<T>(object src, string methodName, object[] args = null, Type[] argTypes = null) {
		try {
			if (args == null) {
				args = new object[]{};
				argTypes = new Type[]{};
			}
			else if (argTypes == null) {
				argTypes = args.ToList().Select(s => s.GetType()).ToArray();
			}
			if (src.GetType().ToString() == "System.RuntimeType" || src.GetType().ToString() == "System.Type") {
				Type type = (Type)src;
				var method = type.GetTypeInfo().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, null, argTypes, null);
				if (method != null) {
					return (T) method.Invoke(null, args);
				}
			}
			else {
				var method = src.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, argTypes, null);
				if (method != null) {
					return (T) method.Invoke(src, args);
				}
			}
		} catch {}
		return default(T);
	}

	public static void Invoke(object src, string methodName, object[] args = null, Type[] argTypes = null) {
		try {
			if (args == null) {
				args = new object[]{};
				argTypes = new Type[]{};
			}
			else if (argTypes == null) {
				argTypes = args.ToList().Select(s => s.GetType()).ToArray();
			}
			if (src.GetType().ToString() == "System.RuntimeType" || src.GetType().ToString() == "System.Type") {
				Type type = (Type)src;
				var method = type.GetTypeInfo().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, null, argTypes, null);
				if (method != null) {
					method.Invoke(null, args);
				}
			}
			else {
				var method = src.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, argTypes, null);
				if (method != null) {
					method.Invoke(src, args);
				}
			}
		} catch {}
	}

	// Methods for specific objects such as items, projectiles and NPCs
	public static T GetItemField<T>(Item item, string modName, string propName) {
		try {
			if (item.ModItem != null && item.ModItem.Mod.Name == modName) {
				return GetValue<T>(item.ModItem, propName);
			}
		} catch {}
		return default(T);
	}

	public static T GetProjectileField<T>(Projectile projectile, string modName, string propName) {
		try {
			if (projectile.ModProjectile != null && projectile.ModProjectile.Mod.Name == modName) {
				return GetValue<T>(projectile.ModProjectile, propName);
			}
		} catch {}
		return default(T);
	}

	public static T GetNPCField<T>(NPC npc, string modName, string propName) {
		try {
			if (npc.ModNPC != null && npc.ModNPC.Mod.Name == modName) {
				return GetValue<T>(npc.ModNPC, propName);
			}
		} catch {}
		return default(T);
	}
}

