using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace videoLibrary.Model
{
    public class Film : BaseModel
    {
        private ObservableCollection<Season> _seasons; 
        private string _name;
        private string _videoFile;
        private string _imageName;
        private string _details;
        private string _releaseDate;
        private string _synopsis;
        private bool _isSelected;

        public Film(string name, string imageName, string videoName, string details, List<Season> seasons )
        {
            Name = name;
            ImageName = imageName;
            VideoFile = videoName;
            Details = details;

            if (seasons != null)
                Seasons = new ObservableCollection<Season>(seasons);
        }

        public ObservableCollection<Season> Seasons
        {
            get { return _seasons; }
            set
            {
                _seasons = value;
                OnPropertyChanged();
            }
        } 

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string VideoFile
        {
            get { return _videoFile; }
            set
            {
                _videoFile = value;
                OnPropertyChanged();
            }
        }

        public string ImageName
        {
            get { return _imageName; }
            set
            {
                _imageName = value;
                OnPropertyChanged();
            }
        }

        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged();
            }
        }

        public string ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                OnPropertyChanged();
            }
        }

        public string Synopsis
        {
            get { return _synopsis; }
            set
            {
                _synopsis = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}
