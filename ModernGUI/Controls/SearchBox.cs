using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernGUI.Controls
{
    public partial class SearchBox : UserControl
    {
        public delegate void OnSearchClick(string searchtext);
        public delegate void OnClearClick();

        public OnSearchClick SearchClick;
        public OnClearClick ClearClick;

        public SingleLineTextField SearchTextBox { get { return SearchText_txtbx; } }

        private string CriteriaText { get { return Search_criteria_lbl.Text; } set { Search_criteria_lbl.Text = value; } }

        List<ModernGUI.Controls.RadioButton> _SearchTypeRadioButtons;
        public List<ModernGUI.Controls.RadioButton> SearchTypeRadioButtons
        {
            set
            {
                _SearchTypeRadioButtons = value;
                foreach (ModernGUI.Controls.RadioButton chbx in value)
                {
                    chbx.CheckedChanged += CheckBoxChecked;
                }
            }
        }

        ModernGUI.Controls.RadioButton _PreviousSelectedCheckbox;
        private void CheckBoxChecked(object? sender, EventArgs e)
        {
            if (_PreviousSelectedCheckbox != ((ModernGUI.Controls.RadioButton)sender))
            {
                SearchText_txtbx.Text = "";
            }
            _PreviousSelectedCheckbox = ((ModernGUI.Controls.RadioButton)sender);
            SearchText_txtbx.Focus();
        }

        public SearchBox()
        {
            InitializeComponent();
            SearchText_txtbx.KeyPress += SearchBx_KeyPress;
        }
        private void Clear_criteria_btn_Click(object sender, EventArgs e)
        {
            CriteriaText = "";
            ClearClick?.Invoke();
        }
        private void Search_btn_Click(object sender, EventArgs e)
        {
            SearchClick?.Invoke(SearchText_txtbx.Text);
        }
        public void Clear()
        {
            SearchText_txtbx.Text = "";
            CriteriaText = "";
        }
        private void SearchBx_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Search_btn.PerformClick();
            }
        }


        Dictionary< string, string> SearchCriteriaList = new Dictionary<string, string>();

        public void AddSerachCriteria(string SearchCriteria, string SearchText)
        {
            if (SearchCriteriaList.ContainsKey(SearchCriteria))
            {
                SearchCriteriaList[SearchCriteria] = SearchText;
            }
            else
            {
                SearchCriteriaList.Add(SearchCriteria, SearchText);
            }
            SearchCriteriaListToDisplay(SearchCriteriaList);

        }

        private void SearchCriteriaListToDisplay(Dictionary<string, string> SearchCriteriaList)
        {
            CriteriaText = "";
            foreach (KeyValuePair<string, string> item in SearchCriteriaList)
            {
                if (CriteriaText == "")
                {
                    CriteriaText = item.Key + ":={" + item.Value + "}";
                }
                else
                {
                    CriteriaText = CriteriaText + ", " + item.Key + ":={" + item.Value + "}";
                }
            }
        }
    }
}
