using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AjaxControlToolkit;
using RO.Facade3;
using RO.Common3;
using RO.Common3.Data;
using RO.WebRules;

namespace RO.Common3.Data
{
	public class AdmRptElm91 : DataSet
	{
		public AdmRptElm91()
		{
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptElmDef")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptElmAdd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptElmUpd")));
			this.Tables.Add(MakeDtlColumns(new DataTable("AdmRptElmDel")));
			this.DataSetName = "AdmRptElm91";
			this.Namespace = "http://Rintagi.com/DataSet/AdmRptElm91";
		}

		private DataTable MakeDtlColumns(DataTable dt)
		{
			DataColumnCollection columns = dt.Columns;
			columns.Add("RptElmId160", typeof(string));
			columns.Add("ReportId160", typeof(string));
			columns.Add("RptElmTypeCd160", typeof(string));
			columns.Add("RptStyleId160", typeof(string));
			columns.Add("ElmHeight160", typeof(string));
			columns.Add("ElmColumns160", typeof(string));
			columns.Add("ElmColSpacing160", typeof(string));
			columns.Add("ElmPrintFirst160", typeof(string));
			columns.Add("ElmPrintLast160", typeof(string));
			return dt;
		}
	}
}

namespace RO.Web
{
	public partial class AdmRptElmModule : RO.Web.ModuleBase
	{

		private const string KEY_lastAddedRow = "Cache:lastAddedRow3_91";
		private const string KEY_lastSortOrd = "Cache:lastSortOrd3_91";
		private const string KEY_lastSortImg = "Cache:lastSortImg3_91";
		private const string KEY_lastSortCol = "Cache:lastSortCol3_91";
		private const string KEY_lastSortExp = "Cache:lastSortExp3_91";
		private const string KEY_lastSortUrl = "Cache:lastSortUrl3_91";
		private const string KEY_lastSortTog = "Cache:lastSortTog3_91";
		private const string KEY_lastImpPwdOvride = "Cache:lastImpPwdOvride3_91";
		private const string KEY_cntImpPwdOvride = "Cache:cntImpPwdOvride3_91";
		private const string KEY_currPageIndex = "Cache:currPageIndex3_91";
		private const string KEY_bHiImpVisible = "Cache:bHiImpVisible3_91";
		private const string KEY_bShImpVisible = "Cache:bShImpVisible3_91";
		private const string KEY_dtAdmRptElmGrid = "Cache:dtAdmRptElmGrid";
		private const string KEY_scrImport = "Cache:scrImport3_91";
		private const string KEY_dtScreenHlp = "Cache:dtScreenHlp3_91";
		private const string KEY_dtClientRule = "Cache:dtClientRule3_91";
		private const string KEY_dtAuthCol = "Cache:dtAuthCol3_91";
		private const string KEY_dtAuthRow = "Cache:dtAuthRow3_91";
		private const string KEY_dtLabel = "Cache:dtLabel3_91";
		private const string KEY_dtCriHlp = "Cache:dtCriHlp3_91";
		private const string KEY_dtScrCri = "Cache:dtScrCri3_91";
		private const string KEY_dsScrCriVal = "Cache:dsScrCriVal3_91";

		private const string KEY_dtReportId160 = "Cache:dtReportId160";
		private const string KEY_dtRptElmTypeCd160 = "Cache:dtRptElmTypeCd160";
		private const string KEY_dtRptStyleId160 = "Cache:dtRptStyleId160";

		private const string KEY_dtSystems = "Cache:dtSystems3_91";
		private const string KEY_sysConnectionString = "Cache:sysConnectionString3_91";
		private const string KEY_bUndoAllVisible = "Cache:bUndoAllVisible3_91";
		private const string KEY_bUpdateVisible = "Cache:bUpdateVisible3_91";

		private const string KEY_bClCriVisible = "Cache:bClCriVisible3_91";
		private byte LcSystemId;
		private string LcSysConnString;
		private string LcAppConnString;
		private string LcAppDb;
		private string LcDesDb;
		private string LcAppPw;
		protected System.Collections.Generic.Dictionary<string, DataRow> LcAuth;
		// *** Custom Function/Procedure Web Rule starts here *** //

