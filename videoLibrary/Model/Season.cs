
namespace videoLibrary.Model
{
    public class Season : BaseModel
    {
        private string _name;
        private string _folderLocation;

        public Season(string name, string folderlocation)
        {
            Name = name;
            FolderLocation = folderlocation;
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
    }
}
