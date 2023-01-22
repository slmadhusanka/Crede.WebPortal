<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfProgramType.aspx.cs" Inherits="WebPortal.SystemPages.ListOfProgramType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pagetitle">  
          <nav>
            <ol class="breadcrumb">
              <li class="breadcrumb-item">Home</li>
              <li class="breadcrumb-item">Table Maintenance</li>
              <li class="breadcrumb-item active">Program Type</li>
            </ol>
          </nav>
        </div><!-- End Page Title -->
    <section class="section">
          <div class="row">
            <div class="col-lg-12">    
    
    
    <div class="card hdl-card">
        <div class="card-header">
            <div class="pull-left">
                <h5 class="card-title">Program Type</h5>
            </div>
            <div class="pull-right">
                <asp:LinkButton ID="btnAddNew" runat="server" PostBackUrl="~/SystemPages/AddProgramType.aspx"
                    CssClass="btn btn-primary btn-sm">
                     <i class="bi bi-plus"></i> Add New
                </asp:LinkButton>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="loading" id="Waiting_UI">Loading&#8230;</div>
                    <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="hide hide1"></asp:LinkButton>
                    <div class="table-responsive">
                        <asp:GridView ID="gvProgramType" runat="server" AutoGenerateColumns="False"
                            OnRowEditing="gvProgramType_RowEditing" OnRowDataBound="gvProgramType_RowDataBound"
                            ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-striped table-hover">
                            <Columns>
                                <asp:BoundField HeaderText="Program Type Code" DataField="ProgramTypeCode" SortExpression="ProgramTypeCode"
                                    ItemStyle-CssClass="hide1" HeaderStyle-CssClass="hide1">
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Program type" DataField="Description" SortExpression="Description">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Active" DataField="IsActive" SortExpression="IsActive">
                                    <ItemStyle HorizontalAlign="Center"  Wrap="False" />
                                    <HeaderStyle Width="100px" Wrap="False" CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="DescriptionShort" DataField="DescriptionShort" SortExpression="DescriptionShort">
                                    <ItemStyle HorizontalAlign="Left" CssClass="hide1" />
                                    <HeaderStyle Wrap="False" CssClass="hide1" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center no-sort" AccessibleHeaderText="Edit">
                                    <HeaderStyle CssClass="hide" Wrap="False" />
                                    <ItemStyle CssClass="hide" Wrap="False" HorizontalAlign="Center"/>
                                    <ItemTemplate>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                                    <asp:LinkButton style="color: #212529;" ID="btnEdit" CssClass="dropdown-item" runat="server" CommandName="Edit" AlternateText="Edit"
                                                        ToolTip="Click to Edit">
                                                        <i class="bi bi-pencil"></i> Edit
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <button style="color: #dc3545;" type="button" class="dropdown-item" onclick='customDeleteDialouge("<%# Eval("ProgramTypeCode") %>", "<%# HttpUtility.JavaScriptStringEncode(Convert.ToString(Eval("Description"))) %>")'>
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
                    <asp:HiddenField ID="hdnIsEditAllowed" runat="server" Visible="false" />
                    <asp:HiddenField ID="hdnIsDeleteAllowed" runat="server" Visible="false" />
                    <div id="dvAlert" style="display: none">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="loading">Loading&#8230;</div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
            </div>
          </div>
    </section>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script type="text/javascript">
        function GenerateTableSerch(select) {
            var div_md_6 = $("<div />", { class: "col-md-9 data-ser" });
            var form_inline = $("<div />", { class: "form-inline search-pan pull-right" });
            var search_box = $("<div />", { class: "search-box form-row" });
            var form_group_1 = $("<div />", { class: "form-group", style: "padding-right: 15px;" });
            var lbl_serch = $("<label />").text('Search by :').attr('for', 'ddlSearch');
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
            $('#MainContent_gvProgramType> thead > tr:eq(0)').find('.no-sort').attr('data-orderable', 'false');

            var table = $("#MainContent_gvProgramType").DataTable({
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
        }

        function customDeleteDialouge(id, message) {
            var text = "Are you sure you want to delete '" + message + "' program type?";
            var title = "Delete '" + message + "' program type";
            $('#dvAlert').html(text);
            
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
                                url: "ListOfProgramType.aspx/DeleteProgramType",
                                contentType: 'application/json; charset=utf-8',
                                data: '{"id":"' + id + '"}',
                                dataType: 'json',
                                success: function (data) {
                                    console.log(data.d);
                                    if (data.d) {
                                        RefreshWindow();
                                    }
                                    else {
                                        $.alert({
                                            title: 'Warning ',
                                            content: 'Unable to delete the selected data due to available dependencies.',
                                        });
                                        console.log('Error in delete')
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