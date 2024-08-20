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

        [RelayCommand]
        public async void GetBook()
        {
            if (String.IsNullOrEmpty(AbbrevToSearch) && String.IsNullOrEmpty(NameToSearch))
            {
                throw new ArgumentNullException("Informe o nome ou abreviação do livro que queira obter");
            }

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
                                    BibleBook? book = JsonSerializer.Deserialize<BibleBook>(jsonElement);

                                    if (book != null)
                                    {
                                        CurrentBook = book;
                                    }
                                }
                            }
                            else if (jsonElement.TryGetProperty("abbrev", out JsonElement abbrevProperty))
                            {
                                string jsonElementAbbrev = abbrevProperty.ToString();
                                if (jsonElementAbbrev == AbbrevToSearch)
                                {
                                    BibleBook? book = JsonSerializer.Deserialize<BibleBook>
                                        (
                                            jsonElement.ToString(), 
                                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                                        );

                                    if (book != null)
                                    {
                                        CurrentBook = book;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            throw new Exception("Livro não encontrado");
        }
    }
}