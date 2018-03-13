using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mortfors_buss.Lib
{
    internal static class ErrorMessage
    {
        public static void Show(string text)
        {
            string caption = string.Empty;
            MessageBox.Show(text, caption, MessageBoxButtons.OK);
        }
    }
}
