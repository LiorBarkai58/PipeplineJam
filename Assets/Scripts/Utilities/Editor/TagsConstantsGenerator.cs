using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

[InitializeOnLoad]
public static class TagConstantsGenerator
{
    private const string FilePath = "Assets/Scripts/Generated/Tags.cs";

    [MenuItem("Tools/Generate Tag Constants")]
    public static void GenerateTagConstants()
    {
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

        if (!Directory.Exists("Assets/Scripts/Generated"))
        {
            Directory.CreateDirectory("Assets/Scripts/Generated");
        }

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("// This file is auto-generated. Do not modify manually.");
        builder.AppendLine("public static class Tags");
        builder.AppendLine("{");

        foreach (string tag in tags)
        {
            string sanitizedTag = tag.Replace(" ", "_"); // Ensure valid C# identifiers
            builder.AppendLine($"\tpublic const string {sanitizedTag} = \"{tag}\";");
        }

        builder.AppendLine("}");

        File.WriteAllText(FilePath, builder.ToString());
        AssetDatabase.Refresh();
        
        Debug.Log("Tags.cs file generated successfully!");
    }
}