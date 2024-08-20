using BibleVersesDatashow.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

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

        [ObservableProperty]
        string abbrevOrNameToSearch;
        [ObservableProperty]
        BibleBook currentBook;
        [ObservableProperty]
        int? verseToStart;
        //TODO: Show pop up that book was not found
        [ObservableProperty]
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

            using (Stream fileStream = await Microsoft.Maui.Storage.FileSystem.OpenAppPackageFileAsync("Resources/Bible/nvi.json"))
            {
                using (JsonDocument jsonDocument = JsonDocument.Parse(fileStream))
                {
                    JsonElement root = jsonDocument.RootElement;
                    if (root.ValueKind == JsonValueKind.Array)
                    {
                        foreach (JsonElement jsonElement in root.EnumerateArray())
                        {
                            if (jsonElement.TryGetProperty("abbrev", out JsonElement abbrevProperty))
                            {
                                string jsonElementAbbrev = abbrevProperty.ToString();
                                if (String.Compare
                                        (
                                            jsonElementAbbrev,
                                            AbbrevOrNameToSearch,
                                            CultureInfo.CurrentCulture,
                                            CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase
                                        ) == 0)
                                {
                                    bookFound = UpdateCurrentBook(jsonElement);
                                    break;
                                }
                                else if(jsonElement.TryGetProperty("name", out JsonElement nameProperty))
                                {
                                    string jsonElementName = nameProperty.ToString();
                                    if(String.Compare
                                        (
                                            jsonElementName, 
                                            AbbrevOrNameToSearch, 
                                            CultureInfo.CurrentCulture, 
                                            CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase
                                        ) == 0)
                                    {
                                        bookFound = UpdateCurrentBook(jsonElement);
                                        break;
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
            if (!bookFound)
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
            Window slideshowWindow = new Window(new Slideshow());
            Application.Current?.OpenWindow(slideshowWindow);
        }
    }
}