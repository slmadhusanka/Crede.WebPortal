<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddErrorCategory.aspx.cs" Inherits="WebPortal.SystemPages.AddErrorCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pagetitle">
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Table Maintenance</li>
                  <li class="breadcrumb-item active">Error Category </li>
                  <li class="breadcrumb-item active">Add Error Category</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
              <div class="row">
                <div class="col-lg-12">
            
            <div class="card hdl-card">
                <div class="card-header">
                    <asp:HyperLink CssClass="btn-circle btn btn-back hide" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfErrorCategory.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                    <h5 class="card-title">Add Error Category</h5>
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
                            <asp:Label ID="lblregionDesc" runat="server" AssociatedControlID="txtRegionDesc" CssClass="form-label">Error Category <span class="red-star">*</span> :</asp:Label>
                            
                                <asp:TextBox ID="txtRegionDesc" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegionDesc"
                                    CssClass="failureNotification" ErrorMessage="*Region is required." Display="Dynamic"
                                    ToolTip="Region is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                           
                        </div>
                    
                       
                        <div class="col-md-12 form-group">
                            
                                <div class="checkbox">
                                    <asp:Label ID="Label8" runat="server" AssociatedControlID="chkIsActive">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="true"/> &nbsp;Active  
                                    </asp:Label>
                                </div>
                        </div>
                        </div>
                        </div>
                </div>
                <div class="card-footer">
                    <div class="pull-right">
                        <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" PostBackUrl="~/SystemPages/ListOfRegion.aspx">
                            <i class="bi bi-x"></i> Cancel
                        </asp:LinkButton>
                        <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="RegisterUserValidationGroup" OnClick="UpdateRegionButton_Click" CssClass="btn btn-primary btn-sm">
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