using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WuDada.Core.SystemApplications.Service;
using WuDada.Core.SystemApplications;
using System.Web.UI;
using WuDada.Core.SystemApplications.Domain;
using Common.Logging;
using Spring.Objects;
using System.IO;
using FredCK.FCKeditorV2;
using System.Collections;
using Spring.Core;
using WuDada.Core.Generic.Util;

/// <summary>
/// UIHelper 的摘要描述
/// </summary>
public class UIHelper
{


    /// <summary>
    /// 後台使用者登入頁
    /// </summary>
    public static readonly string LOGIN_PAGE_MANAGER = "~/admin/Login/login.aspx";



    private static readonly string[] FILL_UIVO_LISTCONTROL_NOTE = new string[] { "ddl", "rdo" };

    public UIHelper()
    {

    }

    public static IList<ListItem> GetYearItemList(int from, int to)
    {
        IList<ListItem> resultList = new List<ListItem>();
        for (int i = from; i <= to; i++)
        {
            resultList.Add(new ListItem(i.ToString()));
        }
        return resultList;
    }
    public static IList<ListItem> GetMonthtemList()
    {
        IList<ListItem> resultList = new List<ListItem>();
        for (int i = 1; i <= 12; i++)
        {
            resultList.Add(new ListItem(i.ToString()));
        }
        return resultList;
    }
    public static IList<ListItem> GetDayItemList(int year, int month)
    {

        int days = DateTime.DaysInMonth(year, month);
        IList<ListItem> resultList = new List<ListItem>();
        for (int i = 1; i <= days; i++)
        {
            DateTime date = new DateTime(year, month, i);
            resultList.Add(new ListItem(i.ToString() + "(" + ConvertUtil.GetChineseDayOfWeek(date) + ")", date.ToShortDateString()));
        }
        return resultList;
    }
    /// <summary>
    /// 設定DropDownlist的值，若無則不設定
    /// </summary>
    /// <param name="ddlMaster"></param>
    /// <param name="p"></param>
    public static void SetItemSelectedValue(ref DropDownList ddlMaster, string p)
    {
        if (!string.IsNullOrEmpty(p))
        {
            foreach (ListItem item in ddlMaster.Items)
            {
                if (item.Value.Equals(p))
                {
                    ddlMaster.SelectedValue = p;
                    break;
                }
            }
        }
    }


    public static void SetItemSelectedValue(ref RadioButtonList ddlMaster, string p)
    {
        if (!string.IsNullOrEmpty(p))
        {
            foreach (ListItem item in ddlMaster.Items)
            {
                if (item.Value.Equals(p))
                {
                    ddlMaster.SelectedValue = p;
                    break;
                }
            }

        }
    }

    /// <summary>
    /// 將Decimal型態轉成int 再取str
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string DecimalToInStr(decimal p)
    {
        return (Decimal.ToInt32(p).ToString());
    }

