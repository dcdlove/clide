﻿using Xunit;

namespace Clide
{
	public static class GlobalServiceLocator
	{
		public static IServiceLocator Instance { get; }

		static GlobalServiceLocator()
		{
			Instance = new ServiceLocator (GlobalServices.Instance);
		}
	}
}
