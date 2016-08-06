using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Windows.Input;
using LibrarySamples.Command;
using videoLibrary.Model;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Net;

namespace videoLibrary.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private List<string> _mediaFolder = new List<string>();
        private string _videoName;
        private string _videoTitle;
        private ObservableCollection<Film> _films = new ObservableCollection<Film>();
        private Film _selectedFilm;
        private bool _showVideo;
        private bool _showFilms;
        private bool _isPiOnline;
        private string _piIp;
        private string _piName;

        public MainWindowViewModel()
        {
            GetMediaFolders();

            GetFilms();

            InitCommands();

            ShowFilms = true;
            ShowVideo = false;
            
            PiIp = "192.168.1.5";
            IsPiOnline = CheckPing(PiIp);
            if (IsPiOnline)
            {
                PiName = GetMachineNameFromIp(PiIp);
            }
        }

        public List<string> MediaFolder
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

        /// <summary>
        /// The file path of the video
        /// </summary>
        public string VideoName
        {
            get
            {
                return _videoName;
            }
            set
            {
                _videoName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The title to display on the media control
        /// </summary>
        public string VideoTitle
        {
            get
            {
                return _videoTitle;
            }
            set
            {
                _videoTitle = value;
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

        public Film SelectedFilm
        {
            get { return _selectedFilm; }
            set
            {
                _selectedFilm = value;
                OnPropertyChanged();
            }
        }

        public bool ShowVideo
        {
            get { return _showVideo; }
            set
            {
                _showVideo = value;
                OnPropertyChanged();
            }
        }

        public bool ShowFilms
        {
            get { return _showFilms; }
            set
            {
                _showFilms = value;
                OnPropertyChanged();
            }
        }

        public bool IsPiOnline
        {
            get { return _isPiOnline; }
            set
            {
                _isPiOnline = value;
                OnPropertyChanged();
            }
        }

        public string PiIp
        {
            get { return _piIp; }
            set
            {
                _piIp = value;
                OnPropertyChanged();
            }
        }

        public string PiName
        {
            get { return _piName; }
            set
            {
                _piName = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler StopRequested;
        public event EventHandler PlayRequested;

        private void GetMediaFolders()
        {
            string[] folders = ReadSetting("MediaFolder");

            foreach (var folder in folders)
            {
                MediaFolder.Add(folder);
            }
        }

        static string[] ReadSetting(string key)
        {
            string[] result = new string[0];

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count == 0)
                {
                    // app settings empty
                }
                else
                {
                    string[] appSettingsKey = appSettings[key].Split(',');

                    if (appSettingsKey != null)
                    {
                        result = appSettingsKey;
                    }
                    else
                    {
                        // no app settings with that key
                    }

                }
            }
            catch (ConfigurationErrorsException)
            {
                // app settings not read properly
            }

            return result;
        }

        public void GetFilms()
        {
            foreach (string folder in MediaFolder)
            {
                if (Directory.Exists(folder))
                {
                    var directories = Directory.GetDirectories(folder);

                    foreach (var dir in directories)
                    {
                        string name;
                        string imageName = "";
                        string videoName = "";

                        int pos = dir.LastIndexOf("\\") + 1;
                        name = dir.Substring(pos, dir.Length - pos);

                        var files = Directory.GetFiles(dir);

                        foreach (var file in files)
                        {
                            if (file.Contains(".jpg"))
                            {
                                imageName = file;
                            }

                            if (file.Contains(".avi"))
                            {
                                videoName = file;
                            }
                        }

                        // Add all season folder names to the film class
                        List<Season> seasons = null;
                        var seasonFolder = Directory.GetDirectories(dir);
                        foreach (var seasonFolderName in seasonFolder)
                        {
                            if (seasons == null)
                                seasons = new List<Season>();

                            int position = seasonFolderName.LastIndexOf("\\") + 1;
                            string seasonName = seasonFolderName.Substring(position, seasonFolderName.Length - position);
                            seasons.Add(new Season(seasonName, seasonFolderName));
                        }
                        
                        AddFilm(name, imageName, videoName, seasons);
                    }
                }
            }
        }

        public void AddFilm(string name, string imageName, string videoName, List<Season> seasons )
        {
            if (string.IsNullOrEmpty(imageName))
                imageName = Directory.GetCurrentDirectory() + @"\Media\FileNotFound.jpg";

            Films.Add(new Film(name, imageName, videoName, seasons));
        }

        private bool CheckPing(string host)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(host, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);

                return true;
            }
            return false;
        }

        private static string GetMachineNameFromIp(string ipAddress)
        {
            string machineName = string.Empty;

            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                // Machine not found
            }

            return machineName;
        }

        #region commands
        public ICommand FilmButtonCommand { get; set; }
        public ICommand FilmSelectedCommand { get; set; }
        public ICommand XCommand { get; set; }

        public void InitCommands()
        {
            FilmButtonCommand = new DelegateCommand(ExecuteFilmButtonCommand, CanExecuteFilmButtonCommand);
            FilmSelectedCommand = new DelegateCommand(ExecuteFilmSelectedCommand, CanExecuteFilmSelectedCommand);
            XCommand = new DelegateCommand(ExecuteXCommand, CanExecuteXCommand);
        }
        
        private bool CanExecuteFilmSelectedCommand(object sender)
        {
            return true;
        }

        private void ExecuteFilmSelectedCommand(object sender)
        {
            SelectedFilm = (Film)((ToggleButton)sender).DataContext;

            foreach (var film in Films)
            {
                film.IsSelected = false;
            }

            SelectedFilm.IsSelected = true;
        }

        private bool CanExecuteFilmButtonCommand(object sender)
        {
            return true;
        }

        private void ExecuteFilmButtonCommand(object sender)
        {
            VideoName = ((Film) sender).VideoFile;
            VideoTitle = ((Film) sender).Name;


            ShowFilms = false;
            ShowVideo = true;

            PlayRequested?.Invoke(this, EventArgs.Empty);
        }

        private bool CanExecuteXCommand(object sender)
        {
            return true;
        }

        private void ExecuteXCommand(object sender)
        {
            ShowFilms = true;
            ShowVideo = false;

            StopRequested?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
