using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace y0m.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabPage : TabbedPage
    {
        public MainTabPage()
        {
            InitializeComponent();

            // HomePage
            Children.Add(CreateTab(new HomePage(), "housesolid.png", "AnaSayfa"));

            //SearchPage
            Children.Add(CreateTab(new SearchPage(), "magnifyingglasssolid.png", "Ara"));

            //listPage
            Children.Add(CreateTab(new ListPage(), "listsolid.png", "Liste"));

            // AboutPage
            Children.Add(CreateTab(new AboutPage(), "circleinfosolid.png", "Hakkında"));

            // CurrentPageChanged olayını işlemek için işleyici ekleyin
            CurrentPageChanged += OnCurrentPageChanged;
        }

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            // Seçili sayfanın rengini beyaz olarak ayarlayın
            if (CurrentPage is NavigationPage selectedNavigationPage)
            {
                selectedNavigationPage.BarBackgroundColor = Color.FromHex("#507DBC");
                selectedNavigationPage.BarTextColor = Color.White;
            }
        } 
        private Page CreateTab(Page page, string icon, string title)
        {
            var navigationPage = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#507DBC"),
                Title = title,
                IconImageSource = icon
            };

            return navigationPage;
        } 
    }
}