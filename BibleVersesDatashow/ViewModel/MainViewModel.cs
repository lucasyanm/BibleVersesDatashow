using BibleVersesDatashow.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        string abbrevToSearch;
        [ObservableProperty]
        string nameToSearch;
        [ObservableProperty]
        BibleBook currentBook;
        //TODO: Show pop up that book was not found
        [ObservableProperty]
        string? popUpErrorMessage;

        [RelayCommand]
        public async Task GetBook()
        {
            if (String.IsNullOrEmpty(AbbrevToSearch) && String.IsNullOrEmpty(NameToSearch))
            {
                PopUpErrorMessage = "Informe o nome ou abreviação do livro que queira obter";
                return;
            }

            bool notFound = true;

            using (Stream fileStream = await Microsoft.Maui.Storage.FileSystem.OpenAppPackageFileAsync("Resources/Bible/nvi.json"))
            {
                using (JsonDocument jsonDocument = JsonDocument.Parse(fileStream))
                {
                    JsonElement root = jsonDocument.RootElement;
                    if (root.ValueKind == JsonValueKind.Array)
                    {
                        foreach (JsonElement jsonElement in root.EnumerateArray())
                        {
                            if (!String.IsNullOrEmpty(NameToSearch)
                                && jsonElement.TryGetProperty("name", out JsonElement nameProperty))
                            {
                                string jsonElementName = nameProperty.ToString();
                                if (jsonElementName == NameToSearch)
                                {
                                    notFound = UpdateCurrentBook(jsonElement);
                                    break;
                                }
                            }
                            else if (jsonElement.TryGetProperty("abbrev", out JsonElement abbrevProperty))
                            {
                                string jsonElementAbbrev = abbrevProperty.ToString();
                                if (jsonElementAbbrev == AbbrevToSearch)
                                {
                                    notFound = UpdateCurrentBook(jsonElement);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (notFound)
            {
                PopUpErrorMessage = "Livro não encontrado";
            }
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
    }
}