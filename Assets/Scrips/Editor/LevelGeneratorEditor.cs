using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelGenerator generator = (LevelGenerator)target;
        EditorGUILayout.Space();

        generator.chunkMode = EditorGUILayout.Toggle("Chunk mode", generator.chunkMode);
        generator.slopeLeftChance = EditorGUILayout.IntSlider("Slope up chance", generator.slopeLeftChance, 0, 100);
        generator.slopeRightChance = EditorGUILayout.IntSlider("Slope down chance", generator.slopeRightChance, 0, 100 - generator.slopeLeftChance);
        generator.cliffLeftChance = EditorGUILayout.IntSlider("Cliff up chance", generator.cliffLeftChance, 0, 100 - generator.slopeLeftChance - generator.slopeRightChance);
        generator.cliffRightChance = EditorGUILayout.IntSlider("Cliff down chance", generator.cliffRightChance, 0, 100 - generator.slopeLeftChance - generator.slopeRightChance - generator.cliffLeftChance);
        //generator.groundChance = (int)EditorGUILayout.IntSlider("Flat ground chance", 0, 0, 100);
        if (!generator.chunkMode)
            return;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Chunk settings:");
        generator.groundRange = EditorGUILayout.Vector2IntField("Ground length", generator.groundRange);
        generator.slopeLeftRange = EditorGUILayout.Vector2IntField("Upwards slope size", generator.slopeLeftRange);
        generator.slopeRightRange = EditorGUILayout.Vector2IntField("Downwards slope size", generator.slopeRightRange);
        generator.cliffLeftRange = EditorGUILayout.Vector2IntField("Upwards cliff height", generator.cliffLeftRange);
        generator.cliffRightRange = EditorGUILayout.Vector2IntField("Downwards cliff height", generator.cliffRightRange);
    }
}
