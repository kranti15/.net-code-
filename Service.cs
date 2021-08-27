using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;
using System.Text;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.IO;


[WebService(Namespace = "RBSS")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class RBSS_Service : System.Web.Services.WebService
{
    SqlParameter[] para;
    DataSet ds = new DataSet();

    public RBSS_Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    //[WebMethod]
    //public string Test(string xml)
    //{

    //    string str = "";
    //    str = xml.Replace("<![CDATA[", "");
    //    str = xml.Replace("]]>", "");
    //    return str;
    //}


    public static string EncryptString(string key, string plainText)
    {
        //byte[] iv = new byte[16];
        string iv = "tataaialifeapiky";
        byte[] array;


        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }
                    array = memoryStream.ToArray();
                }
            }
        }
        return Convert.ToBase64String(array);
    }

    public static string DecryptString(string key, string cipherText)
    {

        //byte[] iv = new byte[16];
        string iv = "tataaialifeapiky";
        byte[] buffer = Convert.FromBase64String(cipherText);


        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);  //iv;				
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);


            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {

                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    [WebMethod]
    public string PushData(string xml)
    {
        string key = "s14ac5898a4e413a";
        string StatusValue = string.Empty;
        string error = "XML Foramt error";

        var decodexml = DecryptString(key, xml);



        try
        {
            string mNode = "";
            int sInd = decodexml.IndexOf("<", 0);
            int lInd = decodexml.IndexOf(">", 0);
            mNode = decodexml.Substring(sInd + 1, lInd - 1);

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(decodexml);

            




            error = " Paramter BUSINESS_TYPE_ID not found";
            string BUSINESS_TYPE_ID = xdoc[mNode].SelectSingleNode("BUSINESS_TYPE_ID").InnerText.ToString();

            error = " Paramter FLOW not found";
            string FLOW = xdoc[mNode].SelectSingleNode("FLOW").InnerText.ToString();

            error = " Paramter GC_PASA_FLAG not found";
            string GC_PASA_FLAG = xdoc[mNode].SelectSingleNode("GC_PASA_FLAG").InnerText.ToString();



            error = " Paramter COMPANY_NAME not found";
            string COMPANY_NAME = xdoc[mNode].SelectSingleNode("COMPANY_NAME").InnerText.ToString();


            error = " Paramter IPAD_LEAD_ID not found";
            string IPAD_LEAD_ID = xdoc[mNode].SelectSingleNode("IPAD_LEAD_ID").InnerText.ToString();
            //long IPAD_LEAD_ID = Convert.ToInt64(IPADLEADID);

            error = " Paramter LEAD_CREATED_DTTM not found";
            string LEAD_CREATED_DTTM = xdoc[mNode].SelectSingleNode("LEAD_CREATED_DTTM").InnerText;
            //  long LEAD_CREATED_DTTM = Convert.ToInt64(LEADCREATEDDTTM);


            error = " Paramter LEAD_STATUS not found";
            string LEAD_STATUS = xdoc[mNode].SelectSingleNode("LEAD_STATUS").InnerText.ToString();


            error = " Paramter LEAD_NAME not found";
            string LEAD_NAME = xdoc[mNode].SelectSingleNode("LEAD_NAME").InnerText.ToString();


            error = " Paramter BIRTH_DATE not found";
            string BIRTH_DATE = xdoc[mNode].SelectSingleNode("BIRTH_DATE").InnerText.ToString();


            error = " Paramter GENDER not found";
            string GENDER = xdoc[mNode].SelectSingleNode("GENDER").InnerText.ToString();


            error = " Paramter CURNT_ADDRESS1 not found";
            string CURNT_ADDRESS1 = xdoc[mNode].SelectSingleNode("CURNT_ADDRESS1").InnerText.ToString();


            error = " Paramter CURNT_ADDRESS2 not found";
            string CURNT_ADDRESS2 = xdoc[mNode].SelectSingleNode("CURNT_ADDRESS2").InnerText.ToString();


            error = " Paramter CURNT_ADDRESS3 not found";
            string CURNT_ADDRESS3 = xdoc[mNode].SelectSingleNode("CURNT_ADDRESS3").InnerText.ToString();


            error = " Paramter CURNT_DISTRICT_LANDMARK not found";
            string CURNT_DISTRICT_LANDMARK = xdoc[mNode].SelectSingleNode("CURNT_DISTRICT_LANDMARK").InnerText.ToString();


            error = " Paramter CURNT_CITY not found";
            string CURNT_CITY = xdoc[mNode].SelectSingleNode("CURNT_CITY").InnerText.ToString();


            error = " Paramter CURNT_STATE not found";
            string CURNT_STATE = xdoc[mNode].SelectSingleNode("CURNT_STATE").InnerText.ToString();


            error = " Paramter CURNT_ZIP_CODE not found";
            string CURNT_ZIP_CODE = xdoc[mNode].SelectSingleNode("CURNT_ZIP_CODE").InnerText.ToString();


            error = " Paramter MOBILE_NO not found";
            string MOBILE_NO = xdoc[mNode].SelectSingleNode("MOBILE_NO").InnerText.ToString();
            //    long MOBILE_NO = Convert.ToInt64(MOBILENO);

            error = " Paramter EMAIL_ID not found";
            string EMAIL_ID = xdoc[mNode].SelectSingleNode("EMAIL_ID").InnerText.ToString();


            error = " Paramter EMPLOYEE_ID not found";
            string EMPLOYEE_ID = xdoc[mNode].SelectSingleNode("EMPLOYEE_ID").InnerText.ToString();


            error = " Paramter ANNUAL_INCOME not found";
            string ANNUAL_INCOME = xdoc[mNode].SelectSingleNode("ANNUAL_INCOME").InnerText.ToString();


            error = " Paramter OPPORTUNITY_ID not found";
            string OPPORTUNITY_ID = xdoc[mNode].SelectSingleNode("OPPORTUNITY_ID").InnerText.ToString();


            error = " Paramter SIS_ID not found";
            string SIS_ID = xdoc[mNode].SelectSingleNode("SIS_ID").InnerText.ToString();


            error = " Paramter SIS_SIGNED_TIMESTAMP not found";
            string SIS_SIGNED_TIMESTAMP = xdoc[mNode].SelectSingleNode("SIS_SIGNED_TIMESTAMP").InnerText.ToString();

            error = " Paramter PLAN_CODE not found";
            string PLAN_CODE = xdoc[mNode].SelectSingleNode("PLAN_CODE").InnerText.ToString();

            error = " Paramter PRODUCT_NAME not found";
            string PRODUCT_NAME = xdoc[mNode].SelectSingleNode("PRODUCT_NAME").InnerText.ToString();

            error = " Paramter SUM_ASSURED not found";
            string SUM_ASSURED = xdoc[mNode].SelectSingleNode("SUM_ASSURED").InnerText.ToString();

            error = " Paramter POLICY_TERM not found";
            string POLICY_TERM = xdoc[mNode].SelectSingleNode("POLICY_TERM").InnerText.ToString();

            error = " Paramter PREMIUM_PAYMENT_TERM not found";
            string PREMIUM_PAYMENT_TERM = xdoc[mNode].SelectSingleNode("PREMIUM_PAYMENT_TERM").InnerText.ToString();

            error = " Paramter PREMIUM_MODE not found";
            string PREMIUM_MODE = xdoc[mNode].SelectSingleNode("PREMIUM_MODE").InnerText.ToString();

            error = " Paramter PREMIUM_BASE not found";
            string PREMIUM_BASE = xdoc[mNode].SelectSingleNode("PREMIUM_BASE").InnerText.ToString();

            error = " Paramter GST not found";
            string GST = xdoc[mNode].SelectSingleNode("GST").InnerText.ToString();


            error = " Paramter PREMIUM not found";
            string PREMIUM = xdoc[mNode].SelectSingleNode("PREMIUM").InnerText.ToString();

            error = " Paramter APPLICATION_ID not found";
            string APPLICATION_ID = xdoc[mNode].SelectSingleNode("APPLICATION_ID").InnerText.ToString();

            error = " Paramter APP_SUBMITTED_TIMESTAMP not found";
            string APP_SUBMITTED_TIMESTAMP = xdoc[mNode].SelectSingleNode("APP_SUBMITTED_TIMESTAMP").InnerText.ToString();

            error = " Paramter POLICY_NO not found";
            string POLICY_NO = xdoc[mNode].SelectSingleNode("POLICY_NO").InnerText.ToString();

            error = " Paramter PAYMENT_ID not found";
            string PAYMENT_ID = xdoc[mNode].SelectSingleNode("PAYMENT_ID").InnerText.ToString();

            error = " Paramter PAYMENT_TIMESTAMP not found";
            string PAYMENT_TIMESTAMP = xdoc[mNode].SelectSingleNode("PAYMENT_TIMESTAMP").InnerText.ToString();

            error = " Paramter PAYMENT_STATUS not found";
            string PAYMENT_STATUS = xdoc[mNode].SelectSingleNode("PAYMENT_STATUS").InnerText.ToString();

            error = " Paramter SMOKER_FLAG not found";
            string SMOKER_FLAG = xdoc[mNode].SelectSingleNode("SMOKER_FLAG").InnerText.ToString();

            error = " Paramter INTERACTION_ID not found";
            string INTERACTION_ID = xdoc[mNode].SelectSingleNode("INTERACTION_ID").InnerText.ToString();

            error = " Paramter PAGE_NAME not found";
            string PAGE_NAME = xdoc[mNode].SelectSingleNode("PAGE_NAME").InnerText.ToString();

            error = " Paramter EVENT_NAME not found";
            string EVENT_NAME = xdoc[mNode].SelectSingleNode("EVENT_NAME").InnerText.ToString();

            error = " Paramter CREATED_DTTM not found";
            string CREATED_DTTM = xdoc[mNode].SelectSingleNode("CREATED_DTTM").InnerText.ToString();


            //error = " Paramter Phone1 not found";
            //string P1 = xdoc[mNode].SelectSingleNode("PLAN CODE").InnerText.ToString();
            //long Phone1 = Convert.ToInt64(P1);

            //error = " Paramter Phone2 not found";
            //string P2 = xdoc[mNode].SelectSingleNode("Phone2").InnerText.ToString();
            //long Phone2 = Convert.ToInt64(P2);

            //error = " Paramter Email not found";
            //string Email = xdoc[mNode].SelectSingleNode("FLOW").InnerText.ToString();

            //error = " Paramter Age not found";
            //string A = xdoc[mNode].SelectSingleNode("Age").InnerText.ToString();
            //long Age = Convert.ToInt64(A);

            //error = " Paramter Pincode not found";
            //string Pin = xdoc[mNode].SelectSingleNode("Pincode").InnerText.ToString();
            //long Pincode = Convert.ToInt64(Pin);



            error = "";

            ds = SaveValueIntoDataBase
                 (
                   BUSINESS_TYPE_ID, FLOW, GC_PASA_FLAG, COMPANY_NAME, IPAD_LEAD_ID, LEAD_CREATED_DTTM, LEAD_STATUS,
                   LEAD_NAME, BIRTH_DATE, GENDER, CURNT_ADDRESS1, CURNT_ADDRESS2, CURNT_ADDRESS3, CURNT_DISTRICT_LANDMARK,
                   CURNT_CITY, CURNT_STATE, CURNT_ZIP_CODE, MOBILE_NO, EMAIL_ID, EMPLOYEE_ID, ANNUAL_INCOME, OPPORTUNITY_ID, SIS_ID,
                   SIS_SIGNED_TIMESTAMP, PLAN_CODE, PRODUCT_NAME, SUM_ASSURED, POLICY_TERM, PREMIUM_PAYMENT_TERM, PREMIUM_MODE, PREMIUM_BASE,
                   GST, PREMIUM, APPLICATION_ID, APP_SUBMITTED_TIMESTAMP, POLICY_NO, PAYMENT_ID, PAYMENT_TIMESTAMP, PAYMENT_STATUS, SMOKER_FLAG,
                   INTERACTION_ID, PAGE_NAME, EVENT_NAME, CREATED_DTTM

                );

            StatusValue = ds.Tables[0].Rows[0][0].ToString();

            StatusValue = EncryptString(key, StatusValue);

            return StatusValue;
        }

        catch (Exception ex)
        {
            return "FAIL : " + error.ToString() + " ( " + ex.Message.ToString() + " )";
        }
    }

    private DataSet SaveValueIntoDataBase
        (
        String BUSINESS_TYPE_ID, String FLOW, String GC_PASA_FLAG, String COMPANY_NAME, String IPAD_LEAD_ID, String LEAD_CREATED_DTTM,
String LEAD_STATUS, String LEAD_NAME, String BIRTH_DATE, String GENDER, String CURNT_ADDRESS1, String CURNT_ADDRESS2, String CURNT_ADDRESS3,
String CURNT_DISTRICT_LANDMARK, String CURNT_CITY, String CURNT_STATE, String CURNT_ZIP_CODE, String MOBILE_NO, String EMAIL_ID, String EMPLOYEE_ID,
String ANNUAL_INCOME, String OPPORTUNITY_ID, String SIS_ID, String SIS_SIGNED_TIMESTAMP, String PLAN_CODE, String PRODUCT_NAME, String SUM_ASSURED,
String POLICY_TERM, String PREMIUM_PAYMENT_TERM, String PREMIUM_MODE, String PREMIUM_BASE, String GST, String PREMIUM, String APPLICATION_ID, String APP_SUBMITTED_TIMESTAMP,
String POLICY_NO, String PAYMENT_ID, String PAYMENT_TIMESTAMP, String PAYMENT_STATUS, String SMOKER_FLAG, String INTERACTION_ID, String PAGE_NAME,
String EVENT_NAME, String CREATED_DTTM


        )
    {

        para = new SqlParameter[62];


        para[0] = new SqlParameter("BUSINESS_TYPE_ID", SqlDbType.VarChar);
        para[0].Value = BUSINESS_TYPE_ID.Trim();

        para[1] = new SqlParameter("FLOW", SqlDbType.VarChar);
        para[1].Value = FLOW;

        para[2] = new SqlParameter("GC_PASA_FLAG", SqlDbType.VarChar);
        para[2].Value = GC_PASA_FLAG;

        para[3] = new SqlParameter("COMPANY_NAME", SqlDbType.VarChar);
        para[3].Value = COMPANY_NAME;


        para[4] = new SqlParameter("IPAD_LEAD_ID", SqlDbType.VarChar);
        para[4].Value = IPAD_LEAD_ID;


        para[5] = new SqlParameter("LEAD_CREATED_DTTM", SqlDbType.VarChar);
        para[5].Value = LEAD_CREATED_DTTM;

        para[6] = new SqlParameter("LEAD_STATUS", SqlDbType.VarChar);
        para[6].Value = LEAD_STATUS;

        para[7] = new SqlParameter("LEAD_NAME", SqlDbType.VarChar);
        para[7].Value = LEAD_NAME;

        para[8] = new SqlParameter("BIRTH_DATE", SqlDbType.VarChar);
        para[8].Value = BIRTH_DATE;

        para[9] = new SqlParameter("GENDER", SqlDbType.VarChar);
        para[9].Value = GENDER;

        para[10] = new SqlParameter("CURNT_ADDRESS1", SqlDbType.VarChar);
        para[10].Value = CURNT_ADDRESS1;

        para[11] = new SqlParameter("CURNT_ADDRESS2", SqlDbType.VarChar);
        para[11].Value = CURNT_ADDRESS2;

        para[12] = new SqlParameter("CURNT_ADDRESS3", SqlDbType.VarChar);
        para[12].Value = CURNT_ADDRESS3;

        para[13] = new SqlParameter("CURNT_DISTRICT_LANDMARK", SqlDbType.VarChar);
        para[13].Value = CURNT_DISTRICT_LANDMARK;

        para[14] = new SqlParameter("CURNT_CITY", SqlDbType.VarChar);
        para[14].Value = CURNT_CITY;

        para[15] = new SqlParameter("CURNT_STATE", SqlDbType.VarChar);
        para[15].Value = CURNT_STATE;

        para[16] = new SqlParameter("CURNT_ZIP_CODE", SqlDbType.VarChar);
        para[16].Value = CURNT_ZIP_CODE;

        para[17] = new SqlParameter("MOBILE_NO", SqlDbType.VarChar);
        para[17].Value = MOBILE_NO;

        para[18] = new SqlParameter("EMAIL_ID", SqlDbType.VarChar);
        para[18].Value = EMAIL_ID;

        para[19] = new SqlParameter("EMPLOYEE_ID", SqlDbType.VarChar);
        para[19].Value = EMPLOYEE_ID;

        para[20] = new SqlParameter("ANNUAL_INCOME", SqlDbType.VarChar);
        para[20].Value = ANNUAL_INCOME;

        para[21] = new SqlParameter("OPPORTUNITY_ID", SqlDbType.VarChar);
        para[21].Value = OPPORTUNITY_ID;

        para[22] = new SqlParameter("SIS_ID", SqlDbType.VarChar);
        para[22].Value = SIS_ID;

        para[23] = new SqlParameter("SIS_SIGNED_TIMESTAMP", SqlDbType.VarChar);
        para[23].Value = SIS_SIGNED_TIMESTAMP;

        para[24] = new SqlParameter("PLAN_CODE", SqlDbType.VarChar);
        para[24].Value = PLAN_CODE;

        para[25] = new SqlParameter("PRODUCT_NAME", SqlDbType.VarChar);
        para[25].Value = PRODUCT_NAME;

        para[26] = new SqlParameter("SUM_ASSURED", SqlDbType.VarChar);
        para[26].Value = SUM_ASSURED;

        para[27] = new SqlParameter("POLICY_TERM", SqlDbType.VarChar);
        para[27].Value = POLICY_TERM;

        para[28] = new SqlParameter("PREMIUM_PAYMENT_TERM", SqlDbType.VarChar);
        para[28].Value = PREMIUM_PAYMENT_TERM;

        para[29] = new SqlParameter("PREMIUM_MODE", SqlDbType.VarChar);
        para[29].Value = PREMIUM_MODE;

        para[30] = new SqlParameter("PREMIUM_BASE", SqlDbType.VarChar);
        para[30].Value = PREMIUM_BASE;

        para[31] = new SqlParameter("GST", SqlDbType.VarChar);
        para[31].Value = GST;

        para[32] = new SqlParameter("PREMIUM", SqlDbType.VarChar);
        para[32].Value = PREMIUM;

        para[33] = new SqlParameter("APPLICATION_ID", SqlDbType.VarChar);
        para[33].Value = APPLICATION_ID;

        para[34] = new SqlParameter("APP_SUBMITTED_TIMESTAMP", SqlDbType.VarChar);
        para[34].Value = APP_SUBMITTED_TIMESTAMP;

        para[35] = new SqlParameter("POLICY_NO", SqlDbType.VarChar);
        para[35].Value = POLICY_NO;

        para[36] = new SqlParameter("PAYMENT_ID", SqlDbType.VarChar);
        para[36].Value = PAYMENT_ID;


        para[37] = new SqlParameter("PAYMENT_TIMESTAMP", SqlDbType.VarChar);
        para[37].Value = PAYMENT_TIMESTAMP;


        para[38] = new SqlParameter("PAYMENT_STATUS", SqlDbType.VarChar);
        para[38].Value = PAYMENT_STATUS;


        para[39] = new SqlParameter("SMOKER_FLAG", SqlDbType.VarChar);
        para[39].Value = SMOKER_FLAG;


        para[40] = new SqlParameter("INTERACTION_ID", SqlDbType.VarChar);
        para[40].Value = INTERACTION_ID;


        para[41] = new SqlParameter("PAGE_NAME", SqlDbType.VarChar);
        para[41].Value = PAGE_NAME;


        para[42] = new SqlParameter("EVENT_NAME", SqlDbType.VarChar);
        para[42].Value = EVENT_NAME;

        para[43] = new SqlParameter("CREATED_DTTM", SqlDbType.VarChar);
        para[43].Value = CREATED_DTTM;




        DataSet dsReturn = new DataSet();
        if (BUSINESS_TYPE_ID == "iWealth" || BUSINESS_TYPE_ID == "WebSolution")
        {
            dsReturn = SqlHelper.ExecuteDataset(GetConnectionstring(), CommandType.StoredProcedure, "USP_API_INSERT_DATA", para);
        }
       if (BUSINESS_TYPE_ID == "HDFCSolution" || BUSINESS_TYPE_ID == "HDFCSecuritiesSolution" ||BUSINESS_TYPE_ID == "IndusSolution" ||BUSINESS_TYPE_ID == "PaytmSolution")
        {
            // dsReturn = SqlHelper.ExecuteDataset(GetConnectionstringHP(), CommandType.StoredProcedure, "USP_API_INSERT_DATA", para);
            dsReturn = SqlHelper.ExecuteDataset(GetConnectionstringHP(), CommandType.StoredProcedure, "USP_API_INSERT_DATA_testapi", para); 
        }
        if (BUSINESS_TYPE_ID == "CitiSolution" || BUSINESS_TYPE_ID == "CitiAPISolution")
        {
            dsReturn = SqlHelper.ExecuteDataset(GetConnectionstringCP(), CommandType.StoredProcedure, "USP_API_INSERT_DATA", para);
        }
 	


        return dsReturn;
    }

    private string GetConnectionstring()
    {
        return "Data Source=172.16.0.76;Database=TATAAIA;uid=sa;pwd=AND@123";
    }

    private string GetConnectionstringHP()
    {
        return "Data Source=172.16.0.76;Database=HDFC_PASA;uid=sa;pwd=AND@123";
    }

    private string GetConnectionstringCP()
    {
        return "Data Source=172.16.0.76;Database=TATA_CITI_PASA;uid=sa;pwd=AND@123";
    }


}
