using Xamarin.Forms;

namespace NavTabs
{
	public partial class NavTabsPage : ContentPage
	{
		public NavTabsPage()
		{
			InitializeComponent();

			if (Device.OS == TargetPlatform.iOS)
				Padding = new Thickness(0, 20, 0, 0);

			tabLayout.AddTab("Von mir");
			tabLayout.AddTab("Für mich");
		}
	}
}
