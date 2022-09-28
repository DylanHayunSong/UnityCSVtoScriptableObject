using UnityEngine;

public static class DataTableRowFactory
{
    public static BaseDataTableRow CreateDataTableRow (BaseDataTable parent) {
        string type = parent.GetType().ToString();
        type += "Row";
        BaseDataTableRow row = ScriptableObject.CreateInstance(type) as BaseDataTableRow;
        return row;
    }
}
