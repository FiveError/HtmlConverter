using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace DatabaseProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        [HttpPut("{id_converter}")]
        public string AddDocumentToQueue(int id_converter)
        {
            string filename = "";
            DB.Instance.RunSqlRequestQuery("BEGIN TRANSACTION");
            string SqlRequest = $"SELECT * FROM documents LIMIT 1";
            var result = DB.Instance.RunSqlRequestReader(SqlRequest);
            while (result.Read())
            {
                filename = Convert.ToString(result["filename"]);
            }
            if (!string.IsNullOrEmpty(filename))
            {
                DB.Instance.RunSqlRequestQuery($"INSERT INTO queues (id_converter,filename) VALUES({id_converter},'{filename}')");
                DB.Instance.RunSqlRequestQuery($"DELETE FROM documents WHERE filename = '{filename}'");
            }
            DB.Instance.RunSqlRequestQuery("COMMIT");
            return filename;
        }

        [HttpGet("{id_converter}")]
        public string GetDocumentFromQueue(int id_converter)
        {
            string filename = "";
            DB.Instance.RunSqlRequestQuery("BEGIN TRANSACTION");
            string SqlRequest = $"SELECT * FROM queues WHERE id_converter = {id_converter}";
            var result = DB.Instance.RunSqlRequestReader(SqlRequest);
            while (result.Read())
            {
                filename = Convert.ToString(result["filename"]);
            }
            DB.Instance.RunSqlRequestQuery("COMMIT");
            return filename;
        }

       [HttpDelete("{id_converter}")]
       public string RemoveFromQueue(int id_converter)
        {
            string filename = "";
            DB.Instance.RunSqlRequestQuery("BEGIN TRANSACTION");
            string SqlRequest = $"SELECT * FROM queues WHERE id_converter = {id_converter}";
            var result = DB.Instance.RunSqlRequestReader(SqlRequest);
            while (result.Read())
            {
                filename = Convert.ToString(result["filename"]);
            }
            if (!string.IsNullOrEmpty(filename))
            {
                DB.Instance.RunSqlRequestQuery($"DELETE FROM queues WHERE id_converter = {id_converter}");
            }
            DB.Instance.RunSqlRequestQuery("COMMIT");
            return filename;
        }

       [HttpPost("{filename}")]
       public int InsertFilenameToDocuments(string filename)
        {
            DB.Instance.RunSqlRequestQuery("BEGIN TRANSACTION");
            string SqlRequest = $"INSERT INTO documents (filename) VALUES ('{filename}')";
            DB.Instance.RunSqlRequestQuery(SqlRequest);
            DB.Instance.RunSqlRequestQuery("COMMIT");
            return 0;
        }

    }
}
