using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// ConfigHelper 的摘要描述
/// </summary>
public class ConfigHelper
{
    /// <summary>
    /// 網址
    /// </summary>
    public string Host { get; set; }

    public string ApiUrl { get; set; }

    public bool ShowFranchiseesCommission { get; set; }

    public bool OnlyAdminCreate { get; set; }

    public string MemberApiUrl
    {
        get
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                return "";
            }
            else
            {
                return string.Format("{0}/member", ApiUrl);
            }            
        }
    }

    public string PostApiUrl
    {
        get
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                return "";
            }
            else
            {
                return string.Format("{0}/post", ApiUrl);
            }
        }
    }

    public string PostFileApiUrl
    {
        get
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                return "";
            }
            else
            {
                return string.Format("{0}/postfile", ApiUrl);
            }
        }
    }

    public ConfigHelper()
	{
        Host = ConfigurationManager.AppSettings["Host"];
        ApiUrl= ConfigurationManager.AppSettings["ApiUrl"];
        ShowFranchiseesCommission = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowFranchiseesCommission"]);
        OnlyAdminCreate = Convert.ToBoolean(ConfigurationManager.AppSettings["OnlyAdminCreate"]);
    }
}