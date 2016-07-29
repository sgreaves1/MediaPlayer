using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using LibrarySamples.Command;
using videoLibrary.Model;

namespace videoLibrary.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _mediaFolder;
        private ObservableCollection<Film> _films = new ObservableCollection<Film>();

        public MainWindowViewModel()
        {
            MediaFolder = "C:\\Users\\SamG\\Downloads\\Films";

            GetFilms();

            InitCommands();
        }

        public string MediaFolder
        {
            get
            {
                return _mediaFolder;
            }
            set
            {
                _mediaFolder = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Film> Films
        {
            get { return _films;}
            set
            {
                _films = value;
                OnPropertyChanged();
            }
        }

        public void GetFilms()
        {
            var directories = Directory.GetDirectories(MediaFolder);

            foreach (var dir in directories)
            {
                string name;
                string imageName = "";

                int pos = dir.LastIndexOf("\\") + 1;
                name = dir.Substring(pos, dir.Length - pos);

                var files = Directory.GetFiles(dir);

                foreach (var file in files)
                {
                    if (file.Contains(".jpg"))
                    {
                        imageName = file;
                    }
                }

                AddFilm(name, imageName);
                
            }
        }

        public void AddFilm(string name, string imageName)
        {
            Films.Add(new Film(name, imageName));
        }

        #region commands
        public ICommand FilmButtonCommand { get; set; }

        public void InitCommands()
        {
            FilmButtonCommand = new DelegateCommand(Execute, CanExecute);
        }

        private bool CanExecute(object sender)
        {
            return true;
        }

        private void Execute(object sender)
        {

        }

        #endregion
    }
}
