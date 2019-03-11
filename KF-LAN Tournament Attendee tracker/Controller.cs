using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KF_LAN_Tournament_Attendee_tracker
{
    public class Controller
    {
        public class Attendee
        {
            public string Name { get; set; }
            public string Table { get; set; }
            public string Seat { get; set; }
            public string Tournament { get; set; }
            public string Team { get; set; }

        }

        string connectionString = "Server=EALSQL1.eal.local; Database= A_DB31_2018; User Id=A_STUDENT31; Password=A_OPENDB31;";

        public void AddAttendees(string Name, string Table, string Seat, string Tournament, string Team)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand AddAttendee = new SqlCommand("AddAttendee", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    AddAttendee.Parameters.Add(new SqlParameter("@AttendeeName", Name));
                    AddAttendee.Parameters.Add(new SqlParameter("@AttendeeTable", Table));
                    AddAttendee.Parameters.Add(new SqlParameter("@AttendeeSeat", Seat));
                    AddAttendee.Parameters.Add(new SqlParameter("@AttendeeTournament", Tournament));
                    AddAttendee.Parameters.Add(new SqlParameter("@AttendeeTeam", Team));

                    AddAttendee.ExecuteNonQuery();


                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        public void GetAttendees(MainWindow mainWindow)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand GetAttendees = new SqlCommand("GetAttendee", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    SqlDataReader reader = GetAttendees.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string AttendeeName = reader["AttendeeName"].ToString();

                            mainWindow.ListOfAttendees.Items.Add(new Attendee {
                                Name = reader["AttendeeName"].ToString(),
                                Table = reader["AttendeeTable"].ToString(),
                                Seat = reader["AttendeeSeat"].ToString(),
                                Team = reader["AttendeeTeam"].ToString(),
                                Tournament = reader["AttendeeTournament"].ToString()
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        public void DeleteAttendee(string Name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand DeleteAttendee = new SqlCommand("DeleteAttendee", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    DeleteAttendee.Parameters.Add(new SqlParameter("@AttendeeName", Name));

                    DeleteAttendee.ExecuteNonQuery();


                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }
    }
}
