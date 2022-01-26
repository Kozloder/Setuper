using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Installers
{
    public class Util
    {
        /*
        public static bool PropertyContainsAttribute<A>(SerializedProperty property) where A : Attribute
        {
            var targetObject = property.serializedObject.targetObject;
            var propertyTargetType = targetObject.GetType();

            var targetField = propertyTargetType.GetField(property.propertyPath);

            if (targetField != null)
            {
                //Debug.Log("hear");

                var attributes = targetField.GetCustomAttributes(typeof(A), true);

                return attributes.Length > 0;
            }

            return false;
        }

        public static FieldInfo[] GetFieldsWithAttribute<A>(Type type) where A : Attribute
        {
            var fieldsWithAttribute = new List<FieldInfo>();

            var attributeType = typeof(A);
            var allFields = type.GetFields(BindingFlags.NonPublic|
                                           BindingFlags.Public   | 
                                           BindingFlags.Instance);

            for (int i = 0; i < allFields.Length; i++)
            {
                var field = allFields[i];

                if (field.GetCustomAttributes(attributeType, true).Length > 0)
                {
                    fieldsWithAttribute.Add(field);
                }
            }

            return fieldsWithAttribute.ToArray();
        }*/

        public static string TreeOfSetups(CompositeSetup root)
        {
            var builder = new StringBuilder();

            void Tree(CompositeSetup setup, int offset)
            {
                var setupName = setup.name;

                if (offset > 0)
                    builder.AppendLine();

                builder.Append(' ', offset);
                builder.Append(setupName);

                for (int i = 0; i < setup.setups.Count; i++)
                {
                    var childSetup = setup.setups[i];

                    if (childSetup is CompositeSetup composite)
                    {
                        Tree(composite, offset + setupName.Length - 1);
                    }
                    else
                    {
                        builder.AppendLine();
                        builder.Append(' ', offset);
                        builder.Append(childSetup.name);
                    }
                }
            }

            Tree(root, 0);

            return builder.ToString();
        }
    }
}