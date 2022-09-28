using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataTableFactoryEdirotWindow : EditorWindow
{
    private enum SelectableFieldTypes
    {
        STRING, INT, FLOAT, VECTOR2, VECTOR3
    }

    private string sheetPath;
    private string sheetName;
    private List<string> fieldNames = new List<string>();
    private List<string> fieldTypes = new List<string>();
    private List<SelectableFieldTypes> selectableFields = new List<SelectableFieldTypes>();

    private string dataTableClass;
    private string dataTableRowClass;

    [MenuItem("Tools/DataTable/New Table")]
    private static void Open()
    {
        DataTableFactoryEdirotWindow win = EditorWindow.GetWindow<DataTableFactoryEdirotWindow>("Define New Datatable");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Find Sheet", EditorStyles.boldLabel);
        if (GUILayout.Button("Browse.."))
        {
            sheetPath = EditorUtility.OpenFilePanel("Find .CSV File", Path.Combine(Application.dataPath, @"/"), "csv");
            sheetName = Path.GetFileName(sheetPath).Split(".")[0];

            SimpleCSVReader.Read(sheetPath, loadedData =>
            {
                fieldNames.Clear();
                fieldTypes.Clear();
                selectableFields.Clear();

                foreach (var key in loadedData[0].Keys)
                {
                    fieldNames.Add(key.ToString());
                    fieldTypes.Add("");
                    selectableFields.Add(SelectableFieldTypes.STRING);
                }
            });
        }
        EditorGUILayout.LabelField(sheetPath, EditorStyles.boldLabel);

        if (fieldNames.Count > 0)
        {
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Enter DataType for each field", EditorStyles.boldLabel);

            for (int i = 0; i < fieldNames.Count; i++)
            {
                selectableFields[i] = (SelectableFieldTypes)EditorGUILayout.EnumPopup(fieldNames[i], selectableFields[i]);
                fieldTypes[i] = selectableFields[i].ToString().ToLower();
            }
        }

        EditorGUILayout.LabelField("");
        if (GUILayout.Button("GenerateScripts"))
        {
            CreateDataTableClass();
            CreateDataTableRowClass();

            AssetDatabase.Refresh();
        }
    }

    private void CreateDataTableClass()
    {
        StreamReader reader = null;
        try
        {
            reader = new StreamReader(FindDir("DataTableClass.txt"));
            dataTableClass = reader.ReadToEnd();
            dataTableClass = dataTableClass.Replace("$ClassName", sheetName + "Data");
            dataTableClass = dataTableClass.Replace("$SheetPath", sheetPath);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
        }

        StreamWriter writer = null;
        try
        {
            FileCheck(sheetName + "Data.cs");
            writer = new StreamWriter("Assets/Script/DataTable/" + sheetName + "Data.cs");
            writer.Write(dataTableClass);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }

    private void CreateDataTableRowClass()
    {
        StreamReader reader = null;
        try
        {
            reader = new StreamReader(FindDir("DataTableRowClass.txt"));
            dataTableRowClass = reader.ReadToEnd();
            dataTableRowClass = dataTableRowClass.Replace("$ClassName", sheetName + "DataRow");

            string member = "";
            string data = "";
            int memberFieldCount = fieldTypes.Count;
            for (int i = 0; i < memberFieldCount; ++i)
            {
                if (fieldTypes[i] == SelectableFieldTypes.STRING.ToString().ToLower())
                {
                    data += string.Format("{0} = data[\"{0}\"].ToString();\n\t\t", fieldNames[i]);
                }
                else if (fieldTypes[i] == SelectableFieldTypes.VECTOR2.ToString().ToLower() || fieldTypes[i] == SelectableFieldTypes.VECTOR3.ToString().ToLower() )
                {
                    fieldTypes[i] = char.ToUpper(fieldTypes[i][0]) + fieldTypes[i].Substring(1); 
                    data += string.Format("{0} = Parse{1}(data[\"{0}\"].ToString());\n\t\t", fieldNames[i], fieldTypes[i]);
                }
                else
                {
                    data += string.Format("{0} = Parse{1}(data[\"{0}\"].ToString());\n\t\t", fieldNames[i], fieldTypes[i].ToUpper());
                }
                Debug.Log(fieldTypes[i]);
                member += string.Format("public {0} {1};\n\t", fieldTypes[i], fieldNames[i]);
            }

            dataTableRowClass = dataTableRowClass.Replace("$MemberFields", member);
            dataTableRowClass = dataTableRowClass.Replace("$DataFields", data);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
        }

        StreamWriter writer = null;
        try
        {
            FileCheck(sheetName + "DataRow.cs");
            writer = new StreamWriter("Assets/Script/DataTable/" + sheetName + "DataRow.cs");
            writer.Write(dataTableRowClass);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }

    private string FindDir(string fileName)
    {
        return Directory.GetFiles(Application.dataPath, fileName, SearchOption.AllDirectories)[0];
    }
    private void FileCheck(string fileName)
    {
        if (!Directory.Exists("Assets/Script"))
        {
            Directory.CreateDirectory("Assets/Script");
        }
        if (!Directory.Exists("Assets/Script/DataTable"))
        {
            Directory.CreateDirectory("Assets/Script/DataTable");
        }
    }
}