    /// <summary>
    ///  將txt欄位的值放入值，若txt為空，則回傳零
    /// </summary>
    /// <param name="txtIn_prices"></param>
    /// <param name="vo"></param>
    /// <param name="p"></param>
    public static int SetCtrlTxtTOIntValue(string value)
    {

        if (!String.IsNullOrEmpty(value))
        {
            return (int.Parse(value));
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 取的分類中的代碼
    /// </summary>
    /// <param name="classify">分類名稱</param>
    /// <param name="isDefalut">是否要預設加入一筆未選</param>
    /// <returns></returns>
    public static IList<ListItem> GetSystemItemList(string classify, bool isDefalut)
    {
        SystemFactory systemFactory = new SystemFactory();
        ISystemService sysemSerivce = systemFactory.GetSystemService();

        IList<ListItem> result = SystemItemToListItem(sysemSerivce.GetAllItemParamByNoDel(classify));

        if (isDefalut)
        {
            result.Insert(0, new ListItem("未選", ""));
        }

        return result;
    }
    /// <summary>
    /// 設定GridViewDataRow屬性
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="attribute"></param>
    /// <param name="value"></param>
    public static void SetDataRowAttribute(ref Control ctrl, string attribute, string value)
    {
        GridViewRow item = (GridViewRow)ctrl;
        if (item.RowType == DataControlRowType.DataRow)
        {
            item.Attributes.Add(attribute, value);
        }
    }
    /// <summary>
    /// 設定GridViewDataRow屬性
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="attribute"></param>
    /// <param name="value"></param>
    public static void SetDataRowAttribute(ref Control ctrl, int[] cellIndex, string attribute, string value)
    {
        GridViewRow item = (GridViewRow)ctrl;
        if (item.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < cellIndex.Length; i++)
            {
                item.Cells[cellIndex[i]].Attributes.Add(attribute, value);
            }
        }
    }

    /// <summary>
    /// 設定DropDownList裡面的值，若無此選項，則會產生一個請選擇的ListItem
    /// </summary>
    /// <param name="ddlList">要設定的DropDownList</param>
    /// <param name="obj">物件</param>
    /// <param name="p">物件裡的值的名字</param>
    public static void SetDDLValue(ListControl ddlList, object obj, string p)
    {
        ListItem listItem = new ListItem("請選擇", "");
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        if (obj == null)
        {
            ddlList.Items.Insert(0, listItem);
            ddlList.SelectedIndex = 0;
        }
        else
        {
            ObjectWrapper wrapper = new ObjectWrapper(obj);
            object objValue = wrapper.GetPropertyValue(p);
            //空值則跳過不處理了
            if (objValue == null)
            {
                return;
            }
            string value = objValue.ToString().Trim();

            if (ddlList.Items.Count > 0)
            {
                foreach (ListItem item in ddlList.Items)
                {
                    if (item.Value.Equals(value))
                    {
                        log.Debug("value:" + value);
                        log.Debug("itemvalue:" + item.Value);
                        ddlList.SelectedValue = value;

                        ddlList.DataBind();
                        return;
                    }
                }
                //沒有包含值
                //ddlList.Items.Insert(0, listItem);
                if (!string.IsNullOrEmpty(value))
                {
                    ListItem listNoItem = new ListItem(value, value);
                    ddlList.Items.Add(listNoItem);
                    ddlList.SelectedValue = value;

                }
                else
                {
                    ddlList.SelectedIndex = 0;
                }                
                ddlList.DataBind();
            }
            else
            {
                ddlList.Items.Insert(0, listItem);
                ddlList.SelectedIndex = 0;
                ddlList.DataBind();
            }
        }
    }


    /// <summary>
    /// 將ddl設定選取的值 若找不到則產生一個新的
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="value"></param>
    public static void SetDDLFromSystemItem(ref DropDownList ddl, string value)
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        log.Debug("value=" + value);

        if (string.IsNullOrEmpty(value))
        {
            ddl.SelectedIndex = -1;
            return;
        }

        ddl.DataBind();
        foreach (ListItem listItem in ddl.Items)
        {
            if (listItem.Value.Equals(value))
            {
                ddl.SelectedValue = value;
                return;
            }
        }

        //找不到值 則建一個新的listItem
        ddl.Items.Add(new ListItem(value, value));

        ddl.SelectedValue = value;

    }


    /// <summary>
    /// 將系統代碼轉為ListItem的LIST
    /// </summary>
    /// <param name="itemParamList"></param>
    /// <returns></returns>
    private static IList<ListItem> SystemItemToListItem(IList<ItemParamVO> itemParamList)
    {
        IList<ListItem> list = new List<ListItem>();

        if (itemParamList != null && itemParamList.Count > 0)
        {
            foreach (ItemParamVO pVO in itemParamList)
            {
                list.Add(new ListItem(pVO.Name, pVO.Value));
            }
        }
        return list;
    }


    /// <summary>
    /// 找出HiddenField 的value
    /// </summary>
    /// <param name="ctrl">UI控制項</param>
    /// <param name="id">找尋的Id</param>
    public static string FindHiddenValue(ref Control ctrl, string id)
    {
        return (((HiddenField)ctrl.FindControl(id)).Value);
    }

    /// <summary>
    /// 找出TextBox 的Text
    /// </summary>
    /// <param name="ctrl">UI控制項</param>
    /// <param name="id">找尋的Id</param>
    public static string FindTextBoxText(ref Control ctrl, string id)
    {
        return (((TextBox)ctrl.FindControl(id)).Text);
    }
    /// <summary>
    /// 找出TextBox 
    /// </summary>
    /// <param name="ctrl">UI控制項</param>
    /// <param name="id">找尋的Id</param>
    public static TextBox FindTextBox(ref Control ctrl, string id)
    {
        return (((TextBox)ctrl.FindControl(id)));
    }
    /// <summary>
    /// 找出LinkButton
    /// </summary>
    /// <param name="ctrl">UI控制項</param>
    /// <param name="id">找尋的Id</param>
    public static LinkButton FindLinkButton(ref Control ctrl, string id)
    {
        return (((LinkButton)ctrl.FindControl(id)));
    }
    /// <summary>
    /// 找出DropDownList
    /// </summary>
    /// <param name="ctrl">UI控制項</param>
    /// <param name="id">找尋的Id</param>
    public static DropDownList FindDropDownList(ref Control ctrl, string id)
    {
        return (((DropDownList)ctrl.FindControl(id)));
    }

