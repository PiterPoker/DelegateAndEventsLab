namespace FileWalker.Models
{
    /// <summary>
    /// Provides event arguments for file processing events.  Contains information about a file and a cancellation flag.
    /// </summary>
    public class FileArgs : EventArgs
    {
        private string _path;
        private string _fullPath;
        private string _fileName;

        /// <summary>
        /// Gets or sets the full path of the file.  Setting this property also automatically extracts the filename.
        /// </summary>
        public string FullPath
        {
            get => _fullPath;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _fileName = value.Split(['\\', '/'])[^1]; //Gets the last element after splitting by '\' or '/'
                }
                _fullPath = value;
            }
        }

        /// <summary>
        /// Gets the name of the file (extracted from <see cref="FullPath"/>).
        /// </summary>
        public string FileName { get => _fileName; }

        /// <summary>
        /// Gets the search path.
        /// </summary>
        public string Path { get => _path; protected set => _path = value; }

        /// <summary>
        /// Gets or sets a flag indicating whether the file processing should be canceled.
        /// Handlers can set this to true to interrupt further processing.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileArgs"/> class.
        /// </summary>
        /// <param name="path">The initial search path.</param>
        /// <param name="cancel">An optional flag to initiate cancellation (defaults to false).</param>
        public FileArgs(string path, bool cancel = false)
        {
            _path = path;
            _fullPath = string.Empty;
            _fileName = string.Empty;
            Cancel = cancel;
        }
    }
}