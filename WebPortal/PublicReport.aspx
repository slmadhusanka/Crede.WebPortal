<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicReport.aspx.cs" Inherits="WebPortal.PublicReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="shortcut icon" href="~/images/favicon.ico" type="image/x-icon" />
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/bootstrap_css") %>
        <%: Styles.Render("~/bundles/jquery_ui_css") %>
        <%: Styles.Render("~/bundles/fontawesome") %>
        <!--Google fonts-->
        <link href="https://fonts.googleapis.com/css?family=Titillium+Web:200,200i,300,300i,400,400i,600,600i,700,700i,900" rel="stylesheet" />
        <%: Styles.Render("~/bundles/fancybox_css") %>
        <%: Styles.Render("~/bundles/site_css") %>
        <%: Styles.Render("~/bundles/default_css") %>

        <%: Scripts.Render("~/bundles/bootstrap_js") %>
        <%: Scripts.Render("~/bundles/jquery_ui_js") %>
        <%: Scripts.Render("~/bundles/fancybox_js") %>
        <%: Scripts.Render("~/bundles/jquery_validate_js") %>
    </asp:PlaceHolder>
</head>
<body onkeydown = "return (event.keyCode!=13)">
    <form id="form1" runat="server">
        <header>
        <div class="container-fluid nopadding">
            <div class="col-xs-12 col-sm-12 col-lg-12 hd-bg" style="background: #eee;margin-bottom: 15px;">
                <div class="col-xs-12 col-sm-6 col-lg-6">
                    <a id="A2" runat="server" href="Login.aspx">
                        <div class="site-logo">
                            <asp:Image Width="80px" ImageUrl="~/images/logo_header.png" AlternateText="Company Logo" runat="server" ID="Image1" />
                        </div>
                        <div class="site-name"><p><b>Wentworth-Halton Clinic Portal</b></p></div>
                    </a>
                </div>
                <div class="col-xs-12 col-sm-6 col-lg-6 pull-right">
                    <div class="logout-text">
                        <asp:HyperLink ID="hp_userAccount" runat="server" NavigateUrl="~/Login.aspx" CssClass="report-login">
                            <i class="fa fa-sign-in" aria-hidden="true"></i> Login
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Public Wentworth-Halton reports</h3>
                    </div>
                    <div class="panel-body">
                        <div class="report-top-text">
                            Click on the <strong>description</strong> to view report
                        </div>
                        <div class="table-responsive">
                            <table id="reportTable" class="table table-bordered table-striped table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            Report No:
                                        </th>
                                        <th>
                                            Report description
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvReportCategory" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                   <%= GetRowNumber()%>
                                                </td>
                                                <td>
                                                    <a href="ReportViewer/ReportContainer.aspx?ReportCategory=Public&ReportCode=<%# Eval("ReportCode") %>&ReportName=<%# Eval("ReportName") %>">
                                                        <%# Eval("ReportDescription")%>
                                                    </a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--footer start-->
    <div class="navbar navbar-default navbar-fixed-bottom footer">
        <div class="container-fluid">
            <center><p class="navbar-text text-center" style="width:100%">
                <a href="http://credetechnologies.com/" target="_blank"> Crede Technologies Inc.</a> © <%=DateTime.Now.Year.ToString() %> v <%=ConfigurationManager.AppSettings["VersionNumber"].ToString()%>
            </p></center>
        </div>
    </div>
    <!--footer end-->
    </form>
</body>
</html>
