using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Telerik.Web.UI;
using System.Net;
using System.IO;
using System.Text;
using System.Globalization;
using System.Data.Odbc;
using System.Drawing;
using System.Diagnostics;
using System.Web.UI.HtmlControls;
using System.Web.UI.Adapters;
using System.Web.Util;


public partial class TATA_AIG : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Constring"].ConnectionString);
    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constring1"].ConnectionString);


    //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Constring1"].ConnectionString);

    SqlConnection sqlIntcon = new SqlConnection(ConfigurationManager.ConnectionStrings["INTERD"].ConnectionString);
    string strQuery = "";
    public string strInsertSatus = "Y";
    int myServiceId = 0;
    int GenericLeadId = 0;
    string strTableName = "";
    string strDbName = "";
    string strLeadID, strlead_ID, strCallType, strHost, strServiceID, strCallNumber, NextDialTime, CallNumber, strdispcode = "", DNI = "";
    string dtm2 = "";
    string strDispParam = "";
    string id = "";
    string BacthID = "";
    
    DateTime dt1, dt3;
    string dtm1 = "", dtm3 = "";
    DataTable dat1=new DataTable();
    


    protected void Page_Load(object sender, EventArgs e)
    {
        //DateTime dtime = new DateTime();
        //dtime = System.DateTime.Now;
        //string Properdatetime = dtime.ToString("HH:mm:ss");
        //try
        //{
        //    RadDateTimePicker1.SelectedDate = Convert.ToDateTime(Properdatetime);
        //}
        //catch (Exception ex)
        //{

        //    throw (ex);
        //}

        try
        {
            strHost = Convert.ToString(Session["HOSTID"].ToString());
        }
        catch
        {


        }
        try
        {
            CallNumber = Convert.ToString(Session["CallNumber"].ToString());

        }
        catch
        {


        }



        try
        {
            strServiceID = Convert.ToString(Session["serviceid"].ToString());
        }
        catch
        {


        }
        try
        {
            strlead_ID = Convert.ToString(Session["LeadId"].ToString());
        }


        catch
        {

            //throw;
        }
        //lblagentname.Text = (string)Session["lblAgentStatusValue"];
        txtlead_idP.Text = strlead_ID;
        //lblagentP.Text = (string)Session["Agentname"];
        //lblagentP.Text = (string)Session["Agentname"];
        try
        {
            lblagentP.Text = Convert.ToString(Session["loginid"].ToString());
        }
        catch
        {
        }
        try
        {
            //DisplayNotepadOutbound();
            //lblleadid.Text = Convert.ToString(Session["LeadId"].ToString());
         //   Session["CallMobNo"] = txtphoneP.Value = Convert.ToString(Request.QueryString["CLI"]);
           // txtphoneP.Value = Convert.ToString(Request.QueryString["CLI"]);
            Session["strcalltypes"] = strCallType = Convert.ToString(Session["CALLTYPE"]);

        }
        catch
        {

            // throw;
        }

	((HtmlControl)(form1.FindControl("Iframe4"))).Attributes["src"] = "http://172.16.0.10/TATA_AIA/Knowledge%20bank_TATA%20AIA.htm";

        //getDisposition();

    
        if (!Page.IsPostBack)
        {
            txtalternate.Text = "";

            leadload_select();
            getLeadID();
            fillhistory();
            getDisp();
            VYMOAPPLOGIN();
            fillcustdetailsgrid();
            filladviserQ1();

            lblbatchP.Text = "RC_16Aug2021";

            if (lblbatchP.Text.Substring(0, 2) == "RC")
            {
                txtadvisorname.Enabled = true;
                txtcustnm.Enabled = true;
                ddladvcontact.Enabled = true;
                ddlleadwise.Enabled = true;
                ddlifno.Enabled = true;
                txtadvreamrks.Enabled = true;
                btnadviser.Enabled = true;
                gvsimul.Visible = true;

            }
            else
            {
                txtadvisorname.Enabled = false;
                txtcustnm.Enabled = false;
                ddladvcontact.Enabled = false;
                ddlleadwise.Enabled = false;
                ddlifno.Enabled = false;
                txtadvreamrks.Enabled = false;
                btnadviser.Enabled = false;
                gvsimul.Visible = false;

            }
            
            
        }


        
        


    }

    protected void VYMOAPPLOGIN()
    {
        if (con.State == ConnectionState.Open) con.Close(); con.Open();

        ddlVYMOAPPLOGIN.Items.Clear();
        ddlVYMOAPPLOGIN.ClearSelection();

        using (SqlCommand cmd = new SqlCommand("proc_get_VYMOAPPLOGIN", con))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter calltype = cmd.Parameters.AddWithValue("@Calltype", Session["strcalltypes"].ToString());
            //SqlParameter disp = cmd.Parameters.AddWithValue("@disp", null);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["VYMOAppLogin"].ToString());
                        ddlVYMOAPPLOGIN.Items.Add(item);
                    }
                }
            }
        }
    }


    public void btnsaveadoc_Click(object sender, EventArgs e)
    {
        //string dt2;
        //string dtm2;

        //dt2 = Convert.ToDateTime(radPKPickupCallBackP.SelectedDate.ToString());
        //       dtm2 = dt2.ToString("yyyy-MM-dd HH:mm:ss");

       // DateTime dt2;
       // string dtm2 = "";

      //  dt2 = Convert.ToDateTime(radPKPickupCallBackP.SelectedDate.ToString());
      //  dtm2 = dt2.ToString("yyyy-MM-dd HH:mm:ss");

      //  save_data();

      //  CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();

      //  CtiWS1.CloseCall("", "", strdispcodevaluesP.Value, dtm2, radPKPickupCallBackP.SelectedDate.ToString(), txtremarkP.Text, strHost);
       // CtiWS1.CloseCall("", "", strdispcodevaluesP.Value, dtm2, "", txtremarkP.Text, strHost);
       // clearFields();

        DateTime dt2;


         if (Convert.ToString(strdispcodevaluesP.Value) == "CBK")
        {
            if (radPKPickupCallBackP.SelectedDate == null || radPKPickupCallBackP.IsEmpty == true)
            {
                Response.Write("<script>alert('Please select Call Back Date and Time')</script>");

                //radPKPickupCallBackP.Focus();
                //radPKPickupCallBackP.SelectedDate = System.DateTime.Now;
                // radPKPickupCallBackP = System.DateTime.Now;
                return;
            }
            else
            {
                if (radPKPickupCallBackP.SelectedDate == null || radPKPickupCallBackP.IsEmpty == true)
                {

                    dtm2 = "''";
                }
                else
                {
                    dt2 = Convert.ToDateTime(radPKPickupCallBackP.SelectedDate.ToString());
                    dtm2 = dt2.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }



            if (Convert.ToString(strdispcodevaluesP.Value) == "CBK")
            {
                if (radPKPickupCallBackP.SelectedDate < System.DateTime.Now)
                {
                    Response.Write("<script>alert('Please select above current date')</script>");
                    return;

                }

            }



           
	}
          string gridremark="";
          int gridcount = gvsimul.Rows.Count;
          
          DataTable dt = new DataTable();
          DataRow dr;
          dt.Columns.Add(new System.Data.DataColumn("Remarks", typeof(String)));
          foreach (GridViewRow row in gvsimul.Rows)
          {

              Label Remarks = (Label)row.FindControl("lblRemarks");
              
              dr = dt.NewRow();
              dr[0] = Remarks.Text;            
              dt.Rows.Add(dr);
          }
        
         if (Convert.ToString(strdispcodevaluesP.Value) == "CC")
         {

             for (int i = 0; i <= gridcount;i++ )
             {

                 gridremark = Convert.ToString(dt.Rows[i]["Remarks"]);

                 if (gridremark == "")
                 {
                     Response.Write("<script>alert('Please Enter CustDetails')</script>");
                     return;

                 }

             }
             //if (gridremark == "")
             //{
                // Response.Write("<script>alert('Please Enter CustDetails')</script>");
                // return;

             //}             
             //if (txtcustnm.Text == "" && txtadvisorname.Text == "" && ddladvcontact.Text == "" && ddlleadwise.Text == "" && ddlifno.Text == "")
             //{
             //    Response.Write("<script>alert('Please Enter CustDetails')</script>");
             //    return;

             //}

             

         }
         

        save_data();

        CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();

        CtiWS1.CloseCall("", "", strdispcodevaluesP.Value, dtm2, radPKPickupCallBackP.SelectedDate.ToString(), txtremarkP.Text, strHost);
        clearFields();
    }


    public void clearFields()
    {

        txtlead_idP.Text = "";
        txtphoneP.Value = "";
        txtPolicy_Number.Text = "";
        txtSUB_DATE.Text = "";
        txtAGENT_CODE.Text = "";
        txSUB_FP.Text = "";
        txtSUB_ANP.Text = "";
        txtPLAN_NAME.Text = "";
        txtPROD_TYPE.Text = "";
        txtSUM_ASSURED.Text = "";
        txtCHANNEL.Text = "";
        txtSUB_STATUS_DESCRIPTION.Text = "";
        txtSTATUS_DESCRIPTION.Text = "";
        txtMED_NON_MED.Text = "";
        txtGENERIC_STATUS.Text = "";
        txtPEDNING_REASONS.Text = "";
        txtCUSTOMERNAME.Text = "";
        txtCUSTOMERMOBILENO.Text = "";
        txtCUSTOMEREMAILADDRESS.Text = "";
        txtPAYMENT_METHOD.Text = "";
        txtpending.Text = "";
        //RadCategory.Text = string.Empty;
       // RadCategory.SelectedItem.Text="";
        RadCategory.SelectedIndex = -1;
        radDispositionP.SelectedIndex = -1;
        RadSubdispositionP.SelectedIndex = -1;
        ddlVYMOAPPLOGIN.SelectedIndex = -1;
        ddlsubdisposition1.SelectedIndex = -1;
        ddlrating.Text = "";
        ddlcomplaint.Text = "";
        ddlaware.Text = "";
        //ddlECM.Text = "";
        ddlCampaign.Text = "";
        radrating.Text = "";

    }
    public void save_data()
    {


        string STRCON = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
        SqlConnection con = new SqlConnection(STRCON);
        // if (con.State == ConnectionState.Open) con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("USP_SAVE_DATALive", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Lead_ID", txtlead_idP.Text);
        cmd.Parameters.AddWithValue("@MOBILE_NO", txtphoneP.Value);
        cmd.Parameters.AddWithValue("@lead_last_agent_name", lblagentP.Text);
        cmd.Parameters.AddWithValue("@lead_import_batch_no", lblbatchP.Text);
        cmd.Parameters.AddWithValue("@MainDisposition", RadCategory.Text);
        cmd.Parameters.AddWithValue("@Disposition", radDispositionP.Text);
        cmd.Parameters.AddWithValue("@SubDisposition", RadSubdispositionP.Text);
        cmd.Parameters.AddWithValue("@CallbackDt", radPKPickupCallBackP.SelectedDate);
        cmd.Parameters.AddWithValue("@lead_remarks", txtremarkP.Text);
        cmd.Parameters.AddWithValue("@Jobdiscription", RadJobDiscription.Text);
        //---
        cmd.Parameters.AddWithValue("@VYMOAppLogin", ddlVYMOAPPLOGIN.Text);
        cmd.Parameters.AddWithValue("@SubDisposition1", ddlsubdisposition1.Text);
        cmd.Parameters.AddWithValue("@Rating", ddlrating.Text);
        cmd.Parameters.AddWithValue("@complaint", ddlcomplaint.Text);

        cmd.Parameters.AddWithValue("@Aware_feature_VYMO", ddlaware.Text);
        cmd.Parameters.AddWithValue("@ECM_Customer", ddlECM.Text);
        cmd.Parameters.AddWithValue("@Campaign_Name", ddlCampaign.Text);
        cmd.Parameters.AddWithValue("@radrating", radrating.Text);
        cmd.Parameters.AddWithValue("@alternateNORemarks", txtalternateremark.Text);

        cmd.Parameters.AddWithValue("@PayoutInfoGiven  ", ddlpayoutinfo.Text);

        cmd.Parameters.AddWithValue("@Awareness_About_RakshaConnect", ddlrakshacon.Text);
        cmd.Parameters.AddWithValue("@Usage_of_RakshaConnect", ddlusrakshacon.Text);
        cmd.Parameters.AddWithValue("@Feedback", ddlfeedback.Text);
        //cmd.Parameters.AddWithValue("@Advisor_name", txtadvisorname.Text);
        //cmd.Parameters.AddWithValue("@Customer_Name ", txtcustnm.Text);
        //cmd.Parameters.AddWithValue("@cust_Contact", ddladvcontact.Text);
        //cmd.Parameters.AddWithValue("@Lead_wise_Response", ddlleadwise.Text);
        //cmd.Parameters.AddWithValue("@IF_NO ", ddlifno.Text);


        
        

        //cmd.Parameters.AddWithValue("@lead_last_dial_status", strdispcodevalues);
        con.Open();
        int i = cmd.ExecuteNonQuery();

        con.Close();

        Response.Write("<script>alert('Data Submitted')</script>");

           clearFields();


    }

    
    protected void leadload_select()
    {
        if (con.State == ConnectionState.Open) con.Close(); con.Open();

        using (SqlCommand cmd = new SqlCommand("USP_DISPLAY_DATA", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter disp = cmd.Parameters.AddWithValue("@LEAD_ID", strlead_ID);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {

                        txtlead_idP.Text = dr["lead_id"].ToString();
                        lblbatchP.Text = dr["lead_import_batch_no"].ToString();
                        
                        txtphoneP.Value  = dr["lead_phone"].ToString();
                        

                        if (txtphoneP.Value != "" && txtphoneP.Value.Length >= 5)
                        {
                             String maskingPrimaryNo3 = txtphoneP.Value;
                            txtmask_txtmobilenoP.Text = "*****" + maskingPrimaryNo3.Substring(maskingPrimaryNo3.Length - 5, 5);
                        }


                        txtPolicy_Number.Text = dr["Policy_Number"].ToString();
                        txtSUB_DATE.Text = dr["SUB_DATE"].ToString();
                        txtAGENT_CODE.Text = dr["AGENT_CODE"].ToString();
                        txSUB_FP.Text = dr["SUB_FP"].ToString();
                        txtSUB_ANP.Text = dr["SUB_ANP"].ToString();
                        txtPLAN_NAME.Text = dr["PLAN_NAME"].ToString();
                        txtPROD_TYPE.Text = dr["PROD_TYPE"].ToString();
                        txtSUM_ASSURED.Text = dr["SUM_ASSURED"].ToString();
                        txtCHANNEL.Text = dr["CHANNEL"].ToString();
                        txtSUB_STATUS_DESCRIPTION.Text = dr["SUB_STATUS_DESCRIPTION"].ToString();
                        txtSTATUS_DESCRIPTION.Text = dr["STATUS_DESCRIPTION"].ToString();
                        txtMED_NON_MED.Text = dr["MED_NON_MED"].ToString();
                        txtGENERIC_STATUS.Text = dr["GENERIC_STATUS"].ToString();
                        txtPEDNING_REASONS.Text = dr["PEDNING_REASONS"].ToString();
                        txtCUSTOMERNAME.Text = dr["CUSTOMER_NAME"].ToString();
                        txtCUSTOMERMOBILENO.Text = dr["MOBILE_NO"].ToString();
                        txtCUSTOMEREMAILADDRESS.Text = dr["EMAIL_ADDRESS"].ToString();
                        txtPAYMENT_METHOD.Text = dr["PAYMENT_METHOD"].ToString();
                        txtpending.Text = dr["PEDNING_REASONS"].ToString();

                    }

                }
            }
            //txtpolicyno.Text = txtpolicynumber.Text;

        }

        

        //GetagentRemark();

    }



    protected void getLeadID()
    {

        using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["INTERD"].ConnectionString))
        {
            try
            {
                sqlConn.Open();
                if (strCallType == "I" || strCallType == "M")
                {
                    using (var command = sqlConn.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "idg_sp_insertlead";

                        command.Parameters.Add("@nServiceId", SqlDbType.NVarChar).Value = "59";
                        command.Parameters.Add("@szCLI", SqlDbType.NVarChar).Value = txtphoneP.Value;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                strLeadID = reader["Lead_Id"].ToString();
                                txtlead_idP.Text = strLeadID;
                                CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();

                                CtiWS1.SetCallParameters("", "", "LEAD_ID", strLeadID, strHost);
                            }
                        }


                    }

                }
            }

            catch (SqlException ex) // This will catch all SQL exceptions
            {

            }

            sqlConn.Close();
        }
    }




    protected void getDisp()
    {
        if (con.State == ConnectionState.Open) con.Close(); con.Open();


        RadCategory.Items.Clear();
        RadCategory.ClearSelection();



        using (SqlCommand cmd = new SqlCommand("Proc_get_dispN", con))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter calltype = cmd.Parameters.AddWithValue("@Calltype", Session["strcalltypes"].ToString());
            SqlParameter disp = cmd.Parameters.AddWithValue("@disp", null);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["category"].ToString());
                        RadCategory.Items.Add(item);
                    }
                }
            }
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();

        CtiWS1.MakeCall("", "", "1234", txtphoneP.Value, "", "", strHost);
        CtiWS1.SendPbxDigits("", "", txtphoneP.Value, strHost);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();
        CtiWS1.Hangup("", "", strHost);
    }


    protected void btnClosP_Click(object sender, EventArgs e)
    {


        DateTime dt2, dt3;
        string dtm2 = "";
        string dtm3 = "";

        //if (Convert.ToString(strdispcodevaluesP.Value) == "CBAP" || Convert.ToString(strdispcodevaluesP.Value) == "PDDFU" || strdispcodevaluesP.Value == "CBFFU" || strdispcodevaluesP.Value == "CAFBEF" || strdispcodevalues.Value == "CNARN")
        //{
        //    if (radPKPickupCallBackP.SelectedDate == null || radPKPickupCallBackP.IsEmpty == true)
        //    {

        //        Response.Write("<script>alert('Please select callback date')</script>");
        //        return;
        //    }
        //    else
        //    {
        //        if (radPKPickupCallBackP.SelectedDate == null || radPKPickupCallBackP.IsEmpty == true)
        //        {

        //            dtm2 = "''";
        //        }
        //        else
        //        {
        //            dt2 = Convert.ToDateTime(radPKPickupCallBackP.SelectedDate.ToString());
        //            dtm2 = dt2.ToString("yyyy-MM-dd HH:mm:ss");
        //            // dtm2 = "isnull(CONVERT(CHAR(23),CONVERT(DATETIME,'" + dt2.ToString("dd/MM/yyy hh:mm:ss") + "',101),121),'') ";
        //        }

        //    }
        //}

        if (Convert.ToString(strdispcodevaluesP.Value) == "CBAP" || Convert.ToString(strdispcodevaluesP.Value) == "PDDFU" || strdispcodevaluesP.Value == "CBFFU" || strdispcodevaluesP.Value == "CAFBEF" || strdispcodevaluesP.Value == "CNARN" || strdispcodevaluesP.Value == "CBK")
        {
            if (radPKPickupCallBackP.SelectedDate < System.DateTime.Now || radPKPickupCallBackP.SelectedDate == null  )
            {
                Response.Write("<script>alert('Please select above current date')</script>");
                return;

            }

        }

        if (txtremarkP.Text == "")
        {
            Response.Write("<script>alert('Please select Remarks')</script>");
            return;

        }







    }

    protected void RadSubdispositionP_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //RadSubdisposition.Items.Clear();
        //RadSubdisposition.ClearSelection();
        //RadSubdisposition.Text = string.Empty;
        if (con.State == ConnectionState.Open) con.Close(); con.Open();
        using (SqlCommand cmd = new SqlCommand("proc_get_dispCodeN", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter category = cmd.Parameters.AddWithValue("@catogry", RadCategory.Text);
            SqlParameter disp = cmd.Parameters.AddWithValue("@disp", radDispositionP.Text);
            SqlParameter subdisp = cmd.Parameters.AddWithValue("@SubDisposition", RadSubdispositionP.SelectedItem.Text);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {


                        strdispcodevaluesP.Value = dr["Disp_Code"].ToString();

                    }
                }
            }

        }
        if (strdispcodevaluesP.Value == "CBK")
        {

            radPKPickupCallBackP.Enabled = true;
        }
        else
        {
            radPKPickupCallBackP.Enabled = false;
        }


    }

    protected void RadCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (con.State == ConnectionState.Open) con.Close(); con.Open();
	if (RadCategory.Text == "Not Contactable")
        {

            ddlVYMOAPPLOGIN.Text = "";
            ddlVYMOAPPLOGIN.Enabled = false;
            ddlsubdisposition1.Text = "";
            ddlsubdisposition1.Enabled = false;
            ddlrating.Text = "";
            ddlrating.Enabled = false;

            ddlcomplaint.Text = "";
            ddlcomplaint.Enabled = false;
            ddlaware.Text = "";
            ddlaware.Enabled = false;
            //ddlECM.Text = "";
            ddlECM.Enabled = false;
           // ddlCampaign.Text = "";
            ddlCampaign.Enabled = false;
           // radrating.Text = "";
            radrating.Enabled = false;
            ddlpayoutinfo.Enabled = false;
            ddlrakshacon.Enabled = false;
            ddlusrakshacon.Enabled = false;
            ddlfeedback.Enabled = false;
        }
	else
	{
	
            ddlVYMOAPPLOGIN.Enabled = true;
            
            ddlsubdisposition1.Enabled = true;
            
            ddlrating.Enabled = true;

            
            ddlcomplaint.Enabled = true;
           
            ddlaware.Enabled = true;
            
            ddlECM.Enabled = true;
           
            ddlCampaign.Enabled = true;
           
            radrating.Enabled = true;
            ddlpayoutinfo.Enabled = true;
            ddlrakshacon.Enabled = true;
            ddlusrakshacon.Enabled = true;
            ddlfeedback.Enabled = true;
   }



        radDispositionP.Items.Clear();
        radDispositionP.ClearSelection();



        using (SqlCommand cmd = new SqlCommand("Proc_get_dispN", con))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter calltype = cmd.Parameters.AddWithValue("@Calltype", Session["strcalltypes"].ToString());
            SqlParameter dispparam = cmd.Parameters.AddWithValue("@disp", RadCategory.Text);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["Disposition"].ToString());
                        radDispositionP.Items.Add(item);
                    }
                }
            }
        }

    }


    protected void radDispositionP_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (con.State == ConnectionState.Open) con.Close(); con.Open();


        RadSubdispositionP.Items.Clear();
        RadSubdispositionP.ClearSelection();



        using (SqlCommand cmd = new SqlCommand("proc_get_SubdispN", con))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter calltype = cmd.Parameters.AddWithValue("@Calltype", Session["strcalltypes"].ToString());
            SqlParameter maindispparam = cmd.Parameters.AddWithValue("@catogry", RadCategory.Text);
            SqlParameter dispparam = cmd.Parameters.AddWithValue("@disp", radDispositionP.Text);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["SubDisposition"].ToString());
                        RadSubdispositionP.Items.Add(item);
                    }
                }
            }
        }

    }
   
    protected void HangUpP_Click(object sender, EventArgs e)
    {
        CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();
        CtiWS1.Hangup("", "", strHost);
    }
    protected void DialP_Click(object sender, EventArgs e)
    {
        CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();

        CtiWS1.MakeCall("", "", "7359", txtphoneP.Value, "", "", strHost);
        CtiWS1.SendPbxDigits("", "", txtphoneP.Value, strHost);
    }

    protected void Dialalternate_Click(object sender, EventArgs e)
    {
        if (txtalternate.Text.Length == 10)
        {
            txtalternate.Text = "0" + txtalternate.Text;
        }
        CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();

        CtiWS1.MakeCall("", "", "7359", txtalternate.Text, "", "", strHost);
        CtiWS1.SendPbxDigits("", "", txtalternate.Text, strHost);
    }

    protected void HoldP_Click(object sender, EventArgs e)
    {

    }
    protected void HangUpalternate_Click(object sender, EventArgs e)
    {
        CtiWS.CtiWS CtiWS1 = new CtiWS.CtiWS();
        CtiWS1.Hangup("", "", strHost);
    }
    protected void fillhistory()
    {
        string STRCON = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
        SqlConnection con = new SqlConnection(STRCON);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        SqlCommand cmd = new SqlCommand("History_Outbound", con);
        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.AddWithValue("@lead_id", txtlead_idP.Text);

        con.Open();


        da.SelectCommand = cmd;
        da.Fill(dt);
        GrdHistory.DataSource = dt;
        GrdHistory.DataBind();

        con.Close();
    }
    protected void GrdHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void AppLogin_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        ddlsubdisposition1.Items.Clear();
        ddlsubdisposition1.ClearSelection();
        //ddlrating.Items.Clear();
       // ddlrating.ClearSelection();
        using (SqlCommand cmd = new SqlCommand("proc_get_VYMOAPPLOGIN_Subdisp", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlParameter dispparam = cmd.Parameters.AddWithValue("@disp", radDispositionP.SelectedItem.Text);
            SqlParameter subdisp = cmd.Parameters.AddWithValue("@p_VYMOAPPLOGIN", ddlVYMOAPPLOGIN.Text);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["SubDisposition1"].ToString());
                        ddlsubdisposition1.Items.Add(item);
                    }
                }
            }

        }
        con.Close();

    }
    protected void ddlsubdisp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        ddlrating.Items.Clear();
        ddlrating.ClearSelection();
        ddlrating.SelectedIndex = -1;
        using (SqlCommand cmd = new SqlCommand("proc_get_VYMOAPPLOGIN_rating", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlParameter dispparam = cmd.Parameters.AddWithValue("@disp", radDispositionP.SelectedItem.Text);
            SqlParameter subdisp = cmd.Parameters.AddWithValue("@p_VYMOAPPLOGIN", ddlVYMOAPPLOGIN.Text);
            SqlParameter subdisp1 = cmd.Parameters.AddWithValue("@p_SubDisposition1", ddlsubdisposition1.Text);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["Rating"].ToString());
                        ddlrating.Items.Add(item);
                        strdispcodevaluesApplogin.Value = dr["Code"].ToString();
                    }
                }
            }

        }
        con.Close();

    }
    protected void ddlECM_SelectedIndexChanged(object sender, EventArgs e)
    {

        con.Open();
        ddlCampaign.Items.Clear();
        ddlCampaign.ClearSelection();
        ddlCampaign.SelectedIndex = -1;
        using (SqlCommand cmd = new SqlCommand("proc_get_CampaignName", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlParameter dispparam = cmd.Parameters.AddWithValue("@disp", radDispositionP.SelectedItem.Text);
            SqlParameter strecm = cmd.Parameters.AddWithValue("@p_ISECM", ddlECM.Text);
            
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["CampaignName"].ToString());
                        ddlCampaign.Items.Add(item);
                        
                    }
                }
            }

        }
        con.Close();



       // if (ddlECM.SelectedItem.Text=="Yes")
       // {

       //     ddlCampaign.Items.Insert(0, new ListItem("PASA Campaign", ""));
       //     ddlCampaign.Items.Insert(1, new ListItem("RaskhaConnect_Customer", ""));
       //     ddlCampaign.Items.Insert(2, new ListItem("Coupon and Dividend", ""));
       //     ddlCampaign.Items.Insert(3, new ListItem("Medix", ""));
       //     ddlCampaign.Items.Insert(4, new ListItem("Maturity", ""));
       //     ddlCampaign.Items.Insert(5, new ListItem("Suraksha", ""));
       //     ddlCampaign.Items.Insert(6, new ListItem("Other", ""));

       // }
       //else
       // {

       //  ddlCampaign.Items.Clear();

       // }

    }
    protected void ddlsubdisp1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        con.Open();
        ddlrating.Items.Clear();
        ddlrating.ClearSelection();
        ddlrating.SelectedIndex = -1;
        using (SqlCommand cmd = new SqlCommand("proc_get_VYMOAPPLOGIN_rating", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlParameter dispparam = cmd.Parameters.AddWithValue("@disp", radDispositionP.SelectedItem.Text);
            SqlParameter subdisp = cmd.Parameters.AddWithValue("@p_VYMOAPPLOGIN", ddlVYMOAPPLOGIN.Text);
            SqlParameter subdisp1 = cmd.Parameters.AddWithValue("@p_SubDisposition1", ddlsubdisposition1.Text);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["Rating"].ToString());
                        ddlrating.Items.Add(item);
                        strdispcodevaluesApplogin.Value = dr["Code"].ToString();
                    }
                }
            }

        }
        con.Close();


    }

    protected void gvsimul_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "getSelectedData")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = gvsimul.Rows[rowIndex];

                //Fetch value of Name.
                string selectedLeadCode = (row.FindControl("lblLead_Code") as Label).Text;
                //txtlead_idP.Text = selectedLeadCode;

                if (selectedLeadCode != "")
                {
                    SHOWCustData(selectedLeadCode);

                }
                else
                {
                    Response.Write("<script>alert('CRM lead id not found!!!')</script>");
                    clearFields();
                    return;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void fillcustdetailsgrid()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                //using (MySqlCommand cmdTCStatus = new MySqlCommand("USP_ShowVerifierLeads", con))
                using (SqlCommand cmdTCStatus = new SqlCommand("Usp_Get_Advisor_Details", con))
                {
                    cmdTCStatus.CommandType = CommandType.StoredProcedure;



                    //cmdTCStatus.Parameters.AddWithValue("@Advisor_Code", txtAGENT_CODE.Text);
                    cmdTCStatus.Parameters.AddWithValue("@Advisor_Code", "4599826");

                    //cmdTCStatus.Parameters.AddWithValue("@P_POS_APPLICATION_NO", "1100065468307");



                    using (SqlDataAdapter sda = new SqlDataAdapter(cmdTCStatus))
                    {
                        cmdTCStatus.Connection = con;
                        sda.SelectCommand = cmdTCStatus;
                        using (dat1 = new DataTable())
                        {
                            con.Open();
                            sda.Fill(dat1);
                            gvsimul.DataSource = dat1;
                            gvsimul.DataBind();
                            con.Close();
                            
                       
                            
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    
    }

    public void SHOWCustData(string LeadCode)
    { 
        string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Usp_Get_AdvisorCust_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Lead_Code", LeadCode.ToString());
                



                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {


                    if (dr.Read())
                    {
                        lblleadcode.Text = dr["Lead_Code"].ToString();

                        txtadvisorname.Text = dr["Advisor_name"].ToString();

                        txtcustnm.Text = dr["Customer_name"].ToString();
                        txtadvreamrks.Text = dr["Remarks"].ToString();


                    }
                }
            }
        }

    
    }


    protected void btnadviser_Click(object sender, EventArgs e)
    {
        string STRCON = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
        SqlConnection con = new SqlConnection(STRCON);
        // if (con.State == ConnectionState.Open) con.Close(); con.Open();
        SqlCommand cmd = new SqlCommand("USP_SAVE_Adviserdetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Lead_ID", txtlead_idP.Text);
        cmd.Parameters.AddWithValue("@MOBILE_NO", txtphoneP.Value);
        cmd.Parameters.AddWithValue("@lead_last_agent_name", lblagentP.Text);
        cmd.Parameters.AddWithValue("@lead_import_batch_no", lblbatchP.Text);
        cmd.Parameters.AddWithValue("@MainDisposition", RadCategory.Text);
        cmd.Parameters.AddWithValue("@Disposition", radDispositionP.Text);
        cmd.Parameters.AddWithValue("@SubDisposition", RadSubdispositionP.Text);
        cmd.Parameters.AddWithValue("@CallbackDt", radPKPickupCallBackP.SelectedDate);
        cmd.Parameters.AddWithValue("@lead_remarks", txtremarkP.Text);
        cmd.Parameters.AddWithValue("@Jobdiscription", RadJobDiscription.Text);
        //---
        cmd.Parameters.AddWithValue("@VYMOAppLogin", ddlVYMOAPPLOGIN.Text);
        cmd.Parameters.AddWithValue("@SubDisposition1", ddlsubdisposition1.Text);
        cmd.Parameters.AddWithValue("@Rating", ddlrating.Text);
        cmd.Parameters.AddWithValue("@complaint", ddlcomplaint.Text);

        cmd.Parameters.AddWithValue("@Aware_feature_VYMO", ddlaware.Text);
        cmd.Parameters.AddWithValue("@ECM_Customer", ddlECM.Text);
        cmd.Parameters.AddWithValue("@Campaign_Name", ddlCampaign.Text);
        cmd.Parameters.AddWithValue("@radrating", radrating.Text);
        cmd.Parameters.AddWithValue("@alternateNORemarks", txtalternateremark.Text);

        cmd.Parameters.AddWithValue("@PayoutInfoGiven  ", ddlpayoutinfo.Text);

        cmd.Parameters.AddWithValue("@Awareness_About_RakshaConnect", ddlrakshacon.Text);
        cmd.Parameters.AddWithValue("@Usage_of_RakshaConnect", ddlusrakshacon.Text);
        cmd.Parameters.AddWithValue("@Feedback", ddlfeedback.Text);
        cmd.Parameters.AddWithValue("@Lead_Code", lblleadcode.Text);
        cmd.Parameters.AddWithValue("@Advisor_name", txtadvisorname.Text);
        cmd.Parameters.AddWithValue("@Customer_Name ", txtcustnm.Text);
        cmd.Parameters.AddWithValue("@cust_Contact", ddladvcontact.Text);
        cmd.Parameters.AddWithValue("@Lead_wise_Response", ddlleadwise.Text);
        cmd.Parameters.AddWithValue("@IF_NO ", ddlifno.Text);
        cmd.Parameters.AddWithValue("@advsRemarks ", txtadvreamrks.Text);
        cmd.Parameters.AddWithValue("@Advisor_Code ", txtAGENT_CODE.Text);   //txtAGENT_CODE.Text);  "4599826"





        //cmd.Parameters.AddWithValue("@lead_last_dial_status", strdispcodevalues);
        con.Open();
        int i = cmd.ExecuteNonQuery();

        con.Close();

        //Response.Write("<script>alert('Data Submitted')</script>");

        fillcustdetailsgrid();
       // clearadviserfield();

    }
    public void filladviserQ1()
    {
        if (con.State == ConnectionState.Open) con.Close(); con.Open();


        ddladvcontact.Items.Clear();
        ddladvcontact.ClearSelection();
        ddlleadwise.Items.Clear();
        ddlleadwise.ClearSelection();
        ddlifno.Items.Clear();
        ddlifno.ClearSelection();



        using (SqlCommand cmd = new SqlCommand("proc_get_AdviserQ1", con))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter calltype = cmd.Parameters.AddWithValue("@Calltype", Session["strcalltypes"].ToString());
            SqlParameter disp = cmd.Parameters.AddWithValue("@commandname", "Question1");
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["Question1"].ToString());
                        ddladvcontact.Items.Add(item);
                    }
                }
            }
        }

    }
    protected void ddladvcontact_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (con.State == ConnectionState.Open) con.Close(); con.Open();


        ddlleadwise.Items.Clear();
        ddlleadwise.ClearSelection();
        ddlifno.Items.Clear();
        ddlifno.ClearSelection();



        using (SqlCommand cmd = new SqlCommand("proc_get_AdviserQ1", con))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter calltype = cmd.Parameters.AddWithValue("@Calltype", Session["strcalltypes"].ToString());
            SqlParameter disp = cmd.Parameters.AddWithValue("@commandname", "Question2");
            SqlParameter disp1 = cmd.Parameters.AddWithValue("@Question1", ddladvcontact.Text);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["Question2"].ToString());
                        ddlleadwise.Items.Add(item);
                    }
                }
            }
        }

    }
    protected void ddlleadwise_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (con.State == ConnectionState.Open) con.Close(); con.Open();


        ddlifno.Items.Clear();
        ddlifno.ClearSelection();



        using (SqlCommand cmd = new SqlCommand("proc_get_AdviserQ1", con))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter calltype = cmd.Parameters.AddWithValue("@Calltype", Session["strcalltypes"].ToString());
            SqlParameter disp = cmd.Parameters.AddWithValue("@commandname", "Question3");
            SqlParameter disp1 = cmd.Parameters.AddWithValue("@Question1", ddladvcontact.Text);
            SqlParameter disp2 = cmd.Parameters.AddWithValue("@Question2", ddlleadwise.Text);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem(dr["Question3"].ToString());
                        ddlifno.Items.Add(item);
                    }
                }
            }
        }
    }

    public void clearadviserfield()
    {

        //lblleadcode.Text = "";
        txtadvisorname.Text = "";
        txtcustnm.Text = "";
        ddladvcontact.Text = "";
        ddlleadwise.Text = "";
        ddlifno.Text = "";
        txtadvreamrks.Text = "";
    }
}

