using System;
using AutoMapper;
using Wörter.AppData.Entities;
using Wörter.ViewModels;

namespace Wörter.AppLib;

public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{		
		CreateMap<Wort, WortVM>().BeforeMap((src, dest) =>
		{
			foreach (var prop in typeof(WortVM).GetProperties())
			{
				if (prop.PropertyType == typeof(string) && prop.CanWrite)
				{
					var val = prop.GetValue(src) as string;
					if (val != null) prop.SetValue(src, val.Trim());
				}
			}
		}).ReverseMap();

		CreateMap<Notiz, NotizVM>().ReverseMap();
	}
}
