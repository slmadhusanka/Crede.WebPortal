<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClinic.aspx.cs" Inherits="WebPortal.SystemPages.AddFacilityM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="pagetitle">
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Clinic & Equipment</li>
                    <li class="breadcrumb-item">Clinic</li>
                  <li class="breadcrumb-item active">Add Clinic Information</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
                      <div class="row">
                        <div class="col-lg-12">
            
            <div class="card hdl-card">
                <div class="card-header">
                    <asp:HyperLink CssClass="btn-circle btn btn-back hide" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfClinic.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                    <h5 class="card-title">Add Clinic Information</h5>
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
                    <div class="col-md-8">
                        <div class="row g-3">
                            <div class="col-md-6 form-group required">
                                <asp:Label ID="FacilityDetailsLabel" runat="server" AssociatedControlID="txtFacilityDetails" CssClass="form-label">Clinic <span class="red-star">*</span> :</asp:Label>
                                <asp:TextBox ID="txtFacilityDetails" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtFacilityDetails" Display="Dynamic"
                                                                    CssClass="failureNotification" ErrorMessage="*Clinic is required." ToolTip="Clinic name is required."
                                                                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                
                            </div>
                            
                            <div class="col-md-6 form-group required">
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="ddlRegion" CssClass="form-label">Region <span class="red-star">*</span> :</asp:Label>
                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRegion"
                                                                        CssClass="failureNotification" ErrorMessage="*Region is required." ToolTip="Region is required." Display="Dynamic"
                                                                        ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                            </div>
                            
                            <div class="col-md-6 form-group required">
                                <asp:Label ID="ProgramLabel" runat="server" AssociatedControlID="ddlFacilityType" CssClass="form-label">Clinic type <span class="red-star">*</span> :</asp:Label>
                                <asp:DropDownList ID="ddlFacilityType" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="FacilityTypeRequired" runat="server" ControlToValidate="ddlFacilityType" Display="Dynamic"
                                                                        CssClass="failureNotification" ErrorMessage="*Clinic type is required." ToolTip="Clinic type is required."
                                                                        ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                
                            </div>
                            
                            <div class="col-md-6 form-group">
                                <asp:Label ID="DescriptionShortLabel" runat="server" AssociatedControlID="txtDescriptionShort" CssClass="form-label">Description short :</asp:Label>
                                <asp:TextBox ID="txtDescriptionShort" runat="server" CssClass="form-control"></asp:TextBox>
                                
                            </div>
                            
                            <div class="col-md-12 form-group">
                                <asp:Label ID="DescriptionLongLabel" runat="server" AssociatedControlID="txtDescriptionLong" CssClass="form-label">Description long :</asp:Label>
                                <asp:TextBox ID="txtDescriptionLong" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                                
                            </div>
                                    <div class="col-md-12 form-group">
                                        <div class="checkbox">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true"/>
                                            <asp:Label ID="Label8" runat="server" AssociatedControlID="chkIsActive">
                                                Active Clinic
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
                            <i class="bi bi-save"></i> &nbsp; Save
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
