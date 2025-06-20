using System;

namespace Wörter.AppLib;

public static class ListExtensions
{
	private static Random rng = new Random();

	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1); // Random index
			(list[k], list[n]) = (list[n], list[k]); // Swap
		}
	}
}
