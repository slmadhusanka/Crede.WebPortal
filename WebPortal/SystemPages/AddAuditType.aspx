<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAuditType.aspx.cs" Inherits="WebPortal.SystemPages.AddAuditType" %>
<%@ Register TagPrefix="ajaxtool" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=20.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uppnl_Step1" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:HyperLink CssClass="btn-circle btn btn-back" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfAuditTypes.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                    <h3 class="panel-title">Add audit type</h3>
                </div>
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

                    <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6">
                        <div class="form-group">
                            <asp:Label ID="lblAuditType" runat="server" AssociatedControlID="txtAuditType" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Audit type <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtAuditType" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AuditTypeRequired" runat="server" ControlToValidate="txtAuditType" Display="Dynamic"
                                    CssClass="failureNotification" ErrorMessage="*Audit type is required." ToolTip="Audit type is required."
                                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6">
                        <div class="form-group">
                            <asp:Label ID="lblSortOredr" runat="server" AssociatedControlID="txtSortOredr" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Sort order :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtSortOredr" runat="server" CssClass="form-control"></asp:TextBox>
                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="filtertextboc1" TargetControlID="txtSortOredr"
                                    FilterMode="ValidChars" FilterType="Numbers">
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-4 col-lg-offset-3 col-sm-7 col-lg-8">
                                <div class="checkbox">
                                    <asp:Label ID="lblIaActive" runat="server" AssociatedControlID="cbIsActive">
                                        <asp:CheckBox ID="cbIsActive" runat="server" Checked="true" />Active
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="pull-right">
                        <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn btn-warning" CausesValidation="false" PostBackUrl="~/SystemPages/ListOfAuditTypes.aspx"><i class="fa fa-times" aria-hidden="true"></i> Cancel</asp:LinkButton>
                        <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="RegisterUserValidationGroup" OnClick="UpdateUserButton_Click" CssClass="btn btn-success"><i class="fa fa-check" aria-hidden="true"></i> Save</asp:LinkButton>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
