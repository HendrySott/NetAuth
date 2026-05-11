using System.ComponentModel.DataAnnotations;

namespace Authentication;

public class AuthenticationModel
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    
    [Required,EmailAddress]
    public string EMAIL { get; set; }= string.Empty;
    
    [Required, MinLength(5)]
    public string PASSWORD { get; set; }
    
  
    public string ROLE { get; set; }

    public int STATE { get; set; }

    public string PERSONAL_EMAIL { get; set; }

    public bool REMEMBERME{get;set;}

   

}

public class Autentication
{
    public int ID { get; set; }
    public int UID { get; set; }
    public string EMAIL { get; set; }
    public string PASSWORD { get; set; }
    public int STATE { get; set; }
}

public class InstitutionModel
{

    public int Tax_ID { get; set; }
    public string TAX_NAME { get; set; }        
    public string NAME { get; set; }
    public string INSTITUTIONALEMAIL { get; set; }
    public string INSTITUTION { get; set; }
    public string DEF_PASS { get; set; }
    public int CODE { get; set; }
}

public class PersonProfile
{
    public int ID { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    public string F_NAME { get; set; }
    public string S_NAME { get; set; }
    public string F_LASTN { get; set; }
    public string S_LASTN { get; set; }
    public string GRADE { get; set; }
    public string ADDRESS { get; set; }
    public string PHONE { get; set; }
    public string IDENTIFICATION { get; set; }
    public string PERSONAL_EMAIL { get; set; }
    public int POSITION_KEY { get; set; }
    public int TEAM_ID { get; set; }
    public string STREET { get; set; }
    public int HOME { get; set; }
    public string COUNTY { get; set; }
    public string COUNTRY { get; set; }
    public string ADMITION_DATE{get;set;}
    public string ALT_PHONE{get;set;}
    public string RESUME{get;set;}
    public string COVERLETTER{get;set;}
    public string SKILLS{get;set;}
   
    public string COMPANY_NAME{get;set;}
    public string REPORT_TO{get;set;}
    public string DATE_TIME_START{get;set;}
    public string DATE_TIME_FINISH{get;set;}
    public string LEAVE_REASON{get;set;}
    public float LAST_WAGE{get;set;}
    public int WORKING_EX{get;set;}
    public string CARRER_NAME{get;set;}
    public int IS_GRADUATED{get;set;}
    public float DESIRED_WAGE{get;set;}
    public int CURRENT_STATUS{get;set;}
    public string DATE_TIME{get;set;}
    public int POSITION_ID{get;set;}

    public string INS_EMAIL{get;set;}
    public string DATA{get;set;}

 

}

public class TimeTrackerModel
{
    public int ID { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    public string TOTAL_OFFLINE { get; set; }
    public string TOTAL_ONLINE { get; set; }
    public string START { get; set; }
    public string LUNCH { get; set; }
    public string LUNCH_ { get; set; }
    public string LUNCH_S { get; set; }
    public string BREAK { get; set; }
    public string BREAK_ { get; set; }
    public string BREAK_S { get; set; }

    public string BREAKL { get; set; }
    public string BREAKL_ { get; set; }
    public string BREAK_SL { get; set; }
    public string OTHER { get; set; }
    public string OTHER_ { get; set; }
    public string OTHER_S { get; set; }
    public string END { get; set; }
    public string DATE { get; set; }
    public string TOTAL { get; set; }
    public string STATE { get; set; }

    public string DATESH { get; set; }
    public int AUTHORIZED_BY { get; set; }

}

public class PositionProfile
{
    public int ID { get; set; }
    public string POSITION_NAME { get; set; }
    public int TEAM_ID { get; set; }
    public string REASON { get; set; }
    public int JOB_PROFILE { get; set; }
    public int JOB_QUANTITY { get; set; }
    public int LOCATION_PROFILE { get; set; }
    public int JOB_TYPE { get; set; }
    public int CONTRACT_TYPE { get; set; }
    public int ACADEMIC_GRADE { get; set; }
    public int SHIFT { get; set; }
    public string SKILLS_REQUIRED { get; set; }
    
    public string AVAILABLE_FROM { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }

    public string SKILS { get; set; }
    public string OBJETIVE { get; set; }
    public float PRICE { get; set; }
    public int WAGE_PORSENT { get; set; }
    public int REPORT_TO { get; set; }

    public float PAYMENTCALCTYPE { get; set; }

}

public class AddressProfile
{
    public int ID { get; set; }
    public int LOCATION_TYPE { get; set; }
    public string BUILDING_NAME { get; set; }
    public string STREET { get; set; }
    public string PROVINCE { get; set; }
    public int NUMBER { get; set; }
    public string STATE { get; set; }
    public string CITY { get; set; }
    public string CONTRY { get; set; }
    public int ZIP { get; set; }

    public string PHONE { get; set; }
    public string FAX { get; set; }
    public string INSTITUTION { get; set; }
    
}

public class Link{
    public string ADDRESS { get; set; }
    public string DESCRIPTION { get; set; }
    public int CODE { get; set; }

}

public class AuthenticationProcedure{
    public string Institution{get;set;}
    public int Code {get;set;}
    public string Date{get;set;}
    public string time{get;set;}
    public string Ip{get;set;}
    public string sha {get;set;}
    public bool State {get;set;}
    public string Value {get;set;}
}