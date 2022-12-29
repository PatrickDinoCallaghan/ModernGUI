using ModernGUI.Properties;
using System.Runtime.InteropServices;

namespace ModernGUI
{
    /// <summary>
    /// This instantiate additional cursors as and when they are required in one neat easy to use class.
    /// </summary>
    public class MoreCursors
    {
        public Dictionary<Name, Cursor> _LoadedCursors;

        public MoreCursors()
        {
            _LoadedCursors = new Dictionary<Name, Cursor>();
        }
        public enum Name
        {
            Hand,
            Tracker,
            ZoomIn32,
            ZoomOut32
        }

        /// <summary>
        /// Load colored cursor handle from a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "LoadCursorFromFileW", CharSet = CharSet.Unicode)]
        static private extern IntPtr LoadCursorFromFile(string fileName);

        /// <summary>
        /// Create cursor from embedded cursor
        /// </summary>
        /// <param name="cursorResourceName">embedded cursor resource name</param>
        /// <returns>cursor</returns>
        private Cursor CreateCursorFromFileName(String cursorResourceName)
        {
            // read cursor resource binary data
            Stream inputStream = GetEmbeddedResourceStream(cursorResourceName);
            byte[] buffer = new byte[inputStream.Length];
            inputStream.Read(buffer, 0, buffer.Length);
            inputStream.Close();

            // create temporary cursor file
            String tmpFileName = System.IO.Path.GetRandomFileName();
            FileInfo tempFileInfo = new FileInfo(tmpFileName);
            FileStream outputStream = tempFileInfo.Create();
            outputStream.Write(buffer, 0, buffer.Length);
            outputStream.Close();

            // create cursor
            IntPtr cursorHandle = LoadCursorFromFile(tmpFileName);
            Cursor cursor = new Cursor(cursorHandle);

            tempFileInfo.Delete();  // delete temporary cursor file
            return cursor;
        }

        private Cursor CreateCursorFromFile(byte[] resourceFileByteArray)
        {
            // read cursor resource binary data
            Stream inputStream = new MemoryStream(resourceFileByteArray);
            byte[] buffer = new byte[inputStream.Length];
            inputStream.Read(buffer, 0, buffer.Length);
            inputStream.Close();

            // create temporary cursor file
            String tmpFileName = System.IO.Path.GetRandomFileName();
            FileInfo tempFileInfo = new FileInfo(tmpFileName);
            FileStream outputStream = tempFileInfo.Create();
            outputStream.Write(buffer, 0, buffer.Length);
            outputStream.Close();

            // create cursor
            IntPtr cursorHandle = LoadCursorFromFile(tmpFileName);
            Cursor cursor = new Cursor(cursorHandle);

            tempFileInfo.Delete();  // delete temporary cursor file
            return cursor;
        }

        /// <summary>
        /// Get embedded resource stream
        /// </summary>
        /// <param name="resourceName">resource name</param>
        /// <returns>the stream of embedded resource</returns>
        private Stream GetEmbeddedResourceStream(string resourceName)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }

        public Cursor this[Name name]
        {
            get
            {
                if ((_LoadedCursors.ContainsKey(name)))
                {
                    return _LoadedCursors[name];
                }

                switch (name)
                {
                    case Name.Hand:
                        _LoadedCursors.Add(name, CreateCursorFromFile(Resources.Hand));
                        return _LoadedCursors[name];
                    case Name.Tracker:
                        _LoadedCursors.Add(name, CreateCursorFromFile(Resources.Tracker));
                        return _LoadedCursors[name];
                    case Name.ZoomIn32:
                        _LoadedCursors.Add(name, CreateCursorFromFile(Resources.ZoomIn32));
                        return _LoadedCursors[name];
                    case Name.ZoomOut32:
                        _LoadedCursors.Add(name, CreateCursorFromFile(Resources.ZoomOut32));
                        return _LoadedCursors[name];
                    default:
                        return System.Windows.Forms.Cursors.Default;
                }

            }
        }

    }
}
