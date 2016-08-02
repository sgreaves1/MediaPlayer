using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using LibrarySamples.Command;
using videoLibrary.Model;
using System.Net.NetworkInformation;
using System.Text;

namespace videoLibrary.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private List<string> _mediaFolder = new List<string>();
        private string _videoName;
        private string _videoTitle;
        private ObservableCollection<Film> _films = new ObservableCollection<Film>();
        private bool _showVideo;
        private bool _showFilms;
        private bool _isPiOnline;

        public MainWindowViewModel()
        {
            //nN34iy9ie9
            MediaFolder.Add("C:\\Users\\SamG\\Downloads\\Films");
            MediaFolder.Add("C:\\Users\\Sam\\Downloads\\Films");
            MediaFolder.Add(@"\\RASPBERRYPI\Films\Films");

            GetFilms();

            InitCommands();

            ShowFilms = true;
            ShowVideo = false;

            //IsPiOnline = CheckPing("192.168.1.5");
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

        public event EventHandler StopRequested;
        public event EventHandler PlayRequested;

        public void GetFilms()
        {
            if (Directory.Exists(MediaFolder[0]))
            {
                var directories = Directory.GetDirectories(MediaFolder[0]);

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

                    AddFilm(name, imageName, videoName);

                }
            }
        }

        public void AddFilm(string name, string imageName, string videoName)
        {
            if (string.IsNullOrEmpty(imageName))
                imageName = Directory.GetCurrentDirectory() + @"\Media\FileNotFound.jpg";

            Films.Add(new Film(name, imageName, videoName));
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

        #region commands
        public ICommand FilmButtonCommand { get; set; }
        public ICommand XCommand { get; set; }

        public void InitCommands()
        {
            FilmButtonCommand = new DelegateCommand(ExecuteFilmButtonCommand, CanExecuteFilmButtonCommand);
            XCommand = new DelegateCommand(ExecuteXCommand, CanExecuteXCommand);
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
