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

	public ConfigHelper()
	{
        Host = ConfigurationManager.AppSettings["Host"];
	}
}