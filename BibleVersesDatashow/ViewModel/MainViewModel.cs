using BibleVersesDatashow.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleVersesDatashow.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private DatashowStyle _style;
        public MainViewModel()
        {
            _style = DatashowStyle.GetDatashowStyle();
        }

        [ObservableProperty]
        int fontSize;

        [RelayCommand]
        public void SaveStyle()
        {
            if (FontSize <= 0) return;

            Console.WriteLine(FontSize);
            _style.FontSize = FontSize;
        }
    }
}