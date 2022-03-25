using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPILengthOfSelectedPipes
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            ElementCategoryFilter pipesCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);
            ElementClassFilter pipesInstancesFilter = new ElementClassFilter(typeof(FamilyInstance));

            LogicalAndFilter pipesFilter = new LogicalAndFilter(pipesCategoryFilter, pipesInstancesFilter);

            var pipes = new FilteredElementCollector(doc)
                .WherePasses(pipesFilter)
                .Cast<FamilyInstance>()
                .ToList();
            foreach (var pipe in pipes)
            {
                if (pipe is FamilyInstance)
                {
                    using (Transaction ts = new Transaction(doc, "Set parameters"))
                    {
                        ts.Start();
                        var familyInstance = pipe as FamilyInstance;
                        Parameter commentParameter = familyInstance.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
                        commentParameter.Set("Труба");
                        ts.Commit();
                    }
                }
            }
            IList<Reference> selectedElementRefList = null;
            selectedElementRefList = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, new PipeFilter(), "Выберите элементы");

            var elementList = new List<Element>();
            foreach (var selectedElement in selectedElementRefList)
            {
                Element element = doc.GetElement(selectedElement);
                double pipeLength = !!!!!!!!
                elementList.Add(element);
            }



            TaskDialog.Show("Сообщение", "Тест");
            return Result.Succeeded;

        }
    }
}
