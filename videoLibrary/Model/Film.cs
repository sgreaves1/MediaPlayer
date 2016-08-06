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
        private string _releaseDate;
        private string _synopsis;
        private double _rating;
        private bool _isSelected;

        public Film(string name, string imageName, string videoName,  List<Season> seasons )
        {
            Name = name;
            ImageName = imageName;
            VideoFile = videoName;

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

        public double Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
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

        public void ExtractDetailsFromJsonString(string jsonString)
        {
            if (jsonString.Contains("Released"))
            {
                var start = jsonString.IndexOf("Released") + 11;
                var match = jsonString.Substring(start);

                var released = match.Substring(0, match.IndexOf("\""));

                ReleaseDate = released;
            }

            if (jsonString.Contains("Plot"))
            {
                var start = jsonString.IndexOf("Plot") + 7;
                var match = jsonString.Substring(start);

                var plot = match.Substring(0, match.IndexOf("Language") - 3);

                var formatPlot = plot.Replace("\\", "");

                Synopsis = formatPlot;

            }

            if (jsonString.Contains("imdbRating"))
            {
                var start = jsonString.IndexOf("imdbRating") + 13;
                var match = jsonString.Substring(start);

                var rating = match.Substring(0, match.IndexOf("imdbVotes")-3);

                double rate = 0.0;
                double.TryParse(rating, out rate);

                Rating = rate;
            }
        }
    }
}
