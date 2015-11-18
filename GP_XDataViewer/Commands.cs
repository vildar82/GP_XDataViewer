using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using GP_XDataViewer.Model.XDataViewer;

[assembly: CommandClass(typeof(GP_XDataViewer.Commands))]
[assembly: ExtensionApplication(typeof(GP_XDataViewer.Commands))]

namespace GP_XDataViewer
{
   public class Commands : IExtensionApplication
   {
      public void Initialize()
      {
         var doc = Application.DocumentManager.MdiActiveDocument;
         if (doc == null) return;
         Editor ed = doc.Editor;
         ed.WriteMessage("\nGP_XDataViewer загружена. Команда: GP-XDataShow");
      }

      public void Terminate()
      {
      }

      [CommandMethod("PIK", "GP-XDataShow", CommandFlags.Modal)]
      public void XDataShowCommand()
      {
         var doc = Application.DocumentManager.MdiActiveDocument;
         if (doc == null) return;
         Editor ed = doc.Editor;

         var opt = new PromptEntityOptions("Выбери приметив:");
         var res = ed.GetEntity(opt);
         if (res.Status == PromptStatus.OK)
         {
            TypedValue[] typedValues = null;
            string entName = string.Empty;
            using (var ent = res.ObjectId.Open(OpenMode.ForRead) as Entity)
            {
               if (ent.XData == null)
               {
                  ed.WriteMessage("\nНет расширенных данных у {0}", ent);
                  return;
               }
               else
               {
                  typedValues = ent.XData.AsArray();
                  entName = ent.ToString();
               }
            }            
            FormXDataView formXdataView = new FormXDataView(typedValues, entName);
            Application.ShowModalDialog(formXdataView);
         }
      }
   }
}
