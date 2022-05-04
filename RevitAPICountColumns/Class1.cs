using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPICountColumns
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            ElementCategoryFilter columnsCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            ElementClassFilter columnsInstancesFilter = new ElementClassFilter(typeof(FamilyInstance));

            LogicalAndFilter columnsFilter = new LogicalAndFilter(columnsCategoryFilter, columnsInstancesFilter);

            var columns = new FilteredElementCollector(doc)
                .WherePasses(columnsFilter)
                .Cast<FamilyInstance>()
                .ToList();

            TaskDialog.Show("Количество колон в проекте: ", columns.Count.ToString());
            return Result.Succeeded;
        }
    }
}
