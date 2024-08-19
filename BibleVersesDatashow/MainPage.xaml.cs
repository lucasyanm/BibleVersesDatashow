using BibleVersesDatashow.ViewModel;

namespace BibleVersesDatashow
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
