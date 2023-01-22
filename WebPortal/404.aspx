<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="WebPortal._404" %>

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
    <script type="text/javascript">
        function goBack() {
            window.history.back();
        }
    </script>
</head>
<body class="404-page-style">
    <form id="form1" runat="server">
        
        <main>
            <div class="container">
        
              <section class="section error-404 min-vh-100 d-flex flex-column align-items-center justify-content-center">
                <h1>404</h1>
                <h2>404 Page Not Found</h2>
                  
                
                  <a href="#" onclick="goBack()" class="btn">
                                                  <i class="fa fa-arrow-left" aria-hidden="true"></i> Go back
                                              </a>
                  <strong> </strong> 
                <div style="padding-top: 50px;" class="credits">
                  
                    <asp:HyperLink CssClass="linkLogout" runat="server" ID="HyperLink2" NavigateUrl="http://credetechnologies.com/" Target="_blank">Crede Technologies Inc.</asp:HyperLink> &copy; <%= DateTime.Now.Year %>
                                v <%=ConfigurationManager.AppSettings["VersionNumber"].ToString()%> 
                </div>
              </section>
        
            </div>
          </main><!-- End #main -->
        
        
        
    </form>
</body>
</html>
