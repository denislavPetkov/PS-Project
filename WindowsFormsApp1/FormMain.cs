using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Deployment.Application;
using System.Reflection;
using BaseForms;

namespace WindowsFormsApp1
{
	public partial class FormMain : Form, IMessageFilter
    {
		public class FormWrap
		{
			public Type m_frm = null;
			public Int32 m_nIdx = 0;

			public FormWrap(
				Type frm,
				Int32 nIdx)
			{
				m_frm = frm;
				m_nIdx = nIdx;
			}


		}

		public FormMain()
		{
			InitializeComponent();

			ListViewItem lvi;
			Int32 nCnt = 0;

            bool f = new OptionContext().TestOptionsIfEmpty();
            foreach (Option option in new OptionContext().Options)
            {
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(option.Image));
                imageList.Images.Add(x);
                lvi = listViewMain.Items.Add(option.Name, nCnt);
                lvi.Tag = new FormWrap(typeof(Form0), nCnt);
                nCnt++;
            }

			//if (_InitApp.bForm0)
			//{
   //             Image x = (Bitmap)((new ImageConverter()).ConvertFrom(option2.Image));
   //             imageList.Images.Add(x);
			//	lvi = listViewMain.Items.Add(option2.Name, nCnt);
			//	lvi.Tag = new FormWrap(typeof(Form0), _InitApp.IdxForm0);
			//	nCnt++;
			//}
   //         if (_InitApp.bForm1)
   //         {
   //             imageList.Images.Add(WindowsFormsApp1.Properties.Resources.Number_1_Icon_32);
   //             lvi = listViewMain.Items.Add("Form 1", nCnt);
   //             lvi.Tag = new FormWrap(typeof(Form1), _InitApp.IdxForm1);
   //             nCnt++;
   //         }
   //         if (_InitApp.bForm2)
   //         {
   //             imageList.Images.Add(WindowsFormsApp1.Properties.Resources.Number_2_Icon_32);
   //             lvi = listViewMain.Items.Add("Form 2", nCnt);
   //             lvi.Tag = new FormWrap(typeof(Form2), _InitApp.IdxForm2);
   //             nCnt++;
   //         }
   //         if (_InitApp.bForm3)
   //         {
   //             imageList.Images.Add(WindowsFormsApp1.Properties.Resources.Number_3_Icon_32);
   //             lvi = listViewMain.Items.Add("Form 3", nCnt);
   //             lvi.Tag = new FormWrap(typeof(Form3), _InitApp.IdxForm3);
   //             nCnt++;
   //         }
   //         if (_InitApp.bForm4)
   //         {
   //             imageList.Images.Add(WindowsFormsApp1.Properties.Resources.Number_4_Icon_32);
   //             lvi = listViewMain.Items.Add("Form 4", nCnt);
   //             lvi.Tag = new FormWrap(typeof(Form4), _InitApp.IdxForm4);
   //             nCnt++;
   //         }
   //         if (_InitApp.bForm5)
   //         {
   //             imageList.Images.Add(WindowsFormsApp1.Properties.Resources.Number_5_Icon_32);
   //             lvi = listViewMain.Items.Add("Form 5", nCnt);
   //             lvi.Tag = new FormWrap(typeof(Form5), _InitApp.IdxForm5);
   //             nCnt++;
   //         }
   //         if (_InitApp.bForm6)
   //         {
   //             imageList.Images.Add(WindowsFormsApp1.Properties.Resources.Number_6_Icon_32);
   //             lvi = listViewMain.Items.Add("Form 6", nCnt);
   //             lvi.Tag = new FormWrap(typeof(Form6), _InitApp.IdxForm6);
   //             nCnt++;
   //         }
   //         if (_InitApp.bForm7)
   //         {
   //             imageList.Images.Add(WindowsFormsApp1.Properties.Resources.Number_7_Icon_32);
   //             lvi = listViewMain.Items.Add("Form 7", nCnt);
   //             lvi.Tag = new FormWrap(typeof(Form7), _InitApp.IdxForm7);
   //             nCnt++;
   //         }

            Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
		}

		private void listViewMain_DoubleClick(object sender, EventArgs e)
		{
            try
            {
                if (listViewMain.SelectedItems.Count == 0)
                {
                    return;
                }

                //ErrorProvider.HideAll();

                Type selectedItemType = ((FormWrap) listViewMain.SelectedItems[0].Tag).m_frm;

                List<Form> appOpenForms = new List<Form>(this.OwnedForms);
                if (appOpenForms.Exists(x => x.GetType() == selectedItemType))
                {
                    Form alreadyForm = appOpenForms.Find(x => x.GetType() == selectedItemType);
                    if (alreadyForm.WindowState == FormWindowState.Minimized)
                        alreadyForm.WindowState = FormWindowState.Normal;
                    alreadyForm.Focus();
                    return;
                }

                foreach (Form form in appOpenForms)
                        form.Close();

                GC.Collect();
                GC.Collect();

                Form frm;
                if (selectedItemType.IsSubclassOf(typeof(BaseForm)))
                    frm = (Form)Activator.CreateInstance(selectedItemType, args: FormHierarchy.Primary);
                else
                    frm = (Form)Activator.CreateInstance(selectedItemType);

                frm.Show(this);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message + " " + E.ToString());

					 this.Visible = true;
            }
		}


		private void FormMain_Load(object sender, EventArgs e)
		{
			labelLogo.Text += " - " + DateTime.Now.Year.ToString();

		    this.Location = Screen.FromControl(this).WorkingArea.Location;
            this.Location = new Point(
                this.Location.X - SystemInformation.Border3DSize.Width - SystemInformation.BorderSize.Width,
                this.Location.Y);
            this.Size = new Size(this.Width, Screen.FromControl(this).WorkingArea.Height);
		}

        private void listViewMain_MouseHover(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        private void listViewMain_MouseLeave(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        // P/Invoke declarations
        private const int WM_MOUSEWHEEL = 0x20a;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
                // find the control at screen position m.LParam
                Point pos = new Point(m.LParam.ToInt32());
                IntPtr hWnd = WindowFromPoint(pos);
                if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null)
                {
                    SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
                    return true;
                }
            } 
            return false;
        }

        private void listViewMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}