    public static string FindLabelText(ref Control ctrl, string id)
    {
        return (((Label)ctrl.FindControl(id)).Text);
    }

    /// <summary>
    /// 從找到的Ctrl裡去設Lable Text
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="id"></param>
    /// <param name="value"></param>
    public static void SetLabelText(ref Control ctrl, string id, string value)
    {
        ((Label)ctrl.FindControl(id)).Text = value;
    }


    /// <summary>
    /// 從找到的Ctrl裡去設Hidden Text
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="id"></param>
    /// <param name="value"></param>
    public static void SetHiddenValue(ref Control ctrl, string id, string value)
    {
        ((HiddenField)ctrl.FindControl(id)).Value = value;
    }

    /// <summary>
    ///  從找到的Ctrl裡去設TextBox Text
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="id"></param>
    /// <param name="value"></param>
    public static void SetTextBoxText(ref Control ctrl, string id, string value)
    {
        ((TextBox)ctrl.FindControl(id)).Text = value;
    }

    /// <summary>
    /// 設超連結的位置
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="id"></param>
    /// <param name="value"></param>
    public static void SetHyperLinkNUrl(ref Control ctrl, string id, string value)
    {
        ((HyperLink)ctrl.FindControl(id)).NavigateUrl = value;
    }

    /// <summary>
    /// 加入第一筆 預設請選擇的listItem
    /// </summary>
    /// <param name="ddl"></param>
    public static void InsertDefaultListItem(ref DropDownList ddl)
    {
        ddl.Items.Insert(0, new ListItem("請選擇", ""));
    }


    public static void GoToErrPage(string errMsg)
    {
        HttpContext.Current.Response.Redirect("~/admin/common/Error.aspx?ErrMsg=" + errMsg);
    }



    public static string FindHyperLinkText(ref Control ctrl, string p)
    {
        return (((HyperLink)ctrl.FindControl(p)).Text);
    }

    public static void SetHyperLinkText(ref Control ctrl, string id, string text)
    {
        ((HyperLink)ctrl.FindControl(id)).Text = text;
    }

    public static void SetContrlVisible(ref Control ctrl, string p, bool isVisible)
    {
        ctrl.FindControl(p).Visible = isVisible;
    }

    public static void SetCheckBoxChecked(ref Control ctrl, string id, bool isCheck)
    {
        ((CheckBox)ctrl.FindControl(id)).Checked = isCheck;
    }
    public static void SetCheckBoxChecked(ref Control ctrl, string id, bool isCheck, bool isEnable)
    {
        ((CheckBox)ctrl.FindControl(id)).Checked = isCheck;
        ((CheckBox)ctrl.FindControl(id)).Enabled = isEnable;
    }
    public static CheckBox FindCheckBox(ref Control ctrl, string p)
    {
        return (((CheckBox)ctrl.FindControl(p)));
    }

