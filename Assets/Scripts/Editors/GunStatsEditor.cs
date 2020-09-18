using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponStats)), CanEditMultipleObjects]
public class GunstatsEditor : Editor
{
    WeaponStats gunstats;
    public SerializedProperty
        WeaponType,
        Bullet,
        Range,
        BulletSpeed,
        FireRate,
        ExplosionRadius,
        ExplodeTime,
        ShotAngle,
        yTarget,
        ySmoothtime,
        yMaxSpeed,
        xTarget,
        xSmoothtime,
        xMaxSpeed,
        BulletAmount;

    void OnEnable()
    {
        gunstats = (WeaponStats)target;
        WeaponType = serializedObject.FindProperty("WeaponType");
        Bullet = serializedObject.FindProperty("Bullet");
        Range = serializedObject.FindProperty("Range");
        BulletSpeed = serializedObject.FindProperty("bulletspeed");
        FireRate = serializedObject.FindProperty("Firerate");
        ExplosionRadius = serializedObject.FindProperty("ExplosionRadius");
        ExplodeTime = serializedObject.FindProperty("ExplodeTime");
        ShotAngle = serializedObject.FindProperty("ShotAngle");
        yTarget = serializedObject.FindProperty("yTarget");
        ySmoothtime = serializedObject.FindProperty("ySmoothtime");
        yMaxSpeed = serializedObject.FindProperty("yMaxSpeed");
        xTarget = serializedObject.FindProperty("xTarget");
        xSmoothtime = serializedObject.FindProperty("xSmoothtime");
        xMaxSpeed = serializedObject.FindProperty("xMaxSpeed");
        BulletAmount = serializedObject.FindProperty("BulletAmount");
    }
    public override void OnInspectorGUI()
    {
        EditorUtility.SetDirty(target);
        serializedObject.Update();

        EditorGUILayout.PropertyField(WeaponType);

        WeaponStats.Weapon_type Type = (WeaponStats.Weapon_type)WeaponType.enumValueIndex;
        switch (Type)
        {
            case WeaponStats.Weapon_type.Rifle:
                gunstats.Bullet = EditorGUILayout.ObjectField("Bullet", gunstats.Bullet, typeof(GameObject), false) as GameObject;
                EditorGUILayout.PropertyField(FireRate, new GUIContent("Fire rate"));
                EditorGUILayout.PropertyField(BulletSpeed, new GUIContent("Bullet speed"));
                EditorGUILayout.PropertyField(Range, new GUIContent("Range"));
                break;

            case WeaponStats.Weapon_type.Shotgun:
                gunstats.Bullet = EditorGUILayout.ObjectField("Bullet", gunstats.Bullet, typeof(GameObject), false) as GameObject;
                EditorGUILayout.PropertyField(FireRate, new GUIContent("Fire rate"));
                EditorGUILayout.PropertyField(BulletSpeed, new GUIContent("Bullet speed"));
                EditorGUILayout.PropertyField(Range, new GUIContent("Range"));
                EditorGUILayout.PropertyField(ShotAngle, new GUIContent("Shot angle"));
                EditorGUILayout.PropertyField(BulletAmount, new GUIContent("Bullet amount"));
                break;

            case WeaponStats.Weapon_type.GrenadeLauncher:
                gunstats.Bullet = EditorGUILayout.ObjectField("Bullet", gunstats.Bullet, typeof(GameObject), false) as GameObject;
                EditorGUILayout.PropertyField(FireRate, new GUIContent("Fire rate"));
                EditorGUILayout.PropertyField(BulletSpeed, new GUIContent("Bullet speed"));
                EditorGUILayout.PropertyField(ExplosionRadius, new GUIContent("Explosion Radius"));
                EditorGUILayout.PropertyField(ExplodeTime, new GUIContent("Explode time"));
                EditorGUILayout.PropertyField(ShotAngle, new GUIContent("Shot angle"));
                EditorGUILayout.PropertyField(yTarget, new GUIContent("Y target"));
                EditorGUILayout.PropertyField(ySmoothtime, new GUIContent("Y Smoothtime"));
                EditorGUILayout.PropertyField(yMaxSpeed, new GUIContent("Y Max speed"));
                EditorGUILayout.PropertyField(xTarget, new GUIContent("X target"));
                EditorGUILayout.PropertyField(xSmoothtime, new GUIContent("X Smoothtime"));
                EditorGUILayout.PropertyField(xMaxSpeed, new GUIContent("X max speed"));
                break;
            case WeaponStats.Weapon_type.RocketLauncher:
                gunstats.Bullet = EditorGUILayout.ObjectField("Bullet", gunstats.Bullet, typeof(GameObject), false) as GameObject;
                EditorGUILayout.PropertyField(FireRate, new GUIContent("Fire rate"));
                EditorGUILayout.PropertyField(BulletSpeed, new GUIContent("Bullet speed"));
                EditorGUILayout.PropertyField(ExplosionRadius, new GUIContent("Explosion Radius"));

                break;
            case WeaponStats.Weapon_type.RayGun:
                gunstats.Bullet = EditorGUILayout.ObjectField("Ray", gunstats.Bullet, typeof(GameObject), false) as GameObject;
                EditorGUILayout.PropertyField(FireRate, new GUIContent("Fire rate"));
                break;
            case WeaponStats.Weapon_type.Melee:
                EditorGUILayout.PropertyField(FireRate, new GUIContent("Swing rate"));
                EditorGUILayout.PropertyField(Range, new GUIContent("Swing range"));
                break;
            case WeaponStats.Weapon_type.Watergun:
                gunstats.Bullet = EditorGUILayout.ObjectField("Bullet", gunstats.Bullet, typeof(GameObject), false) as GameObject;
                break;
            case WeaponStats.Weapon_type.Throw:
                gunstats.Bullet = EditorGUILayout.ObjectField("Object", gunstats.Bullet, typeof(GameObject), false) as GameObject;
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}