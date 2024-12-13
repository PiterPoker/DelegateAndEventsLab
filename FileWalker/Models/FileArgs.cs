namespace FileWalker.Models
{
    public class FileSearcherArgs : FileArgs
    {
        private string _oldDirectory;
        private string _currentDirectory;
        /// <summary>
        /// Новая дерриктория поиска
        /// </summary>
        public string CurrentDirectory 
        { 
            get=> _currentDirectory; 
            set 
            {
                _oldDirectory = _currentDirectory;
                _path = _currentDirectory = value;
            } 
         }
        /// <summary>
        /// Старая дерриктория поиска
        /// </summary>
        public string OldDirectory { get => _oldDirectory; }

        public FileSearcherArgs(string path, string fileName, bool cancel = false)
            : base(path, fileName, cancel)
        {
            _currentDirectory = path;
            _oldDirectory = path;
        }
        
    }
    public class FileArgs : EventArgs
    {
        protected string _path;
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// Путь поиска
        /// </summary>
        public string Path { get=>_path; protected set=> _path = value; }
        /// <summary>
        /// Флаг отмены, чтобы обработчики могли прервать дальнейший поиск.
        /// </summary>
        public bool Cancel { get; set;}

        public FileArgs(string path, string fileName, bool cancel = false)
        {
            _path = path;
            FileName = fileName;
            Cancel = cancel;
        }
    }
}