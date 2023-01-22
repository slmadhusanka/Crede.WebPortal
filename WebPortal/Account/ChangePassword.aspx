<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WebPortal.Account.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pagetitle">
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Account Settings</li>
                    <li class="breadcrumb-item">Update user information</li>
                  <li class="breadcrumb-item active">Change password</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
              <div class="row">
                <div class="col-lg-12">
                    <div class="card hdl-card">
                        <div class="card-header">
                            <asp:HyperLink CssClass="btn-circle btn btn-back hide" ID="HyperLink1" runat="server" NavigateUrl="~/Account/UpdateAccountSettings.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                            <h5 class="card-title">Change password</h5>
                        </div>
                        <div class="card-body">
                            <!--Success alert-->
                            <div class="alert alert-success alert-dismissable" id="success_alert" runat="server" visible="false">
                               
                                <strong>Success!</strong> <asp:Literal ID="SuccessMessage" runat="server"></asp:Literal>
                            </div>
                            <!--Error alerrt-->
                            <div class="alert alert-danger alert-dismissable" id="error_alert" runat="server" visible="false">
                               
                                <strong>Error!</strong> <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                            </div>

                            <div class="col-md-4">
                                <div class="row g-3">
                                <div class="col-md-12 form-group">
                                    <asp:Label ID="CurrentPasswordLabel" runat="server" CssClass="form-label" AssociatedControlID="CurrentPassword">Old password <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" Display="Dynamic" ControlToValidate="CurrentPassword"
                                            CssClass="failureNotification" ErrorMessage="*Password is required." ToolTip="Old password is required."
                                            ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>
                                <div class="col-md-12 form-group">
                                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword" CssClass="form-label">New password <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="NewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" Display="Dynamic" ControlToValidate="NewPassword"
                                            CssClass="failureNotification" ErrorMessage="*New password is required." ToolTip="New password is required."
                                            ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="NewPasswordRegularExpression" runat="server" Display="Dynamic"
                                            ErrorMessage="*Password must be at least 8 characters long and include at least one special character, one number, and one uppercase and lowercase letter."
                                            ValidationGroup="ChangeUserPasswordValidationGroup" ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).*$"
                                            ControlToValidate="NewPassword" CssClass="failureNotification">
                                        </asp:RegularExpressionValidator>
                                    
                                </div>
                                <div class="col-md-12 form-group">
                                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword" CssClass="form-label">Retype password <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                            CssClass="failureNotification" ErrorMessage="*Retype password is required." Display="Dynamic"
                                            ToolTip="Retype password is required." ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" Display="Dynamic"
                                            ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" ErrorMessage="*The confirm new password must match the 'New password' entry."
                                            ValidationGroup="ChangeUserPasswordValidationGroup"></asp:CompareValidator>
                                    
                                </div>
                            </div>
                                </div>
                            
                        </div>
                        <div class="card-footer">
                            <div class="pull-right">
                                <asp:LinkButton ID="DeleteUserButton" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="DeleteUserButton_Click">
                                    <i class="bi bi-x"></i> Cancel
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="ChangeUserPasswordValidationGroup" OnClick="UpdateUserButton_Click" CssClass="btn btn-primary btn-sm">
                                    <i class="bi bi-save"></i>&nbsp; Save
                                </asp:LinkButton>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
              </div>
            </section>
                
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="loading">
                Loading&#8230;</div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
