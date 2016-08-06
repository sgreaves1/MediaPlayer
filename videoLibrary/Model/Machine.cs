
namespace videoLibrary.Model
{
    public class Machine : BaseModel
    {
        private string _ip;
        private string _machineName;
        private bool _isOnline;

        public string Ip
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged();
            }
        }

        public string MachineName
        {
            get { return _machineName; }
            set
            {
                _machineName = value;
                OnPropertyChanged();
            }
        }

        public bool IsOnline
        {
            get { return _isOnline; }
            set
            {
                _isOnline = value;
                OnPropertyChanged();
            }
        }
    }
}
