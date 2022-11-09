using ModernGUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernGUITest
{
    public partial class Example : ModernGUI.Controls.Form
    {
        private readonly SkinManager _SkinManager;
        public Example()
        {
            InitializeComponent();

            // Initialize MaterialSkinManager
            _SkinManager = SkinManager.Instance;
            _SkinManager.AddFormToManage(this);
            _SkinManager.Theme = SkinManager.Themes.LIGHT;
            _SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Indigo);
        }

        private void Example_Load(object sender, EventArgs e)
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
    }
}
