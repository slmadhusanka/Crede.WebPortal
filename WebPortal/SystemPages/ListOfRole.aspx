<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfRole.aspx.cs" Inherits="WebPortal.SystemPages.ListOfRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="pagetitle">
        <h1>Role list </h1>
        <nav>
            <ol class="breadcrumb">
                <li class="breadcrumb-item">Home</li>
                <li class="breadcrumb-item">System Configuration</li>
                <li class="breadcrumb-item active">Roles</li>
            </ol>
        </nav>
    </div>

<section class="section">
        <div class="row">
            <div class="col-lg-12">
                <div class="card hdl-card">
                    <div class="card-header">
                        <h5 class="card-title">Role list </h5>
                        <%--<button type="button" runat="server" ID="btnAddNew" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#largeModal">
                             <button type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#largeModal" onclick="ShowPanel('create')">
                           <i class="bi bi-plus"></i> Add New
                       </button>
                       --%>
                                       
                        <asp:LinkButton ID="btnAddNew" runat="server" PostBackUrl="~/SystemPages/AddRole.aspx" CssClass="btn btn-primary btn-sm">
                            <i class="bi bi-plus"></i> Add New
                        </asp:LinkButton>
                    </div>
                    <div class="card-body">

                    <div class="loading" id="Waiting_UI">Loading&#8230;</div>
                    <div class="alert alert-danger alert-dismissable fade in" id="error_alert" runat="server"
                        visible="false">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong>
                            Error!</strong>
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </div>
                    <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="hide hide1"></asp:LinkButton>
                    <div class="table-responsive">
                        <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvRole_RowDataBound" OnRowEditing="gvRole_RowEditing" OnSelectedIndexChanging="gvRole_SelectedIndexChanging"
                           CssClass="table table-bordered table-striped table-hover">
                            <Columns>
                                <asp:BoundField HeaderText="Role Code" DataField="RoleCode" 
                                    ItemStyle-CssClass="hide1" HeaderStyle-CssClass="hide1">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Role" DataField="Description" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Active" DataField="IsActive" ItemStyle-CssClass="hide1" HeaderStyle-CssClass="hide1">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
 
                                <asp:BoundField HeaderText="Active" DataField="Active">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle  Wrap="False" CssClass="text-center"/>
                                </asp:BoundField>
 
                                <asp:TemplateField HeaderText="Settings" AccessibleHeaderText="Select" HeaderStyle-CssClass="text-center no-sort">
                                        <HeaderStyle Width="60px" Wrap="False" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />                                    
                                        <ItemTemplate>
                                            <div class="btn-group">
                                              <button class="btn btn-outline-primary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                <i class="bi bi-gear"></i>
                                              </button>
                                              <ul class="dropdown-menu">
                                                <li>
                                                    <asp:LinkButton  style="color: #212529;" ID="btnEdit" CssClass="dropdown-item" runat="server" CommandName="Edit" AlternateText="Edit" meta:resourcekeyToolTip="Click to Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>
                                                        <i class="bi bi-pencil"></i> Edit
                                                    </asp:LinkButton> 
                                                </li>
                                                <li>
                                                        <button style="color: #dc3545;" type="button" class="dropdown-item" onclick='customDeleteDialouge("<%# Eval("RoleCode") %>", "<%# HttpUtility.JavaScriptStringEncode(Convert.ToString(Eval("Description"))) %>")'>
                                                                <i class="bi bi-trash"></i> Delete
                                                        </button>   
                                                </li>
                                              </ul>
                                            </div>
                                            
                                        </ItemTemplate>                                    
                                   
                                    </asp:TemplateField>
                            </Columns>
                            
                        </asp:GridView>
                    </div>
                    <asp:HyperLink data-fancybox-type="iframe" CssClass="various" ID="lnkHidden" runat="server"
                        NavigateUrl="~/Account/ReassignUserRole.aspx" Style="display: none;"></asp:HyperLink>
                    <asp:HiddenField ID="hdnIsEditAllowed" runat="server" Visible="false" />
                    <asp:HiddenField ID="hdnIsDeleteAllowed" runat="server" Visible="false" />
                    <div id="dvAlert" style="display: none">
                    </div>
                        <!-- End Default Table Example -->
                    </div>
        </div>
                <div class="modal fade" id="largeModal" tabindex="-1">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header">

                            <h5 id="popup_header">Add role information</h5>

                                 <button type="button"  class="close" data-bs-dismiss="modal" aria-label="Close">
                                          <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <form class="row">
                                    
                                    <div class="row mb-3">
                                        <label for="Role_description" class="col-sm-5 col-form-label">Role description</label>
                                        <div class="col-sm-7">
                                            <input type="text" id="MainContent_txtRoleDescription" class="form-control">
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <label class="col-sm-5 col-form-label">Template role :</label>
                                        <div class="col-sm-7">
                                            <select class="form-control" id="MainContent_ddlTemplateRole">
                                        
                                            </select>
                                        </div>
                                    </div>

                                </form>
                             </div>
                             <div class="modal-footer">
                                 <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                 <button type="button" id ="btnSave" class="btn btn-primary" onclick="EditSaveData()">Save</button>
                             </div>               
                
                
                                    </div>
                                 </div>
                            </div>
                
                <!-- End Large Modal-->


            </div>
        </div>

    
    
    
    
 </section>

    
    
 <!-- mmm -->  
    
    
    <div class="panel panel-default">
        <div class="panel-heading">

            <div class="clearfix">
            </div>
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                </ContentTemplate>
            </asp:UpdatePanel>
             <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="loading">
                        Loading&#8230;</div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script type="text/javascript">
        function GenerateTableSerch(select) {
            var div_md_6 = $("<div />", { class: "col-md-9 data-ser" });
            var form_inline = $("<div />", { class: "form-inline search-pan pull-right" });
            var search_box = $("<div />", { class: "search-box form-row" });
            var form_group_1 = $("<div />", { class: "form-group", style: "padding-right: 15px;" });
            var lbl_serch = $("<label />").text('Search by : ').attr('for', 'ddlSearch');
            var form_group = $("<div />", { class: "form-group" });
            var search_input = $('<input type="text" class="form-control form-control-sm search-ddl txtSearch data-search-text" id="txtSearch" placeholder="Search"/>');
            var search_button = $('<button type="button" class="btn btn-outline-secondary btn-sm data-search-btn" id="btnClear">').append($("<i class='fa fa-times' aria-hidden='true'></i>"));
            
            
            form_group_1.append(lbl_serch);
            form_group_1.append(select);

            form_group.append(search_input);
            form_group.append(search_button);

            search_box.append(form_group_1);
            search_box.append(form_group);

            form_inline.append(search_box);
            div_md_6.append(form_inline);
            return div_md_6;
        }

        function pageLoad() {
            $('#MainContent_gvRole > thead > tr:eq(0)').find('.no-sort').attr('data-orderable', 'false');

            var table = $("#MainContent_gvRole").DataTable({
                pageLength: 100,
                "order": [[0, "desc"]],
                dom: "lrt<'myinfo'i>p",
                initComplete: function () {
                    var select = $('<select class="form-control form-control-sm search-ddl" id="ddlSearch"/>');
                    $('<option />', { value: '', text: 'Any' }).appendTo(select);
                    $(this).find('thead > tr:eq(0) > th:visible').not('.no-sort').each(function () {
                        $('<option />', { value: $(this).index(), text: $(this).text() }).appendTo(select);
                    });

                    var div_md_6 = GenerateTableSerch(select);
                    $(".dataTables_length > label").wrap("<div class='col-md-3 data-sot'></div>");
                    $(".dataTables_length").append(div_md_6);

                    $('#Waiting_UI').remove();

                    var column_no = 0;
                    $('#ddlSearch').on('change', function () {
                        column_no = Number($(this).val());
                        table.search('').columns().search('').draw();
                        $('#txtSearch').val("");
                    });

                    $('#txtSearch').on('input', function () {
                        console.log('input');
                        if ($('#ddlSearch').val() == "") {
                            if (table.search() !== $('#txtSearch').val()) {
                                table.search($('#txtSearch').val()).draw();
                            }
                        }
                        else {
                            if (table.columns([column_no]).search() !== $('#txtSearch').val()) {
                                table.columns([column_no]).search($('#txtSearch').val()).draw();
                            }
                        }
                    });

                    $('#btnClear').on('click', function () {
                        table.search('').columns().search('').draw();
                        $('#txtSearch,#ddlSearch').val("");
                    });
                }
            });

            $(".fancybox").fancybox();

            $("#<%=lnkHidden.ClientID%>").fancybox({
                Width: '400px',
                maxHeight: 700,
                fitToView: false,
                width: '50%',
                height: '80%',
                autoSize: true,
                closeClick: false,
                openEffect: 'elastic',
                closeEffect: 'elastic'
            });
        }
        function ShowUserList() {
            $("#<%=lnkHidden.ClientID%>").fancybox().trigger('click');
        }

        function customDeleteDialouge(id, message) {
            var text = "Are you sure you want to delete '" + message + "' user role ?";
            var title = "Delete '" + message + "' user role";
            var jc = $.confirm({
                title: title,
                content: text,
                buttons: {
                    cancel: {
                        text: '<i class="fa fa-times" aria-hidden="true"></i> Cancel',
                        btnClass: 'btn btn-warning',
                        action: function () {}
                    },
                    delete: {
                        text: '<i class="fa fa-check" aria-hidden="true"></i> Delete',
                        btnClass: 'btn btn-danger',
                        action: function () {
                            $.ajax({
                                type: "POST",
                                url: "ListOfRole.aspx/DeleteRole",
                                contentType: 'application/json; charset=utf-8',
                                data: '{"id":"' + id + '"}',
                                dataType: 'json',
                                success: function (data) {
                                    console.log(data.d);
                                    if (data.d === 1) {
                                        RefreshWindow();
                                    }
                                    else if (data.d === 0) {
                                        console.log('Error in delete');
                                    }
                                    else {
                                        $.alert({
                                            title: 'Alert',
                                            content: 'You cannot delete <b>'+ message + '</b> role. The role is already assigned to users of the system.',
                                        });
                                    }
                                },
                                error: function (data) {
                                    console.log('Error = ' + JSON.stringify(data));
                                }
                            });
                        }
                    }
                }
            });
        }

        function RefreshWindow() {
            __doPostBack('ctl00$MainContent$btnRefresh', '');
        }
    </script>
</asp:Content>