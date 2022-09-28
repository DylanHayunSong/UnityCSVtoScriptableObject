using UnityEngine;

public class DataTableSimpleManager : MonoBehaviour
{
    [SerializeField]
    private BaseDataTable[] dataTables;

    public T GetTable<T>() where T : BaseDataTable
    {
        Debug.Log(dataTables.Length);
        foreach (var elem in dataTables)
        {
            Debug.Log(elem.GetType());
            Debug.Log(elem.GetType() is T);
            Debug.Log(typeof(T));
            if (elem.GetType() == typeof(T))
            {
                return (T)elem;
            }
        }
        return null;
    }
}
