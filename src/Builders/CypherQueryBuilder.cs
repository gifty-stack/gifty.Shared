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

        public static ICypherQueryBuilder Create()       
            => new CypherQueryBuilder(new StringBuilder());

        ICypherQueryBuilder ICypherQueryBuilder.Match(string pattern, object node)
        {
            var cypherNode = Neo4jNodeTransformer.Transform(node);
            return Match(pattern, cypherNode);
        }

        ICypherQueryBuilder ICypherQueryBuilder.Match(string pattern, object node, string variable)
        {
            var cypherNode = Neo4jNodeTransformer.Transform(node, variable);
            return Match(pattern, cypherNode);
        }

        ICypherQueryBuilder ICypherQueryBuilder.Match(string pattern, object[] nodes)
        {
            var cypherNodes = nodes.Select(n => Neo4jNodeTransformer.Transform(n)).ToArray();
            return Match(pattern, cypherNodes);
        }

        ICypherQueryBuilder ICypherQueryBuilder.Match(string pattern, object[] nodes, string[] variables)
        {
            if(nodes.Length != variables.Length)
                throw new GiftyException(ErrorType.IncorrectData, "Invalid number of params or nodes in Cypher's query");

            var cypherNodes = nodes.Select((n, index) => Neo4jNodeTransformer.Transform(n, variables[index])).ToArray();
            return Match(pattern, cypherNodes);
        }

        ICypherQueryBuilder Match(string pattern, dynamic cypherNodes)
        {
            var cypherPattern = String.Format(pattern, cypherNodes);

            _query.AppendLine($"MATCH {cypherPattern}");
            return this;
        }

        ICypherQueryBuilder ICypherQueryBuilder.Where(string condition)
        {
            _query.AppendLine($"WHERE {condition}");
            return this;
        }

        ICypherQueryBuilder ICypherQueryBuilder.With(string accessors)
        {
            _query.AppendLine($"WITH {accessors}");
            return this;
        }

        ICypherQueryBuilder ICypherQueryBuilder.OrderBy(string pattern)
        {
            _query.AppendLine($"ORDER BY {pattern}");
            return this;
        }

        ICypherQueryBuilder ICypherQueryBuilder.Skip(uint skipNumber)
        {
            _query.AppendLine($"SKIP {skipNumber}");
            return this;
        }

        ICypherQueryBuilder ICypherQueryBuilder.Take(uint takeNumber)
        {
            _query.AppendLine($"TAKE {takeNumber}");
            return this;
        }

        ICypherQueryBuilder ICypherQueryBuilder.Query(string query)
        {
            _query.AppendLine(query);
            return this;
        }

        string ICypherQueryBuilder.Return(string result)
        {
            _query.AppendLine($"RETURN {result}");
            return _query.ToString();
        }        
    }
}