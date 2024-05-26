using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class ProfileVariableTester : MonoBehaviour
{
    [MenuItem("LoadyProfiles/Test Profile Variable")]
    private static void TestProfileVariable()
    {
        AddressableAssetSettings addressableAssetSettings = AddressableAssetSettingsDefaultObject.Settings;
        AddressableAssetProfileSettings profileSettings = addressableAssetSettings.profileSettings;
        string activeProfileID = addressableAssetSettings.activeProfileId;

        var variables = new ProfileVariables(profileSettings.GetVariableNames());
        
        for (int i = 0; i < variables.count; i++)
        {
            string variableName = variables.names[i];
            var value = profileSettings.GetValueByName(activeProfileID, variableName);
            variables.values.Add(value);

            var editorValue = profileSettings.EvaluateString(activeProfileID, value);
            variables.editorValues.Add(editorValue);
        }
        Debug.Log(variables);
    }
}

class ProfileVariables
{
    public int count;
    public List<string> names;
    public List<string> values;
    public List<string> editorValues;

    public ProfileVariables(List<string> names)
    {
        this.count = names.Count;
        this.names = names;
        values = new List<string>(count);
        editorValues = new List<string>(count);
    }

    public override string ToString()
    {
        string message = string.Empty;
        for (int i = 0; i < count; i++)
        {
            message += $"{names[i]} = '{values[i]}' -> '{editorValues[i]}'\n";
        }

        return message;
    }
}