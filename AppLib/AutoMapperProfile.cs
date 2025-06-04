using System;
using AutoMapper;
using Wörter.AppData.Entities;
using Wörter.ViewModels;

namespace Wörter.AppLib;

public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Wort, WortVM>().ReverseMap();
	}
}
