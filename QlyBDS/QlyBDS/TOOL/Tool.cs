using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QlyBDS.DAO
{
    class Tools
    {
        // hàm clear các textbox, checkbox, combobox
        public void ClearGroup(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                Type cControls = c.GetType();
                switch (cControls.Name)
                {
                    case "TextBox":
                        ((TextBox)c).Text = string.Empty;
                        break;
                    case "CheckBox":
                        ((CheckBox)c).Checked = false;
                        break;
                    case "ComboBox":
                        ((ComboBox)c).SelectedIndex = -1;
                        break;
                    case "RichTextBox":
                        ((RichTextBox)c).Text = string.Empty;
                        break;
                    default:
                        break;
                }
                if (c.Controls != null)
                {
                    ClearGroup(c.Controls);
                }
            }
        }

        public bool CheckInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < '0' || input[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
