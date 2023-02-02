using System;
using System.Collections.Generic;
using System.Reflection;

namespace DALib.Functions;

public static class FieldResolver {
	static Dictionary<Type, FieldInfo[]> fieldCache = new();
	public static string FindConstantName<T>(Type containingType, T value) {
		EqualityComparer<T> comparer = EqualityComparer<T>.Default;

		FieldInfo[] fields;
		if (!fieldCache.TryGetValue(containingType, out fields)) {
			fields = containingType.GetFields(BindingFlags.Static | BindingFlags.Public);
			fieldCache.TryAdd(containingType, fields);
		}

		foreach (FieldInfo field in fields) {
			if (field.FieldType == typeof(T) && comparer.Equals(value, (T) field.GetValue(null))) {
				return field.Name;
			}
		}
		return null;
	}
}
