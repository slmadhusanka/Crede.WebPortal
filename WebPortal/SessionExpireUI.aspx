<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpireUI.aspx.cs" Inherits="WebPortal.SessionExpireUI" %>

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
        <%: Styles.Render("~/bundles/jquery_confirm_css") %>
        <%: Styles.Render("~/bundles/site_css") %>
        <%: Styles.Render("~/bundles/default_css") %>

        <%: Scripts.Render("~/bundles/bootstrap_js") %>
        <%: Scripts.Render("~/bundles/jquery_ui_js") %>
        <%: Scripts.Render("~/bundles/fancybox_js") %>
        <%: Scripts.Render("~/bundles/jquery_confirm_js") %>
        <%: Scripts.Render("~/bundles/jquery_validate_js") %>

        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/clientjs@0.1.11/dist/client.min.js"></script>
    </asp:PlaceHolder>
</head>
<body onkeydown = "return (event.keyCode!=13)">
<div  class="container">
    <form id="form1" runat="server">
        
        <main>
            <div class="container">
        
              <section class="section error-404 min-vh-100 d-flex flex-column align-items-center justify-content-center">
                 <span style="width: 100%;text-align: center;"><asp:Image ID="Image3" CssClass="img-responsive"  runat="server" ImageUrl="~/images/time-out.png"/></span>
                <h2>Your session has expired.</h2>
                 <asp:LinkButton CssClass="btn" ID="LinkButton1" runat="server" OnClick="LnkLogin_Click"><b>Back to login</b>&nbsp;</asp:LinkButton>
                
                <div class="credits">
                  
                  <strong> </strong> 
                                 
                    <br />       
                    <br />   
                    <br />   
                    
                   <a href="http://credetechnologies.com/" target="_blank"> Crede Technologies Inc.</a> © <%=DateTime.Now.Year %> v <%=ConfigurationManager.AppSettings["VersionNumber"].ToString()%> 
                    
                    
                </div>
              </section>
        
            </div>
          </main><!-- End #main -->
        
        
        
        
        
        
        
        
        
        
    </form>
</div>
</body>
</html>
