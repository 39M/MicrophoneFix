#if UNITY_EDITOR_OSX
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;
using System.IO;

public class XcodeProjectMod : MonoBehaviour
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)
    {
        if (BuildTarget == BuildTarget.iOS)
        {
            // Get plist
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            // Get root
            PlistElementDict rootDict = plist.root;

            // Change value of NSMicrophoneUsageDescription in Xcode plist
            rootDict.SetString("NSMicrophoneUsageDescription", "喵喵喵喵喵喵？？？");

            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}
#endif
