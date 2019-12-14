using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorWave.Services
{
    public class NowPlayingPageService : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool isPaused { get; set; } = false;
        public bool IsPaused 
        { 
            get => isPaused;
            set
            {
                isPaused = value;
                OnPropertyChanged();
            } 
        }
    
        private string currentAlbumId;

        public string CurrentAlbumId
        {
            get => currentAlbumId;
            set
            {
                currentAlbumId = value;
                OnPropertyChanged();
            }
        }

        private int currentTrackIndex;

        public int CurrentTrackIndex
        {
            get => currentTrackIndex;
            set
            {
                currentTrackIndex = value;
                OnPropertyChanged();
            }
        }
    }
}
