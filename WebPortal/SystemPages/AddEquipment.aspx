<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEquipment.aspx.cs" Inherits="WebPortal.SystemPages.AddUnitM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="pagetitle">    
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Clinic & Equipment</li>
                    <li class="breadcrumb-item">Equipment</li>
                  <li class="breadcrumb-item active">Add Equipment Information</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
              <div class="row">
                <div class="col-lg-12">
            
                    <div class="card hdl-card">
                        <div class="card-header">
                            <asp:HyperLink CssClass="btn btn-primary btn-sm hide" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfEquipment.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                            <h5 class="card-title">Add Equipment Information</h5>
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
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="UnitNameLabel" runat="server" AssociatedControlID="txtUnitName" CssClass="form-label">Equipment name<span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="txtUnitName" runat="server" CssClass=" form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUnitName" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="*Equipment name is required." ToolTip="Equipment name is required."
                                            ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="lblUnitType1" runat="server" AssociatedControlID="ddlUnitType1" CssClass="form-label">Equipment type <span class="red-star">*</span> :</asp:Label>
                                   
                                        <asp:DropDownList ID="ddlUnitType1" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvUnitType1" runat="server" ControlToValidate="ddlUnitType1" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="*Equipment type  is required." ToolTip="Equipment type  is required."
                                            ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>






                                <div class="col-md-6 form-group hide" >
                                    <asp:Label ID="lblUnitType2" runat="server" AssociatedControlID="ddlUnitType2" CssClass="form-label">Unit type 2 <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:DropDownList ID="ddlUnitType2" runat="server" CssClass="form-control"></asp:DropDownList>      
                                        <%-- <asp:RequiredFieldValidator ID="rfvUnitType2" runat="server" ControlToValidate="ddlUnitType2" Display="Dynamic" --%>
                                        <%--     CssClass="failureNotification" ErrorMessage="*Unit type 2 is required." ToolTip="Unit type 2 is required." --%>
                                        <%--     ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator> --%>
                                    
                                </div>
                                <div class="col-md-6 form-group hide">
                                    <asp:Label ID="ProgramLabel" runat="server" AssociatedControlID="ddlProgramme" CssClass="form-label">Program <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RoleRequired" runat="server" ControlToValidate="ddlProgramme" Display="Dynamic" --%>
                                        <%--     CssClass="failureNotification" ErrorMessage="*Program is required." ToolTip="Program is required." --%>
                                        <%--     ValidationGroup="RegisterUserValidationGroup"> </asp:RequiredFieldValidator> --%>
                                    
                                </div>
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="FacilityLabel" runat="server" AssociatedControlID="ddlFacility" CssClass="form-label">Clinic <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:DropDownList ID="ddlFacility" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFacility" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="*Clinic name is required." ToolTip="Clinic name is required."
                                            ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>
                           
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="lblunitdescriptionshort" runat="server" AssociatedControlID="txtunitdescriptionshort" CssClass="form-label">Equipment description short <span class="red-star"></span> :</asp:Label>
                                    
                                        <asp:TextBox ID="txtunitdescriptionshort" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvunitcodesortdesc" runat="server" ControlToValidate="txtunitdescriptionshort"
                                            CssClass="failureNotification" ErrorMessage="*Equipment description short is required." Display="Dynamic"
                                            ToolTip="Equipment description short is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>--%>
                                    
                                </div>
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="lblunitdescriptionlong" runat="server" AssociatedControlID="txtunitdescriptionlong" CssClass="form-label">Equipment description long <span class="red-star"></span> :</asp:Label>
                                    
                                        <asp:TextBox ID="txtunitdescriptionlong" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                       <%-- <asp:RequiredFieldValidator ID="rfvunitcodedesc" runat="server" ControlToValidate="txtunitdescriptionlong"
                                            CssClass="failureNotification" ErrorMessage="*Equipment description long is required." Display="Dynamic"
                                            ToolTip="Equipment description long is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>--%>
                                    
                                </div>
                                <div class="col-md-6 form-group hide hide1">
                                    <asp:Label ID="Label2" runat="server" CssClass="form-label" AssociatedControlID="txtOrderID">Order id:</asp:Label>
                                    
                                        <asp:TextBox ID="txtOrderID" runat="server" CssClass="textEntry">
                                        </asp:TextBox>
                                </div>
                                 <%-- <div class="col-md-6 form-group">
                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtbeds" CssClass="form-label">Approximate number of beds <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="txtbeds" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbeds"
                                            CssClass="failureNotification" ErrorMessage="*Equipment description short is required." Display="Dynamic"
                                            ToolTip="Approximate number of beds is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>--%>
                                <div class="col-md-6 form-group">
                                        <div class="checkbox">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                                            <asp:Label ID="Label8" runat="server" AssociatedControlID="chkIsActive">
                                                Active
                                            </asp:Label>
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
                        </div>
                    </div>
            
            
            
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="loading">
                Loading&#8230;</div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
