using System;
using System.Linq;
using System.Text;
using gifty.Shared.Exceptions;
using gifty.Shared.Neo4j;

namespace gifty.Shared.Builders
{
    public class CypherQueryBuilder : ICypherQueryBuilder
    {
        private static StringBuilder _query;

        private CypherQueryBuilder(StringBuilder query)
        {
            _query = query;
        }

        public static ICypherQueryBuilder Init()       
            => new CypherQueryBuilder(new StringBuilder());

        public ICypherQueryBuilder Match(string pattern, object[] nodes, string[] variables = null, bool isDefaultValueFilter = false)
        {
            if(variables != null && nodes.Length != variables.Length)
                throw new GiftyException(ErrorType.IncorrectData, "Invalid number of params or nodes in Cypher's query");

            var cypherNodes = nodes.Select((n, index) => 
                { 
                    return (variables!= null) ? Neo4jNodeTransformer.Transform(n, isDefaultValueFilter, variables[index])
                                              : Neo4jNodeTransformer.Transform(n, isDefaultValueFilter, null);
                }).ToArray();

            return Match(pattern, cypherNodes);
        }

        private ICypherQueryBuilder Match(string pattern, dynamic cypherNodes)
        {
            var cypherPattern = String.Format(pattern, cypherNodes);

            _query.AppendLine($"MATCH {cypherPattern}");
            return this;
        }

        public ICypherQueryBuilder Create(string pattern, object node, string variable)
        {
            var cypherNode = Neo4jNodeTransformer.Transform(node, false, variable);
            var cypherPattern = String.Format(pattern, cypherNode);

            _query.AppendLine($"CREATE {cypherPattern}");
            return this;
        }        

        public ICypherQueryBuilder Where(string condition)
        {
            _query.AppendLine($"WHERE {condition}");
            return this;
        }

        public ICypherQueryBuilder With(string accessors)
        {
            _query.AppendLine($"WITH {accessors}");
            return this;
        }

        public ICypherQueryBuilder OrderBy(string pattern)
        {
            _query.AppendLine($"ORDER BY {pattern}");
            return this;
        }

        public ICypherQueryBuilder Skip(uint skipNumber)
        {
            _query.AppendLine($"SKIP {skipNumber}");
            return this;
        }

        public ICypherQueryBuilder Take(uint takeNumber)
        {
            _query.AppendLine($"TAKE {takeNumber}");
            return this;
        }

        public ICypherQueryBuilder Query(string query)
        {
            _query.AppendLine(query);
            return this;
        }

        public string Return(string result)
        {
            _query.AppendLine($"RETURN {result}");
            return _query.ToString();
        }     

        public string Delete(string variable)
        {
            _query.AppendLine($"DELETE {variable}");
            return _query.ToString();
        }
    }
}