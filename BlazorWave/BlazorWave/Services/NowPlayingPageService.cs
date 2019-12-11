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

        private string nowPlaying;

        public string NowPlaying
        {
            get => nowPlaying;
            set
            {
                nowPlaying = value;
                OnPropertyChanged();
            }
        }
    }
}
