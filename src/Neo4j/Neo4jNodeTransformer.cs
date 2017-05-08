using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace gifty.Shared.Neo4j
{
    public static class Neo4jNodeTransformer
    {
        public static string Transform(object node, string variable = null)
        {
            var nodeType = node.GetType();
            var nodeLabel = nodeType.Name;
            var stringBuilder = new StringBuilder();

            stringBuilder
                .Append("(")
                .AppendIfNotNull(variable, ":")
                .Append($"{nodeLabel}")
                .Append("{");

            var nodePropertyInfos = nodeType.GetProperties();
            var lastNodePropertyInfo = nodePropertyInfos.Last();

            foreach(var propertyInfo in nodePropertyInfos)
            {
                var propertyName = propertyInfo.Name.ToPascalCase();
                var propertyValue = propertyInfo.GetValue(node, null);

                stringBuilder.Append($"{propertyName}: '{propertyValue}'");

                if(!propertyInfo.Equals(lastNodePropertyInfo))
                    stringBuilder.Append(", ");
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }

        private static string ToPascalCase(this string text)
        {
            var firstChar = Char.ToLower(text[0]);
            var leftText = text.Skip(1).Select(l => l).ToArray();
            return firstChar + new string(leftText);  
        }

        private static StringBuilder AppendIfNotNull(this StringBuilder stringBuilder, string value, string additional = null)
        {
            return (value == null) ? stringBuilder 
                 : (additional == null) ? stringBuilder.Append(value) 
                 :  stringBuilder.Append(value + additional);
        }
    }
}