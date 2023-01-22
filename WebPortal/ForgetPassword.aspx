<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="WebPortal.ForgetPassword" %>

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
        function CloseFancyBox() {
            ResizeFancyBox();
            setTimeout(function () { parent.jQuery.fancybox.close(); }, 4000);
        }
        function ResizeFancyBox() {
            parent.$('.fancybox-content').css('height', $('#row').css('height'));
        }
        var client = new ClientJS();
        $(document).ready(function () {
            $('#hdnFingerPrint').val(client.getFingerprint());
        });
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
                                    
                                    <div class="col-md-12" id="divChangePassword" runat="server" visible="false">
                                        

                                        <div class="card hdl-card">
                                            <div class="card-header">
                                                <h5 class="card-title pull-left">
                                                    Change password
                                                </h5>
                                                <div class="pull-right">
                                                    <asp:LinkButton ID="lnkButtonLogin" runat="server" OnClick="lnkButtonLogin_Click" Visible="false" CssClass="btn btn-success">
                                                        <i class="fa fa-play" aria-hidden="true"></i> Click here to continue
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div class="card-body">
                                                <div class="alert alert-success alert-dismissable fade in" id="pwd_change_success" role="alert" runat="server">
                                                    <strong>Success !</strong> <asp:Literal ID="SuccessMessage" runat="server"></asp:Literal>
                                                </div>
                                                
                                                
                                                <div class="alert alert-danger alert-dismissable fade in" id="pwd_change_error" role="alert" runat="server">
                                                    <strong>Error !</strong> <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                                </div>
                    
                                                <div class="form form-horizontal">
                                                    <div class="form-group">
                                                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword" CssClass="form-label">New password <span class="red-star">*</span> :</asp:Label>
                                                        
                                                            <asp:TextBox placeholder="Enter your new password" ID="NewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                                                CssClass="failureNotification" ErrorMessage="* New password is required." ToolTip="New password is required."
                                                                ValidationGroup="ChangeUserPasswordValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="NewPasswordRegularExpression" runat="server"
                                                                ErrorMessage="password must be at least 8 characters long and include at least one special character, one number, and one uppercase and lowercase letter."
                                                                ValidationGroup="ChangeUserPasswordValidationGroup" ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).*$"
                                                                ControlToValidate="NewPassword" CssClass="failureNotification" Display="Dynamic"></asp:RegularExpressionValidator>
                                                       
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword" CssClass="form-label">Confirm new password <span class="red-star">*</span> :</asp:Label>
                                                        
                                                            <asp:TextBox placeholder="Confirm new password" ID="ConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                                                CssClass="failureNotification" ErrorMessage="* Confirm new password is required." Display="Dynamic"
                                                                ToolTip="Confirm new password is required." ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                                                ControlToValidate="ConfirmNewPassword" CssClass="failureNotification"   Display="Dynamic"
                                                                ErrorMessage="The Confirm new password must match the New password entry." ValidationGroup="ChangeUserPasswordValidationGroup"></asp:CompareValidator>
                                                      
                                                    </div>
                                                </div>  
                                            </div>
                                            <div class="card-footer">
                                                <div class="pull-left">
                                                     <asp:HyperLink CssClass="btn btn-secondary btn-sm" ID="back_to_login_2" runat="server" NavigateUrl="~/Login.aspx">
                                                         <i class="bi bi-arrow-left-short"></i>&nbsp;Back to login
                                                     </asp:HyperLink>
                                                </div>
                                                <div class="pull-right">
                                                   <asp:LinkButton ID="CancelPushButton" runat="server" CssClass="btn btn-secondary btn-sm hide" CommandName="Cancel" CausesValidation="false">
                                                         <i class="bi bi-x"></i> Cancel
                                                   </asp:LinkButton>
                                                    <asp:LinkButton ID="ChangePasswordPushButton" runat="server" ValidationGroup="ChangeUserPasswordValidationGroup" CommandName="ChangePassword" OnClick="ChangePasswordPushButton_Click" CssClass="btn btn-primary btn-sm">
                                                       <i class="bi bi-check2"></i> Change password
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                        
                                            </div>
                </div>
                
                <div class="col-md-12" id="divSendPassword" runat="server">
                    
                    <div class="card hdl-card">
                        <div class="card-header">
                            <h5 class="card-title">
                                Forgot password
                            </h5>
                        </div>
                        <div class="card-body">
                            <div class="alert alert-success alert-dismissable fade in" id="pwd_send_success" runat="server" visible="false">
                                <strong>Success !</strong> <asp:Literal ID="success_text" runat="server"></asp:Literal>
                            </div>
                            <div class="alert alert-danger alert-dismissable fade in" id="pwd_send_error" runat="server" visible="false">
                                <strong>Error !</strong> <asp:Literal runat="server" ID="error_text"></asp:Literal>
                            </div>
                            <p>If you have forgotten your password, then please enter your Username in the box below and click the "Submit" button. Password reset link will be emailed to you. </p>
                            
                                <div class="form-group has-feedback">
                                    <asp:Label ID="lblUserid" runat="server" AssociatedControlID="txtUserid" CssClass="form-label">Username <span class="red-star">*</span> :</asp:Label>
                                    <asp:TextBox placeholder="Enter your username" ID="txtUserid" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserid"
                                        ErrorMessage="Username is required." ToolTip="Username is required." CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroups"
                                        ForeColor="Red">Username is required.</asp:RequiredFieldValidator>
                                </div>
                            			                
                        </div>
                        <div class="card-footer">
                            <div class="pull-left">
                                <asp:HyperLink CssClass="btn btn-secondary btn-sm" ID="back_to_login" runat="server" NavigateUrl="~/Login.aspx">
                                    <i class="bi bi-arrow-left-short"></i>&nbsp;Back to login
                                </asp:HyperLink>
                                                            
                            </div>
                            <div class="pull-right">
                                <asp:LinkButton ID="LnkResetPassword" runat="server" CssClass="btn btn-primary btn-sm" OnClick="LnkResetPassword_Click" ValidationGroup="RegisterUserValidationGroups">
                                    <i class="bi bi-send"></i> Submit
                                </asp:LinkButton>
                            </div>
                            
                        </div>
                    </div>
                    
                    
                </div>
                <div class="text-center col">
                    <a style="padding-top: 20px;" href="http://credetechnologies.com/" target="_blank"> Crede Technologies Inc.</a> © <%=DateTime.Now.Year %> v <%=ConfigurationManager.AppSettings["VersionNumber"].ToString()%> 
                </div>
            </div>
              <asp:HiddenField ID="hdnFingerPrint" runat="server" ClientIDMode="Static" />
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="width: 600px;" class="container hide">
                <div class="nobackground col text-center">
                    <div class="loading">Loading&#8230;</div>
                </div>
            </div>
            
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
