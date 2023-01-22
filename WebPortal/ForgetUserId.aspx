<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetUserId.aspx.cs" Inherits="WebPortal.ForgetUserId" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="shortcut icon" href="~/images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/bootstrap-icons/bootstrap-icons.css")%>">
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
        function closeMe() {
            window.location = "Login.aspx";
        }
        function CloseFancyBox() {
            ResizeFancyBox();
            setTimeout(function () { parent.jQuery.fancybox.close(); }, 4000);
        }
        function ResizeFancyBox() {
            parent.$('.fancybox-content').css('height', $('#row').css('height'));
        }
    </script>
</head>
<body class="password-reset" onkeydown = "return (event.keyCode!=13)">
    <form id="form1" runat="server" AutoPostBack="false" autocomplete="off">
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
        <section style="padding-top: 0 !important;" class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-5">
        <div style="max-width: 400px;" class="container" id="row">
            <div style="margin-bottom: 10px;" class="text-center col">  
            <span style="width: 100%;text-align: center; margin-bottom: 10px;">
                <asp:Image ID="Image3" Width="230px" CssClass="img-responsive"  runat="server" ImageUrl="~/images/login-logo.png"/>
            </span>
            </div>

            
            
            
             <div class="row justify-content-md-center">
                <!-- Main Form -->
                 
                 <div class="card hdl-card">
                         <div class="card-header">
                             <h5 class="card-title pull-left">Forgot Username</h5>
                         </div>
                     <div class="card-body">
                         <div class="alert alert-success alert-dismissable fade in" id="email_success" runat="server" visible="false">
                                                
                             <strong>Success !</strong> <asp:Literal ID="success_text" runat="server" /></asp:Literal>
                         </div>
                         <div class="alert alert-danger alert-dismissable fade in" id="email_error" runat="server" visible="false">
                            
                             <strong>Error !</strong> <asp:Literal ID="error_text" runat="server" />
                         </div>
                         <p>If you have forgotten your Username, then please enter your email address in the box below and click "Submit" button. Your Username will be emailed to you. </p>
                         <div class="form-group has-feedback">
                             <label for="txtEmailId" class="form-label">Email address <span class="red-star">*</span> :</label>
                             <asp:TextBox ID="txtEmailId" runat="server"  placeholder="Email address" CssClass="form-control foget-id-textbox"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV" runat="server" ControlToValidate="txtEmailId"
                                 CssClass="failureNotification" ErrorMessage="*Email address is required." Display="Dynamic"
                                 ToolTip="Email address is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ErrorMessage="*Invalid email Address"
                                 ValidationGroup="RegisterUserValidationGroup" ValidationExpression=".+@.+" ControlToValidate="txtEmailId"
                                 CssClass="failureNotification" Display="Dynamic"></asp:RegularExpressionValidator>
                         </div>
                         
                         
                         
                     </div>
                     <div class="card-footer">
                         <div class="pull-left">
                             <asp:HyperLink CssClass="btn btn-secondary btn-sm" ID="back_to_login_3" runat="server" NavigateUrl="~/Login.aspx">
                                 <i class="bi bi-arrow-left-short"></i>&nbsp;Back to login
                             </asp:HyperLink>
                                                         
                         </div>
                         <div class="pull-right">
                              <asp:LinkButton ID="LnkResetPassword" runat="server" CssClass="btn btn-primary btn-sm" ValidationGroup="RegisterUserValidationGroup" OnClick="LnkResetPassword_Click">
                                   <i class="bi bi-send"></i> Submit
                              </asp:LinkButton>
                         </div>
                        
                     </div>
                </div>




                    
            </div>
        </div>
        </section>
        </ContentTemplate>
        </asp:UpdatePanel>
         <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div class="nobackground hide">
                    <div class="loading">Loading&#8230;</div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>
