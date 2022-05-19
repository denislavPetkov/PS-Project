using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1;


namespace BaseForms
{
    public enum FormHierarchy
    {
        Primary,
        Secondary,
        Tertiary,
        Expanded,
        Dialog
    }

    public partial class BaseForm : Form
    {
        public FormHierarchy Hierarchy = FormHierarchy.Dialog;
        public bool WasShown = false;

        protected Point LocationToBe;

        [Obsolete("Designer only", true)]
        private BaseForm()
        { }

        public BaseForm(FormHierarchy hierarchy)
        {
            InitializeComponent();

            Hierarchy = hierarchy;

            this.FormClosing += BaseFormClosing;
            this.Shown += BaseFormLoad;
        }

        private void BaseFormClosing(object sender, EventArgs e)
        {
            //ErrorProvider.HideAll();
        }

        private void BaseFormLoad(object sender, EventArgs e)
        {
            SizeSet();
            WasShown = true;
        }

        //pinvoke
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        private void SizeSet()
        {
            double distance;
            double acceptableDistance = 15;
            int formMainXOffset = SystemInformation.Border3DSize.Width + SystemInformation.BorderSize.Width;
            int primaryXOffset = (this.Width - this.ClientSize.Width) / 2;
            if (System.Windows.Forms.SystemInformation.VerticalScrollBarWidth - primaryXOffset < 5)
                primaryXOffset /= 2;

            switch (this.Hierarchy)
            {
                case FormHierarchy.Primary:
                    if (Owner != null)
                    {
                        distance = Math.Sqrt(
                            Math.Pow(Owner.Location.X - Screen.FromControl(this).Bounds.Location.X, 2)
                            + Math.Pow(Owner.Location.Y - Screen.FromControl(this).Bounds.Location.Y, 2));

                        if (distance <= acceptableDistance)
                        {
                            if (Owner.Size.Width + Size.Width > Screen.FromControl(Owner).WorkingArea.Width)
                            {
                                LocationToBe = new Point(
                                    Screen.FromControl(Owner).WorkingArea.Location.X
                                        + Screen.FromControl(Owner).WorkingArea.Width
                                        - Width,
                                    Screen.FromControl(Owner).WorkingArea.Location.Y);
                            }
                            else
                            {
                                Point _newLocation = new Point(
                                    Screen.FromControl(Owner).WorkingArea.Location.X
                                            + Owner.Size.Width - formMainXOffset - primaryXOffset * 2,
                                        Owner.Location.Y);
                                LocationToBe = _newLocation;
                            }
                        }
                    }
                    break;
                case FormHierarchy.Secondary:
                    if (Owner != null)
                    {
                        LocationToBe = new Point(
                            Screen.FromControl(Owner).WorkingArea.X
                                + Screen.FromControl(Owner).WorkingArea.Width
                                - Width,
                            Screen.FromControl(Owner).WorkingArea.Location.Y);
                    }
                    break;
                case FormHierarchy.Expanded:
                    if (Owner != null)
                    {
                        if (Owner.FormBorderStyle == FormBorderStyle.Sizable
                            && FormBorderStyle == FormBorderStyle.Sizable)
                        {
                            if (Math.Abs(Owner.Height - Screen.FromControl(Owner).WorkingArea.Height) < acceptableDistance)
                                Owner.Height = Convert.ToInt32(Owner.Height * 0.6);
                        }

                        if (FormBorderStyle == FormBorderStyle.Sizable)
                        {
                            int sizeHeight;
                            int sizeWidth;
                            int locationY;
                            int locationX;

                            // Size - Height
                            if (Owner is BaseForm
                                && (Owner as BaseForm).Hierarchy == FormHierarchy.Expanded)
                                sizeHeight = Owner.Height;
                            else
                                sizeHeight = Math.Max(
                                    Screen.FromControl(Owner).WorkingArea.Height - (Owner.Location.Y + Owner.Height),
                                    MinimumSize.Height);

                            // Location - Y
                            if (Owner is BaseForm
                                    && (Owner as BaseForm).Hierarchy == FormHierarchy.Expanded)
                                locationY = Owner.Location.Y - sizeHeight;
                            else if (Screen.FromControl(Owner).WorkingArea.Height - (Owner.Location.Y + Owner.Height)
                                    >= sizeHeight)
                                locationY = Screen.FromControl(Owner).WorkingArea.Location.Y
                                    + (Owner.Location.Y + Owner.Height);
                            else
                                locationY = Screen.FromControl(Owner).WorkingArea.Location.Y
                                        + Screen.FromControl(Owner).WorkingArea.Height
                                        - sizeHeight;

                            // Size - Width
                            // Location - X
                            Form primaryOwner = OwnerOfHierarchy(FormHierarchy.Primary);
                            if (primaryOwner != null)
                            {
                                sizeWidth = Screen.FromControl(Owner).WorkingArea.Width -
                                    (primaryOwner.Location.X - Screen.FromControl(Owner).WorkingArea.Location.X);
                                locationX = primaryOwner.Location.X;
                            }
                            else
                            {
                                sizeWidth = Size.Width;
                                locationX = Location.X;
                            }

                            Size = new Size(sizeWidth, sizeHeight);
                            LocationToBe = new Point(locationX, locationY);
                        }
                    }
                    break;
            }

            if (LocationToBe != new Point())
            {
                Location = LocationToBe;
                if (Location.X != LocationToBe.X)
                {
                    //Проверката е смислена, при разработката се бъгва на втори екран
                }
            }
        }

