using Hackaton.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Hackaton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TaskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT TaskId,
                                    TaskName,
                                    TaskDescription,
                                    convert(varchar(10),DateOfCreation,120) as DateOfCreation,
                                    convert(varchar(10),DateOfExpiration,120) as DateOfExpiration,
                                    ExpectedDuration
                             FROM
                                    dbo.Task";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TaskAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]

        public JsonResult Post(Task task)
        {
            string query = @"INSERT INTO 
                                    dbo.Task (TaskName,TaskDescription,DateOfCreation,DateOfExpiration,ExpectedDuration)
                            VALUES
                            (
                            '" + task.TaskName + @"'
                            ,'" + task.TaskDescription + @"'
                            ,'" + task.DateOfCreation + @"'
                            ,'" + task.DateOfExpiration + @"'
                            ,'" + task.ExpectedDuration + @"'
                            )";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TaskAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added succesfully");
        }

        [HttpPut]
        public JsonResult Put(Task task)
        {
            string query = @"UPDATE dbo.Task SET 
                            TaskName = '" + task.TaskName + @"'
                            ,TaskDescription = '" + task.TaskDescription + @"'
                            ,DateOfCreation = '" + task.DateOfCreation + @"'
                            ,DateOfExpiration = '" + task.DateOfExpiration + @"'
                            ,ExpectedDuration = '" + task.ExpectedDuration + @"'
                            where TaskId = " + task.TaskId + @"
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TaskAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated succesfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE FROM
                                dbo.Task
                             WHERE TaskId = " + id + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TaskAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted succesfully");
        }

    }
}
