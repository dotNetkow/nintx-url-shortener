using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NintxUrlShortener.Storage
{
    public class UrlManager
    {
        public static string InsertUrl(string longUrl) {

            int foundId;
            string encodedValue;
            string addUrlQuery = @"INSERT INTO Urls (EncodedUrl, OrigUrl) VALUES ('empty', @OrigUrl)";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(addUrlQuery, connection))
            {
                // Insert original URL
                command.Parameters.AddWithValue("@OrigUrl", longUrl);

                connection.Open();
                command.ExecuteNonQuery();

                // Retrieve current database index for use as unique encoded Id
                using (SqlCommand identityCommand = new SqlCommand("Select IDENT_CURRENT('Urls')", connection))
                {
                    SqlDataReader reader = identityCommand.ExecuteReader();
                    reader.Read();
                    foundId = int.Parse(reader[0].ToString());
                    reader.Close();

                    encodedValue = UrlProcessing.UrlProcessor.Encode(foundId);
                }
                
                // Update existing row, adding newly computed url
                string updateQuery = @"UPDATE Urls SET EncodedUrl = @Encoded WHERE ID = @Id";
                using (SqlCommand updateUrlCommand = new SqlCommand(updateQuery, connection))
                {
                    updateUrlCommand.Connection = connection;
                    updateUrlCommand.Parameters.AddWithValue("@Encoded", encodedValue);
                    updateUrlCommand.Parameters.AddWithValue("@Id", foundId);

                    updateUrlCommand.ExecuteNonQuery();
                }

                connection.Close();
            }

            return encodedValue;
        }

        /// <summary>
        /// Retrieve the original url using the encoded Id.
        /// </summary>
        /// <param name="encodedId">short url value</param>
        /// <returns>original long url</returns>
        public static string GetUrlByEncodedId(string encodedId) {
            string originalUrl = "";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (SqlCommand identityCommand = new SqlCommand("SELECT OrigUrl FROM Urls WHERE EncodedUrl = @EncodedUrl", connection))
            {
                connection.Open();

                SqlDataReader reader = identityCommand.ExecuteReader();
                reader.Read();

                if (reader[0] != null)
                {
                    originalUrl = reader[0].ToString();
                }

                connection.Close();
            }

            return originalUrl;
        }
    }
}
