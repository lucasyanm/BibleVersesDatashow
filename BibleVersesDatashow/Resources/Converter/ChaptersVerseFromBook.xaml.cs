using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;

namespace BibleVersesDatashow.Resources.Converter;

public partial class ChaptersVerseFromBook : ResourceDictionary, IMultiValueConverter
{
	public ChaptersVerseFromBook()
	{
		InitializeComponent();
	}

    public object? Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is List<List<string>> chapter
            && values[1] is int chapterNumber
            && values[2] is int verseNumber
            && chapter.Count >= chapterNumber
            && chapter[chapterNumber - 1].Count >= verseNumber)
        {
            return $"{verseNumber}. " + chapter[chapterNumber - 1][verseNumber - 1];
        }

        return string.Empty;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}