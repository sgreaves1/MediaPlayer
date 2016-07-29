namespace videoLibrary.Model
{
    public class Film : BaseModel
    {
        private string _name;
        private string _videoFile;
        private string _imageName;

        public Film(string name, string imageName)
        {
            Name = name;
            ImageName = imageName;
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
    }
}
