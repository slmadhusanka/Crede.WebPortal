<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateClinic.aspx.cs" Inherits="WebPortal.SystemPages.UpdateFacilityM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pagetitle">  
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Clinic & Equipment</li>
                    <li class="breadcrumb-item">Clinic list</li>
                  <li class="breadcrumb-item active">Edit Clinic information</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
              <div class="row">
                <div class="col-lg-12">
            
            
                    <div class="card hdl-card">
                        <div class="card-header">
                            <asp:HyperLink CssClass="btn-circle btn btn-back hide" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfClinic.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                            <h5 class="card-title">Edit Clinic information</h5>
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
                                    <div class="form-group hide hide1">
                                        <asp:Label ID="FacilityCodeLabel" runat="server" CssClass="control-label col-xs-12 col-sm-4 col-lg-3" Visible="false">Clinic code:</asp:Label>
                                        <div class="col-xs-12 col-sm-7 col-lg-8">
                                            <asp:Label ID="lblFacilityCode" runat="server" AssociatedControlID="txtFacilityDetails" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-6 form-group">
                                        <asp:Label ID="FacilityDetailsLabel" runat="server" CssClass="form-label" AssociatedControlID="txtFacilityDetails" >Clinic <span class="red-star">*</span> :</asp:Label>
                                        <asp:TextBox ID="txtFacilityDetails" runat="server"  CssClass="form-control"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtFacilityDetails" Display="Dynamic"
                                                                                        CssClass="failureNotification" ErrorMessage="*Clinic is required." ToolTip="Clinic is required."
                                                                                        ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                       
                                    </div>
                                    
                                    <div class="col-md-6 form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="form-label" AssociatedControlID="ddlRegion">Region <span class="red-star">*</span> :</asp:Label>
                                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRegion"
                                                CssClass="failureNotification" ErrorMessage="*Region is required." ToolTip="Region is required." Display="Dynamic"
                                                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                        
                                    </div>
                                    <div class="col-md-6 form-group">
                                        <asp:Label ID="ProgramLabel" runat="server" CssClass="form-label" AssociatedControlID="ddlFacilityType">Clinic type <span class="red-star">*</span> :</asp:Label>
                                        <asp:DropDownList ID="ddlFacilityType" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="FacilityTypeRequired" runat="server" ControlToValidate="ddlFacilityType" Display="Dynamic"
                                                CssClass="failureNotification" ErrorMessage="*Clinic type is required." ToolTip="Clinic type is required."
                                                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                        
                                    </div>
                                    
                                    <div class="col-md-6 form-group">
                                        <asp:Label ID="DescriptionShortLabel" runat="server" CssClass="form-label" AssociatedControlID="txtDescriptionShort">Description short :</asp:Label>
                                        <asp:TextBox ID="txtDescriptionShort" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    
                                    <div class="col-md-12 form-group">
                                        <asp:Label ID="DescriptionLongLabel" runat="server" CssClass="form-label" AssociatedControlID="txtDescriptionLong">Description long :</asp:Label>
                                        <asp:TextBox ID="txtDescriptionLong" runat="server" CssClass="form-control" Columns="2" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                   
                                    <div class="col-md-12 form-group">
                                            <div class="checkbox">
                                                 <asp:CheckBox ID="chkIsActive" runat="server" />
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
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnCancel_Click">
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
