using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrdaRemote
{
    public partial class About : Form
    {
        // Consturctor
        public About()
        {
            InitializeComponent();
        }

        // Event Key Down
        void About_KeyDown(object sender, KeyEventArgs e)
        {
            // ���� ������ ������ "Enter"
            if (e.KeyCode == Keys.Return) this.Close();
            // ���� ������ ������ "Esc"
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}
        