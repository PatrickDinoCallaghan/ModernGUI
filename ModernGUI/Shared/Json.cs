using Newtonsoft.Json;

namespace ModernGUI.Shared
{
    public static class JSon
    {
        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            catch
            {
                Console.WriteLine("Something went wrong");
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public static void WriteToJsonFileDialoge<T>(T objectToWrite) where T : new()
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
            else
            {
                string path = saveFileDialog1.FileName;

                try
                {
                    if (saveFileDialog1.CheckFileExists)
                    {
                        WriteToJsonFile<T>(path, objectToWrite, true);
                    }
                    else
                    {
                        WriteToJsonFile<T>(path, objectToWrite, false);
                    }

                }
                catch (Exception ex)
                {
                    ModernGUI.Shared.DevTools.ErrorLog(ex, "write to Json file failed");
                }

            }
        }

        public static T ReadFromJsonFileDialoge<T>() where T : new()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Open JsonObject from file";

            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return ReadFromJsonFile<T>(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ModernGUI.Shared.DevTools.ErrorLog(ex, "Read from json file failed");
            }

            return default(T);
        }

        public static T CloneObject<T>(T objectToClone)
        {
            if (ReferenceEquals(objectToClone, null)) return default;
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(objectToClone));
        }

        /*
         * EXAMPLE
        - Write the contents of the variable someClass to a file.
            WriteToBinaryFile<SomeClass>("C:\someClass.txt", object1);

        - Read the file contents back into a variable.
            SomeClass object1= ReadFromBinaryFile<SomeClass>("C:\someClass.txt");
        */
    }
}
