using System;
using System.Windows.Forms;
using ModernGUI;
using ModernGUI.Controls;

namespace MaterialSkinExample
{
    public partial class BasicExample : ModernGUI.Controls.Form
    {
        private readonly SkinManager _SkinManager;
        public BasicExample()
        {
            InitializeComponent();

            // Initialize MaterialSkinManager
            _SkinManager = SkinManager.Instance;
            _SkinManager.AddFormToManage(this);
            _SkinManager.Theme = SkinManager.Themes.LIGHT;
			_SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Indigo); 

			// Add dummy data to the listview
	        seedListView();

			dataGridView1.Rows.Add();
        }

	    private void seedListView()
	    {
			//Define
			var data = new[]
	        {
		        new []{"Lollipop", "392", "0.2", "0"},
				new []{"KitKat", "518", "26.0", "7"},
				new []{"Ice cream sandwich", "237", "9.0", "4.3"},
				new []{"Jelly Bean", "375", "0.0", "0.0"},
				new []{"Honeycomb", "408", "3.2", "6.5"}
	        };

			//Add
			foreach (string[] version in data)
			{
				var item = new ListViewItem(version);
				ListView1.Items.Add(item);
			}
	    }

        private void Button1_Click(object sender, EventArgs e)
        {
            _SkinManager.Theme = _SkinManager.Theme == SkinManager.Themes.DARK ? SkinManager.Themes.LIGHT : SkinManager.Themes.DARK;
        }

	    private int colorSchemeIndex;
        private void RaisedButton1_Click(object sender, EventArgs e)
        {
	        colorSchemeIndex++;
	        if (colorSchemeIndex > 2) colorSchemeIndex = 0;

			//These are just example color schemes
	        switch (colorSchemeIndex)
	        {
				case 0:
                    _SkinManager.ColorScheme = new ColorScheme(ColorSchemes.BlueGrey);
			        break;
				case 1:
                    _SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Indigo);
			        break;
				case 2:
                    _SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Green);
					break;
	        }
        }

        private void RaisedButton2_Click(object sender, EventArgs e)
        {
           ProgressBar1.Value = Math.Min(ProgressBar1.Value + 10, 100);
        }

        private void  FlatButton4_Click(object sender, EventArgs e)
        {
             ProgressBar1.Value = Math.Max(ProgressBar1.Value - 10, 0);
        }
    }
}
