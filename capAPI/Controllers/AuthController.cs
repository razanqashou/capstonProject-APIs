using capAPI.DTOs.Request;
using capAPI.DTOs.Responce;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace capAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginInput input)
        {
            var Response = new LoginOutput();
            try
            {
                if (string.IsNullOrEmpty(input.Email) && string.IsNullOrEmpty(input.Password))
                    throw new Exception("invaild email or pass");

                string conn = "Server=MSI\\SQLEXPRESS13;Database=capstoneProjectDB.bacpac;TrustServerCertificate=True;";
                SqlConnection connection = new SqlConnection(conn);
                string QUERY = $"SELECT userID , CONCAT(FirstName,' ',LastName) AS FullName FROM USERS WHERE EMAIL ={input.Email} ='{input.Password}'  And RoleID=3 ";
                SqlCommand command = new SqlCommand(QUERY, connection);
                command.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                //Mapping Extract
                if (dataTable.Rows.Count == 0)
                    throw new Exception("Invalid Email / Password");
                if (dataTable.Rows.Count > 1)
                    throw new Exception("Query Contains More Than One Element");
                foreach (DataRow row in dataTable.Rows)
                {

                    Response.Id = Convert.ToInt32(row["userID"]);
                    Response.Name = row["FullName"].ToString();
                }
                return Ok(Response);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }
    }
}
