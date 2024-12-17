namespace FileWalker.Models
{
    /// <summary>
    /// Provides event arguments specifically for the file search process.  Inherits from <see cref="FileArgs"/> and adds properties related to directory traversal.
    /// </summary>
    public class FileSearcherArgs : FileArgs
    {
        private string _oldDirectory;
        private string _currentDirectory;

        /// <summary>
        /// Gets or sets the current directory being searched. Setting this updates the base <see cref="FileArgs.Path"/> property and stores the previous directory.
        /// </summary>
        public string CurrentDirectory
        {
            get => _currentDirectory;
            set
            {
                _oldDirectory = _currentDirectory;
                Path = _currentDirectory = value; //Updates the base Path property
            }
        }

        /// <summary>
        /// Gets the previous directory that was being searched.
        /// </summary>
        public string OldDirectory { get => _oldDirectory; }

        /// <summary>
        /// Gets the file search pattern (e.g., "*.txt").
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSearcherArgs"/> class.
        /// </summary>
        /// <param name="path">The initial search path.</param>
        /// <param name="fileName">The file search pattern.</param>
        /// <param name="cancel">An optional flag to initiate cancellation (defaults to false).</param>
        public FileSearcherArgs(string path, string fileName, bool cancel = false)
            : base(path, cancel)
        {
            _currentDirectory = path;
            _oldDirectory = path;
            Pattern = fileName;
        }

    }
}