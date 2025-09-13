using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Common.Extensions;

public static class ObjectExtensions
{
	private static readonly Dictionary<Type, string> TypeAliases = new Dictionary<Type, string>
	{
		{ typeof(bool), "bool" },
		{ typeof(byte), "byte" },
		{ typeof(sbyte), "sbyte" },
		{ typeof(char), "char" },
		{ typeof(decimal), "decimal" },
		{ typeof(double), "double" },
		{ typeof(float), "float" },
		{ typeof(int), "int" },
		{ typeof(uint), "uint" },
		{ typeof(long), "long" },
		{ typeof(ulong), "ulong" },
		{ typeof(object), "object" },
		{ typeof(short), "short" },
		{ typeof(ushort), "ushort" },
		{ typeof(string), "string" },
	};
	public static string AsJson<T>(this T obj)
	{
		return JsonConvert.SerializeObject(obj, Formatting.Indented,
			new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Include
			});
	}

	public static string AsString(this object? obj)
	{
		if (obj == null)
			return "null";

		if (obj is IEnumerable enumerable and not string)
		{
			return ConvertToString(enumerable, obj.GetType());
		}

		var objectType = obj.GetType();
		var sb = new StringBuilder();
		sb.Append(GetFriendlyTypeName(objectType)).Append(" { ");

		var properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		var fields = objectType.GetFields(BindingFlags.Public | BindingFlags.Instance);

		var members = properties.Concat(fields.Cast<MemberInfo>());

		bool firstMember = true;

		foreach (var member in members)
		{
			if (!firstMember)
			{
				sb.Append(", ");
			}
			firstMember = false;

			sb.Append(member.Name).Append(" = ");

			object? value = null;
			try
			{
				if (member is PropertyInfo property)
				{
					if (property.GetIndexParameters().Length > 0)
					{
						sb.Append("Indexed property");
						continue;
					}

					value = property.GetValue(obj);
				}
				else
				{
					value = ((FieldInfo)member).GetValue(obj);
				}
			}
			catch (Exception ex)
			{
				sb.Append($"<Error: {ex.Message}>");
				continue;
			}

			if (value == null)
			{
				sb.Append("null");
			}
			else if (value is string || value.GetType().IsPrimitive || value is decimal)
			{
				sb.Append(ConvertPrimitiveToString(value));
			}
			else
			{
				sb.Append(value.AsString());
			}
		}

		sb.Append(" }");
		return sb.ToString();
	}

	private static string ConvertPrimitiveToString(object value)
	{
		if (value == null)
			return "null";

		switch (value)
		{
			case string str:
				return $"\"{str}\"";
			case bool b:
				return b.ToString().ToLower();
			case byte b:
				return b.ToString();
			case sbyte sb:
				return sb.ToString();
			case char c:
				return $"\'{c}\'";
			case decimal d:
				return d.ToString();
			case double d:
				return d.ToString();
			case float f:
				return f.ToString();
			case int i:
				return i.ToString();
			case uint ui:
				return ui.ToString();
			case long l:
				return l.ToString();
			case ulong ul:
				return ul.ToString();
			case short s:
				return s.ToString();
			case ushort us:
				return us.ToString();
			default:
				return value.ToString()!;
		}
	}

	private static string ConvertToString(object? value, Type? containingType = null)
	{
		if (value == null)
			return "null";

		switch (value)
		{
			case IDictionary dictionary:
				{
					var dictSb = new StringBuilder();
					dictSb.Append("{ ");
					bool firstEntry = true;
					foreach (DictionaryEntry entry in dictionary)
					{
						if (!firstEntry)
						{
							dictSb.Append(", ");
						}
						firstEntry = false;
						dictSb.Append($"{ConvertToString(entry.Key)} = {ConvertToString(entry.Value)}");
					}
					dictSb.Append(" }");
					return $"{GetFriendlyTypeName(containingType ?? value.GetType())} {dictSb}";
				}
			case IEnumerable enumerable and not string:
				{
					var listSb = new StringBuilder();
					listSb.Append("[ ");
					bool firstItem = true;
					foreach (var item in enumerable)
					{
						if (!firstItem)
						{
							listSb.Append(", ");
						}
						firstItem = false;
						listSb.Append(ConvertToString(item));
					}
					listSb.Append(" ]");
					return $"{GetFriendlyTypeName(containingType ?? value.GetType())} {listSb}";
				}
			case string str:
				return $"\"{str}\"";
			default:
				// Use AsString for complex objects
				return value.AsString();
		}
	}

	private static string GetFriendlyTypeName(Type type)
	{
		if (TypeAliases.TryGetValue(type, out var alias))
		{
			return alias;
		}
		else if (type.IsGenericType)
		{
			var genericArguments = type.GetGenericArguments();
			var typeName = type.Name;
			var backtickIndex = typeName.IndexOf('`');
			if (backtickIndex > 0)
			{
				typeName = typeName.Substring(0, backtickIndex);
			}

			var genericArgumentNames = string.Join(", ", genericArguments.Select(GetFriendlyTypeName));

			return $"{typeName}<{genericArgumentNames}>";
		}
		else if (type.IsArray)
		{
			var elementType = type.GetElementType();
			return $"{GetFriendlyTypeName(elementType!)}[]";
		}
		else
		{
			return type.Name;
		}
	}
}