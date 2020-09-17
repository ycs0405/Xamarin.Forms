﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

#if UITEST
using Xamarin.UITest.Queries;
using NUnit.Framework;
#endif

namespace Xamarin.Forms.Controls.Issues
{
#if UITEST
	[NUnit.Framework.Category(Core.UITests.UITestCategories.Github5000)]
#endif
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 1455, "Context action are not changed when selected item changed on Android", PlatformAffected.Android)]
	public partial class Issue1455 : TestContentPage
	{
		public Issue1455()
		{
#if APP
			InitializeComponent();
#endif
		}

		MyViewModel ViewModel
		{
			get
			{
				return (MyViewModel)BindingContext;
			}
		}

		protected override void Init()
		{
			BindingContext = new MyViewModel();
		}


		[Preserve(AllMembers = true)]
		public class MyViewModel : PropertyChangedBase
		{
			bool _isContextActionsLegacyModeEnabled;

			public bool IsContextActionsLegacyModeEnabled
			{
				get
				{
					return _isContextActionsLegacyModeEnabled;
				}
				set
				{
					_isContextActionsLegacyModeEnabled = value;
					OnPropertyChanged();
				}
			}

			public ICommand ToggleLegacyMode { get; }

			public ObservableCollection<ContextMenuItem> Items { get; private set; }

			public MyViewModel()
			{
				IsContextActionsLegacyModeEnabled = false;

				ToggleLegacyMode = new Command(() => IsContextActionsLegacyModeEnabled = !IsContextActionsLegacyModeEnabled);

				Items = new ObservableCollection<ContextMenuItem>();

				Items.Add(new ContextMenuItem { Item1Text = "Lorem", Text = "Cell 1", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Ipsum", Text = "Cell 2", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Dictum", Text = "Cell 3", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Vestibulum", Text = "Cell 4", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Hendrerit", Text = "Cell 5", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Posuere", Text = "Cell 6", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Bibendum", Text = "Cell 7", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Vivamus", Text = "Cell 8", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Enim", Text = "Cell 9", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Quis", Text = "Cell 10", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Phasellus", Text = "Cell 11", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Pretium", Text = "Cell 12", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Aliquam", Text = "Cell 13", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Tristique", Text = "Cell 14", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Malesuada", Text = "Cell 15", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Dolor", Text = "Cell 16", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Id", Text = "Cell 17", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Orci", Text = "Cell 18", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Diam", Text = "Cell 19", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Nibh", Text = "Cell 20", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Non", Text = "Cell 21", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Aliquam", Text = "Cell 22", Type = ContextMenuItemType.Temp2 });
				Items.Add(new ContextMenuItem { Item1Text = "Ultrices", Text = "Cell 23", Type = ContextMenuItemType.Temp1 });
				Items.Add(new ContextMenuItem { Item2Text = "Vulputate", Text = "Cell 24", Type = ContextMenuItemType.Temp2 });
			}
		}

#if UITEST && __ANDROID__
		[Test]
		[Category(Core.UITests.UITestCategories.ListView)]
		public void RefreshContextActions()
		{
			RunningApp.WaitForElement("Cell 1");
			RunningApp.Screenshot("I am at Issue 1455");
			RunningApp.TouchAndHold(q => q.Marked("Cell 4"));
			RunningApp.Screenshot("Long Press Cell 4 to show context actions");
			RunningApp.Tap(q => q.Marked("Cell 5"));
			RunningApp.Screenshot("Clicked another cell and changed menu items");

			Assert.AreEqual(1, RunningApp.Query(c => c.Marked("Hendrerit")).Length);

			RunningApp.Back();

			RunningApp.WaitForElement("Toggle LegacyMode");
			RunningApp.Tap(q => q.Marked("Toggle LegacyMode"));

			RunningApp.TouchAndHold(q => q.Marked("Cell 4"));
			RunningApp.Screenshot("Long Press Cell 4 to show context actions");
			RunningApp.Tap(q => q.Marked("Cell 5"));
			RunningApp.Screenshot("Clicked another cell and changed menu items");

			Assert.AreEqual(1, RunningApp.Query(c => c.Marked("Vestibulum")).Length);
		}
#endif
	}

	public enum ContextMenuItemType
	{
		Temp1,
		Temp2
	}

	[Preserve(AllMembers = true)]
	public class ContextMenuItem
	{
		public ContextMenuItemType Type { get; set; } = ContextMenuItemType.Temp1;
		public string Item1Text { get; set; } = "Text 1";
		public string Item2Text { get; set; } = "Text 2";
		public string Item3Text { get; set; } = "Text 3";
		public string Item4Text { get; set; } = "Text 4";
		public string Text { get; set; }
	}

	[Preserve(AllMembers = true)]
	public class Issue1455DataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate Temp1Template { get; set; }
		public DataTemplate Temp2Template { get; set; }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			return ((ContextMenuItem)item).Type == ContextMenuItemType.Temp1 ? Temp1Template : Temp2Template;
		}
	}
}