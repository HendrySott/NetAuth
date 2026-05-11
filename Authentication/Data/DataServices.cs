
using Authentication;
using MySqlConnector;


namespace NetEmployee.Data;


public class DataServices
{


    public string ConnectionString { get; set; }

    public DataServices(string connectionString)
    {
        ConnectionString = connectionString;
    }


    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
   

    private static Random random = new Random();

    public static string RandomStringGenerator(int length)
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

     public IList<AuthenticationModel>? userLogin(AuthenticationModel data)
    {
       

        List<AuthenticationModel> listx = new List<AuthenticationModel>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE  PASSWORD = '{data.PASSWORD}' and EMAIL = '{data.EMAIL}' ";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        listx.Add(new AuthenticationModel()
                        {

                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            ROLE = reader.GetString("ROLE"),
                            EMAIL = reader.GetString("EMAIL"),
                            // PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),

                        });

                    }
                }

                int result = cmd.ExecuteNonQuery();
                if(listx.Count() == 0){
                    string query1 = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE PERSONAL_EMAIL = '{data.EMAIL}' and PASSWORD = '{data.PASSWORD}'";
                
                    MySqlCommand cmd2 = new MySqlCommand(query1, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            listx.Add(new AuthenticationModel()
                            {

                                CODE = reader.GetInt32("CODE"),
                                INSTITUTION = reader.GetString("INSTITUTION"),
                                PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                                ROLE = reader.GetString("ROLE"),
                                // EMAIL = reader.GetString("EMAIL"),
                                // PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),

                            });

                        }
                    }

                    int result1 = cmd2.ExecuteNonQuery();
                //lblError.Text = "Data Saved";                         
                }
                //lblError.Text = "Data Saved";
            }
            catch (Exception e)
            {
               Console.Write(e);
            }
        }
       
        return listx;

        

    }

    public IList<AuthenticationModel>? LoadUserFromLogin(AuthenticationModel data)
    {
        int a = 0;

        List<AuthenticationModel> list = new List<AuthenticationModel>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE PERSONAL_EMAIL = '{data.EMAIL}' and PASSWORD = '{data.PASSWORD}'";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        list.Add(new AuthenticationModel()
                        {

                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            ROLE = reader.GetString("ROLE"),
                            // PASSWORD = reader.GetString("PASSWORD"),
                            // PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),

                        });

                    }
                }

                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
       
        return list;

        

    }

     

    public IList<AuthenticationModel> ValidateEmail(AuthenticationModel data)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE EMAIL = '{data.EMAIL}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                        });
                    }
                }
                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }
    public IList<AuthenticationModel> ValidatePSWD(AuthenticationModel data)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE PASSWORD = '{data.PASSWORD}' and CODE = {data.CODE} and (INSTITUTION = '{data.INSTITUTION}')";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {
                            STATE = reader.GetInt32("STATE"),

                        });
                    }
                }
                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }

    public IEnumerable<InstitutionModel> LoadInstitutionDetails(string institution)
    {
        List<InstitutionModel> list = new List<InstitutionModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`INSTITUTIONPROFILE` WHERE INSTITUTION = '{institution}';";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new InstitutionModel()
                    {
                        NAME = reader.GetString("NAME"),
                        INSTITUTIONALEMAIL = reader.GetString("INSTITUTIONALEMAIL"),
                        DEF_PASS = reader.GetString("DEF_PASS")


                    });
                }
            }
        }
        return list;
    }




    public IList<AuthenticationModel> AuthCashRegister(AuthenticationModel data)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE CODE = {data.CODE} and PASSWORD = '{data.PASSWORD}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {
                            ID = reader.GetInt32("ID"),
                        });
                    }
                }

                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }

    public IList<AuthenticationModel> GetAuthData(string institution, int code)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {

                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE CODE = {code} and INSTITUTION = '{institution}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {
                            ID = reader.GetInt32("ID"),
                            EMAIL = reader.GetString("EMAIL"),
                            STATE = reader.GetInt32("STATE"),

                            PERSONAL_EMAIL = reader.GetString("AlT_EMAIL")

                        });
                    }
                }

                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }
    public IList<AuthenticationModel> AuthWagePayment(AuthenticationModel data)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE CODE = {data.CODE} and (PASSWORD = '{data.PASSWORD}') and INSTITUTION = '{data.INSTITUTION}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {

                            CODE = reader.GetInt32("CODE"),
                        });
                    }
                }

                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }






    public AuthenticationModel IUpdatePassword(string npaww, int code, string institution)
    {
        string query = $"UPDATE `admbasic`.`AUTHENTICATION`SET `PASSWORD` = '{npaww}' WHERE CODE = {code} and( INSTITUTION = '{institution}');";
        Executor(query);
        return null;
    }

    

    public AuthenticationModel IRegisterNewUser(AuthenticationModel data)
    {
        string auth = $"INSERT INTO `admbasic`.`AUTHENTICATION`(`INSTITUTION`,`CODE`,`EMAIL`,`PASSWORD`,`STATE`)VALUES ('{data.INSTITUTION}',{data.CODE}, '{data.EMAIL}', '{data.PASSWORD}',{data.STATE});";

        Executor(auth);
        return data;
    }


    /// <summary>
    /// usless 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    public IList<PersonProfile> ILoadAllUserByUID(string institution, int id)
    {
        List<PersonProfile> list = new List<PersonProfile>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE INSTITUTION = '{institution}' and CODE = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new PersonProfile()
                    {

                        CODE = reader.GetInt32("CODE"),
                        INSTITUTION = reader.GetString("INSTITUTION"),
                        F_NAME = reader.GetString("F_NAME"),
                        F_LASTN = reader.GetString("F_LASTN"),
                        ADMITION_DATE = reader.GetString("ADMITION_DATE"),
                        PHONE = reader.GetString("PHONE"),
                        PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                        TEAM_ID = reader.GetInt32("TEAM_ID"),
                        POSITION_KEY = reader.GetInt32("POSITION_KEY")
                        
                        



                    });
                }
            }
        }

        return list;
    }
    
    /// <summary>
    /// load speficic user by id and institution, just take one
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="institution"></param>
    /// <returns></returns>
    public IList<PersonProfile> ILoadAllUserByUIDINS(int uid, string institution)
    {
        List<PersonProfile> list = new List<PersonProfile>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE CODE = {uid} and INSTITUTION = '{institution}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new PersonProfile()
                    {

                        CODE = reader.GetInt32("CODE"),
                        INSTITUTION = reader.GetString("INSTITUTION"),
                        F_NAME = reader.GetString("F_NAME"),
                        S_NAME = reader.GetString("S_NAME"),
                        F_LASTN = reader.GetString("F_LASTN"),
                        S_LASTN = reader.GetString("S_LASTN"),



                    });
                }
            }
        }

        return list;
    }
    // public IList<PersonProfile> ILoadAllUserByUIDINSandAuthInformation(int uid, string institution)
    // {
    //     List<PersonProfile> list = new List<PersonProfile>();

    //     using (MySqlConnection conn = GetConnection())
    //     {
    //         conn.Open();
    //         string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE CODE = {uid} and INSTITUTION = '{institution}'";
    //         MySqlCommand cmd = new MySqlCommand(query, conn);
    //         using (MySqlDataReader reader = cmd.ExecuteReader())
    //         {
    //             while (reader.Read())
    //             {
    //                 list.Add(new PersonProfile()
    //                 {

    //                     CODE = reader.GetInt32("CODE"),
    //                     INSTITUTION = reader.GetString("INSTITUTION"),
    //                     F_NAME = reader.GetString("F_NAME"),
    //                     S_NAME = reader.GetString("S_NAME"),
    //                     F_LASTN = reader.GetString("F_LASTN"),
    //                     S_LASTN = reader.GetString("S_LASTN"),
    //                     te = reader.GetInt32("DEPARTMENT_KEY"),
    //                     POSITION_KEY = reader.GetInt32("POSITION_KEY")



    //                 });
    //             }
    //         }
    //     }

    //     return list;
    // }
    /// <summary>
    /// Load all user by institution code
    /// </summary>
    /// <param name="institution"></param>
    /// <returns></returns>
    public IList<PersonProfile> ILoadAllUserOnTheInstitutionByUIDINS(string institution)
    {
        List<PersonProfile> list = new List<PersonProfile>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE INSTITUTION = '{institution}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new PersonProfile()
                    {

                        CODE = reader.GetInt32("CODE"),
                        INSTITUTION = reader.GetString("INSTITUTION"),
                        F_NAME = reader.GetString("F_NAME"),
                        S_NAME = reader.GetString("S_NAME"),
                        F_LASTN = reader.GetString("F_LASTN"),
                        S_LASTN = reader.GetString("S_LASTN"),



                    });
                }
            }
        }

        return list;
    }




    public List<PersonProfile> TestUserExistanceByID(string ID,string EMAIL)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE `IDENTIFICATION` = '{ID}' and PERSONAL_EMAIL = '{EMAIL}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {
                       
                            CODE = reader.GetInt32("CODE"),
                           
                            INSTITUTION = reader.GetString("INSTITUTION"),


                        });
                    }
                }
            }

            return list;
        }

        public PersonProfile registerNewPerson(PersonProfile data)
        {
            
                ///TODO if code exist get a new random
                
               
                    string query = $"INSERT INTO `admbasic`.`PERSON`(`F_NAME`,`S_NAME`,`F_LASTN`,`S_LASTN`,`GRADE`,`ADDRESS`,`PHONE`,`IDENTIFICATION`,`PERSONAL_EMAIL`,`TEAM_ID`,`POSITION_KEY`,`STREET`,`HOME`,`COUNTY`,`COUNTRY`,`ADMITION_DATE`, `DATATR`) VALUE" +
                                   $"('{data.F_NAME}','{data.S_LASTN}','{data.F_LASTN}','{data.S_LASTN}','{data.GRADE}','{data.ADDRESS}','{data.PHONE}','{data.IDENTIFICATION}','{data.PERSONAL_EMAIL}',{data.TEAM_ID},{data.POSITION_KEY},'{data.STREET}',{data.HOME},'{data.COUNTY}','{data.COUNTY}',current_date(),'{data.DATA}');";

                    Executor(query);
                    // IncertSystemAccessPolicy(data.CODE.ToString(), data.INSTITUTION);
                    
                //string InstitutionalEmail = data.F_NAME + data.S_NAME + "" + data.F_LASTN + data.S_LASTN + "@adm.com";
                //string DefaultPassword = "adm123";
                ////Todo add data and time, set the name for the institution create method to load institUTION CODE
                //string auth = $" INSERT INTO `admbasic`.`LOGIN`(`INSTITUTION`,`CODE`,`EMAIL`,`PASSWORD`,`STATE`)VALUES ({code}, '{InstitutionalEmail}', '{DefaultPassword}',1);";

                //Executor(auth);
            
            return data;
        }
        public void IncertSystemAccessPolicy(string code,string institution)
        {
           
                string query = $"INSERT INTO `admbasic`.`SYSTEMDIRACCESS`(`CODE`,`INSTITUTION`,`CASHREGISTER`,`ACCOUNTING`,`HHRR`,`SETTINGSAC`,`SETTINGSMAIN`,`MAININTERFACE`,`STATE`,`INVENTORY`) VALUES ({code},'{institution}','garanted','garanted','garanted','garanted','garanted','garanted','enabled','garanted');";

                Executor(query);
                
           
        // }
        // public SetupModel ISetup(SetupModel data)
        // {
        //     string auth = $"INSERT INTO `admbasic`.`SETUPPROGRESS`(`INSTITUTION`) VALUES ('{data.INSTITUTION}');";

        //     Executor(auth);
        //     return data;
            
            
        }
    public InstitutionModel InstitutionIntance(string data)
        {
            string auth = $"INSERT INTO `admbasic`.`INSTITUTIONPROFILE`(`INSTITUTION`)VALUES('{data}')";

            Executor(auth);

            return null;
        }


    public IEnumerable<PositionProfile> GetPositionById(string institution, int id)
        {
            List<PositionProfile> list = new List<PositionProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`POSITIONS` WHERE INSTITUTION = '{institution}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionProfile()
                        {

                            ID = reader.GetInt32("ID"),
                            
                            POSITION_NAME = reader.GetString("POSITION_NAME"),
                            REASON = reader.GetString("REASON"),
                            JOB_PROFILE = reader.GetInt32("JOB_PROFILE"),
                            JOB_QUANTITY = reader.GetInt32("JOB_QUANTITY"),
                            
                            LOCATION_PROFILE = reader.GetInt32("LOCATION_PROFILE"),
                            JOB_TYPE = reader.GetInt32("JOB_TYPE"),
                            CONTRACT_TYPE = reader.GetInt32("CONTRACT_TYPE"),
                            ACADEMIC_GRADE = reader.GetInt32("ACADEMIC_GRADE"),
                            SHIFT = reader.GetInt32("SHIFT"),
                            SKILLS_REQUIRED = reader.GetString("SKILLS_REQUIRED"),
                            TEAM_ID = reader.GetInt32("TEAM_ID"),
                            
                            
                            
                            


                        });
                    }
                }
            }

            return list;
        }

        public IList<InstitutionModel> GetInstitutionNameUsingInstitutionCode(string data)
        {
            

            List<InstitutionModel> list = new List<InstitutionModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`INSTITUTIONPROFILE` WHERE INSTITUTION = '{data}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new InstitutionModel()
                        {

                       
                            NAME = reader.GetString("NAME"),
                            Tax_ID = reader.GetInt32("Tax_ID"),
                            TAX_NAME = reader.GetString("TAX_NAME"),
                            INSTITUTIONALEMAIL = reader.GetString("INSTITUTIONALEMAIL"),
                            DEF_PASS = reader.GetString("DEF_PASS"),

                            
                            



                        });
                    }
                }
            }

            return list;
        }

        public IEnumerable<AddressProfile> getInstitutionalAddress(string institution)
        {
            List<AddressProfile> list = new List<AddressProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`INSTITUTIONALADDRESS` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AddressProfile()
                        {

                            ID = reader.GetInt32("ID"),
                            BUILDING_NAME = reader.GetString("BUILDING_NAME"),
                            CONTRY = reader.GetString("CONTRY"),
                            NUMBER = reader.GetInt32("NUMBER"),
                            LOCATION_TYPE = reader.GetInt32("LOCATION_TYPE"),
                            PROVINCE = reader.GetString("PROVINCE"),
                            CITY = reader.GetString("CITY"),
                            PHONE = reader.GetString("PHONE"),
                            STREET = reader.GetString("STREET"),
                           



                        }) ;
                    }
                }
            }

            return list;
        }

        public  AddressProfile SaveAdress(AddressProfile data)
        {
       
            string query = $"INSERT INTO `admbasic`.`INSTITUTIONALADDRESS`(`INSTITUTION`,`LOCATION_TYPE`,`BUILDING_NAME`,`NUMBER`,`STREET`,`PROVINCE`,`CITY`,`CONTRY`,`PHONE`)VALUES('{data.INSTITUTION}',{data.LOCATION_TYPE},'{data.BUILDING_NAME}',{data.NUMBER},'{data.STREET}','{data.PROVINCE}','{data.CITY}','{data.CONTRY}','{data.PHONE}');";
             Executor(query);
             return data;
        }


        public InstitutionModel AddInstitutionDetails(InstitutionModel data)
        {
            string auth = $"UPDATE `admbasic`.`INSTITUTIONPROFILE` SET `Tax_ID` = {data.Tax_ID}, `TAX_NAME` = '{data.TAX_NAME}', `NAME` = '{data.NAME}',`INSTITUTIONALEMAIL` = '{data.INSTITUTIONALEMAIL+data.NAME}.com',`DEF_PASS` = '{data.NAME}123' where INSTITUTION = '{data.INSTITUTION}' ";

            Executor(auth);

            
            
            return data;
        }

        public InstitutionModel addInstitutionalemailToCurrentUser(InstitutionModel data)
        {
            string auth = $"UPDATE `admbasic`.`INSTITUTIONPROFILE` SET  `INSTITUTIONALEMAIL` = '{data.INSTITUTIONALEMAIL}',`DEF_PASS` = '{data.DEF_PASS}' where INSTITUTION = '{data.INSTITUTION}' ";

            Executor(auth);

            string InstitutionalEmail = null;
            
            var em = IGetPersonByCODE(data.INSTITUTION ,data.CODE);
            if (em.Count()>0)
            {
                foreach (var item in em)
                {
                    InstitutionalEmail = $"{item.F_NAME}{item.S_NAME}.{item.F_LASTN}{item.S_LASTN}{data.INSTITUTIONALEMAIL}.ToLower()";
                }
            }
            
            //Todo add data and time, set the name for the institution create method to load institUTION CODE
            string insemal = $"UPDATE `admbasic`.`AUTHENTICATION` SET `EMAIL` = '{InstitutionalEmail}' WHERE INSTITUTION = '{data.INSTITUTION}' and (CODE = {data.CODE});";

            Executor(insemal);
            
            return data;
        }

        public IEnumerable<PersonProfile> IGetPersonByCODE(string inst,int id)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` where INSTITUTION = '{inst}'and CODE = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {

                           
                            CODE = reader.GetInt32("CODE"),
                            F_NAME = reader.GetString("F_NAME"),
                            S_NAME = reader.GetString("S_NAME"),
                            F_LASTN = reader.GetString("F_LASTN"),
                            S_LASTN = reader.GetString("S_LASTN"),
                            
                            



                        });
                    }
                }
            }

            return list;
        }


        public IEnumerable<Link> GetLinks()
        {
            List<Link> list = new List<Link>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT ADDRESS, DESCRIPTION FROM `admbasic`.`LINKS` ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Link()
                        {
                            ADDRESS = reader.GetString("ADDRESS"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            
                        });
                    }
                }
            }

            return list;
        }

        public IEnumerable<AuthenticationProcedure> GetShaValue(string institution, int code)
        {
            List<AuthenticationProcedure> list = new List<AuthenticationProcedure>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT sha FROM `admbasic`.`AuthenticationProcedure` where institution = '{institution}' and Code =  {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationProcedure()
                        {

                           
                            sha = reader.GetString("sha"),
                            
                            



                        });
                    }
                }
            }

            return list;
        }


    internal void Executor(string query)
    {
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                int result = cmd.ExecuteNonQuery();

                //lblError.Text = "Data Saved";

            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }

    }

    internal void RegisterNewAuthorization(AuthenticationProcedure auth)
    {
        string query = $"INSERT INTO `admbasic`.`AuthenticationProcedure`(Institution,Code,Date,time,Ip,sha,State,Value)VALUES('{auth.Institution}',{auth.Code},'{auth.Date}','{auth.time}','{auth.Ip}','{auth.sha}',{auth.State},'{auth.Value}');";
             Executor(query);
             
    }
}

