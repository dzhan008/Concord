#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IntersectionMatrix))]
public class IntersectionMatrixEditor : Editor
{
    private const int defaultCellSize = 25; // px
    private const float spaceBetweenCells = 25;
    private const int singleCharacterWidth = 10;

    SerializedProperty gridSize;
    SerializedProperty cells;

    Rect lastRect;

	void OnEnable()
	{
        gridSize = serializedObject.FindProperty("gridSize");
        cells = serializedObject.FindProperty("cells");
	}

    public override void OnInspectorGUI()
    {
        // Always do this at the beginning of InspectorGUI
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        DisplayGrid();
        // Apply changes to all serializedProperties
        // always do this at the end of OnInspectorGUI 
        serializedObject.ApplyModifiedProperties();
    }

    private void InitNewGrid(int newSize)
    {
        cells.ClearArray();

        for(int i = 0; i < newSize; i++)
        {
            cells.InsertArrayElementAtIndex(i);
            SerializedProperty row = GetRowAt(i);

            for(int j = 0; j < newSize; j++)
            {
                row.InsertArrayElementAtIndex(j);
            }
        }
    }

    private void DisplayGrid()
    {
        float maxWordSize = 0f;
        string[] elementNames = Element.GetNames(typeof(Element));
        foreach (string elementName in elementNames) {
            if (elementName.Length > maxWordSize) {
                maxWordSize = elementName.Length;
            }
        }
        // Make the maxWordSize wide enough to fit all the element names
        maxWordSize *= singleCharacterWidth;

        // Print vertical labels above grid
        Rect placePosition = new Rect(maxWordSize, maxWordSize*1.5f, maxWordSize, maxWordSize);
        Vector2 pivot = placePosition.position;
        EditorGUIUtility.RotateAroundPivot(-90f, pivot);
        for (int i = 0; i < elementNames.Length; ++i) {
            EditorGUI.LabelField(placePosition, elementNames.GetValue(i).ToString());
            placePosition.y += spaceBetweenCells;
        }
        EditorGUIUtility.RotateAroundPivot(90f, pivot);

        //Print horizontal labels left of grid
        placePosition.x = spaceBetweenCells;
        placePosition.y = maxWordSize*1.6f;
        for (int i = 0; i < elementNames.Length; ++i) {
            EditorGUI.LabelField(placePosition, elementNames.GetValue(i).ToString());
            placePosition.y += spaceBetweenCells;
        }

        // Display grid
        placePosition.size = new Vector2(defaultCellSize, defaultCellSize);
        placePosition.x = maxWordSize;
        placePosition.y = maxWordSize*1.6f;
        for (int i = 0; i < gridSize.intValue; ++i) {
            SerializedProperty row = GetRowAt(i);
            // Columns
            placePosition.x = maxWordSize;
            for (int j = 0; j < gridSize.intValue; ++j) {
                EditorGUI.PropertyField(placePosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                placePosition.x += spaceBetweenCells;
            }

            placePosition.y += spaceBetweenCells;
            GUILayout.Space(defaultCellSize);
        }
    }

    private SerializedProperty GetRowAt(int index)
    {
        return cells.GetArrayElementAtIndex(index).FindPropertyRelative("row");
    }
}
#endif
