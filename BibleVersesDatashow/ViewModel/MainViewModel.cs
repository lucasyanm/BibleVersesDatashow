using BibleVersesDatashow.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Text.Json;

namespace BibleVersesDatashow.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public SlideshowStyle datashowStyleData;
        public MainViewModel()
        {
            DatashowStyleData = SlideshowStyle.GetDatashowStyle();
            FontSize = DatashowStyleData.FontSize;
        }

        [ObservableProperty]
        int fontSize;

        [RelayCommand]
        public void SaveStyle()
        {
            if (FontSize <= 0)
            {
                FontSize = 1;
                return;
            }

            DatashowStyleData.FontSize = FontSize;
        }

        [ObservableProperty]
        string? abbrevOrNameToSearch;

        [ObservableProperty]
        BibleBook? currentBook;

        [ObservableProperty]
        /// Always start to count as 1 and goes to chapters[currentChapter].count
        int? currentVerse;

        [ObservableProperty]
        //TODO: only update on slideshow page after click on search
        /// Always start to count as 1 and goes to chapters.count
        int currentChapter;

        [ObservableProperty]
        //TODO: Show pop up that book was not found
        string? popUpErrorMessage;

        [RelayCommand]
        public async Task GetBook()
        {
            if (String.IsNullOrEmpty(AbbrevOrNameToSearch))
            {
                PopUpErrorMessage = "Informe o nome ou abreviação do capítulo que queira exibir";
                return;
            }

            bool bookFound = false;
            string abbrevOrName = String.Empty;

            string[] searchSplitted = AbbrevOrNameToSearch.Trim().Split(" ");

            if(searchSplitted.Length > 1
                && int.TryParse(searchSplitted[searchSplitted.Length - 1], out int chapter))
            {
                CurrentChapter = chapter;
                abbrevOrName = String.Join(" ", searchSplitted, 0, searchSplitted.Length - 1);
            }
            else
            {
                abbrevOrName = AbbrevOrNameToSearch.Trim();
                CurrentChapter = 1;
            }

            using (Stream fileStream = await Microsoft.Maui.Storage.FileSystem.OpenAppPackageFileAsync("Resources/Bible/nvi.json"))
            {
                using (JsonDocument jsonDocument = JsonDocument.Parse(fileStream))
                {
                    JsonElement root = jsonDocument.RootElement;
                    if (root.ValueKind == JsonValueKind.Array)
                    {
                        foreach (JsonElement bookJsonElement in root.EnumerateArray())
                        {
                            if (bookJsonElement.TryGetProperty("abbrev", out JsonElement abbrevProperty))
                            {
                                if (String.Compare
                                        (
                                            abbrevProperty.ToString(),
                                            abbrevOrName,
                                            CultureInfo.CurrentCulture,
                                            CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase
                                        ) == 0)
                                {
                                    bookFound = UpdateCurrentBook(bookJsonElement);
                                    break;
                                }
                                else if(bookJsonElement.TryGetProperty("name", out JsonElement nameProperty))
                                {
                                    if(String.Compare
                                        (
                                            nameProperty.ToString(),
                                            abbrevOrName, 
                                            CultureInfo.CurrentCulture, 
                                            CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase
                                        ) == 0)
                                    {
                                        bookFound = UpdateCurrentBook(bookJsonElement);
                                        break;
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
            if (bookFound)
            {
                CurrentChapter = CurrentChapter > 0 && CurrentChapter <= CurrentBook?.chapters.Count ? CurrentChapter : 1;
                CurrentVerse = CurrentVerse > 0 && CurrentVerse <= CurrentBook?.chapters[CurrentChapter - 1].Count ? CurrentVerse : 1;
            }
            else
            {
                PopUpErrorMessage = "Livro não encontrado";
                return;
            }

            OpenSlideshow();
        }

        private bool UpdateCurrentBook(JsonElement newBook)
        {
            BibleBook? book = JsonSerializer.Deserialize<BibleBook>
                                (
                                    newBook.ToString(),
                                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                                );

            if (book != null)
            {
                CurrentBook = book;
                return true;
            }

            return false;
        }

        private void OpenSlideshow()
        {
            if(Application.Current?.Windows.FirstOrDefault(w => w.Page?.GetType() == typeof(Slideshow)) == null)
            {
                Window slideshowWindow = new Window(new Slideshow(this));
                Application.Current?.OpenWindow(slideshowWindow);
            }            
        }

        //TODO: Avançar ou voltar o livro quando chegar no final/começo
        [RelayCommand]
        public void PreviousVerse()
        {
            if(CurrentVerse > 1)
                CurrentVerse--;
            else if(CurrentChapter > 1)
            {
                CurrentChapter--;
                CurrentVerse = CurrentBook?.chapters[CurrentChapter - 1].Count;
            }
        }
        [RelayCommand]
        public void NextVerse()
        {
            if (CurrentVerse < CurrentBook?.chapters[CurrentChapter - 1].Count)
                CurrentVerse++;
            else if(CurrentChapter < CurrentBook?.chapters.Count)
            {
                CurrentChapter++;
                CurrentVerse = 1;
            }
        }
        [RelayCommand]
        public void IncreaseFontScale()
        {
            FontSize+=2;
            SaveStyle();
        }
        [RelayCommand]
        public void DecreaseFontScale()
        {
            FontSize-=2;
            SaveStyle();
        }
    }
}