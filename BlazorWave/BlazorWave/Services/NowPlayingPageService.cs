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

        public bool IsPaused { get; set; } = false;
    
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
