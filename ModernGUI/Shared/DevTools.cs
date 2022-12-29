using System.Diagnostics;
using System.Reflection;

namespace ModernGUI.Shared
{
    public static class DevTools
    {
        public static class Wifi
        {
            /// <summary>
            /// Shows all wifi details including password. 
            /// </summary>
            /// <param name="wifiname">Name of wifi</param>
            /// <returns></returns>
            public static string ConnectedWifiDetails(string wifiname)
            {
                // netsh wlan show profile name=* key=clear
                string argument = "wlan show profile name=\"" + wifiname + "\" key=clear";
                Process processWifi = new Process();
                processWifi.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processWifi.StartInfo.FileName = "netsh";
                processWifi.StartInfo.Arguments = argument;
                processWifi.StartInfo.UseShellExecute = false;
                processWifi.StartInfo.RedirectStandardError = true;
                processWifi.StartInfo.RedirectStandardInput = true;
                processWifi.StartInfo.RedirectStandardOutput = true;
                processWifi.StartInfo.CreateNoWindow = true;
                processWifi.Start();
                //* Read the output (or the error)
                string output = processWifi.StandardOutput.ReadToEnd();
                // Show output commands
                string err = processWifi.StandardError.ReadToEnd();
                // show error commands
                processWifi.WaitForExit();
                return output;
            }
        }

        /// <summary>
        /// This will reset the form position so that it exists clearly within the screen.
        /// </summary>
        /// <param name="form">Form we want to check and reposition</param>
        /// <param name="RefPoint">This is the reference point. the screen in which this point exists will be where we adjust the form to fit within</param>
        public static void ClosestFormScreenCorrection(Form form, Point RefPoint)
        {
            Screen[] screens = Screen.AllScreens;

            foreach (Screen screen in screens)
            {
                if (screen.Bounds.Contains(RefPoint)) // this makes sure it refferencces the screen that where the home instances exists m
                {
                    if (form.Left < screen.Bounds.X)
                    {
                        form.Left = screen.Bounds.X;
                    }
                    if (form.Left + form.Width > screen.Bounds.Width + screen.Bounds.X)
                    {
                        form.Left = screen.Bounds.X + screen.Bounds.Width - form.Width;
                    }
                    if (form.Top < screen.Bounds.Top)
                    {
                        form.Top = screen.Bounds.Y;
                    }
                    if (form.Top + form.Height > screen.Bounds.Height + screen.Bounds.Y)
                    {
                        form.Top = screen.Bounds.Y + screen.Bounds.Height - form.Height;
                    }
                }
            }

        }

        public static byte[] ObjectToByteArray(string fileName)
        {
            return File.ReadAllBytes(fileName);
        }

        public static string ByteArrayToBase64String(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static void SaveByteArrayToObject(byte[] DataBytes)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Save file";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
            {
                MessageBox.Show("Export cancelled by user", "Export cancelled.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string path = saveFileDialog1.FileName;

            try
            {
                File.WriteAllBytes(path, DataBytes);
            }
            catch (Exception)
            {
                MessageBox.Show("The following file is currently being used by another process and cannot be written too; \n" + path, "Export failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        public static void SaveBase64StringToObject(string ExcelBase54String)
        {
            byte[] DataBytes = Convert.FromBase64String(ExcelBase54String);
            SaveByteArrayToObject(DataBytes);
        }

        public static void FileToBase64StringTextFile()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            String FileName = openFileDialog.FileName;

            byte[] bytedata = ObjectToByteArray(FileName);

            string base64stringOfFile = ByteArrayToBase64String(bytedata);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Save file";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
            {
                MessageBox.Show("Export cancelled by user", "Export cancelled.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string path = saveFileDialog1.FileName;

            TextWriter txt = new StreamWriter(path);
            txt.Write(base64stringOfFile);
            txt.Close();


        }

        public static void ErrorLog(Exception ex, string body = "")
        {
            if (body != "")
            {
                body = body + "\n \n";
            }

            System.Windows.Forms.MessageBox.Show(body + "\n\n" + "Error message:  " + ex.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
