﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseForms;

namespace WindowsFormsApp1
{
    public partial class Form2 : BaseForm
    {
        public _ContentObjectEx formObject = new _ContentObjectEx();

        protected override _ContentObjectEx m_ObjEx
        {
            get { return formObject; }
            set { formObject = (value as _ContentObjectEx); }
        }
        public Form2(FormHierarchy hierarchy = FormHierarchy.Dialog)
            : base(hierarchy)
        {
            InitializeComponent();

            FormBorderStyle = BorderBasedOnHierarchy();
        }
    }
}