-------------------------------------------------------------
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TATA_AIG.aspx.cs" Inherits="TATA_AIG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style6
        {
            width: 183px;
        }
        .nav-justified
        {
            width: 1087px;
        }
        .boxes
        {
        }
        .nowrap
        {
        }
          .pageView
        {
            background-image: url(Images/ankur.jpg);
            height: 750px;
            display: block;
            height: auto;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script language="javascript" type="text/javascript">
        function selectTab() {

            var tabstrip = $find('<%= RadTabStrip1.ClientID %>');
            tabstrip.get_tabs().getTab("3").click();
        }   
    </script>
    <%--<script type="text/javascript">

        function OnClientClicked(sender, args) {
            alert('ss');

            var window = $find('<%=UserListDialog.ClientID %>');

            window.close();

        }

    </script>--%>
    <div>
        <div style="margin-left: 80px">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
                <Services>
                    <asp:ServiceReference Path="~/Services/CtiWS.asmx" />
                </Services>
            </cc1:ToolkitScriptManager>
            <telerik:RadTabStrip runat="server" ID="RadTabStrip1" SelectedIndex="0" MultiPageID="RadMultiPage1"
                Style="margin-bottom: 0">
                <Tabs>
                    <telerik:RadTab Text="MASTER DETAILS" meta:resourcekey="RadTabStrip1" PageViewID="PASHA"
                        Enabled="true" Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab Text="CustDetails" meta:resourcekey="RadTabStrip1" PageViewID="SIM_Details">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Knowledge Bank" meta:resourcekey="RadTabStrip1" PageViewID="Scripts"
                    Enabled="true" Selected="True" >
                    </telerik:RadTab>
                    
                   
                    <%--<telerik:RadTab Text="HISTORY" meta:resourcekey="RadTabStrip1" PageViewID="PASHA_HISTORYP">
                    </telerik:RadTab>
                    <telerik:RadTab Text="NOTEPAD" meta:resourcekey="RadTabStrip1" PageViewID="OUTNOTEPAD">
                    </telerik:RadTab>
                   <telerik:RadTab Text="Knowledge Bank" meta:resourcekey="RadTabStrip1" PageViewID="Scripts"
                    Enabled="true" Selected="True" >
                    </telerik:RadTab>
                    <telerik:RadTab Text="ECM" meta:resourcekey="RadTabStrip1" PageViewID="Outbnd">
                    </telerik:RadTab>
                    <telerik:RadTab Text="ECM_HISTORY" meta:resourcekey="RadTabStrip1" PageViewID="HISTORY"
                        Selected="True">
                   </telerik:RadTab>--%>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" EnableEmbeddedScripts="true"
                Font-Names="Times New Roman">
                  <telerik:RadPageView ID="PASHA" runat="server" Height="700px" CssClass="Pageview" >
                    <fieldset style="border: medium double #000080" >
                        <legend>
                            <div style="font-size: 12px; font-family: 'Book Antiqua'; color: #FF0000;">
                                Call Details</div>
                        </legend>
                        <table class="nav-justified" cellspacing="8" cellpadding="5" title="TATA AIA ADOC" 
                            bgcolor="#33CCCC">
                            <caption>
                                <tr>
                                    <td bgcolor="#FFCCFF">
                                        <asp:Label ID="Label37" runat="server" Text="Lead ID"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlead_idP" runat="server" ReadOnly="True" ></asp:TextBox>
                                    </td>
                                    <td bgcolor="#FFCCFF">
                                        <asp:Label ID="Label39" runat="server" Text="Mobile No."></asp:Label>
                                    </td>


                                    
                                <td>

                               
                               <asp:HiddenField ID="txtphoneP" runat="server"/>
                                   

                                    <asp:TextBox ID="txtmask_txtmobilenoP" runat="server" class="textbox" MaxLength="10" ReadOnly="True"
                                        Width="176px"></asp:TextBox>
                                         



                                    
                                    </td>
                                    <td>
                                        <asp:Button ID="DialP" runat="server" Text="Dial" Width="69px" 
                                            BackColor="Silver" BorderStyle="Solid" onclick="DialP_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="HangUpP" runat="server" Text="HangUp" Width="90px" 
                                            BackColor="#FF3399" ForeColor="#FFFF66" onclick="HangUpP_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="HoldP" runat="server" Text="Hold" Width="90px" 
                                            Visible="False" onclick="HoldP_Click" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblbatchP" runat="server" Text="Batch Name" ForeColor="#FF0066"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblagentP" runat="server" Text="Agent Name" ForeColor="#FF33CC"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                <td bgcolor="#FFCCFF">
                                    <asp:Label ID="Label13" runat="server" class="label" Text="Alternate MOBILE_NO"></asp:Label>
                                </td>
                                <td>
                                <asp:TextBox ID="txtalternate" runat="server" class="textbox" MaxLength="10" 
                                        Width="176px"></asp:TextBox>
                                </td>
                                <td>
                                <asp:Button ID="Dialalternate" runat="server" Text="Dial" Width="69px" 
                                            BackColor="Silver" BorderStyle="Solid" onclick="Dialalternate_Click" />
                                
                                </td>
                                <td>
                                <asp:Button ID="HangUpalter" runat="server" Text="HangUp" Width="69px" 
                                            BackColor="Silver" BorderStyle="Solid" onclick="HangUpalternate_Click" />
                                
                                </td>
                                </tr>
                            </caption>
                        </table>
                    </fieldset>
                    <fieldset style="border: medium double #000080">
                        <legend>
                            <div style="font-size: 16px; font-family: Calibri; color: #FF0000;">
                                Customer Demographics
                            </div>
                        </legend>
                        <table  cellpadding="5" cellspacing="8" class="nav-justified" title="TATA AIA ADOC" 
                            width="720" bgcolor="#CCCCCC" >
                            <tr>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label42" runat="server" class="label" Text="Policy_Number" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolicy_Number" runat="server" class="textbox" ReadOnly="True" 
                                        Width="176px"></asp:TextBox>
                                </td>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label46" runat="server" class="label" Text="SUB_DATE" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSUB_DATE" runat="server" class="textbox"  ReadOnly="True"
                                        Width="176px"></asp:TextBox>
                                </td>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label52" runat="server" class="label" Text="AGENT_CODE" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAGENT_CODE" runat="server" class="textbox" ReadOnly="True" 
                                        Width="176px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            
                             <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label64" runat="server" class="label" Text="CUSTOMER NAME" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCUSTOMERNAME" runat="server" class="textbox" ReadOnly="True"
                                         Width="176px"></asp:TextBox>
                                </td>
                            
                               
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label54" runat="server" class="label" Text="SUB_ANP" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSUB_ANP" runat="server" class="textbox"  ReadOnly="True"
                                        Width="176px"></asp:TextBox>
                                </td>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label55" runat="server" class="label" Text="PLAN_NAME" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPLAN_NAME" runat="server" class="textbox" ReadOnly="True"
                                         Width="176px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label56" runat="server" class="label" Text="PROD_TYPE" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPROD_TYPE" runat="server" class="textbox" 
                                        Width="176px"></asp:TextBox>
                                </td>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label57" runat="server" class="label" Text="SUM_ASSURED" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSUM_ASSURED" runat="server" class="textbox" ReadOnly="True"
                                         Width="176px"></asp:TextBox>
                                </td>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label58" runat="server" class="label" Text="CHANNEL" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCHANNEL" runat="server" class="textbox"  ReadOnly="True"
                                        Width="176px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label59" runat="server" class="label" 
                                        Text="SUB_STATUS_DESCRIPTION" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSUB_STATUS_DESCRIPTION" runat="server" class="textbox" ReadOnly="True" 
                                        Width="176px"></asp:TextBox>
                                </td>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label60" runat="server" class="label" Text="STATUS_DESCRIPTION" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSTATUS_DESCRIPTION" runat="server" class="textbox" ReadOnly="True" 
                                        Width="176px"></asp:TextBox>
                                </td>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label61" runat="server" class="label" Text="MED_NON_MED" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMED_NON_MED" runat="server" class="textbox"  ReadOnly="True"
                                        Width="176px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label62" runat="server" class="label" Text="GENERIC_STATUS" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGENERIC_STATUS" runat="server" class="textbox" ReadOnly="True" 
                                        Width="176px"></asp:TextBox>
                                </td>
                                <td bgcolor="#E0E0E0">
                                <asp:Label ID="Label15" runat="server" class="label" Text="CUSTOMER NAME1" 
                                        ForeColor="Blue"></asp:Label>
                                    <asp:Label ID="Label63" runat="server" class="label" Text="PEDNING_REASONS" visible="false"
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPEDNING_REASONS" runat="server" class="textbox"  visible="false"
                                        Width="176px"></asp:TextBox>
                                        <asp:TextBox ID="txtCustname1" runat="server" class="textbox"  
                                        Width="176px"></asp:TextBox>
                                </td>
                               
                               
                               
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label53" runat="server" class="label" Text="SUB_FP" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txSUB_FP" runat="server" class="textbox"  ReadOnly="True"
                                        Width="176px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label65" runat="server" class="label" Text="CUSTOMER MOBILE NO" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCUSTOMERMOBILENO" runat="server" class="textbox" ReadOnly="True" 
                                        Width="176px"></asp:TextBox>
                                </td>
                                  <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label40" runat="server" class="label" 
                                          Text="CUSTOMER EMAIL ADDRESS" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCUSTOMEREMAILADDRESS" runat="server" class="textbox" ReadOnly="True" 
                                        Width="176px"></asp:TextBox>
                                </td>
                                 <td bgcolor="#E0E0E0">
                                    <asp:Label ID="Label41" runat="server" class="label" Text="PAYMENT_METHOD" 
                                         ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPAYMENT_METHOD" runat="server" class="textbox" ReadOnly="True" 
                                        Width="176px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="border: medium double #000080">
                        <legend>
                            <div style="font-size: 16px; font-family: Calibri; color: #FF0000;">
                                PENDING_REASONS:-</div>
                        </legend>
                        <table bgcolor="#99CCFF" cellpadding="5" cellspacing="8" class="nav-justified" title="TATA AIA"
                            width="720">
                            <tr>
                                                               <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label1" runat="server" class="label" Text="PENDING_REASONS:-" ForeColor="Blue"></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                 <td>
                                    <asp:TextBox ID="txtpending" runat="server" AutoPostBack="true" class="textbox" Width="1047px" ReadOnly=true
                                        Height="64px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                 </tr>
                        </table>
                    </fieldset>
                    
                       <fieldset style="border: medium double #000080">
                        <legend>
                            <div style="font-size: 16px; font-family: Calibri; color: #FF0000;">
                                Disposition Details</div>
                        </legend>
                        <table bgcolor="#99CCFF" cellpadding="5" cellspacing="8" class="nav-justified" title="TATA AIA"
                            width="720">
                            <tr>
                            <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label4" runat="server" class="label" Text="Main Dispostion" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCategory" runat="server" AllowCustomText="true" AutoPostBack="true"
                                        Width="150px" OnSelectedIndexChanged="RadCategory_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                 <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label118" runat="server" class="label" Text="Disposition" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="radDispositionP" runat="server" AllowCustomText="true" AutoPostBack="true"
                                        Filter="StartsWith" MarkFirstMatch="true" Width="150px" OnSelectedIndexChanged="radDispositionP_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label119" runat="server" class="label" Text="Subdisposition" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadSubdispositionP" runat="server" AllowCustomText="true"
                                        AutoPostBack="true" Filter="StartsWith" MarkFirstMatch="true" Width="150px" OnSelectedIndexChanged="RadSubdispositionP_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label121" runat="server" class="label" Text="Call Back" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDateTimePicker ID="radPKPickupCallBackP" runat="server" CssClass="boxes"
                                        Culture="en-US" Enabled="True" Height="20px" MinDate="1990-01-01" PopupDirection="BottomLeft"
                                        Width="170px" />
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label130" runat="server" class="label" Text="Remarks" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtremarkP" runat="server" AutoPostBack="true" class="textbox" Width="178px"
                                        Height="64px" TextMode="MultiLine"></asp:TextBox>
                                </td>

                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label3" runat="server" class="label" Text="Alternate NO. Remarks" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtalternateremark" runat="server" AutoPostBack="true" class="textbox" Width="178px"
                                        Height="64px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                 
                               
                                
                                <td>
                                    <asp:HiddenField ID="strdispcodevaluesP" runat="server" />
                                </td>
                            </tr>
                            </table>
                            </fieldset>

                            <fieldset style="border: medium double #000080">
                        <legend>
                            <div style="font-size: 16px; font-family: Calibri; color: #FF0000;">
                                Other Details</div>
                        </legend>
                        <table bgcolor="#99CCFF" cellpadding="5" cellspacing="8" class="nav-justified" title="TATA AIA"
                            width="720">
                            <tr>
                            <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label6" runat="server" class="label" Text="VYMO App Login/VYMO download"  ForeColor="Blue"  ></asp:Label>
                                </td>
                                <td>
                                    
                                     
                                    <telerik:RadComboBox ID="ddlVYMOAPPLOGIN" runat="server"  AutoPostBack="true"
                                         Width="150px" 
                                        onselectedindexchanged="AppLogin_SelectedIndexChanged" >
                                    </telerik:RadComboBox>                              
                                </td>

                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label7" runat="server" class="label" Text="Sub Disposition 1" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                     <telerik:RadComboBox ID="ddlsubdisposition1" runat="server"  AutoPostBack="true"
                                         Width="150px" 
                                        onselectedindexchanged="ddlsubdisp1_SelectedIndexChanged" >
                                    </telerik:RadComboBox>                               
                                </td>
                               <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label8" runat="server" class="label" Text="Sub Disposition 2" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                    <telerik:RadComboBox ID="ddlrating" runat="server" 
                                         Width="150px"> 
  
                                    </telerik:RadComboBox>                                 
                                </td>
                                 <td>
                                    <asp:HiddenField ID="strdispcodevaluesApplogin" runat="server" />
                                </td>
                            
                            </tr>
                            <tr>
                            <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label9" runat="server" class="label" Text="Complaint" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:DropDownList ID="ddlcomplaint" runat="server" Width="150px">
                                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                    </asp:DropDownList>                                
                                </td>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label5" runat="server" class="label" Text="Aware about usage & feature of VYMO" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:DropDownList ID="ddlaware" runat="server" Width="150px">
                                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                    </asp:DropDownList>                                
                                </td>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label12" runat="server" class="label" Text="Rating" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="radrating" runat="server" 
                                        AutoPostBack="true"  Width="150px">

                                         <Items>
                                         <telerik:RadComboBoxItem runat="server" Text="" 
                                                Value="" />
                                            <telerik:RadComboBoxItem runat="server" Text="1" 
                                                Value="1" />
                                                <telerik:RadComboBoxItem runat="server" Text="2" 
                                                Value="2" />
                                                 <telerik:RadComboBoxItem runat="server" Text="3" 
                                                Value="3" />
                                                 <telerik:RadComboBoxItem runat="server" Text="4" 
                                                Value="4" />
                                                 <telerik:RadComboBoxItem runat="server" Text="5" 
                                                Value="5" />

                                        </Items>

                                    </telerik:RadComboBox>
                                </td>
                                 
                                </tr>
                                <tr>
                                 <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label10" runat="server" class="label" Text="Recently have contacted any ECM Customer" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:DropDownList ID="ddlECM" runat="server" Width="150px" AutoPostBack="true"  onselectedindexchanged="ddlECM_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text=" "></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                    </asp:DropDownList>                                
                                </td>
                                 <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label11" runat="server" class="label" Text="Campaign Name" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlCampaign" runat="server" 
                                         Width="150px"> 
  
                                    </telerik:RadComboBox>  
                                    <%--<asp:DropDownList ID="ddlCampaign" runat="server" Width="150px">
                                    
                                    </asp:DropDownList>  --%>                              
                                </td>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label2" runat="server" class="label" Text="Job Description" 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadJobDiscription" runat="server" AllowCustomText="True"
                                         Filter="StartsWith" MarkFirstMatch="True" 
                                        Width="150px">
                                    
                                        <Items>
                                         <telerik:RadComboBoxItem runat="server" Text="" 
                                                Value="" />
                                            <telerik:RadComboBoxItem runat="server" Text="Full Time" 
                                                Value="Full Time" />
                                                <telerik:RadComboBoxItem runat="server" Text="Part Time" 
                                                Value="Part Time" />
                                                 <telerik:RadComboBoxItem runat="server" Text="Prospect Will Inform to BM" 
                                                Value="Prospect Will Inform to BM" />
                                        </Items>
                                    
                                    </telerik:RadComboBox>
                                </td>
                                </tr>
                                <tr>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label14" runat="server" class="label" Text="Payout Information Given" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:DropDownList ID="ddlpayoutinfo" runat="server" Width="150px" AutoPostBack="true" Filter="StartsWith" MarkFirstMatch="True" >
                                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                    </asp:DropDownList>                                
                                </td>

                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label16" runat="server" class="label" Text="Awareness About RakshaConnect" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:DropDownList ID="ddlrakshacon" runat="server" Width="150px" AutoPostBack="true" Filter="StartsWith" MarkFirstMatch="True" >
                                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                    </asp:DropDownList>                                
                                </td>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label17" runat="server" class="label" Text="Usage of RakshaConnect" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:DropDownList ID="ddlusrakshacon" runat="server" Width="150px" AutoPostBack="true" Filter="StartsWith" MarkFirstMatch="True" >
                                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    <asp:ListItem Value="Once every day" Text="Once every day"></asp:ListItem>
                                    <asp:ListItem Value="Once in 2-4 days" Text="Once in 2-4 days"></asp:ListItem>
                                    <asp:ListItem Value="Once a week" Text="Once a week"></asp:ListItem>
                                    <asp:ListItem Value="Once a fortnight" Text="Once a fortnight"></asp:ListItem>
                                    <asp:ListItem Value="Once a month" Text="Once a month"></asp:ListItem>
                                     <asp:ListItem Value="Not adopted VYMO" Text="Not adopted VYMO"></asp:ListItem>
                                    </asp:DropDownList>                                
                                </td>

                                

                                </tr>
                                <tr>
                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label18" runat="server" class="label" Text="Feedback" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:DropDownList ID="ddlfeedback" runat="server" Width="150px" AutoPostBack="true" Filter="StartsWith" MarkFirstMatch="True" >
                                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                    </asp:DropDownList>                                
                                </td>

                                 <td>
                                <asp:Button ID="btnsaveadoc" runat="server" Text="Next Call" 
                                        onclick="btnsaveadoc_Click" BackColor="#99FFCC" BorderColor="#FF0066" 
                                        BorderStyle="Dotted" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                                        Font-Underline="True" ForeColor="Maroon" Height="36px" Width="135px" />
                                </td>
                            </tr>
                        </table>
                           
                    </fieldset>
                    <asp:GridView ID="GrdHistory" runat="server" CssClass="table table-striped table-bordered dt-responsive nowrap"
                    AutoGenerateColumns="false" OnRowDataBound="GrdHistory_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="lead_id" HeaderText="lead id" />
                        <asp:BoundField DataField="lead_last_dial_time" HeaderText="Call Date" />
                        <asp:BoundField DataField="lead_last_agent_name" HeaderText="Agent Name" />
                         <asp:BoundField DataField="Subdisposition" HeaderText="Subdisposition" />
                          <asp:BoundField DataField="lead_remarks" HeaderText="Remarks" />

                        
                    </Columns>
                </asp:GridView>

                </telerik:RadPageView> 

                <telerik:RadPageView ID="SIM_Details" runat="server" Height="700px">
                <fieldset style="border: medium double #000080">
                <legend>
                        <div style="font-size: 15px; font-family: 'Book Antiqua'; font-weight: bold; color: #1a4c78;
                            width: auto;">
                            Simultaneous Customer Details
                        </div>
                    </legend>
                    <div>
                    <table class="nav-justified">

                    <tr>
                    <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label19" runat="server" class="label" Text="Advisor_name:"  ForeColor="Blue"  ></asp:Label>
                                </td>
                                <td>
                                <asp:TextBox ID="txtadvisorname" runat="server" class="textInput"></asp:TextBox>
                                </td>

                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label20" runat="server" class="label" Text="Customer-Name:"  ForeColor="Blue"  ></asp:Label>
                                </td>
                                <td>
                                <asp:TextBox ID="txtcustnm" runat="server" class="textInput"></asp:TextBox>
                                </td>

                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label21" runat="server" class="label" Text="Contact"  ForeColor="Blue"  ></asp:Label>
                                </td>
                                <td>

                                <telerik:RadComboBox ID="ddladvcontact" runat="server" AllowCustomText="true" AutoPostBack="true"
                                        Width="150px" Filter="StartsWith" MarkFirstMatch="true" OnSelectedIndexChanged="ddladvcontact_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                
                                </td>



                    </tr>
                    <tr>    
                                
                    <td bgcolor="#FFFFE8">
                    <asp:Label ID="lblleadcode" runat="server" class="label" Text="Lead-wise Response"  ForeColor="Blue" Visible="false"  ></asp:Label>
                                    <asp:Label ID="Label22" runat="server" class="label" Text="Lead-wise Response"  ForeColor="Blue"  ></asp:Label>
                                </td>
                                <td>
                                  <telerik:RadComboBox ID="ddlleadwise" runat="server" AllowCustomText="true" AutoPostBack="true"
                                        Width="150px" Filter="StartsWith" MarkFirstMatch="true" OnSelectedIndexChanged="ddlleadwise_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                   
                                </td>

                                <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label23" runat="server" class="label" Text="IF NO "  ForeColor="Blue"  ></asp:Label>
                                </td>
                                <td>
                                <telerik:RadComboBox ID="ddlifno" runat="server" AllowCustomText="true" AutoPostBack="true"
                                        Width="150px" Filter="StartsWith" MarkFirstMatch="true">
                                    </telerik:RadComboBox>
                   
                                </td>

                             <td bgcolor="#FFFFE8">
                                    <asp:Label ID="Label24" runat="server" class="label" Text="Reamrks"  ForeColor="Blue"  ></asp:Label>
                                </td>
                                <td>
                                <asp:TextBox ID="txtadvreamrks" runat="server" class="textbox" TextMode="MultiLine" MaxLength="250" 
                                        Width="176px" ></asp:TextBox>
                                </td>

                                </tr>
                                <tr>
                                <td>
                                <asp:Button ID="btnadviser" runat="server" Text="Save" 
                                        onclick="btnadviser_Click" BackColor="#99FFCC" BorderColor="#FF0066" 
                                        BorderStyle="Dotted" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                                        Font-Underline="True" ForeColor="Maroon" Height="36px" Width="135px" />
                                </td>
                                
                                </tr>
                                

                                </table>
                    </div>

                                </fieldset>

                                <fieldset style="border: medium double #000080">
                      <legend>
                        <div style="font-size: 15px; font-family: 'Book Antiqua'; font-weight: bold; color: #1a4c78;
                            width: auto;">
                            
                        </div>
                        </legend>
                        <div class="dvContent">
                        <asp:GridView ID="gvsimul" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager"
                            HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                            Width="900px" OnRowCommand="gvsimul_RowCommand">
                            <Columns>

                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="getSelectedData"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lead Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLead_Code" runat="server" Text='<%#Eval("Lead_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomer_name" runat="server" Text='<%#Eval("Customer_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Advisor name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdvisor_name" runat="server" Text='<%#Eval("Advisor_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATA Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDATA_Name" runat="server" Text='<%#Eval("DATA_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile_number" runat="server" Text='<%#Eval("Mobile_number")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                </asp:GridView>
                                </div>
                                </fieldset>

                    

                                

                </telerik:RadPageView>
 
                <%--</telerik:RadPageView>--%>
                 <telerik:RadPageView ID="Scripts" runat="server" Height="700px">
                    <iframe id="Iframe4" runat="server" frameborder="0" name="ifRightPane" scrolling="yes"
                        width="100%" height="95%"></iframe>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>  
    </form>
</body>
</html>


