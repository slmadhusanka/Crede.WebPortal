<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Permission.aspx.cs" Inherits="WebPortal.SystemPages.Permission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="pagetitle">
            <h1>Assign permissions</h1>
            <nav>
            <ol class="breadcrumb">
                <li class="breadcrumb-item">Home</li>
                <li class="breadcrumb-item">System Configuration</li>
                <li class="breadcrumb-item active">Assign permissions</li>
            </ol>
            </nav>
        </div><!-- End Page Title -->
        <section class="section">
                <div class="row">
                    <div class="col-lg-12">
                        
                        <div class="card hdl-card">
                            <div class="card-header">
                                 <h5 class="card-title">Assign permissions</h5>
                                    <div class="form-inline pr-pan">
                                        <div class="form-group">
                                            <asp:Label  ID="lblRole" CssClass="form-label pr-lbl" runat="server" AssociatedControlID="ddlRole" Text="Role">Role : </asp:Label>
                                            <asp:DropDownList  ID="ddlRole" runat="server" AutoPostBack="true" CssClass="form-control pr-select" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                <div class="pull-right"><%--
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-warning"><span class="glyphicon glyphicon-remove"></span> Cancel</asp:LinkButton>--%>
                                    
                                    <asp:LinkButton ID="LinkButton2" runat="server" onclick="btnSave_Click" CssClass="btn btn-primary btn-sm"><span class="glyphicon glyphicon-ok"></span> Save</asp:LinkButton>
                                
                                    </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="height:75vh;overflow:auto">
                                <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" class="col-md-12">
                                    <ContentTemplate>
                                        <!--Success Alert-->
                                        <div id="success_alert" runat="server" class="alert alert-success alert-dismissable fade in">
                                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                            <strong>Success!</strong> <asp:Literal ID="SuccessMessage" runat="server"></asp:Literal>
                                        </div>
                                        <!--Error Alert-->
                                        <div id="error_alert" runat="server" class="alert alert-danger alert-dismissable fade in">
                                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                            <strong>Error!</strong>  <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                        </div>
                                        
                                        <div class="form-horizontal">
                                            <div class="form-group form-group-sm permission-table">
                                                <div class="col">
                                                    <asp:ListView ID="lvModules1" runat="server" OnItemDataBound="lvModules1_ItemDataBound">
                                                        <ItemTemplate>
                                                            <ul class="treeview card row treeview-list-1">
                                                                <li class="treeview-row-1">
                                                                    <%# Eval("Name")%> <span><%# (!string.IsNullOrEmpty(Convert.ToString(Eval("AccessDescription")))) ?"("+ Convert.ToString(Eval("AccessDescription")) +")" : "" %>
                                                                    <div class="material-switch pull-right">
                                                                        <asp:CheckBox ID="chkModule1" runat="server" Checked="false" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" ClientIDMode="AutoID"/>
                                                                        <asp:Label ID="lblToggleButton" CssClass="label-success" AssociatedControlID="chkModule1" runat="server"/>
                                                                        <asp:HiddenField ID="hdnModule1" runat="server" Value='<%# Eval("ModuleId")%>' />
                                                                        <asp:HiddenField ID="hdnModuleKey1" runat="server" Value='<%# Eval("ModuleKey")%>' />
                                                                    </div>
                                                                 </li>
                                                                <li class="treeview-row-2">
                                                                    <asp:ListView ID="lvModules2" runat="server" OnItemDataBound="lvModules2_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <ul class="treeview treeview-list-2">
                                                                                <li class="treeview-row-2">
                                                                                    <%# Eval("Name")%> <span><%# (!string.IsNullOrEmpty(Convert.ToString(Eval("AccessDescription")))) ?"("+ Convert.ToString(Eval("AccessDescription")) +")" : "" %></span>
                                                                                    <div class="material-switch pull-right">
                                                                                        <asp:CheckBox ID="chkModule2" runat="server" Checked="false" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" ClientIDMode="AutoID"/>
                                                                                        <asp:Label ID="lblToggleButton" CssClass="label-success" AssociatedControlID="chkModule2" runat="server"/>
                                                                                        <asp:HiddenField ID="hdnModule2" runat="server" Value='<%# Eval("ModuleId")%>' />
                                                                                        <asp:HiddenField ID="hdnModuleKey2" runat="server" Value='<%# Eval("ModuleKey")%>' />
                                                                                    </div>
                                                                                </li>
                                                                                <li class="treeview-row-2">
                                                                                    <asp:ListView ID="lvModules3" runat="server" OnItemDataBound="lvModules3_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <ul class="treeview treeview-list-3">
                                                                                                <li class="treeview-row-3">
                                                                                                    <%# Eval("Name")%> <span><%# (!string.IsNullOrEmpty(Convert.ToString(Eval("AccessDescription")))) ?"("+ Convert.ToString(Eval("AccessDescription")) +")" : "" %></span>
                                                                                                    <div class="material-switch pull-right">
                                                                                                        <asp:CheckBox ID="chkModule3" runat="server" Checked="false" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" ClientIDMode="AutoID" />
                                                                                                        <asp:Label ID="lblToggleButton" CssClass="label-success" AssociatedControlID="chkModule3" runat="server"/>
                                                                                                        <asp:HiddenField ID="hdnModule3" runat="server" Value='<%# Eval("ModuleId")%>' />
                                                                                                        <asp:HiddenField ID="hdnModuleKey3" runat="server" Value='<%# Eval("ModuleKey")%>' />
                                                                                                    </div>
                                                                                                </li>
                                                                                                <li class="treeview-row-3">
                                                                                                    <asp:ListView ID="lvModules4" runat="server" OnItemDataBound="lvModules4_ItemDataBound">
                                                                                                        <ItemTemplate>
                                                                                                            <ul class="treeview treeview-list-4">
                                                                                                                <li class="treeview-row-4">
                                                                                                                    <%# Eval("Name")%> <span><%# (!string.IsNullOrEmpty(Convert.ToString(Eval("AccessDescription")))) ?"("+ Convert.ToString(Eval("AccessDescription")) +")" : "" %></span>
                                                                                                                    <div class="material-switch pull-right">
                                                                                                                        <asp:CheckBox ID="chkModule4" runat="server" Checked="false" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" ClientIDMode="AutoID" />
                                                                                                                        <asp:Label ID="lblToggleButton" CssClass="label-success" AssociatedControlID="chkModule4" runat="server"/>
                                                                                                                        <asp:HiddenField ID="hdnModule4" runat="server" Value='<%# Eval("ModuleId")%>' />
                                                                                                                        <asp:HiddenField ID="hdnModuleKey4" runat="server" Value='<%# Eval("ModuleKey")%>' />
                                                                                                                    </div>
                                                                                                                </li>
                                                                                                            </ul>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                </li>
                                                                                            </ul>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </li>
                                                                            </ul>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </li>
                                                            </ul>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
                                <div class="loading">Loading&#8230;</div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        
                    </div>
                </div>
        </section>
    
    
    
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            
            
        </div>
    </div>  
</asp:Content>
