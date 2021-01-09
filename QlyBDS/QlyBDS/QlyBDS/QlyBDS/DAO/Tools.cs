using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_HQTCSDL.DAO
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
                        ((ComboBox)c).SelectedIndex = 0;
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
    }
}
