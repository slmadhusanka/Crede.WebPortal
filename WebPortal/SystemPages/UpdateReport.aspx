<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateReport.aspx.cs" Inherits="WebPortal.SystemPages.UpdateReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:HyperLink CssClass="btn-circle btn btn-back" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfReport.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                    <h3 class="panel-title">Edit report information</h3>
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
                    <asp:HiddenField ID="hdnReportID" runat="server" Visible="false" />
                    <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6">
                        <div class="form-group">
                            <asp:Label ID="lblReportCode" runat="server" AssociatedControlID="txtReportCode" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Report code <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtReportCode" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReportCode" runat="server" ControlToValidate="txtReportCode"
                                    CssClass="failureNotification" ErrorMessage="*Report code is required." ToolTip="Report code is required."
                                    ValidationGroup="ReportValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblReportName" runat="server" AssociatedControlID="txtReportName" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Report name <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtReportName" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReportName" runat="server" ControlToValidate="txtReportName"
                                    CssClass="failureNotification" ErrorMessage="*Report name is required." ToolTip="Report name is required."
                                    ValidationGroup="ReportValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblReportDescription" runat="server" AssociatedControlID="txtReportDescription" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Report description :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtReportDescription" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblReportServerLocation" runat="server" AssociatedControlID="txtReportServerLocation" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Report server location <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtReportServerLocation" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReportServerLocation" runat="server" ControlToValidate="txtReportServerLocation"
                                    CssClass="failureNotification" ErrorMessage="*Report server location is required." ToolTip="Report server location is required."
                                    ValidationGroup="ReportValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6">
                        <div class="form-group">
                            <asp:Label ID="lblReportCategory" runat="server" AssociatedControlID="txtReportCategory" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Report category <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtReportCategory" runat="server" CssClass="form-control"></asp:TextBox> 
                                <asp:RequiredFieldValidator ID="rfvReportCategory" runat="server" ControlToValidate="txtReportCategory"
                                    CssClass="failureNotification" ErrorMessage="*Report category is required." ToolTip="Report category is required."
                                    ValidationGroup="ReportValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblReportSubCategory" runat="server" AssociatedControlID="txtReportSubCategory" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Report sub category :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtReportSubCategory" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblReportKey" runat="server" AssociatedControlID="txtReportKey" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Report key <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtReportKey" runat="server" CssClass="form-control"></asp:TextBox> 
                                <asp:RequiredFieldValidator ID="rfvReportKey" runat="server" ControlToValidate="txtReportKey"
                                    CssClass="failureNotification" ErrorMessage="*Report key is required." ToolTip="Report key is required."
                                    ValidationGroup="ReportValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblSortOrder" runat="server" AssociatedControlID="txtSortOrder" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Sort order :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtSortOrder" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-4 col-lg-offset-3 col-sm-7 col-lg-8">
                                <div class="checkbox">
                                    <asp:Label ID="Label6" runat="server" AssociatedControlID="chkIsSendEnable">
                                        <asp:CheckBox ID="chkIsSendEnable" runat="server" />Send enable
                                    </asp:Label>
                                </div>
                                <div class="checkbox">
                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="chkIsActive">
                                        <asp:CheckBox ID="chkIsActive" runat="server" />Active
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="pull-right">
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click"><i class="fa fa-times" aria-hidden="true"></i> Cancel</asp:LinkButton>
                        <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="ReportValidationGroup" OnClick="btnSave_Click" CssClass="btn btn-success"><i class="fa fa-check" aria-hidden="true"></i> Save</asp:LinkButton>
                    </div>
                    <div class="clearfix"></div>
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