    public static void OutExcelByGridView(GridView gv, string outFileName)
    {
        string excelHeader = "<html><head><meta http-equiv=Content-Type content=text/html; charset=UTF-8></head><body>";
        string excelFooter = "</body></html>";

        outFileName += "_" + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + ".xls";

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + outFileName);

        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        gv.RenderControl(htmlTextWriter);
        HttpContext.Current.Response.Write(excelHeader + stringWriter.ToString() + excelFooter);
        HttpContext.Current.Response.End();
    }

    public static void OutExcelByGridView(IList<GridView> gvList, string outFileName)
    {
        string excelHeader = "<html><head><meta http-equiv=Content-Type content=text/html; charset=UTF-8></head><body>";
        string excelFooter = "</body></html>";

        outFileName += ".xls";

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + outFileName);

        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        foreach (GridView gv in gvList)
        {
            gv.RenderControl(htmlTextWriter);
        }
        HttpContext.Current.Response.Write(excelHeader + stringWriter.ToString() + excelFooter);
        HttpContext.Current.Response.End();
    }
    public static void OutExcelByGridView(IList<GridView> gvList, string outFileName, string headerString, string footerString)
    {
        string excelHeader = "<html><head><meta http-equiv=Content-Type content=text/html; charset=UTF-8></head><body>";
        excelHeader += "<table width='100%'><tr><td align='center' colspan='" + gvList[0].Columns.Count.ToString() + "'><font size=3><b>" + headerString + "</b></font><hr/></td></tr></table>";
        string excelFooter = "<table width='100%'><tr><td align='right' colspan='" + gvList[0].Columns.Count.ToString() + "'>" + footerString + "</td></tr></table>";
        excelFooter += "</body></html>";
        outFileName += ".xls";

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + outFileName);

        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        foreach (GridView gv in gvList)
        {
            gv.RenderControl(htmlTextWriter);
        }
        HttpContext.Current.Response.Write(excelHeader + stringWriter.ToString() + excelFooter);
        HttpContext.Current.Response.End();
    }




    /// <summary>
    /// 將集合項目加入到ListItem內
    /// </summary>
    /// <param name="ddl">DropDownList</param>
    /// <param name="iEnumerator">集合</param>
    /// <param name="textFiled">集合內要放入Text的欄位名</param>
    /// <param name="valueField">集合內要放入Value的欄位名</param>
    public static void AddDropDowListItem(ref DropDownList ddl, IEnumerator iEnumerator, string textFiled, string valueField)
    {
        while (iEnumerator.MoveNext())
        {
            object obj = iEnumerator.Current;

            ListItem item = new ListItem();

            ObjectWrapper objWraper = new ObjectWrapper(obj);
            item.Text = objWraper.GetPropertyValue(textFiled).ToString();
            item.Value = objWraper.GetPropertyValue(valueField).ToString();
            ddl.Items.Add(item);
        }
    }


    /// <summary>
    /// 將VO的值注入UI的control
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="obj"></param>
    public static void FillUI(Control ctrl, Object obj)
    {
        if (ctrl.HasControls())
        {
            foreach (Control cCtrl in ctrl.Controls)
            {
                ParsingFillField(ctrl, obj);
                Control tmpCtrl = cCtrl;
                FillUI(tmpCtrl, obj);
            }
        }
        else
        {
            ParsingFillField(ctrl, obj);
        }
    }

    private static void ParsingFillField(Control ctrl, Object obj)
    {
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        if (string.IsNullOrEmpty(ctrl.ID))
        {
            return;
        }

        ObjectWrapper ow = new ObjectWrapper(obj);

        if (ctrl is TextBox)
        {

            if (!ctrl.ID.StartsWith("txt"))
            {
                return;
            }

            TextBox txtCtrl = (TextBox)ctrl;

            string fieldName = ctrl.ID.Replace("txt", "");

            if (HasProperty(ow, fieldName))
            {
                object value = ow.GetPropertyValue(fieldName);

                if (value != null)
                {
                    if (ow.GetPropertyType(fieldName) == typeof(DateTime) || ow.GetPropertyType(fieldName) == typeof(DateTime?))
                    {
                        string setValue = ConvertUtil.DateToShortDateStr((DateTime)value);
                        log.Debug(string.Format("set {0} = {1}", txtCtrl.ID, setValue));
                        txtCtrl.Text = setValue;
                    }
                    else
                    {
                        log.Debug(string.Format("set {0} = {1}", txtCtrl.ID, value.ToString()));
                        txtCtrl.Text = value.ToString();
                    }
                }
                else
                {
                    txtCtrl.Text = string.Empty;
                }
            }

        }
        else if (ctrl is Label)
        {
            Label lblCtrl = (Label)ctrl;
            string fieldName = ctrl.ID.Replace("lbl", "");
            if (HasProperty(ow, fieldName))
            {
                object value = ow.GetPropertyValue(fieldName);
                if (value != null)
                {
                    log.Debug(string.Format("set {0} = {1}", lblCtrl.ID, value.ToString()));
                    lblCtrl.Text = value.ToString();
                }
            }
        }
        else if (ctrl is ListControl)
        {

            string inStr = checkCtrlInIdStartWith(ctrl.ID, FILL_UIVO_LISTCONTROL_NOTE);

            if (string.IsNullOrEmpty(inStr))
            {
                return;
            }

            string fieldName = ctrl.ID.Replace(inStr, "");

            if (HasProperty(ow, fieldName))
            {
                ListControl ddlCtrl = (ListControl)ctrl;
                log.Debug(string.Format("set {0} = {1}", ddlCtrl.ID, ow.GetPropertyValue(fieldName)));
                UIHelper.SetDDLValue(ddlCtrl, obj, fieldName);
            }
        }

        else if (ctrl is FCKeditor)
        {
            FCKeditor fckCtrl = (FCKeditor)ctrl;
            string fieldName = fckCtrl.ID.Replace("fck", "");
            if (HasProperty(ow, fieldName))
            {
                object value = ow.GetPropertyValue(fieldName);
                if (value != null)
                {
                    log.Debug(string.Format("set {0} = {1}", fckCtrl.ID, value.ToString()));
                    fckCtrl.Value = value.ToString();
                }
            }
        }
    }


    /// <summary>
    /// 檢查是否開頭為在陣列內容的元素，若有則傳回元素，若無傳回null
    /// </summary>
    /// <param name="p"></param>
    /// <param name="FILL_UIVO_LISTCONTROL_NOTE"></param>
    /// <returns></returns>
    private static string checkCtrlInIdStartWith(string p, string[] strArray)
    {
        foreach (string str in strArray)
        {
            if (p.StartsWith(str))
            {
                return str;
            }
        }
        return null;
    }

    /// <summary>
    /// 將UI的值Control注入VO
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="obj"></param>
    public static void FillVO(Control ctrl, Object obj)
    {
        if (ctrl.HasControls())
        {
            foreach (Control cCtrl in ctrl.Controls)
            {
                ParsingFillVO(cCtrl, obj);
                Control tmpCtrl = cCtrl;
                FillVO(tmpCtrl, obj);
            }
        }
        else
        {
            ParsingFillVO(ctrl, obj);
        }
    }

    private static void ParsingFillVO(Control ctrl, object obj)
    {
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        if (string.IsNullOrEmpty(ctrl.ID))
        {
            return;
        }

        ObjectWrapper ow = new ObjectWrapper(obj);

        if (ctrl is TextBox)
        {

            if (!ctrl.ID.StartsWith("txt"))
            {
                return;
            }

            TextBox txtCtrl = (TextBox)ctrl;

            string fieldName = ctrl.ID.Replace("txt", "");

            if (HasProperty(ow, fieldName))
            {
                Type propertyType = ow.GetPropertyType(fieldName);

                object setValue = txtCtrl.Text;

                if (!string.IsNullOrEmpty(setValue.ToString()))
                {
                    if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                    {
                        string date = txtCtrl.Text;
                        if (!string.IsNullOrEmpty(date))
                        {
                            date = date.Replace("-", "/");
                        }
                        setValue = ConvertUtil.ToDateTime(date);
                    }

                    log.Debug(string.Format("set {0}, {1} = {2}", ow.WrappedInstance.GetType().Name, fieldName, setValue));

                    ow.SetPropertyValue(fieldName, setValue);
                }
                else
                {
                    if (propertyType == typeof(Double))
                    {

                        ow.SetPropertyValue(fieldName, default(Double));
                    }
                    else if (propertyType == typeof(int))
                    {

                        ow.SetPropertyValue(fieldName, default(int));
                    }
                    else
                    {
                        ow.SetPropertyValue(fieldName, null);
                    }
                }
            }
        }
        else if (ctrl is ListControl)
        {
            ListControl lstCtrl = (ListControl)ctrl;

            string fieldName = GetFieldName(ctrl);

            if (HasProperty(ow, fieldName))
            {
                object value = lstCtrl.SelectedValue;
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    log.Debug(string.Format("set {0}, {1} = {2}", ow.WrappedInstance.GetType().Name, fieldName, value));
                    ow.SetPropertyValue(fieldName, value);
                }
            }

        }
        else if (ctrl is FCKeditor)
        {
            FCKeditor fckCtrl = (FCKeditor)ctrl;

            string fieldName = fckCtrl.ID.Replace("fck", "");

            if (HasProperty(ow, fieldName))
            {
                Type propertyType = ow.GetPropertyType(fieldName);

                object setValue = fckCtrl.Value;

                log.Debug(string.Format("set {0}, {1} = {2}", ow.WrappedInstance.GetType().Name, fieldName, setValue));

                ow.SetPropertyValue(fieldName, setValue);
            }
        }
    }


    /// <summary>
    /// 清除畫面txtbox的值
    /// </summary>
    /// <param name="ctrl"></param>
    public static void ClearUI(Control ctrl)
    {
        if (ctrl.HasControls())
        {
            foreach (Control cCtrl in ctrl.Controls)
            {
                Control tmpCtrl = cCtrl;
                ClearUI(tmpCtrl);
            }
        }
        else
        {
            if (ctrl is TextBox)
            {
                TextBox txtCtrl = (TextBox)ctrl;
                txtCtrl.Text = "";
            }
            else if (ctrl is CheckBox)
            {
                CheckBox ckCtrl = (CheckBox)ctrl;
                ckCtrl.Checked = false;
            }
        }
    }

    /// <summary>
    /// Disable CheckBox
    /// </summary>
    /// <param name="ctrl"></param>
    public static void DisableCheckBoxUI(Control ctrl)
    {
        if (ctrl.HasControls())
        {
            foreach (Control cCtrl in ctrl.Controls)
            {
                Control tmpCtrl = cCtrl;

                DisableCheckBoxUI(tmpCtrl);
            }
        }
        else
        {
            if (ctrl is CheckBox)
            {
                CheckBox ckbCtrl = (CheckBox)ctrl;
                ckbCtrl.Enabled = false;
            }
        }
    }
    /// <summary>
    /// Disable Button
    /// </summary>
    /// <param name="ctrl"></param>
    public static void DisableButtonUI(Control ctrl)
    {
        if (ctrl.HasControls())
        {
            foreach (Control cCtrl in ctrl.Controls)
            {
                Control tmpCtrl = cCtrl;
                DisableButtonUI(tmpCtrl);
            }
        }
        else
        {
            if (ctrl is Button)
            {
                Button txtCtrl = (Button)ctrl;
                txtCtrl.Enabled = false;
            }
        }
    }
    /// <summary>
    /// Disable TextBox
    /// </summary>
    /// <param name="ctrl"></param>
    public static void DisableTextboxUI(Control ctrl)
    {
        if (ctrl.HasControls())
        {
            foreach (Control cCtrl in ctrl.Controls)
            {
                Control tmpCtrl = cCtrl;
                DisableTextboxUI(tmpCtrl);
            }
        }
        else
        {
            if (ctrl is TextBox)
            {
                TextBox txtCtrl = (TextBox)ctrl;
                txtCtrl.Enabled = false;
            }
        }
    }
    /// <summary>
    /// Disable DropDownList 
    /// </summary>
    /// <param name="ctrl"></param>
    public static void DisableDropDownListUI(Control ctrl)
    {
        if (ctrl.HasControls())
        {
            foreach (Control cCtrl in ctrl.Controls)
            {
                Control tmpCtrl = cCtrl;
                DisableDropDownListUI(tmpCtrl);
            }
        }
        else
        {
            if (ctrl is DropDownList)
            {
                DropDownList ddlCtrl = (DropDownList)ctrl;
                ddlCtrl.Enabled = false;
            }
        }
    }
    /// <summary>
    /// Disable Radiobutton  
    /// </summary>
    /// <param name="ctrl"></param>
    public static void DisableRadioButtonListUI(Control ctrl)
    {
        if (ctrl.HasControls())
        {
            foreach (Control cCtrl in ctrl.Controls)
            {
                Control tmpCtrl = cCtrl;
                DisableRadioButtonListUI(tmpCtrl);
            }
        }
        else
        {
            if (ctrl is RadioButtonList)
            {
                RadioButtonList rdbCtrl = (RadioButtonList)ctrl;
                rdbCtrl.Enabled = false;
            }
        }
    }
    /// <summary>
    /// 將table中的textbox插入鍵盤事件
    /// </summary>
    /// <param name="gv"></param>
    public static void FillKeyInTable(GridView gv)
    {
        List<TextBox> textBoxList = new List<TextBox>();

        int coluntNum = gv.Columns.Count;
        int rowCount = gv.Rows.Count;


        List<int> visibleColunn = new List<int>();

        for (int i = 0; i < gv.Columns.Count; i++)
        {
            if (gv.Columns[i].Visible)
            {
                visibleColunn.Add(i);
            }
        }


        foreach (GridViewRow row in gv.Rows)
        {
            if (row.Visible)
            {
                foreach (int col in visibleColunn)
                {
                    ControlCollection c = row.Cells[col].Controls;

                    foreach (Control ctrl in c)
                    {
                        if (ctrl is TextBox)
                        {
                            textBoxList.Add((TextBox)ctrl);
                        }
                    }
                }
            }
        }

        //沒有TextBox 不處理
        if (textBoxList.Count == 0)
        {
            return;
        }


        for (int i = 0; i < textBoxList.Count; i++)
        {
            TextBox tx = textBoxList[i];
            tx.Attributes.Add("onkeydown", "doKeyDown(this)");

            int rowHasTxtnum = (textBoxList.Count / rowCount);

            //設定onfocus onblur

            tx.Attributes.Add("onFocus", "focuscnt(this)");
            tx.Attributes.Add("onBlur", "blurcnt(this)");
            //end 設定onfocus onblur

            //設定上下左右鍵

            //設定右鍵
            if (i == (textBoxList.Count - 1))
            {
                tx.Attributes.Add("nextid", textBoxList[0].ClientID);
            }
            else
            {
                tx.Attributes.Add("nextid", textBoxList[i + 1].ClientID);
            }

            //設定左鍵
            if (i == 0)
            {
                tx.Attributes.Add("preid", textBoxList[textBoxList.Count - 1].ClientID);
            }

            else
            {
                tx.Attributes.Add("preid", textBoxList[i - 1].ClientID);
            }

            //設定下鍵

            //最末列
            if (i >= ((rowCount - 1) * rowHasTxtnum) && i <= textBoxList.Count)
            {
                tx.Attributes.Add("downid", textBoxList[(i % rowHasTxtnum)].ClientID);
            }
            else
            {
                tx.Attributes.Add("downid", textBoxList[(int)(i + rowHasTxtnum)].ClientID);
            }

            //設定上鍵
            //第一列
            if (i < rowHasTxtnum)
            {
                tx.Attributes.Add("upid", textBoxList[(i + (rowHasTxtnum * (rowCount - 1)))].ClientID);
            }
            else
            {
                tx.Attributes.Add("upid", textBoxList[(int)(i - rowHasTxtnum)].ClientID);
            }
        }
    }
    #region private method

    private static string GetFieldName(Control ctrl)
    {
        if (ctrl is ListControl)
        {
            string id = ctrl.ID;

            foreach (string str in FILL_UIVO_LISTCONTROL_NOTE)
            {
                if (id.StartsWith(str))
                {
                    return id.Replace(str, "");
                }
            }
        }

        return ctrl.ID;
    }



    private static bool HasProperty(ObjectWrapper ow, string fieldName)
    {
        try
        {
            object value = ow.GetPropertyValue(fieldName);
            return true;
        }
        catch (InvalidPropertyException)
        {
            return false;
        }
    }

    #endregion




    public static GridView FindGridView(ref Control ctrl, string id)
    {
        return (((GridView)ctrl.FindControl(id)));
    }
    public static void SetTextBoxAttribute(TextBox txtBox, string attribute, string value)
    {

        txtBox.Attributes.Add(attribute, value);

    }
    /// <summary>
    /// 將IList做分頁顯示
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="AspNetPager1"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static IList<T> SetProducePage<T>(Wuqi.Webdiyer.AspNetPager AspNetPager1, IList<T> list)
    {
        AspNetPager1.RecordCount = list.Count;
        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);
        IList<T> result = new List<T>();
        if (list.Count - startIndex < maxRecord)
        {
            maxRecord = list.Count - startIndex;
        }

        for (int i = startIndex; i < (startIndex + maxRecord); i++)
        {
            result.Add(list[i]);
        }
        return result;
    }


    public static void SetLinkButtonText(ref Control ctrl, string id, string value)
    {
        ((LinkButton)ctrl.FindControl(id)).Text = value;
    }

    /// <summary>
    /// 設定圖片的Url
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="id"></param>
    /// <param name="imgUrl"></param>
    public static void ImageImgUrl(Control ctrl, string id, string imgUrl)
    {
        ((Image)ctrl.FindControl(id)).ImageUrl = imgUrl;
    }

    /// <summary>
    /// 從找到的Ctrl裡去設Literal Text
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="id"></param>
    /// <param name="value"></param>
    public static void SetLiteralText(Control ctrl, string id, string value)
    {
        ((Literal)ctrl.FindControl(id)).Text = value;
    }
}