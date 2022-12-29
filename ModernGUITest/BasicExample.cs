using ModernGUI;

namespace ModernGUITest
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

            // Add dummy data to the MultiSelectTreeview
            seedmultiSelectTreeview();


            dataGridView1.Rows.Add();
        }


        private void seedmultiSelectTreeview()
        {

            List<TreeNode> Studies = new List<TreeNode>();

            TreeNode Study = new TreeNode("Cool");
            TreeNode Study1 = new TreeNode("1");
            TreeNode Study2 = new TreeNode("1123123");
            TreeNode Study3 = new TreeNode("11212312313123");
            Study2.Nodes.Add("cool");
            Studies.Add(Study);
            Studies.Add(Study1);
            Studies.Add(Study2);
            Studies.Add(Study3);
            TreeNode[] Studies_Arr = Studies.ToArray();
            multiSelectTreeview1.Nodes.AddRange(Studies_Arr);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _SkinManager.Theme = _SkinManager.Theme == SkinManager.Themes.DARK ? SkinManager.Themes.LIGHT : SkinManager.Themes.DARK;
        }

        private int colorSchemeIndex = 1;
        private void RaisedButton1_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 3) colorSchemeIndex = 0;

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
                case 3:
                    _SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Red);
                    break;
            }
        }

        private void RaisedButton2_Click(object sender, EventArgs e)
        {
            ProgressBar1.Value = Math.Min(ProgressBar1.Value + 10, 100);
        }

        private void FlatButton4_Click(object sender, EventArgs e)
        {
            ProgressBar1.Value = Math.Max(ProgressBar1.Value - 10, 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void raisedButton3_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = !dataGridView1.ReadOnly;
        }

        private void MoveDGV_raisedButton_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDragDrop = !dataGridView1.AllowUserToDragDrop;
        }

        private void DGVListBoxStyle_raisedButton_Click(object sender, EventArgs e)
        {
            dataGridView1.ListBoxStyle = !dataGridView1.ListBoxStyle;
        }
    }
}
