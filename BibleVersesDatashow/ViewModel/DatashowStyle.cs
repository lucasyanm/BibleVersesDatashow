using CommunityToolkit.Mvvm.ComponentModel;

namespace BibleVersesDatashow.Model
{
    public partial class DatashowStyle : ObservableObject
    {
        private static DatashowStyle? _savedStyle;

        private DatashowStyle() 
        {
            FontSize = 60;
        }

        public static DatashowStyle GetDatashowStyle()
        {
            if(_savedStyle == null)
                _savedStyle = new DatashowStyle();

            return _savedStyle;
        }

        [ObservableProperty]
        int fontSize;
    }
}
