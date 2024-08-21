using CommunityToolkit.Mvvm.ComponentModel;

namespace BibleVersesDatashow.Model
{
    public partial class SlideshowStyle : ObservableObject
    {
        private static SlideshowStyle? _savedStyle;

        private SlideshowStyle() 
        {
            FontSize = 60;
        }

        public static SlideshowStyle GetDatashowStyle()
        {
            if(_savedStyle == null)
                _savedStyle = new SlideshowStyle();

            return _savedStyle;
        }

        [ObservableProperty]
        int fontSize;
    }
}