        protected virtual IContentData m_Data { get; set; }
        protected virtual _ContentObjectEx m_ObjEx { get; set; }

        public _ContentObjectEx MainObject
        {
            get
            {
                if (m_Data != null)
                    return m_Data.AsContentObjectEx;
                else if (m_ObjEx != null)
                    return m_ObjEx;
                else
                    return null;
            }
        }

        public List<BaseForm> OwnedBaseForms
        {
            get
            {
                return new List<Form>(this.OwnedForms)
                    .Where(x => x is BaseForm)
                    .Cast<BaseForm>()
                    .ToList();
            }
        }

        public BaseForm FormByIdIfAlreadyOpen(int id)
        {
            BaseForm alreadyForm = null;

            {
                List<BaseForm> appOpenForms =
                    OwnedBaseForms.Where(x => x.MainObject != null)
                    .Distinct()
                    .ToList();

                List<BaseForm> appOpenFormsIdenticalId =
                    appOpenForms
                        .Where(x => (x.MainObject != null
                                        && x.MainObject.ID == id))
                        .ToList();

                if (appOpenFormsIdenticalId.Count > 0)
                {
                    alreadyForm = appOpenFormsIdenticalId.First();
                }
            }

            return alreadyForm;
        }

        public bool CheckIfIdIsAlreadyOpenElseCloseAll<TForm>(int id, out TForm form) 
            where TForm : BaseForm
        {
            BaseForm alreadyForm = FormByIdIfAlreadyOpen(id);

            if (alreadyForm != null)
            {
                if (alreadyForm.WindowState == FormWindowState.Minimized)
                    alreadyForm.WindowState = FormWindowState.Normal;
                alreadyForm.Focus();
                form = alreadyForm as TForm;
                return true;
            }

                foreach (Form f in OwnedBaseForms)
                    f.Close();

            form = null;
            return false;
        }

        protected FormBorderStyle BorderBasedOnHierarchy()
        {
            FormBorderStyle result = FormBorderStyle;

            if (Hierarchy == FormHierarchy.Secondary
                || Hierarchy == FormHierarchy.Dialog)
                result = FormBorderStyle.FixedDialog;

            return result;
        }

        protected bool ResultInPopUp()
        {
            if (FormBorderStyle == FormBorderStyle.FixedDialog)
                return false;
            else
                return true;
        }

        public FormHierarchy LowerFormHierarchy(FormHierarchy current)
        {
            switch (current)
            {
                case FormHierarchy.Primary:
                    return FormHierarchy.Secondary;
                case FormHierarchy.Secondary:
                case FormHierarchy.Tertiary:
                case FormHierarchy.Expanded:
                    return FormHierarchy.Dialog;
                default:
                    return FormHierarchy.Dialog;
            }
        }

        public Form OwnerOfHierarchy(FormHierarchy targetHierarchy)
        {
            if (this.Hierarchy == targetHierarchy)
                return this;
            else if (this.Owner is FormMain)
                return this.Owner;
            else if (this.Owner as BaseForm == null)
                return null;
            else
                return (this.Owner as BaseForm).OwnerOfHierarchy(targetHierarchy);                
        }

        public List<Form> OwnedOfHierarchy(FormHierarchy targetHierarchy)
        {
            return new List<Form>(this.OwnedForms)
                .Where(x => x is BaseForm)
                .Where(x => (x as BaseForm).Hierarchy == targetHierarchy)
                .ToList();
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ErrorProvider.HideAll();
        }
    }
}
