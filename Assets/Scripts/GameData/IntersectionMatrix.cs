using UnityEngine;

[CreateAssetMenu(fileName = "Intersection Matrix", menuName = "GameData/IntersectionMatrix")]
public class IntersectionMatrix : ScriptableObject
{
    private static int defaultGridSize = Element.GetNames(typeof(Element)).Length;
    public int gridSize = defaultGridSize;
    public CellRow[] cells = new CellRow[defaultGridSize];

    public float GetValue(int column, int row) {
        return cells[column].row[row];
    }
    
    public float[,] GetCells()
    {
        float[,] ret = new float[gridSize, gridSize];
        for (int i = 0; i < gridSize; ++i) {
            for (int j = 0; j < gridSize; ++j){
                ret[i, j] = cells[i].row[j];
            }
        }
        return ret;
    }

    [System.Serializable]
    public class CellRow {
        public float[] row = new float[defaultGridSize];
    }
}
