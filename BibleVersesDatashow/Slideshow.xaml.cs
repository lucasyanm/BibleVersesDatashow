using BibleVersesDatashow.ViewModel;

namespace BibleVersesDatashow
{
    public partial class Slideshow : ContentPage
    {
        public Slideshow(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}