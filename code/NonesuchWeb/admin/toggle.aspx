﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="toggle.aspx.cs" Inherits="admin_toggle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript">
    var iniCols, noCols, o_mf, s;
    function init() {
        o_mf = window.parent.document.getElementById("mainFrameset");
        noCols = iniCols = o_mf.cols;
        if ((pos = noCols.indexOf(",")) != -1) {
            noCols = "0" + noCols.substring(pos);
        }
        s = false;
    }
    function change() {
        s = !s;
        if (s == true) {
            o_mf.cols = noCols;
            document.getElementById("menuSwitch").src = "images/arrow_right_2.gif";
        } else {
            o_mf.cols = iniCols;
            document.getElementById("menuSwitch").src = "images/arrow_left_2.gif";
        }
    }
    </script>

    <style type="text/css">
        html, body
        {
            background-color: #C7C4B1;
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            overflow: hidden;
            cursor: pointer;
        }
    </style>
</head>
<body onload="init();" onclick="change();">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <img align="middle" name="menuSwitch" id="menuSwitch" src="images/arrow_left_2.gif" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
