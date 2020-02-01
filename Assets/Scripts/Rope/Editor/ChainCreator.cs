using UnityEditor;
using UnityEngine;

public class ChainCreator : EditorWindow
{
    Transform sceneRoot;
    GameObject template;
    Vector3 linkOffset;
    int count;

    [MenuItem("Window/Chain creator")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(ChainCreator));
    }

    void OnGUI()
    {
        linkOffset = EditorGUILayout.Vector3Field("Link Offset", linkOffset);
        sceneRoot = EditorGUILayout.ObjectField("Scene Root", sceneRoot, typeof(Transform), true) as Transform;
        template = EditorGUILayout.ObjectField("Template", template, typeof(GameObject), true) as GameObject;
        count = (int) EditorGUILayout.Slider("Count", count, 1, 1000);

        if (GUILayout.Button("ReGenerate"))
        {
            var offsetAccum = Vector3.zero;
            var oldJoint = Object.Instantiate(template, offsetAccum, Quaternion.identity, sceneRoot.transform);
            for (var i = 1; i<count; ++i)
            {
                var newJoint = Object.Instantiate(template, offsetAccum, Quaternion.identity, sceneRoot.transform);
                var joint = newJoint.GetComponent<Joint>();
                var body = oldJoint.GetComponent<Rigidbody>();
                if(joint != null && body != null)
                    joint.connectedBody = body;
                offsetAccum += linkOffset;
                oldJoint = newJoint;
            }
        }
    }
}
