<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateProgram.aspx.cs" Inherits="WebPortal.SystemPages.UpdateProgram" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pagetitle">
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Table Maintenance</li>
                  <li class="breadcrumb-item">Programs</li>
                  <li class="breadcrumb-item active">Edit Program Information</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
                  <div class="row">
                    <div class="col-lg-12">
            
            
            
            <div class="card hdl-card">
                <div class="card-header">
                    <asp:HyperLink CssClass="btn-circle btn btn-back hide" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfProgram.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                    <h5 class="card-title">Edit Program Information</h5>
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
                            <div class="form-group hide hide1">
                                <asp:Label ID="ProgramCodeLabel" runat="server" CssClass="control-label" AssociatedControlID="lblProgramCode" Visible="false">Program Code:</asp:Label>
                                <div class="col-xs-12 col-sm-7 col-lg-8">
                                    <asp:Label ID="lblProgramCode" runat="server" AssociatedControlID="txtProgramDetails" Visible="false"></asp:Label>
                                </div>
                            </div>
                             <div class="col-md-12 form-group">
                                <asp:Label ID="ProgramDetailsLabel" runat="server" CssClass="control-label" AssociatedControlID="txtProgramDetails">Program 
                                    <span class="red-star">*</span> :</asp:Label>
                                <asp:TextBox ID="txtProgramDetails" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtProgramDetails" Display="Dynamic"
                                        CssClass="failureNotification" ErrorMessage="*Program description is required." ToolTip="Program description is required."
                                        ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                               
                            </div>
                            <div class="col-md-12 form-group">
                                    <asp:Label ID="ProgramLabel" runat="server" AssociatedControlID="ddlProgrammeType" CssClass="control-label">Program type 
                                        <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:DropDownList ID="ddlProgrammeType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ProgramTypeRequired" runat="server" ControlToValidate="ddlProgrammeType" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="*Program type is required." ToolTip="Program type is required."
                                            ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>
                            <div class="col-md-12 form-group">
                                <asp:Label ID="lblDescriptionShort" runat="server" AssociatedControlID="txtDescriptionShort" CssClass="form-label">Description 
                                    <span class="red-star">*</span> :</asp:Label>
                                <asp:TextBox ID="txtDescriptionShort" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescriptionShort"
                                        CssClass="failureNotification" ErrorMessage="*Program Description is required." Display="Dynamic"
                                        ToolTip="Program description is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                               
                            </div>
                            <div class="col-md-12 form-group">
                                    <div class="checkbox">
                                        <asp:Label ID="Label6" runat="server" AssociatedControlID="chkIsActive">
                                            <asp:CheckBox ID="chkIsActive" runat="server" /> &nbsp;Active
                                        </asp:Label>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <div class="pull-right">
                        <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnRefresh_Click">
                            <i class="bi bi-x"></i> Cancel
                        </asp:LinkButton>
                        <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="RegisterUserValidationGroup" OnClick="UpdateUserButton_Click" CssClass="btn btn-primary btn-sm">
                            <i class="bi bi-save"></i> Save
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
