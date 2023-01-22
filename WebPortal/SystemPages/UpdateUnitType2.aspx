<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateUnitType2.aspx.cs" Inherits="WebPortal.SystemPages.UpdateUnitType2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:HyperLink CssClass="btn-circle btn btn-back" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfUnitType2.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                    <h3 class="panel-title">Edit Unit Type 2 information</h3>
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
                        <div class="form-group hide1 hide">
                            <asp:Label ID="LabelUnitCodeType2" runat="server" AssociatedControlID="lblUnitType2Code" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Unit Type 2 Code:</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:Label ID="lblUnitType2Code" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblUnitType2Desc" runat="server" AssociatedControlID="txtUnitType2Desc" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Unit type 2 <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtUnitType2Desc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUnitType2Desc" Display="Dynamic"
                                    CssClass="failureNotification" ErrorMessage="*Unit type 2 description is required." ToolTip="Unit type 2 is required."
                                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6">
                        <div class="form-group">
                            <asp:Label ID="lblDescriptionShort" runat="server" AssociatedControlID="txtDescriptionShort" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Short description :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                <asp:TextBox ID="txtDescriptionShort" runat="server" CssClass="form-control">
                                </asp:TextBox>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescriptionShort"
                                    CssClass="failureNotification" ErrorMessage="*Unit Type 2 Description Short is required." Display="Dynamic"
                                    ToolTip="Unit Type 2 Description Short is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-4 col-lg-offset-3 col-sm-7 col-lg-8">
                                <div class="checkbox">
                                    <asp:Label ID="Label8" runat="server" AssociatedControlID="chkIsActive">
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
                        <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="RegisterUserValidationGroup" OnClick="UpdateUserButton_Click" CssClass="btn btn-success"><i class="fa fa-check" aria-hidden="true"></i> Save</asp:LinkButton>
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
