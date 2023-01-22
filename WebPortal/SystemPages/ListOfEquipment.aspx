<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfEquipment.aspx.cs" Inherits="WebPortal.SystemPages.ListOfUnitM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pagetitle">
        <nav>
            <ol class="breadcrumb">
              <li class="breadcrumb-item">Home</li>
              <li class="breadcrumb-item">Clinic & Equipment</li>
              <li class="breadcrumb-item active">Equipment</li>
            </ol>
        </nav>
    </div><!-- End Page Title -->
    <section class="section">
        <div class="row">
            <div class="col-lg-12">

                <div class="card hdl-card">
                    <div class="card-header">
                        <h5 class="card-title">Equipment List</h5>

                        <asp:LinkButton ID="btnAddNew" runat="server" PostBackUrl="~/SystemPages/AddEquipment.aspx" CssClass="btn btn-primary btn-sm">
                            <i class="bi bi-plus"></i> Add New
                        </asp:LinkButton>
                       <%-- <button type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#largeModal" onclick="ShowPanel('create')">
                            <i class="bi bi-plus"></i>
                        </button>--%>

                    </div>
                    <div class="card-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="loading" id="Waiting_UI">Loading&#8230;</div>
                                <!--Error alerrt-->
                                <div class="alert alert-danger alert-dismissable fade in" id="error_alert" runat="server" visible="false">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                    <strong>Error!</strong>
                                    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                </div>
                                <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="hide hide1"></asp:LinkButton>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDimUnit" runat="server" AutoGenerateColumns="False" GridLines="Vertical"
                                        OnRowDataBound="gvDimUnit_RowDataBound" OnRowEditing="gvDimUnit_RowEditing"
                                        ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-striped table-hover">
                                        <Columns>
                                            <asp:BoundField HeaderText="Unit code" DataField="UnitCode"
                                                ItemStyle-CssClass="hide1" HeaderStyle-CssClass="hide1">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Equipment Name (Description)" DataField="Dim_Unit_Desc_long">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="UnitType1Code" DataField="UnitType1Code">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Equipment Type" DataField="UnitType1CodeDesc">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="UnitType2Code" DataField="UnitType2Code">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Unit type 2" DataField="UnitType2CodeDesc">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Clinic code" DataField="FacilityCode">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Clinic" DataField="FacilityCodeDesc">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Program code" DataField="ProgramCode">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Program" DataField="ProgramCodeDesc">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Description" DataField="Description">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="DescriptionLong" DataField="DescriptionLong">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Order id" DataField="OrderID">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Last changed date" DataField="LastChangedDate">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Approximate number of beds" DataField="Beds">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Active" DataField="IsActive">
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                <HeaderStyle Wrap="False" CssClass="text-center" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="DescriptionShort" DataField="DescriptionShort">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                                <HeaderStyle Wrap="False" CssClass="hide1" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center no-sort hide" AccessibleHeaderText="Edit">
                                                <HeaderStyle CssClass="hide" Wrap="False" />
                                                <ItemStyle CssClass="hide" Wrap="False" HorizontalAlign="Center" />
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
                                                                <asp:LinkButton Style="color: #212529;" ID="btnEdit" CssClass="dropdown-item" runat="server" CommandName="Edit" AlternateText="Edit"
                                                                    ToolTip="Click to Edit">
                                                                          <i class="bi bi-pencil"></i> Edit
                                                                </asp:LinkButton>
                                                            </li>
                                                            <li>
                                                                <button style="color: #dc3545;" type="button" class="dropdown-item" onclick='customDeleteDialouge("<%# Eval("UnitCode") %>", "<%# HttpUtility.JavaScriptStringEncode(Convert.ToString(Eval("Dim_Unit_Desc_long"))) %>")'>
                                                                    <i class="bi bi-trash"></i>Delete
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
        debugger;
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
           // debugger;
            $('#MainContent_gvDimUnit > thead > tr:eq(0)').find('.no-sort').attr('data-orderable', 'false');

            var table = $("#MainContent_gvDimUnit").DataTable({
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
            var text = "Are you sure you want to delete '" + message + "' Equipment?";
            var title = "Delete '" + message + "' Equipment";
            $('#dvAlert').html(text);

            var jc = $.confirm({
                title: title,
                content: text,
                buttons: {
                    cancel: {
                        text: '<i class="fa fa-times" aria-hidden="true"></i> Cancel',
                        btnClass: 'btn btn-warning',
                        action: function () { }
                    },
                    delete: {
                        text: '<i class="fa fa-check" aria-hidden="true"></i> Delete',
                        btnClass: 'btn btn-danger',
                        action: function () {
                            $.ajax({
                                type: "POST",
                                url: "ListOfEquipment.aspx/DeleteUnit",
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
                                            content: 'Unable to delete the selected data because it is being referenced elsewhere.',
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