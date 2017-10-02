// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomPropertyDrawer(typeof(BubbleColor.RYB))]

public class RYBDrawer : PropertyDrawer
{
    string name;
    bool cache = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(name));

        //Check if there is enough space to put the name on the same line (to save space)
        if (position.height > 16f)
        {
            position.height = 16f;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += 18f;
        }

        GUI.skin.label.padding = new RectOffset(3, 3, 6, 6);

        //show the X and Y from the point
        EditorGUIUtility.labelWidth = 14f;
        contentPosition.width *= 0.25f;
        float indent = position.width * 0.3f;
        float widthToSpacerRatio = 0.85f;
        float width = widthToSpacerRatio* (position.width-indent) /3 - EditorGUIUtility.labelWidth;
        float spacer = (1f-widthToSpacerRatio) * (position.width-indent) / 3;

        // Calculate rects
        var rRect = new Rect(position.x + indent, position.y, width, position.height);
        var yRect = new Rect(position.x + indent + width + spacer, position.y, width, position.height);
        var bRect = new Rect(position.x + indent + width *2 + spacer*2, position.y, width, position.height);


        // Begin/end property & change check make each field
        // behave correctly when multi-object editing.
        EditorGUI.PropertyField(rRect, property.FindPropertyRelative("Red"), new GUIContent("R"));
        EditorGUI.PropertyField(yRect, property.FindPropertyRelative("Yellow"), new GUIContent("Y"));
        EditorGUI.PropertyField(bRect, property.FindPropertyRelative("Blue"), new GUIContent("B"));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return Screen.width < 333 ? (16f + 18f) : 16f;
    }

}
#endif
