<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CKEditorForMulti.ascx.cs" Inherits="admin_UserControl_CKEditorForMulti" %>
<asp:TextBox ID="mckeditor" runat="server" TextMode="MultiLine" CssClass="table01"></asp:TextBox>

<script type="text/javascript">



    CKEDITOR.replace('<%=mckeditor.ClientID %>',

                            {
                                skin: 'office2003',
                                enterMode: Number(2),
                                shiftEnterMode: Number(1),
                                height: '200',
                                //toolbar : 'Basic',
                                filebrowserBrowseUrl: '<%=ResolveUrl("../ckfinder/ckfinder.html")%>',
                                filebrowserImageBrowseUrl: '<%=ResolveUrl("../ckfinder/ckfinder.html?Type=Images")%>',
                                filebrowserFlashBrowseUrl: '<%=ResolveUrl("../ckfinder/ckfinder.html?Type=Flash")%>',
                                filebrowserUploadUrl: '<%=ResolveUrl("../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files")%>',
                                filebrowserImageUploadUrl: '<%=ResolveUrl("../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images")%>',
                                filebrowserFlashUploadUrl: '<%=ResolveUrl("../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash")%>'
                            });


</script>