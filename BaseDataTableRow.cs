using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BaseDataTableRow : ScriptableObject
{
    virtual public void SetData(Dictionary<string, object> cells)
    {
        if (cells.Count > 0)
        {
            this.name = cells.Values.First().ToString();
        }
    }
    protected float ParseFLOAT(string from)
    {
        float result;

        var match = Regex.Match(from, @"([-+]?[0-9]*\.?[0-9]+)");
        float.TryParse(match.Groups[1].Value, out result);

        return result;
    }

    protected int ParseINT(string from)
    {
        int result;

        var match = Regex.Match(from, @"([-+]?[0-9]*\.?[0-9]+)");
        int.TryParse(match.Groups[1].Value, out result);

        return result;
    }

    protected Vector2 ParseVector2(string from)
    {
        Vector2 result = Vector2.zero;

        if (from.Split(",").Length > 1)
        {
            for (int i = 0; i < from.Split(",").Length; i++)
            {
                result[i] = ParseFLOAT(from.Split(",")[i]);
            }
        }
        if (from.Split("|").Length > 1)
        {
            for (int i = 0; i < from.Split("|").Length; i++)
            {
                result[i] = ParseFLOAT(from.Split("|")[i]);
            }
        }

        return result;
    }

    protected Vector3 ParseVector3(string from)
    {
        Vector3 result = Vector3.zero;

        if (from.Split(",").Length > 1)
        {
            for (int i = 0; i < from.Split(",").Length; i++)
            {
                result[i] = ParseFLOAT(from.Split(",")[i]);
            }
        }

        if (from.Split("|").Length > 1)
        {
            for (int i = 0; i < from.Split("|").Length; i++)
            {
                result[i] = ParseFLOAT(from.Split("|")[i]);
            }
        }

        return result;
    }
}