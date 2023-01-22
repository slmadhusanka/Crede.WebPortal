<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateProgramType.aspx.cs" Inherits="WebPortal.SystemPages.UpdateProgramType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pagetitle">
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Table Maintenance</li>
                  <li class="breadcrumb-item">Program Type</li>
                  <li class="breadcrumb-item active">Edit Program Type information</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
              <div class="row">
                <div class="col-lg-12">
            
            
            <div class="card hdl-card">
                <div class="card-header">
                    <asp:HyperLink CssClass="btn-circle btn btn-back hide" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfProgramType.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                    <h5 class="card-title">Edit Program Type information</h5>
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
                             <asp:Label ID="FacilityCodeLabel" runat="server" 
                                AssociatedControlID="lblProgramTypeCode" CssClass="form-label" Visible="false">Program Type Code:</asp:Label>
                            
                                <asp:Label ID="lblProgramTypeCode" runat="server" AssociatedControlID="txtProgramTypeDescription" Visible="false" ></asp:Label>
                            
                        </div>
                        <div class="col-md-12 form-group">
                            <asp:Label ID="lblDescription" runat="server" CssClass="form-label"
                                AssociatedControlID="txtProgramTypeDescription">Program type <span class="red-star">*</span> :</asp:Label>
                           
                                <asp:TextBox ID="txtProgramTypeDescription" runat="server" CssClass="form-control">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProgramTypeDescription"
                                    CssClass="failureNotification" ErrorMessage="*Program type is required." ToolTip="Program type is required."
                                    ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                            
                        </div>
                 
                        <div class="col-md-12 form-group">
                            <asp:Label ID="lblDescriptionShort" runat="server" CssClass="form-label" AssociatedControlID="txtDescriptionShort">Short description :</asp:Label>
                           
                                <asp:TextBox ID="txtDescriptionShort" runat="server" CssClass="form-control">
                                </asp:TextBox>
                             <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescriptionShort"
                                    CssClass="failureNotification" ErrorMessage="*Program type description is required." Display="Dynamic"
                                    ToolTip="Program type description is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>--%>
                           
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
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnCancel_Click">
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
