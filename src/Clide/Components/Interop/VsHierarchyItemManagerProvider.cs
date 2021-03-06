﻿using System;
using System.ComponentModel.Composition;
using Merq;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;

namespace Clide.Components.Interop
{
	[PartCreationPolicy(CreationPolicy.Shared)]
	internal class VsHierarchyItemManagerProvider
	{
		Lazy<IVsHierarchyItemManager> hierarchyManager;

		[ImportingConstructor]
		public VsHierarchyItemManagerProvider ([Import (typeof (SVsServiceProvider))] IServiceProvider services, IAsyncManager async)
		{
			hierarchyManager = new Lazy<IVsHierarchyItemManager> (() => async.Run (async () => {
				await async.SwitchToMainThread ();
				return services.GetService<SComponentModel, IComponentModel> ().GetService<IVsHierarchyItemManager> ();
			}));
		}

		[Export(ContractNames.Interop.IVsHierarchyItemManager)]
		public IVsHierarchyItemManager HierarchyManager => hierarchyManager.Value;
	}
}
