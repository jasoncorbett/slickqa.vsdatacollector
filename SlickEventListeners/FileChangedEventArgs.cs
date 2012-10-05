namespace SlickEventListeners
{
	public enum FileChangedReason
	{
		Changed,
		Added,
		Removed
	}

	public class FileChangedEventArgs
	{
		public FileChangedReason Changed { get; private set; }
		public string FullPath { get; private set; }

		public FileChangedEventArgs(string fullPath, FileChangedReason changed)
		{
			FullPath = fullPath;
			Changed = changed;
		}
	}
}