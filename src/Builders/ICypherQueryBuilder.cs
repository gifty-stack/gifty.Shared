using System;

namespace gifty.Shared.Builders
{
    public interface ICypherQueryBuilder
    {
         ICypherQueryBuilder Match(string pattern, object[] nodes, string[] variables = null, bool isDefaultValueFilter = false);
         ICypherQueryBuilder Create(string pattern, object node, string variable);        
         ICypherQueryBuilder Where(string condition);
         ICypherQueryBuilder With(string accessors);
         ICypherQueryBuilder Skip(uint skipNumber);
         ICypherQueryBuilder Take(uint takeNumber);
         ICypherQueryBuilder OrderBy(string pattern);
         ICypherQueryBuilder Query(string query);
         string Return(string result);
         string Delete(string variable);
    }
}