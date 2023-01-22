<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateAccountSettings.aspx.cs" Inherits="WebPortal.Account.UpdateAccountSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pagetitle">
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Account Settings</li>
                  <li class="breadcrumb-item active">Update user information</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
              <div class="row">
                <div class="col-lg-12">
            
            
             <div class="card hdl-card">
                <div class="card-header">
                    <div class="pull-left">
                        <h5 class="card-title">Update user information</h5>
                    </div>
                    <div class="pull-right mobile-right-set">
                        <asp:LinkButton ID="btnGo" runat="server" OnClick="btnGo_Click" CssClass="btn btn-info btn-sm all-user">
                            <i class="bi bi-people"></i> View all users
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton style="margin-right: 5px !important;color: #333;" ID="btnChangePassword" runat="server" OnClick="btnChangePassword_Click" CssClass="btn btn-warning btn-sm">
                            <i class="bi bi-lock"></i> Change my password
                        </asp:LinkButton>
                    </div>
                   
                </div>
                <div class="card-body">
                    <!--Success alert-->
                    <div class="alert alert-success alert-dismissable fade in" id="success_alert" runat="server" visible="false">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>Success!</strong> <asp:Literal ID="SuccessMessage" runat="server"></asp:Literal>
                    </div>
                    <!--Error alerrt-->
                    <div class="alert alert-danger alert-dismissable fade in" id="error_alert" runat="server" visible="false">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>Error!</strong> <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </div>

                    <div class="col-md-4">
                        <div class="row g-3">
                        <div class="col-md-12 form-group">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName" CssClass="form-label">User name <span class="red-star">*</span> :</asp:Label>
                            
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName" Display="Dynamic"
                                    CssClass="failureNotification" ErrorMessage="*User name is required." ToolTip="User name is required."
                                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                            
                        </div>
                        <div class="col-md-12 form-group">
                            <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtUserFirstName" CssClass="form-label">First name <span class="red-star">*</span> :</asp:Label>
                            
                                <asp:TextBox ID="txtUserFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstNameRequired" runat="server" ControlToValidate="txtUserFirstName" Display="Dynamic"
                                    CssClass="failureNotification" ErrorMessage="*First name is required." ToolTip="First name is required."
                                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                            
                        </div>
                    
                        <div class="col-md-12 form-group">
                            <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtUserLastName" CssClass="form-label">Last name <span class="red-star">*</span> :</asp:Label>
                            
                                <asp:TextBox ID="txtUserLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLastNameRequired" runat="server" ControlToValidate="txtUserLastName" Display="Dynamic"
                                    CssClass="failureNotification" ErrorMessage="*Last name is required." ToolTip="Last name is required."
                                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                            
                        </div>
                        <div class="col-md-12 form-group">
                            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="form-label">E-mail <span class="red-star">*</span> :</asp:Label>
                            
                                <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                    CssClass="failureNotification" ErrorMessage="*Email is required." Display="Dynamic" ToolTip="E-mail is required."
                                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ErrorMessage="Invalid Email" Display="Dynamic"
                                    ValidationGroup="RegisterUserValidationGroup" ValidationExpression=".+@.+" ControlToValidate="Email"
                                    CssClass="failureNotification"></asp:RegularExpressionValidator>
                            
                        </div>
                        <div class="col-md-12 form-group hide hide1">
                            <asp:Label ID="RoleLabel" runat="server" AssociatedControlID="ddlRoleName" CssClass="form-label" Visible="false">Role <span class="red-star">*</span> :</asp:Label>
                            
                                <asp:DropDownList ID="ddlRoleName" runat="server" Enabled="false" CssClass="form-control" Visible="false">
                                </asp:DropDownList>
                            
                        </div>
                        <div class="col-md-12 form-group hide hide1">
                            <asp:Label ID="FacilityLabel" Visible="true" runat="server" CssClass="form-label" AssociatedControlID="ddlFacility" Enabled="false" >Clinic:</asp:Label>
                            
                                <asp:DropDownList Visible="true" ID="ddlFacility" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged">
                                </asp:DropDownList>
                            
                        </div>
                        <div class="col-md-12 form-group hide hide1">
                            <asp:Label ID="Label1" runat="server" CssClass="control-label col-xs-12 col-sm-4 col-lg-3" AssociatedControlID="ddlUnit">Unit:</asp:Label>
                            
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            
                        </div>
                    </div>
                        </div>
                </div>
                <div class="card-footer">
                    <div class="pull-right">
                          <asp:LinkButton ID="DeleteUserButton" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="DeleteUserButton_Click">
                                    <i class="bi bi-x"></i> Cancel
                                </asp:LinkButton>
                        <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="RegisterUserValidationGroup" OnClick="UpdateUserButton_Click" CssClass="btn btn-primary btn-sm">
                            <i class="bi bi-save"></i> Save
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
            <div class="loading">Loading&#8230;</div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
