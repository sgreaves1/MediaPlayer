
using System.Collections.ObjectModel;

namespace videoLibrary.Model
{
    public class Season : BaseModel
    {
        private ObservableCollection<Film> _episodes = new ObservableCollection<Film>(); 
        private string _name;
        private string _folderLocation;
        private bool _isSelected;
        
        public Season(string name, string folderlocation)
        {
            Name = name;
            FolderLocation = folderlocation;
        }

        public ObservableCollection<Film> Episodes
        {
            get { return _episodes; }
            set
            {
                _episodes = value;
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

        public string FolderLocation
        {
            get { return _folderLocation; }
            set
            {
                _folderLocation = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Weather or not this season is selected in details section
        /// </summary>
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
