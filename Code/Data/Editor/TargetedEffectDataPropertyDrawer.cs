using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(TargetedEffectData))]
public class TargetedEffectDataPropertyDrawer :  PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect propRect = position;
        propRect.height = EditorGUIUtility.singleLineHeight;

        var obj = (SkillEffectData)property.boxedValue;
        GUI.Label(propRect, obj.SkillEffectType().ToString());
        propRect.y += propRect.height;

        var skillPower = property.FindPropertyRelative("skillPower");
        EditorGUI.PropertyField(propRect, skillPower);
        propRect.y += propRect.height;

        var targetTeam = property.FindPropertyRelative("targetTeam");
        EditorGUI.PropertyField(propRect, targetTeam);
        propRect.y += propRect.height;

        if (((TargetedEffectData.ETargetTeam)targetTeam.enumValueIndex) != TargetedEffectData.ETargetTeam.Self)
        {
            var targetGroup = property.FindPropertyRelative("targetGroup");
            EditorGUI.PropertyField(propRect, targetGroup);
            propRect.y += propRect.height;

            if (((TargetedEffectData.ETargetGroup)targetGroup.enumValueIndex) != TargetedEffectData.ETargetGroup.All)
            {
                var count = property.FindPropertyRelative("count");
                EditorGUI.PropertyField(propRect, count);
                propRect.y += propRect.height;
        
                var priority = property.FindPropertyRelative("priority");
                EditorGUI.PropertyField(propRect, priority);
                propRect.y += propRect.height;
            }
        }

        EditorGUI.EndProperty();
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lineCount = 4;
        
        var targetTeam = property.FindPropertyRelative("targetTeam");
        if (((TargetedEffectData.ETargetTeam)targetTeam.enumValueIndex) != TargetedEffectData.ETargetTeam.Self)
        {
            lineCount++;
                
            var targetGroup = property.FindPropertyRelative("targetGroup");
            if (((TargetedEffectData.ETargetGroup)targetGroup.enumValueIndex) != TargetedEffectData.ETargetGroup.All)
            {
                lineCount+=2;
                
                var priority = property.FindPropertyRelative("priority");
                if (priority.isExpanded)
                {
                    lineCount += priority.arraySize + 1;    
                }
                
            }
        }

        
        return EditorGUIUtility.singleLineHeight * lineCount + EditorGUIUtility.standardVerticalSpacing * (lineCount-1);
    }
}

/*
[CustomPropertyDrawer(typeof(SE_Damage_Data))]
public class SkillEffectDamageDataPropertyDrawer :  PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();
       
        var skillPower = property.FindPropertyRelative("skillPower");
        var skillPowerField = new PropertyField(skillPower);
        container.Add(skillPowerField);
        
        var targetTeam = property.FindPropertyRelative("targetTeam");
        var targetTeamField = new PropertyField(targetTeam);
        container.Add(targetTeamField);

        if (((SkillEffectData.ETargetTeam)targetTeam.enumValueIndex) != SkillEffectData.ETargetTeam.Self)
        {
            var targetGroup = property.FindPropertyRelative("targetGroup");
            var targetGroupField = new PropertyField(targetGroup);
            container.Add(targetGroupField);

            if (((SkillEffectData.ETargetGroup)targetGroup.enumValueIndex) != SkillEffectData.ETargetGroup.All)
            {
                var count = property.FindPropertyRelative("count");
                var countField = new PropertyField(count);
                container.Add(countField);
        
                var priority = property.FindPropertyRelative("priority");
                var priorityField = new PropertyField(priority);
                container.Add(priorityField);    
            }
        }

        return container;
    }
}

[CustomPropertyDrawer(typeof(SE_Cure_Data))]
public class SkillEffectCureDataPropertyDrawer :  PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();
       
        var skillPower = property.FindPropertyRelative("skillPower");
        var skillPowerField = new PropertyField(skillPower);
        container.Add(skillPowerField);
        
        var targetTeam = property.FindPropertyRelative("targetTeam");
        var targetTeamField = new PropertyField(targetTeam);
        container.Add(targetTeamField);

        if (((SkillEffectData.ETargetTeam)targetTeam.enumValueIndex) != SkillEffectData.ETargetTeam.Self)
        {
            var targetGroup = property.FindPropertyRelative("targetGroup");
            var targetGroupField = new PropertyField(targetGroup);
            container.Add(targetGroupField);

            if (((SkillEffectData.ETargetGroup)targetGroup.enumValueIndex) != SkillEffectData.ETargetGroup.All)
            {
                var count = property.FindPropertyRelative("count");
                var countField = new PropertyField(count);
                container.Add(countField);
        
                var priority = property.FindPropertyRelative("priority");
                var priorityField = new PropertyField(priority);
                container.Add(priorityField);    
            }
        }

        return container;
    }
}
*/
