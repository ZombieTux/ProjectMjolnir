using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MCoding1.Forms
{
    public partial class frmDebug : Form
    {
        public Test1 game;

        public frmDebug()
        {
            this.FormClosing += frmDebug_FormClosing;
            InitializeComponent();
        }

        void frmDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.game.Exit();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //this.Text = game.scene.Player.Velocity.X.ToString();
            base.OnPaint(e);
        }
    }
}
