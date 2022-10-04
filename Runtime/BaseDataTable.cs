using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class BaseDataTable : ScriptableObject
{
    [HideInInspector]
    public string lastUpdated;

    [SerializeField]
    protected Dictionary<string, BaseDataTableRow> rows = new Dictionary<string, BaseDataTableRow>();
    [SerializeField]
    protected List<BaseDataTableRow> rowList = new List<BaseDataTableRow>();


    #region #IF EDITOR
#if UNITY_EDITOR
    [CustomEditor(typeof(BaseDataTable))]
    public abstract class BaseDataTableEditor : Editor
    {
        protected BaseDataTable table;
        virtual protected void OnEnable () {
            table = (BaseDataTable)target;
        }

        protected override void OnHeaderGUI () {
            base.OnHeaderGUI();

            GUILayout.Label(" ");

            if(GUILayout.Button("Import")) {
                ReadData();
                table.lastUpdated = DateTime.Now.ToString();
            }
            GUILayout.Label("Last Updated : " + table.lastUpdated, "box");
        }

        private void ReadData () {
            SimpleCSVReader.Read(GetPath(), OnResponse_ReadData);
        }

        private void OnResponse_ReadData (List<Dictionary<string, object>> ss) {
            table.Clear();
            AssetDatabase.SaveAssets();

            int totalRow = ss.Count;

            for(int i = 0; i < totalRow; i++) {
                Dictionary<string, object> row = ss[i];
                table.AddRow(row);
            }

            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }

        protected virtual string GetPath () {
            return null;
        }
    }
#endif
    #endregion

    public void AddRow (Dictionary<string, object> cells) {
        BaseDataTableRow subAsset = DataTableRowFactory.CreateDataTableRow(this);
        subAsset.SetData(cells);

        #region #IF EDITOR
#if UNITY_EDITOR
        if(!EditorApplication.isPlaying) {
            AssetDatabase.AddObjectToAsset(subAsset, this);
        }
#endif
        #endregion
        rows.Add(subAsset.name, subAsset);
        rowList.Add(subAsset);
    }

    public void Clear () {
        #region #IF EDITOR
#if UNITY_EDITOR
        if(!EditorApplication.isPlaying) {
            string path = AssetDatabase.GetAssetPath(this);
            UnityEngine.Object[] objs = AssetDatabase.LoadAllAssetsAtPath(path);
            foreach(UnityEngine.Object obj in objs) {
                if(obj is BaseDataTableRow) {
                    DestroyImmediate(obj, true);
                }
            }
        }
#endif
        #endregion

        foreach(var elem in rows) {
            DestroyImmediate(elem.Value);
        }
        rows.Clear();
        rowList.Clear();
    }

    public List<BaseDataTableRow> GetTableRows () {
        return rowList;
    }

    public BaseDataTableRow GetTableRow (int index) {
        return rowList[index];
    }
}