		public AdmRptElmModule()
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LUser.Culture);
			string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			string lang = "en,zh".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;
			try { ScriptManager.RegisterStartupScript(this, this.GetType(), "datepicker_i18n", File.ReadAllText(Request.MapPath("~/scripts/i18n/jquery.ui.datepicker-" + lang + ".js")), true); } catch { };
			if (!IsPostBack) { Session.Remove(KEY_dtScrCri); }
			int ii = 0; DataView dvCri = GetScrCriteria(); SetCriHolder(ii, dvCri);
			bConfirm.Value = "Y";
			// To get around ajax not displaying ErrMsg and InfoMsg; Set them to Y to show immediately:
			bErrNow.Value = "N"; bInfoNow.Value = "N"; bExpNow.Value = "N";
			CtrlToFocus.Value = string.Empty;
			EnableValidators(false);
			if (!IsPostBack)
			{
				DataTable dtMenuAccess = (new MenuSystem()).GetMenu(base.LUser.CultureId, 3, base.LImpr, LcSysConnString, LcAppPw,91, null, null);
				if (dtMenuAccess.Rows.Count == 0 && !IsCronInvoked())
				{
				    string message = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "AccessDeny", null, null, null);
				    throw new Exception(message);
				}
				cFind.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cFindButton.ClientID + "')");
				cPgSize.Attributes.Add("OnKeyDown", "return EnterKeyCtrl(event,'" + cPgSizeButton.ClientID + "')");
				Session[KEY_lastImpPwdOvride] = 0; Session[KEY_cntImpPwdOvride] = 0; Session[KEY_currPageIndex] = 0;
				Session.Remove(KEY_dtAdmRptElmGrid);
				Session.Remove(KEY_lastSortCol);
				Session.Remove(KEY_lastSortExp);
				Session.Remove(KEY_lastSortImg);
				Session.Remove(KEY_lastSortUrl);
				Session.Remove(KEY_dtSystems);
				Session.Remove(KEY_sysConnectionString);
				Session.Remove(KEY_sysConnectionString + "Pwd");
				Session.Remove(KEY_dtScreenHlp);
				Session.Remove(KEY_dtClientRule);
				Session.Remove(KEY_dtAuthCol);
				Session.Remove(KEY_dtAuthRow);
				Session.Remove(KEY_dtLabel);
				Session.Remove(KEY_dtCriHlp);
				Session.Remove(KEY_dtReportId160);
				Session.Remove(KEY_dtRptElmTypeCd160);
				Session.Remove(KEY_dtRptStyleId160);
				SetButtonHlp();
				GetSystems();
				SetColumnAuthority();
				GetGlobalFilter();
				GetScreenFilter();
				GetPageInfo();
				GetCriteria(dvCri);
				DataTable dtHlp = GetScreenHlp();
				cHelpMsg.HelpTitle = dtHlp.Rows[0]["ScreenTitle"].ToString(); cHelpMsg.HelpMsg = dtHlp.Rows[0]["DefaultHlpMsg"].ToString();
				cFootLabel.Text = dtHlp.Rows[0]["FootNote"].ToString();
				if (cFootLabel.Text == string.Empty) { cFootLabel.Visible = false; }
				cTitleLabel.Text = dtHlp.Rows[0]["ScreenTitle"].ToString();
				SetClientRule(null,false);
				IgnoreConfirm(); InitPreserve();
				cSystemId_SelectedIndexChanged(null, null);
				try
				{
					(new AdminSystem()).LogUsage(base.LUser.UsrId, string.Empty, dtHlp.Rows[0]["ScreenTitle"].ToString(), 91, 0, 0, string.Empty, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				// *** Criteria Trigger (before) Web Rule starts here *** //
				PopAdmRptElm91List(sender, e, true, null);
				if (cAuditButton.Attributes["OnClick"] == null || cAuditButton.Attributes["OnClick"].IndexOf("AdmScrAudit.aspx") < 0) { cAuditButton.Attributes["OnClick"] += "SearchLink('AdmScrAudit.aspx?cri0=91&typ=N&sys=3','','',''); return false;"; }
				cRptStyleId160Search_Script();
				// *** Page Load (End of) Web Rule starts here *** //
				cBrowse.Attributes.Add("OnChange", "__doPostBack('" + cBrowseButton.ClientID.Replace("_", "$") + "','')");
				DataTable dtLabel = (new AdminSystem()).GetLabels(base.LUser.CultureId, "cGrid", null, null, null);
				if (dtLabel != null)
				{
				    cGridFtrLabel.Text = dtLabel.Rows[0][1].ToString();
				    cGridFndLabel.Text = dtLabel.Rows[1][1].ToString();
				    cImpPwdLabel.Text = dtLabel.Rows[2][1].ToString();
				    cImportPwd.ToolTip = dtLabel.Rows[3][1].ToString();
				    cGridWksLabel.Text = dtLabel.Rows[4][1].ToString();
				    cGridStrLabel.Text = dtLabel.Rows[5][1].ToString();
				    cStartRow.ToolTip = dtLabel.Rows[6][1].ToString();
				}
			}
			else
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
				if (dt != null)
				{
					string sr = ViewState["_SortColumn"] as string;
					if (sr != null) { dt.DefaultView.Sort = sr.ToString(); }
					string rf = ViewState["_RowFilter"] as string;
					if (rf != null) { dt.DefaultView.RowFilter = rf.ToString(); }
				}
			}
			if (IsPostBack && !ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack) {SetClientRule(null,false);};
			if (!IsPostBack)	// Test for Viewstate being lost.
			{
				bViewState.Text = "Y";
				string xx = Session["Idle:" + Request.Url.PathAndQuery] as string;
				if (xx == "Y")
				{
					Session.Remove("Idle:" + Request.Url.PathAndQuery);
					bInfoNow.Value = "Y"; PreMsgPopup("Page has been idled past preset limit and no longer valid, please be notified that it has been reloaded.");
				}
			}
			else if (string.IsNullOrEmpty(bViewState.Text))		// Viewstate is lost.
			{
				Session["Idle:" + Request.Url.PathAndQuery] = "Y"; this.Redirect(Request.Url.PathAndQuery);
			}
			ScriptManager.RegisterStartupScript(this, this.GetType(), "ScreenLabel", this.ClientID + "={" + string.Join(",", (from dr in GetLabel().AsEnumerable() select string.Format("{0}: {{msg:'{1}',hint:'{2}'}}", "c" + dr.Field<string>("ColumnName") + (dr.Field<int?>("TableId").ToString()), (dr.Field<string>("ErrMessage") ?? string.Empty).Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"), (dr.Field<string>("TbHint") ?? string.Empty).Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"))).ToArray()) + "};", true);
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			// *** Page Init (Front of) Web Rule starts here *** //
			InitializeComponent();
			// *** Page Init (End of) Web Rule starts here *** //
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			CheckAuthentication(true);
			if (LcSysConnString == null)
			{
				SetSystem(3);
				try
				{
					DataTable dt;
					dt = (new AdminSystem()).GetAuthRow(91,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
					Session[KEY_dtAuthRow] = dt;
					DataRow dr = dt.Rows[0];
					if (!((dr["AllowUpd"].ToString() == "N" || dr["ViewOnly"].ToString() == "G")) && (Request.QueryString["enb"] == null || Request.QueryString["enb"] != "N"))
					{
						cAdmRptElmGrid.PreRender += new EventHandler(cAdmRptElmGrid_OnPreRender);
					}
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}

		}
		#endregion

		private void SetSystem(byte SystemId)
		{
			LcSystemId = SystemId;
			LcSysConnString = base.SysConnectStr(SystemId);
			LcAppConnString = base.AppConnectStr(SystemId);
			LcDesDb = base.DesDb(SystemId);
			LcAppDb = base.AppDb(SystemId);
			LcAppPw = base.AppPwd(SystemId);
			try
			{
				base.CPrj = new CurrPrj(((new RobotSystem()).GetEntityList()).Rows[0]);
				DataRow row = base.SystemsList.Rows.Find(SystemId);
				base.CSrc = new CurrSrc(true, row);
				base.CTar = new CurrTar(true, row);
				if ((Config.DeployType == "DEV" || row["dbAppDatabase"].ToString() == base.CPrj.EntityCode + "View") && !(base.CPrj.EntityCode != "RO" && row["SysProgram"].ToString() == "Y") && (new AdminSystem()).IsRegenNeeded(string.Empty,91,0,0,LcSysConnString,LcAppPw))
				{
					(new GenScreensSystem()).CreateProgram(91, "Report Element", row["dbAppDatabase"].ToString(), base.CPrj, base.CSrc, base.CTar, LcAppConnString, LcAppPw);
					this.Redirect(Request.RawUrl);
				}
			}
			catch (Exception e) { bErrNow.Value = "Y"; PreMsgPopup(e.Message); }
		}

		private void EnableValidators(bool bEnable)
		{
			foreach (System.Web.UI.WebControls.WebControl va in Page.Validators)
			{
				if (bEnable) {va.Enabled = true;} else {va.Enabled = false;}
			}
		}

		private void CheckAuthentication(bool pageLoad)
		{
			if (IsCronInvoked())
			{
				AnonymousLogin();
				LCurr.SystemId = 3;
				LCurr.DbId = 3;
			}
			else CheckAuthentication(pageLoad, true);
		}

		private void SetButtonHlp()
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetButtonHlp(91,0,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (dt != null && dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						if (dr["ButtonTypeName"].ToString() == "ClearCri") { cClearCriButton.CssClass = "ButtonImg ClearCriButtonImg"; Session[KEY_bClCriVisible] = base.GetBool(dr["ButtonVisible"].ToString()); cClearCriButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Save") { cSaveButton.CssClass = "ButtonImg SaveButtonImg"; cSaveButton.Text = dr["ButtonName"].ToString(); cSaveButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cSaveButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bUpdateVisible] = cSaveButton.Visible; }
						else if (dr["ButtonTypeName"].ToString() == "ExpTxt") { cExpTxtButton.CssClass = "ButtonImg ExpTxtButtonImg"; cExpTxtButton.Text = dr["ButtonName"].ToString(); cExpTxtButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cExpTxtButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "ExpRtf") { cExpRtfButton.CssClass = "ButtonImg ExpRtfButtonImg"; cExpRtfButton.Text = dr["ButtonName"].ToString(); cExpRtfButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cExpRtfButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "UndoAll") { cUndoAllButton.CssClass = "ButtonImg UndoAllButtonImg"; cUndoAllButton.Text = dr["ButtonName"].ToString(); cUndoAllButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cUndoAllButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bUndoAllVisible] = cUndoAllButton.Visible; }
						else if (dr["ButtonTypeName"].ToString() == "More") { cMoreButton.CssClass = "ButtonImg MoreButtonImg"; cMoreButton.Text = dr["ButtonName"].ToString(); cMoreButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cMoreButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "SaveClose") { cSaveCloseButton.CssClass = "ButtonImg SaveCloseButtonImg"; cSaveCloseButton.Text = dr["ButtonName"].ToString(); cSaveCloseButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cSaveCloseButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Edit") { cEditButton.CssClass = "ButtonImg EditButtonImg"; cEditButton.Text = dr["ButtonName"].ToString(); cEditButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cEditButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Preview") { cPreviewButton.CssClass = "ButtonImg PreviewButtonImg"; cPreviewButton.Text = dr["ButtonName"].ToString(); cPreviewButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cPreviewButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						if (dr["ButtonTypeName"].ToString() == "Audit") { cAuditButton.CssClass = "ButtonImg AuditButtonImg"; cAuditButton.Text = dr["ButtonName"].ToString(); cAuditButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cAuditButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						if (dr["ButtonTypeName"].ToString() == "InsRow") { cInsRowButton.CssClass = "ButtonImg InsRowButtonImg"; cInsRowButton.Text = "ADD"; cInsRowButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cInsRowButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "PgSize") { cPgSizeButton.CssClass = "ButtonImg PgSizeButtonImg"; cPgSizeButton.Text = string.Empty; cPgSizeButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cPgSizeButton.ToolTip = dr["ButtonToolTip"].ToString(); cPgSize.Visible = base.GetBool(dr["ButtonVisible"].ToString()); }
						else if (dr["ButtonTypeName"].ToString() == "First") { cFirstButton.CssClass = "ButtonImg FirstButtonImg"; cFirstButton.Text = string.Empty; cFirstButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cFirstButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Prev") { cPrevButton.CssClass = "ButtonImg PrevButtonImg"; cPrevButton.Text = string.Empty; cPrevButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cPrevButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Next") { cNextButton.CssClass = "ButtonImg NextButtonImg"; cNextButton.Text = string.Empty; cNextButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cNextButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Last") { cLastButton.CssClass = "ButtonImg LastButtonImg"; cLastButton.Text = string.Empty; cLastButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cLastButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Find") { cFindButton.CssClass = "ButtonImg FindButtonImg"; cFindButton.Text = string.Empty; cFindButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cFindButton.ToolTip = dr["ButtonToolTip"].ToString(); cFind.Visible = base.GetBool(dr["ButtonVisible"].ToString()); }
						else if (dr["ButtonTypeName"].ToString() == "Import") { cImportButton.CssClass = "ButtonImg ImportButtonImg"; cImportButton.Text = string.Empty; cImportButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cImportButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "Continue") { cContinueButton.CssClass = "ButtonImg ContinueButtonImg"; cContinueButton.Text = string.Empty; cContinueButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cContinueButton.ToolTip = dr["ButtonToolTip"].ToString(); }
						else if (dr["ButtonTypeName"].ToString() == "HideImp") { cHideImpButton.CssClass = "ButtonImg HideImpButtonImg"; cHideImpButton.Text = string.Empty; cHideImpButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cHideImpButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bHiImpVisible] = base.GetBool(dr["ButtonVisible"].ToString()); cHideImpButton.Visible = false; }
						else if (dr["ButtonTypeName"].ToString() == "ShowImp") { cShowImpButton.CssClass = "ButtonImg ShowImpButtonImg"; cShowImpButton.Text = string.Empty; cShowImpButton.Visible = base.GetBool(dr["ButtonVisible"].ToString()); cShowImpButton.ToolTip = dr["ButtonToolTip"].ToString(); Session[KEY_bShImpVisible] = cShowImpButton.Visible; }
					}
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private DataTable GetClientRule()
		{
			DataTable dtRul = (DataTable)Session[KEY_dtClientRule];
			if (dtRul == null)
			{
				CheckAuthentication(false);
				try
				{
					dtRul = (new AdminSystem()).GetClientRule(91,0,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtClientRule] = dtRul;
			}
			return dtRul;
		}

		private void SetClientRule(ListViewDataItem ee, bool isEditItem)
		{
			DataView dvRul = new DataView(GetClientRule());
			if (dvRul.Count > 0)
			{
				WebControl cc = null;
				string srp = string.Empty;
				string sn = string.Empty;
				string st = string.Empty;
				string sg = string.Empty;
				int ii = 0;
				int jj = 0;
				Regex missing = new Regex("@[0-9]+@");
				string lang2 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
				string lang = "en,zh".IndexOf(lang2) < 0 ? lang2 : Thread.CurrentThread.CurrentCulture.Name;
				foreach (DataRowView drv in dvRul)
				{
					if (ee == null || drv["ScriptEvent"].ToString().Substring(0,2).ToLower() != "on" || (cAdmRptElmGrid.EditIndex > -1 && GetDataItemIndex(cAdmRptElmGrid.EditIndex) == ee.DataItemIndex))
					{
						srp = drv["ScriptName"].ToString();
						srp = srp.Replace("@cLang@", "\'" + lang + "\'");
						if (drv["ParamName"].ToString() != string.Empty)
						{
							StringBuilder sbName =  new StringBuilder();
							StringBuilder sbType =  new StringBuilder();
							StringBuilder sbInGrid =  new StringBuilder();
							sbName.Append(drv["ParamName"].ToString().Trim());
							sbType.Append(drv["ParamType"].ToString().Trim());
							sbInGrid.Append(drv["ParamInGrid"].ToString().Trim());
							ii = 0;
							while (sbName.Length > 0)
							{
								sn = Utils.PopFirstWord(sbName,(char)44); st = Utils.PopFirstWord(sbType,(char)44);sg = Utils.PopFirstWord(sbInGrid,(char)44);
								if (ee != null && sg == "Y")
								{
									if (st.ToLower() == "combobox" && isEditItem) {srp = srp.Replace("@" + ii.ToString() + "@","'"+((RoboCoder.WebControls.ComboBox)ee.FindControl(sn)).KeyID+"'");} else {srp = srp.Replace("@" + ii.ToString() + "@","'"+((WebControl)ee.FindControl(sn + (!isEditItem ? "l" : ""))).ClientID+"'");}
								}
								else
								{
									if (st.ToLower() == "combobox") {srp = srp.Replace("@" + ii.ToString() + "@","'"+((RoboCoder.WebControls.ComboBox)this.FindControl(sn)).KeyID+"'");} else {srp = srp.Replace("@" + ii.ToString() + "@","'"+((WebControl)this.FindControl(sn)).ClientID+"'");}
								}
								ii = ii + 1;
							}
						}
						if (drv["ScriptEvent"].ToString() == "js")
						{
							ScriptManager.RegisterStartupScript(this, this.GetType(), drv["ColName"].ToString() + "_" + drv["ScriptName"].ToString() + jj.ToString(),"<script>"+ drv["ClientRuleProg"].ToString() + srp + "</script>", false);
						}
						else if (drv["ScriptEvent"].ToString() == "css")
						{
							ScriptManager.RegisterClientScriptBlock(this, this.GetType(), drv["ColName"].ToString() + "_" + drv["ScriptName"].ToString() + jj.ToString(),"<style>"+ drv["ClientRuleProg"].ToString() + "</style>", false);
						}
						else if (drv["ScriptEvent"].ToString() == "css_link")
						{
							ScriptManager.RegisterClientScriptBlock(this, this.GetType(), drv["ColName"].ToString() + "_" + drv["ScriptName"].ToString() + jj.ToString(),"<link  rel='stylesheet' type='text/css' href='"+ drv["ClientRuleProg"].ToString() + "' />", false);
						}
						else if (drv["ScriptEvent"].ToString() == "js_link")
						{
							ScriptManager.RegisterClientScriptBlock(this, this.GetType(), drv["ColName"].ToString() + "_" + drv["ScriptName"].ToString() + jj.ToString(),"<script type='text/javascript' src='"+ drv["ClientRuleProg"].ToString() + "'></script>", false);
						}
						else
						{
							srp = missing.Replace(srp, "''");
							string scriptEvent = drv["ScriptEvent"].ToString().TrimStart(new char[]{'_'});
							if (ee != null) {cc = ee.FindControl(drv["ColName"].ToString()) as WebControl;} else {cc = this.FindControl(drv["ColName"].ToString()) as WebControl;}
							if (cc != null && (cc.Attributes[scriptEvent] == null || cc.Attributes[scriptEvent].IndexOf(srp) < 0)) {cc.Attributes[scriptEvent] += srp;}
							if (ee != null && drv["ScriptEvent"].ToString().Substring(0,2).ToLower() != "on")
							{
								cc = ee.FindControl(drv["ColName"].ToString() + "l") as WebControl;
								if (cc != null && (cc.Attributes[scriptEvent] == null || cc.Attributes[scriptEvent].IndexOf(srp) < 0)) {cc.Attributes[scriptEvent] += srp;}
							}
						}
					}
					jj = jj + 1;
				}
			}
		}

		private DataTable GetScreenCriHlp()
		{
			DataTable dtCriHlp = (DataTable)Session[KEY_dtCriHlp];
			if (dtCriHlp == null)
			{
				CheckAuthentication(false);
				try
				{
					dtCriHlp = (new AdminSystem()).GetScreenCriHlp(91,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtCriHlp] = dtCriHlp;
			}
			return dtCriHlp;
		}

		private DataTable GetScreenHlp()
		{
			DataTable dtHlp = (DataTable)Session[KEY_dtScreenHlp];
			if (dtHlp == null)
			{
				CheckAuthentication(false);
				try
				{
					dtHlp = (new AdminSystem()).GetScreenHlp(91,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtScreenHlp] = dtHlp;
			}
			return dtHlp;
		}

		private void GetGlobalFilter()
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetGlobalFilter(base.LUser.UsrId,91,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (dt != null && dt.Rows.Count > 0)
				{
					cGlobalFilter.Text = dt.Rows[0]["FilterDesc"].ToString();
					cGlobalFilter.Visible = true;
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private void GetScreenFilter()
		{
			try
			{
				DataTable dt = (new AdminSystem()).GetScreenFilter(91,base.LUser.CultureId,LcSysConnString,LcAppPw);
				if (dt != null)
				{
					cFilterId.DataSource = dt;
					cFilterId.DataBind();
					if (cFilterId.Items.Count > 0)
					{
						if (Request.QueryString["ftr"] != null) {cFilterId.Items.FindByValue(Request.QueryString["ftr"]).Selected = true;} else {cFilterId.Items[0].Selected = true;}
						cFilterLabel.Text = (new AdminSystem()).GetLabel(base.LUser.CultureId, "QFilter", "QFilter", null, null, null);
						cFilter.Visible = true;
					}
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private void GetSystems()
		{
			Session[KEY_sysConnectionString] = LcSysConnString;
			Session[KEY_sysConnectionString + "Pwd"] = LcAppPw;
			DataTable dtSystems = base.SystemsList;
			if (dtSystems != null)
			{
				Session[KEY_dtSystems] = dtSystems;
				cSystemId.DataSource = dtSystems;
				cSystemId.DataBind();
				if (cSystemId.Items.Count > 1)
				{
					if (Request.QueryString["sys"] != null) {cSystemId.Items.FindByValue(Request.QueryString["sys"]).Selected = true;}
					else
					{
						try { cSystemId.Items.FindByValue(base.LCurr.DbId.ToString()).Selected = true; }
						catch {cSystemId.Items[0].Selected = true;}
					}
					base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
					Session[KEY_sysConnectionString] = Config.GetConnStr(dtSystems.Rows[cSystemId.SelectedIndex]["dbAppProvider"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["ServerName"].ToString(), dtSystems.Rows[cSystemId.SelectedIndex]["dbDesDatabase"].ToString(), "", dtSystems.Rows[cSystemId.SelectedIndex]["dbAppUserId"].ToString());
					Session[KEY_sysConnectionString + "Pwd"] = base.AppPwd(base.LCurr.DbId);
					cSystemLabel.Text = (new AdminSystem()).GetLabel(base.LUser.CultureId, "cSystem", "Label", null, null, null);
					cSystem.Visible = true;
				}
			}
		}

		protected string ColumnWatermark(int idx)
		{
			return GetLabel().Rows[idx]["tbHint"].ToString();
		}
		protected string ColumnHeaderText(int idx)
		{
			return (GetLabel().Rows[idx]["RequiredValid"].ToString() == "Y" ? Config.MandatoryChar : string.Empty)  + GetLabel().Rows[idx]["ColumnHeader"].ToString();
		}
		protected string ColumnToolTip(int idx)
		{
			return GetLabel().Rows[idx]["ToolTip"].ToString();
		}
		protected bool GridColumnVisible(int idx)
		{
			return GetAuthCol().Rows[idx]["ColVisible"].ToString()=="Y";
		}
		protected bool GridColumnEnable(int idx)
		{
			return GetAuthCol().Rows[idx]["ColReadOnly"].ToString()=="N";
		}
		private DataTable GetLabel()
		{
			DataTable dt = (DataTable)Session[KEY_dtLabel];
			if (dt == null)
			{
				try
				{
					dt = (new AdminSystem()).GetScreenLabel(91,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtLabel] = dt;
			}
			return dt;
		}

		private DataTable GetAuthCol()
		{
			DataTable dt = (DataTable)Session[KEY_dtAuthCol];
			if (dt == null)
			{
				try
				{
					dt = (new AdminSystem()).GetAuthCol(91,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtAuthCol] = dt;
			}
			return dt;
		}

		protected DataTable GetAuthRow()
		{
			DataTable dt = (DataTable)Session[KEY_dtAuthRow];
			if (dt == null)
			{
				try
				{
					dt = (new AdminSystem()).GetAuthRow(91,base.LImpr.RowAuthoritys,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
				Session[KEY_dtAuthRow] = dt;
			}
			return dt;
		}

		private void getReport(string eExport)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				CheckAuthentication(false);
				DataView dv = null;
				try {dv = ((DataTable)Session[KEY_dtAdmRptElmGrid]).DefaultView;}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (eExport == "TXT")
				{
					DataTable dtAu;
					dtAu = (new AdminSystem()).GetAuthExp(91,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
					if (dtAu != null)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[1]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[2]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + (char)9 + dtAu.Rows[3]["ColumnHeader"].ToString() + " Text" + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + (char)9);}
						sb.Append(Environment.NewLine);
					}
					foreach (DataRowView drv in dv)
					{
						if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["RptElmId160"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["ReportId160"].ToString() + (char)9 + drv["ReportId160Text"].ToString() + (char)9);}
						if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["RptElmTypeCd160"].ToString().Replace("\"","\"\"") + "\"" + (char)9 + "\"" + drv["RptElmTypeCd160Text"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["RptStyleId160"].ToString() + (char)9 + drv["RptStyleId160Text"].ToString() + (char)9);}
						if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["ElmHeight160"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ElmColumns160"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["ElmColSpacing160"].ToString(),base.LUser.Culture) + (char)9);}
						if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ElmPrintFirst160"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append("\"" + drv["ElmPrintLast160"].ToString().Replace("\"","\"\"") + "\"" + (char)9);}
						sb.Append(Environment.NewLine);
					}
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmRptElm.csv"; Session["ExportStr"] = (Config.ExportExcelCSV ? "sep=\t\n": "") + sb.Replace("\r\n","\n");
				}
				else if (eExport == "RTF")
				{
					sb.Append(@"{\rtf1\ansi\ansicpg1252\uc1\deff0\stshfdbch0\stshfloch0\stshfhich0\stshfbi0\deflang1033\deflangfe1033{\fonttbl{\f0\froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\f37\fswiss\fcharset0\fprq2{\*\panose 020b0604030504040204}Verdana;}
{\f182\froman\fcharset238\fprq2 Times New Roman CE;}{\f183\froman\fcharset204\fprq2 Times New Roman Cyr;}{\f185\froman\fcharset161\fprq2 Times New Roman Greek;}{\f186\froman\fcharset162\fprq2 Times New Roman Tur;}
{\f187\froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\f188\froman\fcharset178\fprq2 Times New Roman (Arabic);}{\f189\froman\fcharset186\fprq2 Times New Roman Baltic;}{\f190\froman\fcharset163\fprq2 Times New Roman (Vietnamese);}
{\f552\fswiss\fcharset238\fprq2 Verdana CE;}{\f553\fswiss\fcharset204\fprq2 Verdana Cyr;}{\f555\fswiss\fcharset161\fprq2 Verdana Greek;}{\f556\fswiss\fcharset162\fprq2 Verdana Tur;}{\f559\fswiss\fcharset186\fprq2 Verdana Baltic;}
{\f560\fswiss\fcharset163\fprq2 Verdana (Vietnamese);}}{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;
\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;\red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;\red192\green192\blue192;}{\stylesheet{
\ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \snext0 Normal;}{\*\cs10 \additive \ssemihidden Default Paragraph Font;}{\*
\ts11\tsrowd\trftsWidthB3\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tscellwidthfts0\tsvertalt\tsbrdrt\tsbrdrl\tsbrdrb\tsbrdrr\tsbrdrdgl\tsbrdrdgr\tsbrdrh\tsbrdrv 
\ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs20\lang1024\langfe1024\cgrid\langnp1024\langfenp1024 \snext11 \ssemihidden Normal Table;}}{\*\latentstyles\lsdstimax156\lsdlockeddef0}{\*\rsidtbl \rsid1574081\rsid2952683
\rsid4674135\rsid4855636\rsid6620441\rsid6645578\rsid7160396\rsid7497391\rsid8133092\rsid8259185\rsid8528326\rsid8936003\rsid9058410\rsid10305085\rsid10505691\rsid13726773\rsid14047122\rsid14576392\rsid15756354\rsid16472473\rsid16525787}{\*\generator Micr
osoft Word 11.0.6359;}{\info{\title [[ScreenTitle]]}{\author }{\operator }{\creatim\yr2004\mo12\dy21\hr12\min7}{\revtim\yr2004\mo12\dy21\hr16\min16}{\version7}{\edmins5}{\nofpages1}{\nofwords6}{\nofchars38}
{\*\company robocoder corporation}{\nofcharsws43}{\vern24703}}\margl1440\margr1440 \widowctrl\ftnbj\aenddoc\noxlattoyen\expshrtn\noultrlspc\dntblnsbdb\nospaceforul\hyphcaps0\formshade\horzdoc\dgmargin\dghspace180\dgvspace180\dghorigin1440\dgvorigin1440
\dghshow1\dgvshow1\jexpand\viewkind1\viewscale100\pgbrdrhead\pgbrdrfoot\splytwnine\ftnlytwnine\htmautsp\nolnhtadjtbl\useltbaln\alntblind\lytcalctblwd\lyttblrtgr\lnbrkrule\nobrkwrptbl\snaptogridincell\allowfieldendsel\wrppunct
\asianbrkrule\rsidroot4855636\newtblstyruls\nogrowautofit \fet0\sectd \linex0\endnhere\sectlinegrid360\sectdefaultcl\sectrsid7497391\sftnbj {\*\pnseclvl1\pnucrm\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl2\pnucltr\pnstart1\pnindent720\pnhang 
{\pntxta .}}{\*\pnseclvl3\pndec\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl4\pnlcltr\pnstart1\pnindent720\pnhang {\pntxta )}}{\*\pnseclvl5\pndec\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl6\pnlcltr\pnstart1\pnindent720\pnhang 
{\pntxtb (}{\pntxta )}}{\*\pnseclvl7\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl8\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl9\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}\pard\plain 
\qc \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid4855636 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 {\b\fs32\insrsid4855636\charrsid6645578 [[TitleLabel]]}{\b\fs32\insrsid16472473\charrsid6645578 
\par }{\insrsid4855636 
\par }{\b\i\fs18\insrsid4855636\charrsid8936003 [[GlobalFilter]]
\par }\pard \ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid8259185 {\insrsid7497391 
\par }\pard \qc \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid4855636 {\f37\fs16\insrsid4855636\charrsid1574081 [[Grid]]
\par }\pard \ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 {\insrsid4855636 
\par }}");
					sb.Replace("[[TitleLabel]]", cTitleLabel.Text);
					sb.Replace("[[GlobalFilter]]", cGlobalFilter.Text);
					sb.Replace("[[Grid]]", GetRtfGrid(dv));
					bExpNow.Value = "Y"; Session["ExportFnm"] = "AdmRptElm.rtf"; Session["ExportStr"] = sb;
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private string GetRtfGrid(DataView dv)
		{
			StringBuilder sb = new StringBuilder("");
			try
			{
				int iColCnt = 0;
				DataTable dtAu;
				dtAu = (new AdminSystem()).GetAuthExp(91,base.LUser.CultureId,base.LImpr,base.LCurr,LcSysConnString,LcAppPw);
				if (dtAu != null)
				{
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {iColCnt = iColCnt + 1;}
					//Create Header
					sb.Append(@"\trowd \irow0\irowband0\lastrow \ts15\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \trbrdrh\brdrs\brdrw10 \trbrdrv\brdrs\brdrw10 ");
					sb.Append(@"\trftsWidth1\trftsWidthB3\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblrsid2981395\tbllkhdrrows\tbllklastrow\tbllkhdrcols\tbllklastcol ");
					sb.Append("\r\n");
					sb.Append(GenCellx(iColCnt));
					sb.Append(@"\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\adjustright\rin0\lin0 \lang1033\langfe1033\cgrid\langnp1033\langfenp1033 ");
					sb.Append("\r\n");
					sb.Append(@"\b");
					sb.Append(@"{");
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[0]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[1]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[2]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[3]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[4]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[5]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[6]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[7]["ColumnHeader"].ToString() + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(dtAu.Rows[8]["ColumnHeader"].ToString() + @"\cell ");}
					sb.Append(@"}");
					sb.Append(@"\b0");
					sb.Append("\r\n");
					sb.Append(@"\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\adjustright\rin0\lin0 {");
					sb.Append(@"\insrsid2981395 \trowd \irow0\irowband0\lastrow \ts15\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \trbrdrh\brdrs\brdrw10 \trbrdrv\brdrs\brdrw10 ");
					sb.Append(@"\trftsWidth1\trftsWidthB3\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblrsid2981395\tbllkhdrrows\tbllklastrow\tbllkhdrcols\tbllklastcol ");
					sb.Append("\r\n");
					sb.Append(GenCellx(iColCnt));
					sb.Append("\r\n");
					sb.Append(@"\row }");
					sb.Append("\r\n");
				}
				//Create Data Rows
				foreach (DataRowView drv in dv)
				{
					sb.Append(@"\trowd \irow0\irowband0\lastrow \ts15\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \trbrdrh\brdrs\brdrw10 \trbrdrv\brdrs\brdrw10 ");
					sb.Append(@"\trftsWidth1\trftsWidthB3\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblrsid2981395\tbllkhdrrows\tbllklastrow\tbllkhdrcols\tbllklastcol ");
					sb.Append("\r\n");
					sb.Append(GenCellx(iColCnt));
					sb.Append(@"\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\adjustright\rin0\lin0 \lang1033\langfe1033\cgrid\langnp1033\langfenp1033 ");
					sb.Append("\r\n");
					sb.Append(@"{");
					if (dtAu.Rows[0]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["RptElmId160"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[1]["ColExport"].ToString() == "Y") {sb.Append(drv["ReportId160Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[2]["ColExport"].ToString() == "Y") {sb.Append(drv["RptElmTypeCd160Text"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[3]["ColExport"].ToString() == "Y") {sb.Append(drv["RptStyleId160Text"].ToString() + @"\cell ");}
					if (dtAu.Rows[4]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["ElmHeight160"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[5]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("0",drv["ElmColumns160"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[6]["ColExport"].ToString() == "Y") {sb.Append(RO.Common3.Utils.fmNumeric("2",drv["ElmColSpacing160"].ToString(),base.LUser.Culture) + @"\cell ");}
					if (dtAu.Rows[7]["ColExport"].ToString() == "Y") {sb.Append(drv["ElmPrintFirst160"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					if (dtAu.Rows[8]["ColExport"].ToString() == "Y") {sb.Append(drv["ElmPrintLast160"].ToString().Replace("\r\n",@"\par ") + @"\cell ");}
					sb.Append(@"}");
					sb.Append("\r\n");
					sb.Append(@"\pard \ql \li0\ri0\widctlpar\intbl\aspalpha\aspnum\adjustright\rin0\lin0 {");
					sb.Append(@"\insrsid2981395 \trowd \irow0\irowband0\lastrow \ts15\trgaph108\trleft-108\trbrdrt\brdrs\brdrw10 \trbrdrl\brdrs\brdrw10 \trbrdrb\brdrs\brdrw10 \trbrdrr\brdrs\brdrw10 \trbrdrh\brdrs\brdrw10 \trbrdrv\brdrs\brdrw10 ");
					sb.Append(@"\trftsWidth1\trftsWidthB3\trautofit1\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\tblrsid2981395\tbllkhdrrows\tbllklastrow\tbllkhdrcols\tbllklastcol ");
					sb.Append("\r\n");
					sb.Append(GenCellx(iColCnt));
					sb.Append("\r\n");
					sb.Append(@"\row }");
					sb.Append("\r\n");
				}
				sb.Append(@"\pard \par }");
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			return sb.ToString();
		}

		private StringBuilder GenCellx(int CellCnt)
		{
			StringBuilder sb = new StringBuilder("");
			string strRowPre = @"\clvertalt\clbrdrt\brdrs\brdrw10 \clbrdrl\brdrs\brdrw10 \clbrdrb\brdrs\brdrw10 \clbrdrr\brdrs\brdrw10 \cltxlrtb\clftsWidth3\clwWidth4428\clshdrawnil ";
			for (int cnt=0; cnt<CellCnt; cnt++) {sb.Append(strRowPre + @"\cellx" + cnt.ToString() + " ");}
			return sb;
		}

		public void cRptStyleId160Search_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			RoboCoder.WebControls.ComboBox cc = ((RoboCoder.WebControls.ComboBox)cAdmRptElmGrid.EditItem.FindControl("cRptStyleId160")); cc.SetTbVisible(); Session["CtrlAdmRptStyle"] = cc.FocusID;
		}

		public void cRptStyleId160Search_Script()
		{
			if (cAdmRptElmGrid.EditIndex > -1 && cAdmRptElmGrid.Items.Count > cAdmRptElmGrid.EditIndex)
			{
				ImageButton ib = (ImageButton)cAdmRptElmGrid.EditItem.FindControl("cRptStyleId160Search");
				if (ib != null)
				{
			    	TextBox pp = ((TextBox)cAdmRptElmGrid.EditItem.FindControl("cRptElmId160"));
					RoboCoder.WebControls.ComboBox cc = (RoboCoder.WebControls.ComboBox)cAdmRptElmGrid.EditItem.FindControl("cRptStyleId160");
					string ss = "&dsp=ComboBox"; if (cSystem.Visible) {ss = ss + "&sys=" + cSystemId.SelectedValue;} else {ss = ss + "&sys=3";}
					ib.Attributes["onclick"] = "SearchLink('AdmRptStyle.aspx?col=RptStyleId" + ss + "&typ=N" + "','" + cc.FocusID + "','',''); return false;";
				}
			}
		}

		public void cExpTxtButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			getReport("TXT");
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cExpRtfButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			getReport("RTF");
			// *** System Button Click (after) Web Rule starts here *** //
		}

		private void GetPageInfo()
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetLastPageInfo(91, base.LUser.UsrId, LcSysConnString, LcAppPw);
				cPgSize.Text = dt.Rows[0]["LastPageInfo"].ToString();
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		public void cClearCriButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
			DataView dvCri = GetScrCriteria();
			foreach (DataRowView drv in dvCri)
			{
			    if (drv["DisplayName"].ToString() == "ComboBox")	// Reset to page 1 by reassigning the datasource:
			    {
					cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cComboBox != null && cComboBox.Items.Count > 0) { cComboBox.DataSource = cComboBox.DataSource; cComboBox.SelectByValue(cComboBox.Items[0].Value, string.Empty, true); }
			    }
			    else if (drv["DisplayName"].ToString() == "DropDownList")
			    {
					cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cDropDownList != null && cDropDownList.Items.Count > 0) { cDropDownList.SelectedIndex = 0; }
			    }
			    else if (drv["DisplayName"].ToString() == "ListBox")
			    {
					cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cListBox != null && cListBox.Items.Count > 0) { cListBox.SelectedIndex = 0; }
			    }
			    else if (drv["DisplayName"].ToString() == "RadioButtonList")
			    {
					cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cRadioButtonList != null && cRadioButtonList.Items.Count > 0) { cRadioButtonList.SelectedIndex = 0; }
			    }
			    else if (drv["DisplayName"].ToString() == "Calendar")
			    {
					cCalendar = (System.Web.UI.WebControls.Calendar)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cCalendar != null)
					{
					    if (drv["RequiredValid"].ToString() == "N") { cCalendar.SelectedDates.Clear(); } else { cCalendar.SelectedDate = System.DateTime.Today; }
					}
			    }
			    else if (drv["DisplayName"].ToString() == "CheckBox")
			    {
					cCheckBox = (CheckBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cCheckBox != null) { cCheckBox.Checked = false; }
			    }
			    else
			    {
					cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cTextBox != null)
					{
					    if (drv["RequiredValid"].ToString() == "N") { cTextBox.Text = string.Empty; } else if (drv["DisplayMode"].ToString().IndexOf("Date") >= 0) { cTextBox.Text = System.DateTime.Today.ToString(); } else { cTextBox.Text = "0"; }
					}
			    }
			}
			cCriButton_Click(sender, e);
			if (cFilterId.Items.Count > 0) { cFilterId.SelectedIndex = 0; }
			// *** System Button Click (after) Web Rule starts here *** //
		}

		private void GetCriteria(DataView dvCri)
		{
			try
			{
				DataTable dt;
				dt = (new AdminSystem()).GetLastCriteria(int.Parse(dvCri.Count.ToString()) + 1, 91, 0, base.LUser.UsrId, LcSysConnString, LcAppPw);
				cCriPanel.Visible = true;   // Enable cCriteria.Visible to be set:
				if (dt.Rows.Count <= 0) { cCriteria.Visible = false; }
				if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
				{
					cCriPanel.Visible = false; cClearCriButton.Visible = false;
				}
				else
				{
					cCriPanel.Visible = (cCriteria.Visible && cCriteria.Controls.Count > 2) || cFilter.Visible;
					if ((bool)Session[KEY_bClCriVisible]) {cClearCriButton.Visible = cCriteria.Visible && cCriteria.Controls.Count > 2;} else {cClearCriButton.Visible = false;}
				}
				if (!IsPostBack)
				{
					int jj = 0; // Zero-based to be consistent with SQL reporting.
					foreach (DataRowView drv in dvCri)
					{
						if (Request.QueryString["Cri" + jj.ToString()] != null)
						{
							dt.Rows[jj+1]["LastCriteria"] = Request.QueryString["Cri" + jj.ToString()].ToString();
						}
						jj = jj + 1;
					}
				}
			    SetCriteria(dt, dvCri);
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private void GetSelectedItems(ListBox cObj, string selectedItems)
		{
			string selectedItem;
			bool finish;
			if (selectedItems == string.Empty)
			{
				try {cObj.SelectedIndex = 0;} catch {}
			}
			else
			{
				finish = false;
				while (!finish)
				{
					selectedItem = GetSelectedItem(ref selectedItems);
					if (selectedItem == string.Empty) { finish = true; }
					else
					{
						try { cObj.Items.FindByValue(selectedItem).Selected = true; }
						catch { finish = true; try {cObj.SelectedIndex = 0;} catch {} }
					}
				}
			}
		}

		private string GetSelectedItem(ref string selectedItems)
		{
			string selectedItem;
			int sIndex = selectedItems.IndexOf("'");
			int eIndex = selectedItems.IndexOf("'",sIndex + 1);
			if (sIndex >= 0 && eIndex >= 0)
			{
				selectedItem = selectedItems.Substring(sIndex + 1, eIndex - sIndex - 1);
			}
			else
			{
				selectedItem = string.Empty;
			}
			selectedItems = selectedItems.Substring(eIndex + 1, selectedItems.Length - eIndex - 1);
			return selectedItem;
		}

		protected void cCriButton_Click(object sender, System.EventArgs e)
		{
			// *** Criteria Trigger (before) Web Rule starts here *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if (cAdmRptElmGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
			if (IsPostBack && cCriteria.Visible) ReBindCriteria(GetScrCriteria());
			UpdCriteria(true);
			PopAdmRptElm91List(sender, e, false, null);
			}
		}

		private void SetCriteria(DataTable dt, DataView dvCri)
		{
			Label cLabel;
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
			DataTable dtCriHlp = GetScreenCriHlp();
			int ii = 1;
			try
			{
				foreach (DataRowView drv in dvCri)
				{
					cLabel = (Label)cCriteria.FindControl("x" + drv["ColumnName"].ToString() + "Label");
					if (drv["DisplayName"].ToString() == "ComboBox")
					{
						cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = null; try {val=dt.Rows[ii]["LastCriteria"].ToString();} catch {};
						base.SetCriBehavior(cComboBox, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("91", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(dt.Rows[ii]["LastCriteria"], string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cDropDownList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("91", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cDropDownList.DataSource = dv;
						cDropDownList.DataBind();
						try { cDropDownList.Items.FindByValue(dt.Rows[ii]["LastCriteria"].ToString()).Selected = true; } catch { try { cDropDownList.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "ListBox")
					{
						cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = null; try {val=dt.Rows[ii]["LastCriteria"].ToString();} catch {};
						base.SetCriBehavior(cListBox, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("91", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, dt.Rows[ii]["LastCriteria"].ToString());
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cRadioButtonList, null, cLabel, dtCriHlp.Rows[ii-1]);
						(new AdminSystem()).MkGetScreenIn("91", drv["ScreenCriId"].ToString(), "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), LcAppDb, LcDesDb, drv["MultiDesignDb"].ToString(), LcSysConnString, LcAppPw);
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cRadioButtonList.DataSource = dv;
						cRadioButtonList.DataBind();
						try { cRadioButtonList.Items.FindByValue(dt.Rows[ii]["LastCriteria"].ToString()).Selected = true; } catch { try { cRadioButtonList.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "Calendar")
					{
						cCalendar = (System.Web.UI.WebControls.Calendar)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cCalendar, null, cLabel, dtCriHlp.Rows[ii-1]);
						if (dt.Rows[ii]["LastCriteria"].ToString() == string.Empty)
						{
							cCalendar.SelectedDates.Clear();
						}
						else
						{
							cCalendar.SelectedDate = DateTime.Parse(dt.Rows[ii]["LastCriteria"].ToString()); cCalendar.VisibleDate = cCalendar.SelectedDate;
						}
					}
					else if (drv["DisplayName"].ToString() == "CheckBox")
					{
						cCheckBox = (CheckBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cCheckBox, null, cLabel, dtCriHlp.Rows[ii-1]);
						if (dt.Rows[ii]["LastCriteria"].ToString() != string.Empty) { cCheckBox.Checked = base.GetBool(dt.Rows[ii]["LastCriteria"].ToString()); }
					}
					else
					{
						cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						base.SetCriBehavior(cTextBox, null, cLabel, dtCriHlp.Rows[ii-1]);
						cTextBox.Text = dt.Rows[ii]["LastCriteria"].ToString();
					}
					ii = ii + 1;
				}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		}

		private void ReBindCriteria(DataView dvCri)
		{
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			try
			{
				foreach (DataRowView drv in dvCri)
				{
					if (string.IsNullOrEmpty(drv["DdlFtrColumnId"].ToString())  && drv["DisplayMode"].ToString() != "AutoListBox" ) continue;
					if (drv["DisplayName"].ToString() == "ComboBox")
					{
						cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cComboBox.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", val, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cComboBox.DataSource = dv;
						try { cComboBox.SelectByValue(val, string.Empty, false); } catch { try { cComboBox.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "DropDownList")
					{
						cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cDropDownList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cDropDownList.DataSource = dv;
						cDropDownList.DataBind();
						try { cDropDownList.Items.FindByValue(val).Selected = true; } catch { try { cDropDownList.SelectedIndex = 0; } catch { } }
					}
					else if (drv["DisplayName"].ToString() == "ListBox")
					{
						cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						TextBox cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString() + "Hidden");
						string selectedValues = string.Join(",", cListBox.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => "'" + x.Value + "'").ToArray());
						if (drv["DisplayMode"].ToString() == "AutoListBox" && cTextBox != null) selectedValues = cTextBox.Text ;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)), drv["RequiredValid"].ToString(), 0, string.Empty, drv["DisplayMode"].ToString() != "AutoListBox", selectedValues, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cListBox.DataSource = dv;
						cListBox.DataBind();
						GetSelectedItems(cListBox, selectedValues);
					}
					else if (drv["DisplayName"].ToString() == "RadioButtonList")
					{
						cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
						string val = cRadioButtonList.SelectedValue;
						DataView dv = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)), drv["RequiredValid"].ToString(), 0, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId)));
						FilterCriteriaDdl(cCriteria, dv, drv);
						cRadioButtonList.DataSource = dv;
						cRadioButtonList.DataBind();
						try { cRadioButtonList.Items.FindByValue(val).Selected = true; } catch { try { cRadioButtonList.SelectedIndex = 0; } catch { } }
					}
				}
			}
			catch { }
		}

		private DataView GetScrCriteria()
		{
		    DataTable dtScrCri = (DataTable)Session[KEY_dtScrCri];
		    if (dtScrCri == null)
		    {
				try
				{
					dtScrCri = (new AdminSystem()).GetScrCriteria("91", LcSysConnString, LcAppPw);
				}
				catch (Exception err) { ErrorTrace(err, "critical"); bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
		        Session[KEY_dtScrCri] = dtScrCri;
		    }
		    return dtScrCri.DefaultView;
		}

		private void MakeScrGrpLab(DataRowView drv)
		{
		    Literal cLiteral = new Literal();
			string sLabelCss = drv["LabelCss"].ToString();
			if (sLabelCss != string.Empty)
			{
				if (sLabelCss.StartsWith(".")) {cLiteral.Text = "<div class=\"" + sLabelCss.Substring(1) + "\">";} else {cLiteral.Text = "<div class=\"r-labelL\" style=\"" + sLabelCss + "\">";}
			}
			else {cLiteral.Text = "<div class=\"r-labelL\">";}
		    cCriteria.Controls.Add(cLiteral);
		    Label cLabel = new Label(); cLabel.ID = "x" + drv["ColumnName"].ToString() + "Label"; cLabel.CssClass = "inp-lbl"; cCriteria.Controls.Add(cLabel);
		    cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
		}

		private void MakeScrGrpInp(DataRowView drv)
		{
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
		    Literal cLiteral = new Literal();
			string sContentCss = drv["ContentCss"].ToString();
			if (sContentCss != string.Empty)
			{
				if (sContentCss.StartsWith(".")) {cLiteral.Text = "<div class=\"" + sContentCss.Substring(1) + "\">";} else {cLiteral.Text = "<div class=\"r-content\" style=\"" + sContentCss + "\">";}
			}
			else {cLiteral.Text = "<div class=\"r-content\">";}
		    cCriteria.Controls.Add(cLiteral);
		    if (drv["DisplayName"].ToString() == "ComboBox")
		    {
		        cComboBox = new RoboCoder.WebControls.ComboBox(); cComboBox.ID = "x" + drv["ColumnName"].ToString(); cComboBox.CssClass = "inp-ddl"; cComboBox.SelectedIndexChanged += new EventHandler(cCriButton_Click);
		        cComboBox.DataValueField = drv["DdlKeyColumnName"].ToString(); cComboBox.DataTextField = drv["DdlRefColumnName"].ToString(); cComboBox.AutoPostBack = true;
		        if (drv["DisplayMode"].ToString() == "AutoComplete")
		        {
			        WebControl wcFtr = (WebControl)cCriteria.FindControl("x" + drv["DdlFtrColumnName"].ToString());
		            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
		            context["method"] = "GetScreenIn";
			        context["addnew"] = "Y";
		            context["sp"] = "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString();
		            context["requiredValid"] = drv["RequiredValid"].ToString();
		            context["mKey"] = drv["DdlKeyColumnName"].ToString();
		            context["mVal"] = drv["DdlRefColumnName"].ToString();
		            context["mTip"] = drv["DdlRefColumnName"].ToString();
		            context["mImg"] = drv["DdlRefColumnName"].ToString();
		            context["ssd"] = Request.QueryString["ssd"];
		            context["scr"] = "91";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            cComboBox.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
		            cComboBox.DataContext = context;
		            cComboBox.Mode = "A";
		        }
		        cCriteria.Controls.Add(cComboBox);
		    }
		    else if (drv["DisplayName"].ToString() == "DropDownList")
		    {
		        cDropDownList = new DropDownList(); cDropDownList.ID = "x" + drv["ColumnName"].ToString(); cDropDownList.CssClass = "inp-ddl"; cDropDownList.SelectedIndexChanged += new EventHandler(cCriButton_Click);
		        cDropDownList.DataValueField = drv["DdlKeyColumnName"].ToString(); cDropDownList.DataTextField = drv["DdlRefColumnName"].ToString(); cDropDownList.AutoPostBack = true;
		        cCriteria.Controls.Add(cDropDownList);
		    }
		    else if (drv["DisplayName"].ToString() == "ListBox")
		    {
		        cListBox = new ListBox(); cListBox.ID = "x" + drv["ColumnName"].ToString(); cListBox.SelectionMode = ListSelectionMode.Multiple; cListBox.CssClass = "inp-pic"; cListBox.SelectedIndexChanged += new EventHandler(cCriButton_Click);
		        cListBox.DataValueField = drv["DdlKeyColumnName"].ToString(); cListBox.DataTextField = drv["DdlRefColumnName"].ToString(); cListBox.AutoPostBack = true;
		        if (drv["RowSize"].ToString() != string.Empty) {cListBox.Rows = int.Parse(drv["RowSize"].ToString()); cListBox.Height = int.Parse(drv["RowSize"].ToString()) * 16 + 7; }
		        cCriteria.Controls.Add(cListBox);
		        if (drv["DisplayMode"].ToString() == "AutoListBox")
		        {
			        WebControl wcFtr = (WebControl)cCriteria.FindControl("x" + drv["DdlFtrColumnName"].ToString());
		            System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
		            context["method"] = "GetScreenIn";
			        context["addnew"] = "Y";
		            context["sp"] = "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString();
		            context["requiredValid"] = drv["RequiredValid"].ToString();
		            context["mKey"] = drv["DdlKeyColumnName"].ToString();
		            context["mVal"] = drv["DdlRefColumnName"].ToString();
		            context["mTip"] = drv["DdlRefColumnName"].ToString();
		            context["mImg"] = drv["DdlRefColumnName"].ToString();
		            context["ssd"] = Request.QueryString["ssd"];
		            context["scr"] = "91";
		            context["csy"] = "3";
		            context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
		            context["isSys"] = "N";
		            context["conn"] = drv["MultiDesignDb"].ToString() == "N" ? string.Empty : KEY_sysConnectionString;
		            context["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            context["refCol"] = wcFtr != null ? drv["DdlFtrColumnName"].ToString() : null;
		            context["refColDataType"] = wcFtr != null ? drv["DdlFtrDataType"].ToString() : null;
		            Session["AdmRptElm91" + cListBox.ID] = context;
		            cListBox.Attributes["ac_context"] = "AdmRptElm91" + cListBox.ID;
		            cListBox.Attributes["ac_url"] = "AutoComplete.aspx/DdlSuggestsEx";
		            cListBox.Attributes["refColCID"] = wcFtr != null ? wcFtr.ClientID : null;
		            cListBox.Attributes["searchable"] = "";
		            TextBox tb = new TextBox(); tb.ID = "x" + drv["ColumnName"].ToString() + "Hidden";tb.Attributes["style"] = "display:none;";
		            cCriteria.Controls.Add(tb);
		        }
		    }
		    else if (drv["DisplayName"].ToString() == "RadioButtonList")
		    {
		        cRadioButtonList = new RadioButtonList(); cRadioButtonList.ID = "x" + drv["ColumnName"].ToString(); cRadioButtonList.CssClass = "inp-rad"; cRadioButtonList.SelectedIndexChanged += new EventHandler(cCriButton_Click);
		        cRadioButtonList.DataValueField = drv["DdlKeyColumnName"].ToString(); cRadioButtonList.DataTextField = drv["DdlRefColumnName"].ToString(); cRadioButtonList.AutoPostBack = true; cRadioButtonList.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
		        cRadioButtonList.Attributes["OnClick"] = "this.focus();";
		        cLiteral = new Literal(); cLiteral.Text = "<div>"; cCriteria.Controls.Add(cLiteral);
		        cCriteria.Controls.Add(cRadioButtonList);
		        cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
		    }
		    else if (drv["DisplayName"].ToString() == "Calendar")
		    {
		        cCalendar = new System.Web.UI.WebControls.Calendar(); cCalendar.ID = "x" + drv["ColumnName"].ToString(); cCalendar.CssClass = "inp-txt calendar"; cCalendar.SelectionChanged += new EventHandler(cCriButton_Click); cCriteria.Controls.Add(cCalendar);
		    }
		    else if (drv["DisplayName"].ToString() == "CheckBox")
		    {
		        cCheckBox = new CheckBox(); cCheckBox.ID = "x" + drv["ColumnName"].ToString(); cCheckBox.CssClass = "inp-chk"; cCheckBox.AutoPostBack = true; cCheckBox.CheckedChanged += new EventHandler(cCriButton_Click); cCriteria.Controls.Add(cCheckBox);
		        cCheckBox.Attributes["OnClick"] = "this.focus();";
		    }
		    else
		    {
		        cTextBox = new TextBox(); cTextBox.ID = "x" + drv["ColumnName"].ToString(); cTextBox.CssClass = "inp-txt"; cTextBox.AutoPostBack = true; cTextBox.TextChanged += new EventHandler(cCriButton_Click);
		        if (drv["DisplayMode"].ToString() == "MultiLine") { cTextBox.TextMode = TextBoxMode.MultiLine; }
		        else if (drv["DisplayMode"].ToString() == "Password") { cTextBox.TextMode = TextBoxMode.Password; }
				if (drv["ColumnJustify"].ToString() == "L") { cTextBox.Style.Value = "text-align:left;"; }
				else if (drv["ColumnJustify"].ToString() == "R") { cTextBox.Style.Value = "text-align:right;"; }
				else { cTextBox.Style.Value = "text-align:center;"; }
		        cCriteria.Controls.Add(cTextBox);
		    }
		    cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
		}

		private void SetCriHolder(int ii, DataView dvCri)
		{
			Literal cLiteral;
			if (dvCri.Count > 0)
			{
			    foreach (DataRowView drv in dvCri)
			    {
			        cLiteral = new Literal(); cLiteral.Text = "<div class=\"r-criteria\">"; cCriteria.Controls.Add(cLiteral);
			        MakeScrGrpLab(drv);
			        MakeScrGrpInp(drv);
			        cLiteral = new Literal(); cLiteral.Text = "</div>"; cCriteria.Controls.Add(cLiteral);
			    }
			}
		}

		private DataTable MakeColumns(DataTable dt)
		{
		    DataColumnCollection columns = dt.Columns;
		    DataView dvCri = GetScrCriteria();
		    foreach (DataRowView drv in dvCri)
		    {
		        if (drv["DataTypeSysName"].ToString() == "DateTime") { columns.Add(drv["ColumnName"].ToString(), typeof(DateTime)); }
		        else if (drv["DataTypeSysName"].ToString() == "Byte") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte)); }
		        else if (drv["DataTypeSysName"].ToString() == "Int16") { columns.Add(drv["ColumnName"].ToString(), typeof(Int16)); }
		        else if (drv["DataTypeSysName"].ToString() == "Int32") { columns.Add(drv["ColumnName"].ToString(), typeof(Int32)); }
		        else if (drv["DataTypeSysName"].ToString() == "Int64") { columns.Add(drv["ColumnName"].ToString(), typeof(Int64)); }
		        else if (drv["DataTypeSysName"].ToString() == "Single") { columns.Add(drv["ColumnName"].ToString(), typeof(Single)); }
		        else if (drv["DataTypeSysName"].ToString() == "Double") { columns.Add(drv["ColumnName"].ToString(), typeof(Double)); }
		        else if (drv["DataTypeSysName"].ToString() == "Byte[]") { columns.Add(drv["ColumnName"].ToString(), typeof(Byte[])); }
		        else if (drv["DataTypeSysName"].ToString() == "Object") { columns.Add(drv["ColumnName"].ToString(), typeof(Object)); }
		        else { columns.Add(drv["ColumnName"].ToString(), typeof(String)); }
		    }
		    return dt;
		}

		private DataSet UpdCriteria(bool bUpdate)
		{
			System.Web.UI.WebControls.Calendar cCalendar;
			ListBox cListBox;
			RoboCoder.WebControls.ComboBox cComboBox;
			DropDownList cDropDownList;
			RadioButtonList cRadioButtonList;
			CheckBox cCheckBox;
			TextBox cTextBox;
			DataSet ds = new DataSet();
			ds.Tables.Add(MakeColumns(new DataTable("DtScreenIn")));
			DataRow dr = ds.Tables["DtScreenIn"].NewRow();
			DataView dvCri = GetScrCriteria();
			foreach (DataRowView drv in dvCri)
			{
			    if (drv["DisplayName"].ToString() == "ListBox")
			    {
					cListBox = (ListBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					bool isRequired = drv["RequiredValid"].ToString() == "Y";
					if (cListBox != null)
					{
						int CriCnt = (new AdminSystem()).CountScrCri(drv["ScreenCriId"].ToString(), drv["MultiDesignDb"].ToString(), drv["MultiDesignDb"].ToString() == "N" ? LcSysConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId));
						int TotalChoiceCnt = new DataView((new AdminSystem()).GetScreenIn("91", "GetDdl" + drv["ColumnName"].ToString() + LCurr.SystemId.ToString() + "C" + drv["ScreenCriId"].ToString(), CriCnt, drv["RequiredValid"].ToString(), 0, string.Empty, true, string.Empty, base.LImpr, base.LCurr, drv["MultiDesignDb"].ToString() == "N" ? LcAppConnString : (string)Session[KEY_sysConnectionString], base.AppPwd(LCurr.DbId))).Count;
						string selectedValues = string.Join(",", cListBox.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => "'" + x.Value + "'").ToArray());
						bool noneSelected = string.IsNullOrEmpty(selectedValues) || selectedValues == "''";
					    dr[drv["ColumnName"].ToString()] = "(";
					    if (noneSelected && CriCnt+1 > TotalChoiceCnt) dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + "'-1'";
					    foreach (ListItem li in cListBox.Items)
					    {
					        if (li.Selected || (noneSelected && !isRequired && !string.IsNullOrEmpty(li.Value)))
					        {
					            if (dr[drv["ColumnName"].ToString()].ToString() != "(")
					            {
					                dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + ",";
					            }
					            dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + "'" + li.Value + "'";
					        }
					    }
					}
					if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cListBox.SelectedValue))
					{
						bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");
					}
					if (dr[drv["ColumnName"].ToString()].ToString() == "(''" || dr[drv["ColumnName"].ToString()].ToString() == "(") { dr[drv["ColumnName"].ToString()] = string.Empty; } else { dr[drv["ColumnName"].ToString()] = dr[drv["ColumnName"].ToString()].ToString() + ")"; }
			    }
			    else if (drv["DisplayName"].ToString() == "Calendar")
			    {
					cCalendar = (System.Web.UI.WebControls.Calendar)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cCalendar != null && cCalendar.SelectedDate > DateTime.Parse("0001-01-01")) { dr[drv["ColumnName"].ToString()] = drv["DisplayMode"].ToString() == "CalendarUTC" ? base.SetDateTimeUTC(cCalendar.SelectedDate.ToString("yyyy/MM/dd"), !bUpdate) : cCalendar.SelectedDate.ToString("yyyy/MM/dd"); }
			    }
			    else if (drv["DisplayName"].ToString() == "ComboBox")
			    {
					cComboBox = (RoboCoder.WebControls.ComboBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cComboBox.SelectedValue))
					{
						bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");
					}
					if (cComboBox != null && cComboBox.SelectedIndex >= 0 && cComboBox.SelectedValue != string.Empty) { dr[drv["ColumnName"].ToString()] = cComboBox.SelectedValue; }
			    }
			    else if (drv["DisplayName"].ToString() == "DropDownList")
			    {
					cDropDownList = (DropDownList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cDropDownList.SelectedValue))
					{
						bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");
					}
					if (cDropDownList != null && cDropDownList.SelectedIndex >= 0 && cDropDownList.SelectedValue != string.Empty) { dr[drv["ColumnName"].ToString()] = cDropDownList.SelectedValue; }
			    }
			    else if (drv["DisplayName"].ToString() == "RadioButtonList")
			    {
					cRadioButtonList = (RadioButtonList)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (IsPostBack && drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cRadioButtonList.SelectedValue))
					{
						bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");
					}
					if (cRadioButtonList != null && cRadioButtonList.SelectedIndex >= 0 && cRadioButtonList.SelectedValue != string.Empty) { dr[drv["ColumnName"].ToString()] = cRadioButtonList.SelectedValue; }
			    }
			    else if (drv["DisplayName"].ToString() == "CheckBox")
			    {
					cCheckBox = (CheckBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (cCheckBox != null) { dr[drv["ColumnName"].ToString()] = base.SetBool(cCheckBox.Checked); }
			    }
			    else
			    {
					cTextBox = (TextBox)cCriteria.FindControl("x" + drv["ColumnName"].ToString());
					if (drv["RequiredValid"].ToString() == "Y" && string.IsNullOrEmpty(cTextBox.Text))
					{
						if (IsPostBack) { bErrNow.Value = "Y"; PreMsgPopup("Criteria column: " + drv["ColumnName"].ToString() + " cannot be empty. Please rectify and try again.");}
						cTextBox.Text = drv["DisplayMode"].ToString().Contains("Date") ? DateTime.Today.Date.ToShortDateString() : "?";
					}
					if (cTextBox != null && cTextBox.Text != string.Empty) { dr[drv["ColumnName"].ToString()] = (",DateUTC,DateTimeUTC,ShortDateTimeUTC,LongDateTimeUTC,".IndexOf("," + drv["DisplayMode"].ToString() + ",") >= 0) ? SetDateTimeUTC(cTextBox.Text,!bUpdate) : (drv["DisplayName"].ToString().Contains("Date") ? (DateTime.Parse(cTextBox.Text,System.Threading.Thread.CurrentThread.CurrentCulture).ToString()): cTextBox.Text); }
			    }
			}
			ds.Tables["DtScreenIn"].Rows.Add(dr);
			if (bUpdate) 
			{
				try
				{
					(new AdminSystem()).UpdScrCriteria("91", "AdmRptElm91", dvCri, base.LUser.UsrId, cCriteria.Visible, ds, LcAppConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}
			Session[KEY_dsScrCriVal] = ds;
			return ds;
		}

		protected void cbReportId160(object sender, System.EventArgs e)
		{
			SetReportId160((RoboCoder.WebControls.ComboBox)sender,string.Empty,cAdmRptElmGrid.EditItem);
		}

		private void SetReportId160(RoboCoder.WebControls.ComboBox ddl, string keyId, ListViewItem li)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlReportId3S1599";
			context["addnew"] = "Y";
			context["mKey"] = "ReportId160";
			context["mVal"] = "ReportId160Text";
			context["mTip"] = "ReportId160Text";
			context["mImg"] = "ReportId160Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "91";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(91,"GetDdlReportId3S1599",true,false,0,keyId,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					ddl.DataSource = dv; Session[KEY_dtReportId160] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private void SetRptElmTypeCd160(DropDownList ddl, string keyId)
		{
			DataTable dt = (DataTable)Session[KEY_dtRptElmTypeCd160];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (ddl != null)
			{
				string ss = string.Empty;
				ListItem li = null;
				bool bFirst = false;
				bool bAll = false; if (ddl.Enabled && ddl.Visible) {bAll = true;}
				if (dv == null)
				{
					bFirst = true;
					try
					{
						dv = new DataView((new AdminSystem()).GetDdl(91,"GetDdlRptElmTypeCd3S1600",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),string.Empty,base.LImpr,base.LCurr));
					}
					catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				}
				if (dv != null)
				{
					if (!string.IsNullOrEmpty(keyId) && !ddl.Enabled) { ss = ss + (string.IsNullOrEmpty(ss) ? string.Empty : " AND ") + "RptElmTypeCd160 is not NULL"; }
					dv.RowFilter = ss;
					ddl.DataSource = dv; ddl.DataBind();
					li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					else if (!bFirst)
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(91,"GetDdlRptElmTypeCd3S1600",true,bAll,0,keyId,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					if (dv.Count <= 1 && keyId != string.Empty)	// In case invalid value casued by copy action.
					{
						try
						{
							dv = new DataView((new AdminSystem()).GetDdl(91,"GetDdlRptElmTypeCd3S1600",true,true,0,keyId,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),string.Empty,base.LImpr,base.LCurr));
						}
						catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
						dv.RowFilter = ss; ddl.DataSource = dv; ddl.DataBind();
						li = ddl.Items.FindByValue(keyId); if (li != null) {li.Selected = true;}
					}
					Session[KEY_dtRptElmTypeCd160] = dv.Table;
				}
			}
		}

		protected void cbRptStyleId160(object sender, System.EventArgs e)
		{
			SetRptStyleId160((RoboCoder.WebControls.ComboBox)sender,string.Empty,cAdmRptElmGrid.EditItem);
		}

		private void SetRptStyleId160(RoboCoder.WebControls.ComboBox ddl, string keyId, ListViewItem li)
		{
			System.Collections.Generic.Dictionary<string, string> context = new System.Collections.Generic.Dictionary<string, string>();
			context["method"] = "GetDdlRptStyleId3S1601";
			context["addnew"] = "Y";
			context["mKey"] = "RptStyleId160";
			context["mVal"] = "RptStyleId160Text";
			context["mTip"] = "RptStyleId160Text";
			context["mImg"] = "RptStyleId160Text";
			context["ssd"] = Request.QueryString["ssd"];
			context["scr"] = "91";
			context["csy"] = "3";
			context["filter"] = Utils.IsInt(cFilterId.SelectedValue)? cFilterId.SelectedValue : "0";
			context["isSys"] = "N";
			context["conn"] = KEY_sysConnectionString;
			ddl.AutoCompleteUrl = "AutoComplete.aspx/DdlSuggests";
			ddl.DataContext = context;
			if (ddl != null)
			{
			    DataView dv = null;
				if (keyId == string.Empty && ddl.SearchText.StartsWith("**")) {keyId = ddl.SearchText.Substring(2);}
				try
				{
					dv = new DataView((new AdminSystem()).GetDdl(91,"GetDdlRptStyleId3S1601",true,false,0,keyId,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),string.Empty,base.LImpr,base.LCurr));
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (dv != null)
				{
					ddl.DataSource = dv; Session[KEY_dtRptStyleId160] = dv.Table;
				    try { ddl.SelectByValue(keyId,string.Empty,true); } catch { try { ddl.SelectedIndex = 0; } catch { } }
				}
			}
		}

		private void PopAdmRptElm91List(object sender, System.EventArgs e, bool bPageLoad, object ID)
		{
			DataTable dt = null;
			CheckAuthentication(false);
			int filterId = 0; if (Utils.IsInt(cFilterId.SelectedValue)) { filterId = int.Parse(cFilterId.SelectedValue); }
			string key = string.Empty;
			if (!string.IsNullOrEmpty(Request.QueryString["key"]) && bUseCri.Value == string.Empty)
		    {
		        key = Request.QueryString["key"].ToString();
				cScreenSearch.Visible = false; cSystem.Visible = false; cEditButton.Visible = true; cSaveCloseButton.Visible = cSaveButton.Visible;
		    }
			try
			{
				try {dt = (new AdminSystem()).GetLis(91,"GetLisAdmRptElm91",false,"Y",0,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
				catch {dt = (new AdminSystem()).GetLis(91,"GetLisAdmRptElm91",false,"N",0,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),filterId,key,string.Empty,GetScrCriteria(),base.LImpr,base.LCurr,UpdCriteria(false));}
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			if (dt != null)
			{
				CheckAuthentication(false);
				SetFunctionality();
			    if (!dt.Columns.Contains("_NewRow")) { dt.Columns.Add("_NewRow"); }
				Session[KEY_dtAdmRptElmGrid] = dt;
				cAdmRptElmGridDataPager.PageSize = Int16.Parse(cPgSize.Text); GotoPage(0);
			    if (Session[KEY_lastSortCol] != null)
			    {
					cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs((string)Session[KEY_lastSortExp], SortDirection.Ascending));
			    }
				else
			    {
					cAdmRptElmGrid_DataBind(dt.DefaultView);
			    }
				cFindButton_Click(sender, e);
			}
			ShowDirty(false); PanelTop.Update();
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false; Session.Remove(KEY_scrImport);
			// *** List Selection (End of) Web Rule starts here *** //
		}

		private void SetColumnAuthority()
		{
			DataTable dtAuth = GetAuthCol();
			DataTable dtLabel = GetLabel();
		}

		private void SetFunctionality()
		{
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
				if (dr["AllowUpd"].ToString() == "N" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					cShowImpButton.Visible = false;
				}
				if (dr["AllowAdd"].ToString() == "N" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N")) { cInsRowButton.Visible = false; }
				if ((dr["AllowDel"].ToString() == "N" && dr["AllowUpd"].ToString() == "N" && dr["AllowAdd"].ToString() == "N") || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					cUndoAllButton.Visible = false; cSaveButton.Visible = false;
				}
				else
				{
					if ((bool)Session[KEY_bUndoAllVisible]) {cUndoAllButton.Visible = true; cUndoAllButton.Enabled = true;}
					if ((bool)Session[KEY_bUpdateVisible]) {cSaveButton.Visible = true; cSaveButton.Enabled = true;}
				}
			}
		}

		protected void cFilterId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if (cAdmRptElmGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				PopAdmRptElm91List(sender, e, false, null);
			}
		}

		protected void cSystemId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			base.LCurr.DbId = byte.Parse(cSystemId.SelectedValue);
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if (cAdmRptElmGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				Session[KEY_sysConnectionString] = SysConnectStr(byte.Parse(cSystemId.SelectedValue));
				Session[KEY_sysConnectionString + "Pwd"] = base.AppPwd(base.LCurr.DbId);
				Session.Remove(KEY_dtReportId160);
				Session.Remove(KEY_dtRptElmTypeCd160);
				Session.Remove(KEY_dtRptStyleId160);
				GetCriteria(GetScrCriteria());
				PopAdmRptElm91List(sender, e, false, null);
				cRptStyleId160Search_Script();
			}
		}

		protected void cRptStyleId160_TextChanged(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cRptStyleId160_DDFindClick(object sender, System.EventArgs e)
		{
			ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)sender).FocusID);
		}

		protected void cRptStyleId160_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// *** On Change/ On Click Web Rule starts here *** //
			EnableValidators(true);  // Do not remove; Need to reenable after postback, especially in the grid.
			TextBox pwd = null;
			if (cAdmRptElmGrid.EditIndex > -1 && cAdmRptElmGrid.Items.Count > cAdmRptElmGrid.EditIndex)
			{
				DataView dvAdmRptElmGrid = ((DataTable)Session[KEY_dtAdmRptElmGrid]).DefaultView;
				if (Session[KEY_dtRptStyleId160] != null)
				{
					DataView dv3 = ((DataTable)Session[KEY_dtRptStyleId160]).DefaultView;
				}
				Session[KEY_dtAdmRptElmGrid] = dvAdmRptElmGrid.Table;
				cRptStyleId160Search_Script();
			}
			if (pwd != null) {ScriptManager.GetCurrent(Parent.Page).SetFocus(pwd.ClientID);} else {ScriptManager.GetCurrent(Parent.Page).SetFocus(SenderFocusId(sender));}
		}

		public void cFindButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				if (cAdmRptElmGrid.EditIndex < 0 || UpdateGridRow(cAdmRptElmGrid, new CommandEventArgs("Save", "")))
				{
					string rf = string.Empty;
					if (cFind.Text != string.Empty) { rf = "(" + base.GetExpression(cFind.Text.Trim(), GetAuthCol(), 0, cFindFilter.SelectedValue) + ")"; }
					if (rf != string.Empty) { rf = "((" + rf + " or _NewRow = 'Y' ))"; }
					dv.RowFilter = rf;
					ViewState["_RowFilter"] = rf;
					GotoPage(0); cAdmRptElmGrid_DataBind(dv);
					if (GetCurrPageIndex() != (int)Session[KEY_currPageIndex] && (int)Session[KEY_currPageIndex] < GetTotalPages()) { GotoPage((int)Session[KEY_currPageIndex]); cAdmRptElmGrid_DataBind(dv);}
					try {ScriptManager.GetCurrent(Parent.Page).SetFocus(((TextBox)sender).ClientID);} catch {}
				}
			}
			grdCount.InnerText = "(" + RO.Common3.Utils.fmNumeric("0",dv.Count.ToString(),base.LUser.Culture) + " found)";
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cFirstButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			GotoPage(0);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cLastButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			GotoPage(GetTotalPages() - 1);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cNextButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			GotoPage(GetCurrPageIndex() + 1);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cPrevButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			GotoPage(GetCurrPageIndex() - 1);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cInsRowButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if (dt != null && (ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")))
			{
				DataRow dr = dt.NewRow();
				int? sortorder = null;
				if (dt.Columns.Contains("_SortOrder"))
				{
				    try
				    {
				        sortorder = 1;
				    } catch {};
				}
				dt.Rows.InsertAt(dr, 0);
				dt.Rows[0]["_NewRow"] = "Y";
				if (sortorder.HasValue) dr["_SortOrder"] = sortorder;
				Session[KEY_lastAddedRow] = 0; Session[KEY_dtAdmRptElmGrid] = dt; Session[KEY_currPageIndex] = 0; GotoPage(0);
				cAdmRptElmGrid_DataBind(dt.DefaultView);
				cAdmRptElmGrid_OnItemEditing(cAdmRptElmGrid, new ListViewEditEventArgs(0));
				// *** Default Value (Grid) Web Rule starts here *** //
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cPgSizeButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if (cAdmRptElmGrid.EditIndex < 0 || (dt != null && UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				try {if (int.Parse(cPgSize.Text) < 1 || int.Parse(cPgSize.Text) > 200) {cPgSize.Text = "10";}} catch {cPgSize.Text = "10";}
				cAdmRptElmGridDataPager.PageSize = int.Parse(cPgSize.Text); GotoPage(0);
				if (dt.Rows.Count <= 0 || (GetAuthRow().Rows[0]["AllowAdd"].ToString() == "Y" && dt.Rows.Count == 1)) {cAdmRptElmGrid_DataBind(dt.DefaultView);} else {cFindButton_Click(sender, e);}
				try
				{
					(new AdminSystem()).UpdLastPageInfo(91, base.LUser.UsrId, cPgSize.Text, LcSysConnString, LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); }
			}
		}

		public void cGoto_TextChanged(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			try {GotoPage(Convert.ToInt32(cGoto.Text) - 1);} catch {} finally {cGoto.Text = (GetCurrPageIndex() + 1).ToString();}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cHideImpButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			cImport.Visible = false; cHideImpButton.Visible = false; cShowImpButton.Visible = (bool)Session[KEY_bShImpVisible];
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false;
			if ((cShowImpButton.Attributes["OnClick"] == null || cShowImpButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cShowImpButton.Visible) {cShowImpButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		public void cShowImpButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			cImport.Visible = true; cShowImpButton.Visible = false; cHideImpButton.Visible = (bool)Session[KEY_bHiImpVisible];
			cNaviPanel.Visible = false; cImportPwdPanel.Visible = false;
			if ((cImportButton.Attributes["OnClick"] == null || cImportButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cImportButton.Visible) {cImportButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if ((cHideImpButton.Attributes["OnClick"] == null || cHideImpButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && cHideImpButton.Visible) {cHideImpButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cSchemaImage.Visible)
			{
				try
				{
					Session["ImportSchema"] = (new AdminSystem()).GetSchemaScrImp(91,base.LUser.CultureId,LcSysConnString,LcAppPw);
				}
				catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
				if (cSchemaImage.Attributes["OnClick"] == null || cSchemaImage.Attributes["OnClick"].IndexOf("ImportSchema.aspx") < 0) {cSchemaImage.Attributes["OnClick"] += "SearchLink('ImportSchema.aspx?scm=S&key=91&csy=3','','',''); return false;";}
			}
			// *** System Button Click (after) Web Rule starts here *** //
		}

		private void ScrImportDdl(DataView dvd, DataRowView drv, bool bComboBox, string PKey, string CKey, string CNam, int MaxLen, string MatchCd, bool bAllowNulls)
		{
			if (dvd != null)
			{
				bool bFound = false; bool bUnique = true;
				if (!string.IsNullOrEmpty(drv[CKey].ToString().Trim()))   // Always do this first.
				{
					foreach (DataRowView drvd in dvd)
					{
						if (drvd[0].ToString().Trim() == drv[CKey].ToString().Trim()) {drv[CNam] = drvd[1].ToString(); bFound = true; break;}
					}
				}
				if (!string.IsNullOrEmpty(drv[CNam].ToString().Trim()))
				{
					if (drv[CNam].ToString().Length > MaxLen) { drv[CNam] = drv[CNam].ToString().Substring(0,MaxLen); }
					string cnam = drv[CNam].ToString(); object ckey = drv[CKey];	// Saved for reverse later.
					if (!bFound && "2,3,4".IndexOf(MatchCd) >= 0)  // Exact match.
					{
						foreach (DataRowView drvd in dvd)
						{
							if (drvd[1].ToString().Trim().ToLower() == cnam.Trim().ToLower())
							{
								drv[CKey] = drvd[0].ToString();
								if (bComboBox) {drv[CNam] = drvd[1].ToString();}
								if (!bFound) {bFound = true;}
								else { bUnique = false; drv[CNam] = cnam; drv[CKey] = ckey; break; }
							}
							else if (bFound) break;
						}
					}
					if (!bFound && "3,4".IndexOf(MatchCd) >= 0)  // StartsWith.
					{
						foreach (DataRowView drvd in dvd)
						{
							if (drvd[1].ToString().Trim().ToLower().StartsWith(cnam.Trim().ToLower()))
							{
								drv[CKey] = drvd[0].ToString();
								if (bComboBox) {drv[CNam] = drvd[1].ToString();}
								if (!bFound) {bFound = true;}
								else { bUnique = false; drv[CNam] = cnam; drv[CKey] = ckey; break; }
							}
							else if (bFound) break;
						}
					}
					if (!bFound && "4".IndexOf(MatchCd) >= 0)  // Wild search.
					{
						foreach (DataRowView drvd in dvd)
						{
							if (drvd[1].ToString().Trim().ToLower().IndexOf(cnam.Trim().ToLower()) >= 0)
							{
								drv[CKey] = drvd[0].ToString();
								if (bComboBox) {drv[CNam] = drvd[1].ToString();}
								if (!bFound) {bFound = true;}
								else { bUnique = false; drv[CNam] = cnam; drv[CKey] = ckey; break; }
							}
							else if (bFound) break;
						}
					}
					if (!(bFound && bUnique) && MatchCd != "1") {drv[CNam] = "Invalid>" + drv[CNam].ToString(); bErrNow.Value = "Y"; PreMsgPopup("Import has invalid data, please check for \"Invalid>\", rectify and try again.");}
				}
				if (!bFound && !string.IsNullOrEmpty(drv[CKey].ToString()))
				{
					drv[CNam] = "Invalid>" + drv[CKey].ToString(); bErrNow.Value = "Y"; PreMsgPopup("Import has invalid data, please check for \"Invalid>\", rectify and try again.");
				    drv[CKey] = null;
				}
			}
		}

		protected void ScrImport(bool bClear)
		{
			int iRow;
			cNaviPanel.Visible = true; cImportPwdPanel.Visible = false;
			DataTable dti = (DataTable)Session[KEY_scrImport];
			DataTable dtAdmRptElmGrid = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if (dti != null && dtAdmRptElmGrid != null)
			{
				if (bClear)
				{
					dtAdmRptElmGrid.Rows.Clear();
				}
				cFind.Text = string.Empty;
				int iExist = dtAdmRptElmGrid.Rows.Count;
				// Validate dropdown, combobox, etc.
				int jj = 0;
				foreach (DataRowView drv in dti.DefaultView)
				{
					if ((drv.Row.RowState == System.Data.DataRowState.Added || drv.Row.RowState == System.Data.DataRowState.Detached) && jj >= iExist)
					{
						DataTable dtd;
						dtd = jj==iExist ? null : (DataTable)Session[KEY_dtReportId160];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(91,"GetDdlReportId3S1599",true,true,0,string.Empty,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtReportId160] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, true, "RptElmId", "ReportId160", "ReportId160Text", 50, "2", false);
						dtd = (DataTable)Session[KEY_dtRptElmTypeCd160];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(91,"GetDdlRptElmTypeCd3S1600",true,true,0,string.Empty,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtRptElmTypeCd160] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, false, "RptElmId", "RptElmTypeCd160", "RptElmTypeCd160Text", 20, "2", false);
						dtd = jj==iExist ? null : (DataTable)Session[KEY_dtRptStyleId160];
						if (dtd == null)
						{
							try
							{
								dtd = (new AdminSystem()).GetDdl(91,"GetDdlRptStyleId3S1601",true,true,0,string.Empty,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),string.Empty,base.LImpr,base.LCurr);
							}
							catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
							Session[KEY_dtRptStyleId160] = dtd;
						}
						ScrImportDdl(dtd.DefaultView, drv, true, "RptElmId", "RptStyleId160", "RptStyleId160Text", 300, "2", true);
					}
					jj = jj + 1;
				}
				iRow = dti.DefaultView.Count - iExist;
				if (!dti.Columns.Contains("_NewRow")) { dti.Columns.Add("_NewRow"); }
				Session[KEY_dtAdmRptElmGrid] = dti;
				cAdmRptElmGrid_DataBind(dti.DefaultView);
				string msg = iRow.ToString() + " rows from selected source imported successfully. " + Session[KEY_cntImpPwdOvride].ToString() + " rows validated by Password Override. Please press Save button to save to database.";
				if (Session["CtrlAcctList"] != null)
				{
					System.Collections.Hashtable ht = (System.Collections.Hashtable) Session["CtrlAcctList"];
					string ss = string.Empty;
					System.Collections.IDictionaryEnumerator de = ht.GetEnumerator();
					while ( de.MoveNext() )
					{
						ss = ss + de.Key + " (" + de.Value.ToString() + "), ";
					}
					msg = msg + " List of Password Override items: " + ss.Remove(ss.Length - 2, 2) + ".";
				}
				Session.Remove(KEY_scrImport); Session.Remove("CtrlAcctList");
				cImportPwd.Text = string.Empty;
				ShowDirty(true);
				Session[KEY_lastImpPwdOvride] = 0; Session[KEY_cntImpPwdOvride] = 0;
				bInfoNow.Value = "Y"; PreMsgPopup(msg);
			}
		}

		public void cContinueButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (before) Web Rule starts here *** //
			ScrImport(false);
			// *** System Button Click (after) Web Rule starts here *** //
		}

		protected void cBrowseButton_Click(object sender, EventArgs e)
		{
			if (cBrowse.HasFile && cBrowse.PostedFile.FileName != string.Empty)
			{
				string fileAndPath = cBrowse.PostedFile.FileName;
				string fNameO = string.Empty;
				try {
				    foreach (var c in Path.GetInvalidPathChars()) { fileAndPath = fileAndPath.Replace(c, '_'); }
				    fNameO = Path.GetFileName(fileAndPath);
				}
				catch (Exception err) {
				    bErrNow.Value = "Y"; PreMsgPopup("Invalid characters in file and/or path \"" + fileAndPath + "\": " + err.Message);
				    return;
				}
				try {
					string fName;
					if (fNameO.LastIndexOf(".") >= 0)
					{
						fName = fNameO.Insert(fNameO.LastIndexOf("."), "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
					}
					else
					{
						fName = fNameO + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
					}
					fName = fName.Replace(":","").Replace("..","");
					if (!Directory.Exists(Config.PathTmpImport)) { Directory.CreateDirectory(Config.PathTmpImport); }
					cBrowse.PostedFile.SaveAs(Config.PathTmpImport + fName);
					cWorkSheet.DataSource = (new XLSImport(Config.WsXlsUrl)).GetSheetNames(Config.PathTmpImport + fName); cWorkSheet.DataBind();
					cFNameO.Text = fNameO; cFName.Text = fName;
				}
				catch (Exception err) {bErrNow.Value = "Y"; PreMsgPopup("Unable to retrieve sheet names from \"" + fNameO + "\": " + err.Message); }
			}
		}

		public void cImportButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			if (cFNameO.Text != string.Empty && cWorkSheet.Items.Count > 0 && cWorkSheet.SelectedItem.Text != string.Empty && cStartRow.Text != string.Empty)
			{
				DataTable dtAdmRptElmGrid = (DataTable)Session[KEY_dtAdmRptElmGrid];
				try
				{
					if (cAdmRptElmGrid.EditIndex < 0 || UpdateGridRow(cAdmRptElmGrid, new CommandEventArgs("Save", "")))
					{
						Session[KEY_scrImport] = ImportFile(dtAdmRptElmGrid.Copy(),cFNameO.Text,cWorkSheet.SelectedItem.Text,cStartRow.Text,Config.PathTmpImport + cFName.Text);
					}
					cHideImpButton_Click(sender,new EventArgs());
				}
				catch (Exception err) {bErrNow.Value = "Y"; PreMsgPopup("Error in spreadsheet \"" + cFNameO.Text + "\":<br>" + err.Message); return; }
				finally {cWorkSheet.Items.Clear();}
				ScrImport(false);
			}
			else
			{
				bInfoNow.Value = "Y"; PreMsgPopup("Please select a spreadsheet, then a worksheet, indicate the starting row and try again.");
			}
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private DataTable ImportFile(DataTable dt, string fileName, string workSheet, string startRow, string fileFullName)
		{
			try
			{
				DataTable dtImp = RO.Common3.XmlUtils.XmlToDataSet((new XLSImport(Config.WsXlsUrl)).ImportFile(fileName,workSheet,startRow,fileFullName)).Tables[0];
				DataRowCollection rows = dtImp.Rows;
				DataColumnCollection cols = dt.Columns;
				Func<string, string, bool> isDateCol = (cl, c) => ("," + cl + ",").IndexOf("," + c + ",") >= 0;
				Func<string, string, bool> isDateUTCCol = (cl, c) => ("," + cl + ",").IndexOf("," + c + ",") >= 0;
				string ss;
				int iStart = int.Parse(startRow) - 1;
				int idv = dt.Rows.Count;
				int idel = 0;
				System.Collections.Generic.List<string> ErrLst = new System.Collections.Generic.List<string>();
				bool bHasErr = false;
				for ( int iRow = iStart; iRow < rows.Count; iRow++ )
				{
					if (rows[iRow][1].ToString() == string.Empty && rows[iRow][2].ToString() == string.Empty && rows[iRow][3].ToString() == string.Empty && rows[iRow][4].ToString() == string.Empty && rows[iRow][7].ToString() == string.Empty && rows[iRow][10].ToString() == string.Empty && rows[iRow][11].ToString() == string.Empty) {idel = idel + 1;}
					else
					{
						dt.Rows.Add(dt.NewRow());
						for ( int iCol = 0; iCol < cols.Count; iCol++ )
						{
							try { ss = rows[iRow][iCol].ToString().Trim(); } catch { ss = string.Empty; }
							try {
							    if (!string.IsNullOrEmpty(ss)) {
							        dt.Rows[iRow - iStart - idel + idv][iCol] = isDateCol("", iCol.ToString()) ? base.ToIntDateTime(ss, isDateUTCCol("", iCol.ToString()), true) : ss;
							    }
							}
							catch (Exception ex) {
							    if (ss.EndsWith("%")) {
							        try { dt.Rows[iRow - iStart - idel + idv][iCol] = ss.Left(ss.Length - 1); } catch { }
							    }
							    else {
							        bHasErr = true;
							        ErrLst.Add("Row " + (iRow + 1).ToString() + " Col " + Utils.Num2ExcelCol(iCol + 1) + ": " + Server.HtmlEncode(ex.Message));
							    }
							}
						}
					}
				}
				if (bHasErr) { throw new Exception(string.Join("<br>", ErrLst)); }
			}
			catch(Exception e) { throw (e); }
			return dt;
		}

		private void GotoPage(int pageNo)
		{
			if (pageNo >= 0 && pageNo < GetTotalPages())
			{
				if (cAdmRptElmGrid.EditIndex < 0 || UpdateGridRow(cAdmRptElmGrid, new CommandEventArgs("Save", "")))
				{
					try { cAdmRptElmGridDataPager.SetPageProperties(cAdmRptElmGridDataPager.PageSize * pageNo, cAdmRptElmGridDataPager.MaximumRows, false); cAdmRptElmGrid_DataBind(null); }
					catch
					{
						try { cAdmRptElmGridDataPager.SetPageProperties(0, cAdmRptElmGridDataPager.MaximumRows, false); cAdmRptElmGrid_DataBind(null); } catch {}
					}
				}
			}
		}

		protected void cAdmRptElmGrid_OnItemCommand(object sender, ListViewCommandEventArgs e)
		{
		}

		private void cAdmRptElmGrid_DataBind(DataView dvAdmRptElmGrid)
		{
			if (dvAdmRptElmGrid == null)
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
				dvAdmRptElmGrid = dt != null ? dt.DefaultView : null;
			}
			if (dvAdmRptElmGrid != null)
			{
				LcAuth = GetAuthCol().AsEnumerable().ToDictionary<DataRow,string>(dr=>dr["ColName"].ToString());
				cAdmRptElmGrid.DataSource = dvAdmRptElmGrid;
				cAdmRptElmGrid.DataBind();
				int totalPages = GetTotalPages(); if (totalPages <= 0) { totalPages = 1; }
				cPageNoLabel.Text = " of " + totalPages.ToString(); cGoto.Text = (GetCurrPageIndex() + 1).ToString();
			}
			if (cFindFilter.Items.Count <= 0)
			{
				DataTable dtAuth = GetAuthCol();
				DataTable dtLabel = GetLabel();
				int ii = 0;
				ListItem li = new ListItem();
				li.Value = string.Empty; li.Text = "All";
				cFindFilter.Items.Add(li);
				foreach (DataRow dr in dtLabel.Rows)
				{
					if (ii >= 0 && !string.IsNullOrEmpty(dr["ColumnHeader"].ToString()) && !string.IsNullOrEmpty(dr["TableId"].ToString()) && dtAuth.Rows[ii]["ColVisible"].ToString() == "Y")
					{
						li = new ListItem();
						li.Value = ii.ToString(); li.Text = dr["ColumnHeader"].ToString().Replace("*", string.Empty);
						cFindFilter.Items.Add(li);
					}
					ii = ii + 1;
				}
			}
		}

		protected void cAdmRptElmGrid_OnSorting(object sender, ListViewSortEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null)
			{
				if (cAdmRptElmGrid.EditIndex < 0 || UpdateGridRow(cAdmRptElmGrid, new CommandEventArgs("Save", "")))
				{
					string ftr = string.Empty;
					if (!dv.Table.Columns.Contains("_SortOrder")) { dv.Table.Columns.Add("_SortOrder"); }
					if (dv.RowFilter != string.Empty) { ftr = dv.RowFilter; dv.RowFilter = string.Empty; }
					if (Session[KEY_lastSortUrl] == null)   // First time.
					{
					    dv.Sort = "_SortOrder DESC, " + e.SortExpression; Session[KEY_lastSortUrl] = "~/images/ArrowUp.png"; Session[KEY_lastSortTog] = "N";
					}
					else if ((string)Session[KEY_lastSortTog] == "Y")
					{
					    if (((string)Session[KEY_lastSortUrl]).IndexOf("ArrowDn") >= 0)
					    {
							dv.Sort = "_SortOrder DESC, " + e.SortExpression; Session[KEY_lastSortUrl] = "~/images/ArrowUp.png";
					    }
					    else
					    {
							dv.Sort = "_SortOrder DESC, " + e.SortExpression + " DESC"; Session[KEY_lastSortUrl] = "~/images/ArrowDn.png";
					    }
					    Session[KEY_lastSortTog] = "N";
					}
					else if (((string)Session[KEY_lastSortUrl]).IndexOf("ArrowDn") >= 0) { dv.Sort = "_SortOrder DESC, " + e.SortExpression + " DESC"; } else { dv.Sort = "_SortOrder DESC, " + e.SortExpression; }
					ViewState["_SortColumn"] = dv.Sort;
					if (ftr != string.Empty) { dv.RowFilter = ftr; }
					Session[KEY_dtAdmRptElmGrid] = dt;
					cAdmRptElmGrid_DataBind(dv);
				}
			}
		}

		protected void cAdmRptElmGrid_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
		{
		    cAdmRptElmGridDataPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
		    DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
		    DataView dv = dt != null ? dt.DefaultView : null;
			cAdmRptElmGrid_DataBind(dv); cAdmRptElmGrid.EditIndex = -1;
		}

		private void GridChkPgDirty(ListViewItem lvi)
		{
				WebControl cc = null;
				cc = ((WebControl)lvi.FindControl("cReportId160"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cRptElmTypeCd160"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cRptStyleId160"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cRptStyleId160Search"));
				if (cc.Attributes["OnClick"] == null || cc.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cc.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
				cc = ((WebControl)lvi.FindControl("cElmHeight160"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cElmColumns160"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cElmColSpacing160"));
				if ((cc.Attributes["OnChange"] == null || cc.Attributes["OnChange"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnChange"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty();";}
				cc = ((WebControl)lvi.FindControl("cElmPrintFirst160"));
				if ((cc.Attributes["OnClick"] == null || cc.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
				cc = ((WebControl)lvi.FindControl("cElmPrintLast160"));
				if ((cc.Attributes["OnClick"] == null || cc.Attributes["OnClick"].IndexOf("ChkPgDirty") < 0) && cc.Visible && cc.Enabled) {cc.Attributes["OnClick"] += "document.getElementById('" + bPgDirty.ClientID + "').value='Y'; ChkPgDirty(); this.focus();";}
		}

		protected void cAdmRptElmGrid_OnItemDataBound(object sender, ListViewItemEventArgs e)
		{
			// *** GridItemDataBound (before) Web Rule End *** //
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			bool isEditItem = false;
			DataView dvAdmRptElmGrid = dt != null ? dt.DefaultView : null;
			if (cAdmRptElmGrid.EditIndex > -1 && GetDataItemIndex(cAdmRptElmGrid.EditIndex) == e.Item.DataItemIndex)
			{
				isEditItem = true;
				base.SetGridEnabled(e.Item, GetAuthCol(), GetLabel(), 0);
				SetReportId160((RoboCoder.WebControls.ComboBox)e.Item.FindControl("cReportId160"),dvAdmRptElmGrid[e.Item.DataItemIndex]["ReportId160"].ToString(), e.Item);
				SetRptElmTypeCd160((DropDownList)e.Item.FindControl("cRptElmTypeCd160"),dvAdmRptElmGrid[e.Item.DataItemIndex]["RptElmTypeCd160"].ToString());
				SetRptStyleId160((RoboCoder.WebControls.ComboBox)e.Item.FindControl("cRptStyleId160"),dvAdmRptElmGrid[e.Item.DataItemIndex]["RptStyleId160"].ToString(), e.Item);
				CheckBox cb = null;
				cb = (CheckBox)e.Item.FindControl("cElmPrintFirst160");
				if (cb != null)
				{
					cb.Checked = base.GetBool(dvAdmRptElmGrid[e.Item.DataItemIndex]["ElmPrintFirst160"].ToString());
				}
				cb = (CheckBox)e.Item.FindControl("cElmPrintLast160");
				if (cb != null)
				{
					cb.Checked = base.GetBool(dvAdmRptElmGrid[e.Item.DataItemIndex]["ElmPrintLast160"].ToString());
				}
				GridChkPgDirty(e.Item);
			}
			SetClientRule((ListViewDataItem) e.Item,isEditItem);
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
			    Control cc = null;
				if (dr["AllowDel"].ToString() == "N" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					cc = e.Item.FindControl("cAdmRptElmGridDelete"); if (cc != null) { cc.Visible = false; }
				}
				if (!AllowRowEdit(GetAuthRow(),dvAdmRptElmGrid[e.Item.DataItemIndex]["RptElmId160"].ToString()) || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
					cc = e.Item.FindControl("cAdmRptElmGridEdit"); if (cc != null) { cc.Visible = false; }
				}
				else
				{
			        HtmlTableRow tr = e.Item.FindControl("cAdmRptElmGridRow") as HtmlTableRow;
			        LinkButton lb = e.Item.FindControl("cAdmRptElmGridEdit") as LinkButton;
			        if (tr != null && lb != null) { SetDefaultCtrl(tr, lb, string.Empty); }
				}
			}
			if (cAdmRptElmGrid.EditIndex > -1 && GetDataItemIndex(cAdmRptElmGrid.EditIndex) == e.Item.DataItemIndex)
			{
			}
			else
			{
			}
			// *** GridItemDataBound (after) Web Rule End *** //
		}

		protected void cRptElmId160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "0") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "0"; Session[KEY_lastSortExp] = "RptElmId160";Session[KEY_lastSortImg] = "cRptElmId160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("RptElmId160", SortDirection.Ascending));
		}

		protected void cReportId160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "1") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "1"; Session[KEY_lastSortExp] = "ReportId160Text";Session[KEY_lastSortImg] = "cReportId160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("ReportId160Text", SortDirection.Ascending));
		}

		protected void cRptElmTypeCd160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "2") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "2"; Session[KEY_lastSortExp] = "RptElmTypeCd160Text";Session[KEY_lastSortImg] = "cRptElmTypeCd160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("RptElmTypeCd160Text", SortDirection.Ascending));
		}

		protected void cRptStyleId160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "3") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "3"; Session[KEY_lastSortExp] = "RptStyleId160Text";Session[KEY_lastSortImg] = "cRptStyleId160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("RptStyleId160Text", SortDirection.Ascending));
		}

		protected void cElmHeight160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "4") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "4"; Session[KEY_lastSortExp] = "ElmHeight160";Session[KEY_lastSortImg] = "cElmHeight160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("ElmHeight160", SortDirection.Ascending));
		}

		protected void cElmColumns160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "5") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "5"; Session[KEY_lastSortExp] = "ElmColumns160";Session[KEY_lastSortImg] = "cElmColumns160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("ElmColumns160", SortDirection.Ascending));
		}

		protected void cElmColSpacing160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "6") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "6"; Session[KEY_lastSortExp] = "ElmColSpacing160";Session[KEY_lastSortImg] = "cElmColSpacing160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("ElmColSpacing160", SortDirection.Ascending));
		}

		protected void cElmPrintFirst160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "7") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "7"; Session[KEY_lastSortExp] = "ElmPrintFirst160";Session[KEY_lastSortImg] = "cElmPrintFirst160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("ElmPrintFirst160", SortDirection.Ascending));
		}

		protected void cElmPrintLast160hl_Click(object sender, System.EventArgs e)
		{
			if (Session[KEY_lastSortCol] == null || (string)Session[KEY_lastSortCol] != "8") { Session.Remove(KEY_lastSortUrl); }
			Session[KEY_lastSortTog] = "Y"; Session[KEY_lastSortCol] = "8"; Session[KEY_lastSortExp] = "ElmPrintLast160";Session[KEY_lastSortImg] = "cElmPrintLast160hi";
			cAdmRptElmGrid_OnSorting(sender, new ListViewSortEventArgs("ElmPrintLast160", SortDirection.Ascending));
		}

		private double AggrGrid(DataView dv, string ColName, string AggrInst)
		{
			int dvCnt = dv.Count;
			bool bFirst = true;
			if (AggrInst == "C") {return dvCnt;}
			else try
			{
				double tt = 0;
				double vv = 0;
				double mm = 0;
				double ii = 0;
				foreach (DataRowView drv in dv)
				{
					if (drv[ColName].ToString() != string.Empty)
				    {
				        vv = Double.Parse(drv[ColName].ToString());
				        if (bFirst) { mm = vv; ii = vv; bFirst = false; }
				        else
				        {
				            if (vv > mm) { mm = vv; }
				            if (vv < ii) { ii = vv; }
				        }
				        tt = tt + vv;
				    }
				}
				if (AggrInst == "S") {return tt;}
				else if (AggrInst == "M") { return mm; }
				else if (AggrInst == "I") { return ii; }
				else if (AggrInst == "A" && dvCnt > 0) {return tt/dvCnt;} else {return 0;}
			} catch { return 0; }
		}
		protected string GetAggr(string columnName, string typ)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if (dt != null) return AggrGrid(dt.DefaultView, columnName, typ).ToString("F9"); //to prevent scientific notation
			else return string.Empty;
		}

		protected void cAdmRptElmGrid_OnItemEditing(object sender, ListViewEditEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if ((ValidPage()) && UpdateGridRow(sender, new CommandEventArgs("Save", "")) && dt != null && dt.DefaultView.Count > GetDataItemIndex(e.NewEditIndex))
			{
				cAdmRptElmGrid.EditIndex = e.NewEditIndex;
				cAdmRptElmGrid_DataBind(null);
				cRptStyleId160Search_Script();
				Control wc = null;
				string idx = Page.Request["__EVENTARGUMENT"];
				if (!string.IsNullOrEmpty(idx))
				{
				    Control ctrlToFocus = cAdmRptElmGrid.EditItem.FindControl("c" + idx);
				    if (((WebControl)ctrlToFocus).Enabled) { wc = ctrlToFocus; }
				}
				if (wc == null) { wc = FindEditableControl(cAdmRptElmGrid.EditItem); }
				if (wc != null)
             {
                 if (wc is RoboCoder.WebControls.ComboBox)
                 {
                     if (isMobile.Text != "isMobile") ScriptManager.GetCurrent(Parent.Page).SetFocus(((RoboCoder.WebControls.ComboBox)wc).FocusID);
                 }
                 else if (wc is CheckBox && ("c" + idx) == wc.ID)
                 {
                     ((CheckBox)wc).Checked = !((CheckBox)wc).Checked; ShowDirty(true);
                 }
                 else
                 {
                     if (isMobile.Text != "isMobile") ScriptManager.GetCurrent(Parent.Page).SetFocus(wc.ClientID);
                 }
             }
             MkMobileLabel(cAdmRptElmGrid.EditItem);
		    }
		    else { e.Cancel = true; }
		}

		private void MkMobileLabel(ListViewItem lvi)
		{
		    Label ml = null;
		    ml = lvi.FindControl("cRptElmId160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(0); }
		    ml = lvi.FindControl("cReportId160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(1); }
		    ml = lvi.FindControl("cRptElmTypeCd160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(2); }
		    ml = lvi.FindControl("cRptStyleId160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(3); }
		    ml = lvi.FindControl("cElmHeight160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(4); }
		    ml = lvi.FindControl("cElmColumns160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(5); }
		    ml = lvi.FindControl("cElmColSpacing160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(6); }
		    ml = lvi.FindControl("cElmPrintFirst160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(7); }
		    ml = lvi.FindControl("cElmPrintLast160ml") as Label;
		    if (ml != null) { ml.Text = ColumnHeaderText(8); }
		}

		protected void GridFill(ListViewItem lvi, DataTable dt, DataRow dr, bool bInsert)
		{
			RoboCoder.WebControls.ComboBox cbb = null;
			cbb = (RoboCoder.WebControls.ComboBox)lvi.FindControl("cReportId160");
			if (cbb != null)
			{
				if (cbb.SelectedValue != string.Empty)
				{
					dr["ReportId160"] = cbb.SelectedValue;
					dr["ReportId160Text"] = cbb.SelectedText;
				}
				else
				{
					dr["ReportId160"] = System.DBNull.Value;
					dr["ReportId160Text"] = System.DBNull.Value;
				}
			}
			DropDownList ddl = null;
			ddl = (DropDownList)lvi.FindControl("cRptElmTypeCd160");
			if (ddl != null)
			{
				if (ddl.SelectedIndex >= 0 && ddl.SelectedValue.ToString() != string.Empty)
				{
					dr["RptElmTypeCd160"] = ddl.SelectedValue;
					dr["RptElmTypeCd160Text"] = ddl.SelectedItem.Text;
				}
				else
				{
					dr["RptElmTypeCd160"] = System.DBNull.Value;
					dr["RptElmTypeCd160Text"] = System.DBNull.Value;
				}
			}
			cbb = (RoboCoder.WebControls.ComboBox)lvi.FindControl("cRptStyleId160");
			if (cbb != null)
			{
				if (cbb.SelectedValue != string.Empty)
				{
					dr["RptStyleId160"] = cbb.SelectedValue;
					dr["RptStyleId160Text"] = cbb.SelectedText;
				}
				else
				{
					dr["RptStyleId160"] = System.DBNull.Value;
					dr["RptStyleId160Text"] = System.DBNull.Value;
				}
			}
			TextBox tb = null;
			tb = (TextBox)lvi.FindControl("cElmHeight160");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["ElmHeight160"] = tb.Text;} else {dr["ElmHeight160"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cElmColumns160");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["ElmColumns160"] = tb.Text;} else {dr["ElmColumns160"] = System.DBNull.Value;}
			}
			tb = (TextBox)lvi.FindControl("cElmColSpacing160");
			if (tb != null)
			{
				if (tb.Text != string.Empty) {dr["ElmColSpacing160"] = tb.Text;} else {dr["ElmColSpacing160"] = System.DBNull.Value;}
			}
			CheckBox cb = null;
			cb = (CheckBox)lvi.FindControl("cElmPrintFirst160");
			if (cb != null)
			{
				dr["ElmPrintFirst160"] = base.SetBool(cb.Checked);
			}
			cb = (CheckBox)lvi.FindControl("cElmPrintLast160");
			if (cb != null)
			{
				dr["ElmPrintLast160"] = base.SetBool(cb.Checked);
			}
		    if (bInsert) { dt.Rows.InsertAt(dr, 0); }
			Session[KEY_dtAdmRptElmGrid] = dt;
			cAdmRptElmGrid.EditIndex = -1;
			cAdmRptElmGrid_DataBind(dt.DefaultView);
		}

		protected void cAdmRptElmGrid_OnItemUpdating(object sender, ListViewUpdateEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			if (dt != null && dt.Rows.Count > 0)
			{
				DataRow dr = dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row;
			    GridFill(cAdmRptElmGrid.EditItem, dt, dr, false);
			    dr["_NewRow"] = "N";
			    if (dt.Columns.Contains("_SortOrder")) dr["_SortOrder"] = null;
			}
		}

		protected void cAdmRptElmGrid_OnItemCanceling(object sender, ListViewCancelEventArgs e)
		{
			PanelTop.Update();
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && dv[GetDataItemIndex(e.ItemIndex)]["_NewRow"].ToString() == "Y")
			{
				cAdmRptElmGrid_OnItemDeleting(sender, new ListViewDeleteEventArgs(e.ItemIndex));
			}
			if (dt.Columns.Contains("_SortOrder")) dt.DefaultView[GetDataItemIndex(e.ItemIndex)].Row["_SortOrder"] = null;
			cAdmRptElmGrid.EditIndex = -1; cAdmRptElmGrid_DataBind(null);
		}

		protected void cDeleteAllButton_Click(object sender, System.EventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			if (dv != null && (cAdmRptElmGrid.EditIndex < 0 || UpdateGridRow(sender, new CommandEventArgs("Save", ""))))
			{
				GotoPage(0);
				while (dv.Count > 0) {dv.Delete(0);}
				Session[KEY_dtAdmRptElmGrid] = dt;
				cAdmRptElmGrid.EditIndex = -1; cAdmRptElmGrid_DataBind(dv);
			}
		}

		protected void cAdmRptElmGrid_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
		{
			DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
			DataView dv = dt != null ? dt.DefaultView : null;
			DataRow dr = dv != null ? dv[GetDataItemIndex(e.ItemIndex)].Row : null;
			if (dv != null && (cAdmRptElmGrid.EditIndex == e.ItemIndex || UpdateGridRow(sender, new CommandEventArgs("Save",""))))
			{
				// *** Delete Grid Row (before) Web Rule End *** //
				dr.Delete();
				Session[KEY_dtAdmRptElmGrid] = dt;
				cAdmRptElmGrid.EditIndex = -1; cAdmRptElmGrid_DataBind(dv);
			}
		}

		public void cUndoAllButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			PopAdmRptElm91List(sender, e, false, null);
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cPreviewButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cEditButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			cSystem.Visible = true;
			bUseCri.Value = "Y"; GetCriteria(GetScrCriteria());
			Session.Remove(KEY_dtAdmRptElmGrid); PopAdmRptElm91List(sender, e, false, null);
			cEditButton.Visible = false;
			// *** System Button Click (After) Web Rule starts here *** //
		}

		public void cSaveCloseButton_Click(object sender, System.EventArgs e)
		{
			// *** System Button Click (Before) Web Rule starts here *** //
			try {
			    string msg = SaveDb(sender, new EventArgs());
			    if (msg != string.Empty)
			    {
			        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "closeDlg", @"<script type='text/javascript'>window.parent.closeParentDlg();</script>", false);
			    }
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
			// *** System Button Click (After) Web Rule starts here *** //
		}

		private string SaveDb(object sender, System.EventArgs e)
		{
			string rtn = string.Empty;
			bool noTrans = Config.NoTrans;
			int commandTimeOut = Config.CommandTimeOut;
			// *** System Button Click (Before) Web Rule starts here *** //
			if (ValidPage() && UpdateGridRow(cAdmRptElmGrid, new CommandEventArgs("Save", "")))
			{
				DataTable dt = (DataTable)Session[KEY_dtAdmRptElmGrid];
				DataView dv = dt != null ? dt.DefaultView : null;
				string ftr = string.Empty;
				if (dv.RowFilter != string.Empty) { ftr = dv.RowFilter; dv.RowFilter = string.Empty; }
				AdmRptElm91 ds = PrepAdmRptElmData(dv,false);
				if (ftr != string.Empty) {dv.RowFilter = ftr;}
					bool bValid7 = false;
					if (ds != null && (new AdminSystem()).UpdData(91,false,base.LUser,base.LImpr,base.LCurr,ds,(string)Session[KEY_sysConnectionString],base.AppPwd(base.LCurr.DbId),base.CPrj,base.CSrc,noTrans,commandTimeOut)) {bValid7 = true;}
					if (bValid7)
					{
						Session[KEY_currPageIndex] = GetCurrPageIndex();
						ShowDirty(false); bUseCri.Value = "Y"; PopAdmRptElm91List(sender, e, false, null);
						rtn = GetScreenHlp().Rows[0]["UpdMsg"].ToString();
					}
			}
			// *** System Button Click (After) Web Rule starts here *** //
			return rtn;
		}

		public void cSaveButton_Click(object sender, System.EventArgs e)
		{
			try {
			    string msg = SaveDb(sender, e);
			    if (msg != string.Empty && Config.PromptMsg) { bInfoNow.Value = "Y"; PreMsgPopup(msg); }
			}
			catch (Exception err) { bErrNow.Value = "Y"; PreMsgPopup(err.Message); return; }
		}

		private AdmRptElm91 PrepAdmRptElmData(DataView dv, bool bAdd)
		{
			AdmRptElm91 ds = new AdmRptElm91();
			if (dv != null)
			{
				ds.Tables["AdmRptElmDef"].Rows.Add(MakeTypRow(ds.Tables["AdmRptElmDef"].NewRow()));
				ds.Tables["AdmRptElmDef"].Rows.Add(MakeDisRow(ds.Tables["AdmRptElmDef"].NewRow()));
					dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmRptElmUpd"].Rows.Add(MakeColRow(ds.Tables["AdmRptElmUpd"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.Added;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmRptElmAdd"].Rows.Add(MakeColRow(ds.Tables["AdmRptElmAdd"].NewRow(), drv, true));
					}
					dv.RowStateFilter = DataViewRowState.Deleted;
					foreach (DataRowView drv in dv)
					{
						ds.Tables["AdmRptElmDel"].Rows.Add(MakeColRow(ds.Tables["AdmRptElmDel"].NewRow(), drv, false));
					}
					dv.RowStateFilter = DataViewRowState.CurrentRows;
			}
			return ds;
		}

		public bool CanAct(char typ) { return CanAct(typ, GetAuthRow(), null); }

		private bool ValidPage()
		{
			EnableValidators(true);
			Page.Validate();
			if (!Page.IsValid) { PanelTop.Update(); return false; }
			DataTable dt = null;
			dt = (DataTable)Session[KEY_dtReportId160];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'ReportId', please investigate."); return false;
			}
			dt = (DataTable)Session[KEY_dtRptElmTypeCd160];
			if (dt != null && dt.Rows.Count <= 0)
			{
				bErrNow.Value = "Y"; PreMsgPopup("Value is expected for 'RptElmTypeCd', please investigate."); return false;
			}
			return true;
		}

		private DataRow MakeTypRow(DataRow dr)
		{
			dr["RptElmId160"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["ReportId160"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["RptElmTypeCd160"] = System.Data.OleDb.OleDbType.Char.ToString();
			dr["RptStyleId160"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["ElmHeight160"] = System.Data.OleDb.OleDbType.Decimal.ToString();
			dr["ElmColumns160"] = System.Data.OleDb.OleDbType.Numeric.ToString();
			dr["ElmColSpacing160"] = System.Data.OleDb.OleDbType.Decimal.ToString();
			dr["ElmPrintFirst160"] = System.Data.OleDb.OleDbType.Char.ToString();
			dr["ElmPrintLast160"] = System.Data.OleDb.OleDbType.Char.ToString();
			return dr;
		}

		private DataRow MakeDisRow(DataRow dr)
		{
			dr["RptElmId160"] = "TextBox";
			dr["ReportId160"] = "AutoComplete";
			dr["RptElmTypeCd160"] = "DropDownList";
			dr["RptStyleId160"] = "AutoComplete";
			dr["ElmHeight160"] = "TextBox";
			dr["ElmColumns160"] = "TextBox";
			dr["ElmColSpacing160"] = "TextBox";
			dr["ElmPrintFirst160"] = "CheckBox";
			dr["ElmPrintLast160"] = "CheckBox";
			return dr;
		}

		private DataRow MakeColRow(DataRow dr, DataRowView drv, bool bAdd)
		{
			DataTable dtAuth = GetAuthCol();
			if (dtAuth != null)
			{
				dr["RptElmId160"] = drv["RptElmId160"].ToString().Trim();
				dr["ReportId160"] = drv["ReportId160"];
				dr["RptElmTypeCd160"] = drv["RptElmTypeCd160"];
				dr["RptStyleId160"] = drv["RptStyleId160"];
				if (bAdd && dtAuth.Rows[3]["ColReadOnly"].ToString() == "Y" && dr["RptStyleId160"].ToString() == string.Empty) {dr["RptStyleId160"] = System.DBNull.Value;}
				dr["ElmHeight160"] = drv["ElmHeight160"].ToString().Trim();
				dr["ElmColumns160"] = drv["ElmColumns160"].ToString().Trim();
				if (bAdd && dtAuth.Rows[5]["ColReadOnly"].ToString() == "Y" && dr["ElmColumns160"].ToString() == string.Empty) {dr["ElmColumns160"] = System.DBNull.Value;}
				dr["ElmColSpacing160"] = drv["ElmColSpacing160"].ToString().Trim();
				if (bAdd && dtAuth.Rows[6]["ColReadOnly"].ToString() == "Y" && dr["ElmColSpacing160"].ToString() == string.Empty) {dr["ElmColSpacing160"] = System.DBNull.Value;}
				dr["ElmPrintFirst160"] = drv["ElmPrintFirst160"];
				dr["ElmPrintLast160"] = drv["ElmPrintLast160"];
			}
			return dr;
		}

		private bool UpdateGridRow(object sender, CommandEventArgs e)
		{
			if (cAdmRptElmGrid.EditIndex > -1)
			{
				cAdmRptElmGrid_OnItemUpdating(sender, new ListViewUpdateEventArgs(cAdmRptElmGrid.EditIndex));
			}
			return true;
		}

		protected void cAdmRptElmGrid_OnPreRender(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.Image hi = null;
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cRptElmId160hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cReportId160hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cRptElmTypeCd160hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cRptStyleId160hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cElmHeight160hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cElmColumns160hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cElmColSpacing160hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cElmPrintFirst160hi"); if (hi != null) { hi.Visible = false; }
			hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl("cElmPrintLast160hi"); if (hi != null) { hi.Visible = false; }
			if (Session[KEY_lastSortImg] != null)
			{
				hi = (System.Web.UI.WebControls.Image)cAdmRptElmGrid.FindControl((string)Session[KEY_lastSortImg]);
				if (hi != null) { hi.ImageUrl = Utils.AddTilde((string)Session[KEY_lastSortUrl]); hi.Visible = true; }
			}
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cRptElmId160hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cReportId160hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cRptElmTypeCd160hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cRptStyleId160hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cElmHeight160hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cElmColumns160hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cElmColSpacing160hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cElmPrintFirst160hl"));
			IgnoreHeaderConfirm((LinkButton)cAdmRptElmGrid.FindControl("cElmPrintLast160hl"));

			// Take care of aggregates:
			Label aggr = null;
			aggr = cAdmRptElmGrid.FindControl("cElmHeight160fl") as Label;
			if (aggr != null) { aggr.Text = RO.Common3.Utils.fmNumeric("2",GetAggr("ElmHeight160","S"),base.LUser.Culture); }
		}

		private string GetButtonId(ListViewItem lvi)
		{
			string ButtonID = String.Empty;
			Control c = lvi.FindControl("cAdmRptElmGridEdit");
			if (c != null) { ButtonID = c.UniqueID; }
			return ButtonID;
		}

		private void SetDefaultCtrl(HtmlTableRow tr, LinkButton lb, string ctrlId)
		{
			tr.Attributes.Add("onclick", "document.getElementById('" + bConfirm.ClientID + "').value='N'; fFocusedEdit('" + lb.UniqueID + "','" + ctrlId + "',event);");
		}

		private int GetCurrPageIndex()
		{
		    return cAdmRptElmGridDataPager.StartRowIndex / cAdmRptElmGridDataPager.PageSize;
		}

		private int GetTotalPages()
		{
		    return (int)Math.Ceiling((double)cAdmRptElmGridDataPager.TotalRowCount / cAdmRptElmGridDataPager.PageSize);
		}

		private int GetDataItemIndex(int editIndex)
		{
		    return cAdmRptElmGridDataPager.StartRowIndex + editIndex;
		}

		protected void cAdmRptElmGrid_OnLayoutCreated(object sender, EventArgs e)
		{
		    // Header:
		    LinkButton lb = null;
		    lb = cAdmRptElmGrid.FindControl("cRptElmId160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(0); lb.ToolTip = ColumnToolTip(0); lb.Parent.Visible = GridColumnVisible(0); }
		    lb = cAdmRptElmGrid.FindControl("cReportId160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(1); lb.ToolTip = ColumnToolTip(1); lb.Parent.Visible = GridColumnVisible(1); }
		    lb = cAdmRptElmGrid.FindControl("cRptElmTypeCd160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(2); lb.ToolTip = ColumnToolTip(2); lb.Parent.Visible = GridColumnVisible(2); }
		    lb = cAdmRptElmGrid.FindControl("cRptStyleId160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(3); lb.ToolTip = ColumnToolTip(3); lb.Parent.Visible = GridColumnVisible(3); }
		    lb = cAdmRptElmGrid.FindControl("cElmHeight160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(4); lb.ToolTip = ColumnToolTip(4); lb.Parent.Visible = GridColumnVisible(4); }
		    lb = cAdmRptElmGrid.FindControl("cElmColumns160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(5); lb.ToolTip = ColumnToolTip(5); lb.Parent.Visible = GridColumnVisible(5); }
		    lb = cAdmRptElmGrid.FindControl("cElmColSpacing160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(6); lb.ToolTip = ColumnToolTip(6); lb.Parent.Visible = GridColumnVisible(6); }
		    lb = cAdmRptElmGrid.FindControl("cElmPrintFirst160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(7); lb.ToolTip = ColumnToolTip(7); lb.Parent.Visible = GridColumnVisible(7); }
		    lb = cAdmRptElmGrid.FindControl("cElmPrintLast160hl") as LinkButton;
		    if (lb != null) { lb.Text = ColumnHeaderText(8); lb.ToolTip = ColumnToolTip(8); lb.Parent.Visible = GridColumnVisible(8); }
		    // Hide DeleteAll:
			DataTable dtAuthRow = GetAuthRow();
			if (dtAuthRow != null)
			{
				DataRow dr = dtAuthRow.Rows[0];
				if (dr["AllowDel"].ToString() == "N" || (Request.QueryString["enb"] != null && Request.QueryString["enb"] == "N"))
				{
		            lb = cAdmRptElmGrid.FindControl("cDeleteAllButton") as LinkButton; if (lb != null) { lb.Visible = false; }
				}
			}
		    // footer:
		    Label gc = null;
		    gc = cAdmRptElmGrid.FindControl("cRptElmId160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(0); }
		    gc = cAdmRptElmGrid.FindControl("cReportId160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(1); }
		    gc = cAdmRptElmGrid.FindControl("cRptElmTypeCd160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(2); }
		    gc = cAdmRptElmGrid.FindControl("cRptStyleId160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(3); }
		    gc = cAdmRptElmGrid.FindControl("cElmHeight160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(4); }
		    gc = cAdmRptElmGrid.FindControl("cElmColumns160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(5); }
		    gc = cAdmRptElmGrid.FindControl("cElmColSpacing160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(6); }
		    gc = cAdmRptElmGrid.FindControl("cElmPrintFirst160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(7); }
		    gc = cAdmRptElmGrid.FindControl("cElmPrintLast160fl") as Label;
		    if (gc != null) { gc.Parent.Visible = GridColumnVisible(8); }
		}

		protected void cbPostBack(object sender, System.EventArgs e)
		{
		}

		protected void IgnoreHeaderConfirm(LinkButton lb)
		{
		    if (lb != null && (lb.Attributes["OnClick"] == null || lb.Attributes["OnClick"].IndexOf("_bConfirm") < 0) && lb.Visible) { lb.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';"; }
		}

		protected void IgnoreConfirm()
		{
			if (cExpTxtButton.Attributes["OnClick"] == null || cExpTxtButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cExpTxtButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cExpRtfButton.Attributes["OnClick"] == null || cExpRtfButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cExpRtfButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cUndoAllButton.Attributes["OnClick"] == null || cUndoAllButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cUndoAllButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cSaveCloseButton.Attributes["OnClick"] == null || cSaveCloseButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cSaveCloseButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cSaveButton.Attributes["OnClick"] == null || cSaveButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cSaveButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cInsRowButton.Attributes["OnClick"] == null || cInsRowButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cInsRowButton.Attributes["onclick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cBrowse.Attributes["OnClick"] == null || cBrowse.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cBrowse.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cPgSizeButton.Attributes["OnClick"] == null || cPgSizeButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cPgSizeButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cFirstButton.Attributes["OnClick"] == null || cFirstButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cFirstButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cPrevButton.Attributes["OnClick"] == null || cPrevButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cPrevButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cNextButton.Attributes["OnClick"] == null || cNextButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cNextButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cLastButton.Attributes["OnClick"] == null || cLastButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cLastButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cFindButton.Attributes["OnClick"] == null || cFindButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cFindButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cHideImpButton.Attributes["OnClick"] == null || cHideImpButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cHideImpButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
			if (cShowImpButton.Attributes["OnClick"] == null || cShowImpButton.Attributes["OnClick"].IndexOf("_bConfirm") < 0) {cShowImpButton.Attributes["OnClick"] += "document.getElementById('" + bConfirm.ClientID + "').value='N';";}
		}

		protected void InitPreserve()
		{
			cSystemId.Attributes["onchange"] = "javascript:return CanPostBack(true,this);";cSystemId.Attributes["NeedConfirm"] = "Y";
			cFilterId.Attributes["onchange"] = "javascript:return CanPostBack(true,this);";cFilterId.Attributes["NeedConfirm"] = "Y";
		}

		protected void ShowDirty(bool bShow)
		{
			if (bShow) {bPgDirty.Value = "Y";} else {bPgDirty.Value = "N";}
		}

		private void PreMsgPopup(string msg) { PreMsgPopup(msg,null,null); }

		private void PreMsgPopup(string msg, RoboCoder.WebControls.ComboBox cb, WebControl wc)
		{
		    if (string.IsNullOrEmpty(msg)) return;
		    if (IsCronInvoked()) { ErrorTrace(new Exception(msg), bErrNow.Value == "N" ? "warning" : "error"); return; }
		    int MsgPos = msg.IndexOf("RO.SystemFramewk.ApplicationAssert");
		    string iconUrl = "images/warning.gif";
		    string focusOnCloseId = cb != null ? cb.FocusID : (wc != null ? wc.ClientID : string.Empty);
		    string msgContent = ReformatErrMsg(msg);
		    if (MsgPos >= 0 && LUser.TechnicalUsr != "Y") { msgContent = ReformatErrMsg(msg.Substring(0, MsgPos - 3)); }
		    if (bErrNow.Value == "Y") { iconUrl = "images/error.gif"; bErrNow.Value = "N"; }
		    else if (bInfoNow.Value == "Y") { iconUrl = "images/info.gif"; bInfoNow.Value = "N"; }
			string script =
			@"<script type='text/javascript' lang='javascript'>
			PopDialog('" + iconUrl + "','" + msgContent.Replace(@"\", @"\\").Replace("'", @"\'") + "','" + focusOnCloseId + @"');
			</script>";
			ScriptManager.RegisterStartupScript(cMsgContent, typeof(Label), "Popup", script, false);
		}

		private Control FindEditableControl(Control root)
		{
		    Control found = null;
		    if (IsEditableControl(root)) { found = root; return found; }
		    foreach (Control c in root.Controls)
		    {
		        found = FindEditableControl(c); if (found != null) return found;
		    }
		    return null;
		}
	}
}

