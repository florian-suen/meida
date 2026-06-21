using System;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

// Required for AssetLoader

namespace Meida.Desktop.ViewModels;

public class SongsViewModel : ViewModelBase
{
    public SongsViewModel()
    {
        Songs =
        [
            new Song
            {
                Name = "When I Come Around",
                Album = "Dookie",
                Source = LoadBitmap("avares://Meida.Desktop/Assets/Songs/dookie.jpg")
            },
            new Song
            {
                Name = "Back In Black",
                Album = "Back In Black",
                Source = LoadBitmap("avares://Meida.Desktop/Assets/Songs/backinblack.jpg")
            },
            new Song
            {
                Name = "Nightrain",
                Album = "Appetite for Destruction",
                Source = LoadBitmap("avares://Meida.Desktop/Assets/Songs/appetitefordestruction.jpg")
            },
            new Song
            {
                Name = "Come As You Are",
                Album = "Nevermind",
                Source = LoadBitmap("avares://Meida.Desktop/Assets/Songs/nevermind.jpg")
            },
            new Song
            {
                Name = "Owner of a Lonely Heart",
                Album = "90125",
                Source = LoadBitmap("avares://Meida.Desktop/Assets/Songs/90125.jpg")
            },
            new Song
            {
                Name = "The Boys Are Back in Town",
                Album = "Jailbreak",
                Source = LoadBitmap("avares://Meida.Desktop/Assets/Songs/jailbreak.jpg")
            }
        ];
    }

    public ObservableCollection<Song> Songs { get; set; }

    // Helper method to resolve avares:// URIs into Bitmaps
    private Bitmap LoadBitmap(string uriString)
    {
        var uri = new Uri(uriString);
        using var stream = AssetLoader.Open(uri);
        return new Bitmap(stream);
    }
}

public class Song
{
    public string Name { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public Bitmap? Source { get; set; }
}