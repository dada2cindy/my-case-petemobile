<%@ Page Title="首頁" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebUI._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>修改此範本，以快速建立您的 ASP.NET 應用程式。</h2>
            </hgroup>
            <p>
                若要深入了解 ASP.NET，請造訪 <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>。
                此頁面提供 <mark>視訊、教學課程及範例，</mark> 協助您充分利用 ASP.NET。
                如有 ASP.NET 的相關問題，請造訪<a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">我們的論壇</a>。
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>建議的動作如下:</h3>
    <ol class="round">
        <li class="one">
            <h5>開始使用</h5>
            ASP.NET Web Forms 可讓您使用熟悉的拖放式、事件驅動模型，建置動態網站。
            您可利用一個設計介面和數百個控制項和元件，迅速建置具備資料存取功能，且複雜而強大的 UI 驅動站台。
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">深入了解…</a>
        </li>
        <li class="two">
            <h5>新增 NuGet 套件，並快速建立您的程式碼</h5>
            NuGet 可讓您輕易安裝及更新免費的程式庫和工具。
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">深入了解…</a>
        </li>
        <li class="three">
            <h5>尋找虛擬主機</h5>
            您可輕易找到虛擬主機服務公司，為您提供適當的功能和價格。
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">深入了解…</a>
        </li>
    </ol>
</asp:Content>
