using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System;

public class SimpleCSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static void Read(string filePath, Action<List<Dictionary<string, object>>> OnDataLoaded) {
        var list = new List<Dictionary<string, object>>();

        string source;
        StreamReader sr = new StreamReader(Path.GetFullPath(filePath));
        source = sr.ReadToEnd();
        sr.Close();

        var lines = Regex.Split(source, LINE_SPLIT_RE);

        if(lines.Length <= 1) {
            OnDataLoaded.Invoke(list);
        }

        var header = Regex.Split(lines[0], SPLIT_RE);
        for(var i = 1; i < lines.Length; i++) {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if(values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for(var j = 0; j < header.Length && j < values.Length; j++) {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                if(int.TryParse(value, out n)) {
                    finalvalue = n;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }

        OnDataLoaded.Invoke(list);
    }
}
