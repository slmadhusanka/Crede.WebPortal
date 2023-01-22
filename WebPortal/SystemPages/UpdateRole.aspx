<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateRole.aspx.cs" Inherits="WebPortal.SystemPages.UpdateRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        
        <ContentTemplate>
            <div class="pagetitle">
              <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">Home</li>
                    <li class="breadcrumb-item">System Configuration</li>
                    <li class="breadcrumb-item">Roles</li>
                  <li class="breadcrumb-item active">Edit Role Information</li>
                </ol>
              </nav>
            </div>
        <!-- End Page Title --> 
        <section class="section">
          <div class="row">
            <div class="col-lg-12">
            <div class="card hdl-card">
                <div class="card-header">
                    <asp:HyperLink CssClass="btn-circle btn btn-back hide" ID="HyperLink1" runat="server" NavigateUrl="~/Account/ListOfUser.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                    <h5 class="card-title">Edit Role Information</h5>
                </div>         
           
            <div class="card-body">
            <div class="panel panel-default">            
                 <div class="panel-body">
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
                     <asp:HiddenField ID="hdnRoleCode" runat="server" Visible="false" />
                     <div class="col-md-4">
                    <div class="row g-3">
                     <div class="col-md-12 form-group">
                             <asp:Label ID="lblRole" runat="server" AssociatedControlID="txtRoleDescription" CssClass="form-label">Role description <span class="red-star">*</span> :</asp:Label>
                             
                                 <asp:TextBox ID="txtRoleDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvRoleDescription" runat="server" ControlToValidate="txtRoleDescription"
                                     CssClass="failureNotification" ErrorMessage="*Role description is required." ToolTip="Role description is required."
                                     ValidationGroup="RoleValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                             
                     </div>
                     <div class="col-md-12 form-group">
                                 <div class="checkbox">
                                     <asp:Label ID="Label6" runat="server" AssociatedControlID="chkIsActive">
                                         <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" /> &nbsp;Active
                                     </asp:Label>
                                 </div>
                     </div>
                 </div>       
                </div>
                </div>
            
              </div>
        
                <div class="card-footer">
                    <div class="pull-right">
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnCancel_Click">
                            <i class="fa fa-times"></i> Cancel
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSave" runat="server" CommandName="MoveNext" ValidationGroup="RoleValidationGroup" OnClick="btnSave_Click" CssClass="btn btn-primary btn-sm">
                            <i class="fa fa-check"></i> Save
                        </asp:LinkButton>
                    </div>                
                </div>
                <div class="panel-footer">

                    <div class="clearfix"></div>
                </div>

            <asp:HyperLink data-fancybox-type="iframe" CssClass="various" ID="lnkHidden" runat="server" NavigateUrl="~/Account/ReassignUserRole.aspx" style="display:none;"></asp:HyperLink>
           
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