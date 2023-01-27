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
    public partial class SingleControlTest : Form
    {
        public SingleControlTest()
        {
            InitializeComponent();
            this.autoCompleteTextBox1.Text = string.Empty;

            // Add some sample auto complete entry items...
            this.autoCompleteTextBox1.Items.Add(new ModernGUI.Controls.AutoCompleteEntry("Phoenix, Az", "Phoenix, Az", "Az", "PHX"));
            this.autoCompleteTextBox1.Items.Add(new ModernGUI.Controls.AutoCompleteEntry("Tempe, Az", "Tempe, Az", "Az", "TPE"));
            this.autoCompleteTextBox1.Items.Add(new ModernGUI.Controls.AutoCompleteEntry("Chandler, Az",    "Chandler, Az", "Az", "CHA"));
            this.autoCompleteTextBox1.Items.Add(new ModernGUI.Controls.AutoCompleteEntry("Boxford, Ma", "Boxford, Ma", "Ma", "BXF"));
            this.autoCompleteTextBox1.Items.Add(new ModernGUI.Controls.AutoCompleteEntry("Topsfield, Ma",   "Topsfield, Ma", "Ma", "TPF"));
            this.autoCompleteTextBox1.Items.Add(new ModernGUI.Controls.AutoCompleteEntry("Danvers, Ma",     "Danvers, Ma", "Ma", "DNV"));

            this.Validate();
        }
    }
}
