using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleVersesDatashow.Model
{
    public class DatashowStyle
    {
        private static DatashowStyle? _savedStyle;

        private DatashowStyle() { }

        public static DatashowStyle GetDatashowStyle()
        {
            if(_savedStyle == null)
                _savedStyle = new DatashowStyle();

            return _savedStyle;
        }

        public int FontSize { get; set; }
    }
}
