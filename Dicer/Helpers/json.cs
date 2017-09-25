using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Dicer
{
	/// <summary>
	/// Used for converting from the json data received by site
	/// Code copied from http://www.codeproject.com/Articles/272335/JSON-Serialization-and-Deserialization-in-ASP-NET
	/// </summary>
	class json
	{
		public static T JsonDeserialize<T>(string jsonString)
		{
			try
			{
                return JsonConvert.DeserializeObject<T>(jsonString);
			}
			catch (Exception E)
			{


				throw E;
			}
		}

		public static string JsonSerializer<T>(T t)
		{
            return JsonConvert.SerializeObject(t);
		}
		public static string ToDateString(DateTime Value)
		{

			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			TimeSpan dt = Value - DateTime.Parse("1970/01/01 00:00:00", System.Globalization.DateTimeFormatInfo.InvariantInfo);
			decimal mili = (decimal)dt.TotalMilliseconds;
			return ((long)mili).ToString();

		}

		public static DateTime ToDateTime2(string milliseconds)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			try
			{
				DateTime dotNetDate = new DateTime(1970, 1, 1);
				dotNetDate = dotNetDate.AddMilliseconds(long.Parse(milliseconds));
				if (dotNetDate.Year < 1972)
				{
					dotNetDate = new DateTime(1970, 1, 1);
					dotNetDate = dotNetDate.AddSeconds(long.Parse(milliseconds));
				}
				return dotNetDate;
			}
			catch
			{
				try
				{
					string s = milliseconds.ToLower().Replace("z", " ").Replace("t", " ");
					DateTime dotNetDate = DateTime.Parse(s, System.Globalization.DateTimeFormatInfo.InvariantInfo);
					return dotNetDate;
				}
				catch
				{
					return new DateTime();
				}
			}
		}

	}
}
