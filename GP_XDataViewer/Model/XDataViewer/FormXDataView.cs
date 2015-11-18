using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;

namespace GP_XDataViewer.Model.XDataViewer
{
   public partial class FormXDataView : Form
   {
      public FormXDataView(TypedValue[] typedValues, string entName)
      {
         InitializeComponent();
         label1.Text = entName;
         richTextBox1.Text = getXdataAllText(typedValues);         
      }

      private string getXdataAllText(TypedValue[] typedValues)
      {
         StringBuilder sbText = new StringBuilder();
         foreach (var item in typedValues)
         {
            sbText.AppendLine(string.Format("TypeCode: {0}; Value: {1}", item.TypeCode, item.Value));
         }
         return sbText.ToString();
      }
   }
}
