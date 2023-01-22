<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfSuggestedPriorityNew.aspx.cs" Inherits="WebPortal.SystemPages.ListOfSuggestedPriorityNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pagetitle">
         
          <nav>
            <ol class="breadcrumb">
              <li class="breadcrumb-item">Home</li>
              <li class="breadcrumb-item">Table Maintenance</li>
              <li class="breadcrumb-item active">Suggested Priority </li>
            </ol>
          </nav>
        </div><!-- End Page Title -->
    
    <section class="section">
      <div class="row">
        <div class="col-lg-12">
    
            <div class="card hdl-card">
                <div class="card-header">
                    <div class="pull-left">
                        <h5 class="card-title">Suggested Priority</h5>
                    </div>
                    <div class="pull-right">
                        <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" CssClass="btn btn-primary btn-sm">
                             <i class="bi bi-plus"></i> Add New
                        </asp:LinkButton>

                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="card-body">
                    <!-- Modal -->
                    
                    
                    <div class="modal fade" id="cartModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                        <div class="modal-content">
                          <div class="modal-header border-bottom-0">
                            <h5 class="modal-title" id="exampleModalLabel">
                              Your Shopping Cart
                            </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                              <span aria-hidden="true">&times;</span>
                            </button>
                          </div>
                          <div class="modal-body">
                             
                            Test
                              
                          </div>
                          <div class="modal-footer border-top-0 d-flex justify-content-between">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-success">Checkout</button>
                          </div>
                        </div>
                      </div>
                    </div>
            
            
            
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="loading" id="Waiting_UI">Loading&#8230;</div>
                    <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="hide hide1"></asp:LinkButton>
                    <div class="table-responsive">
                        <asp:GridView ID="gvSuggestedPriority" runat="server" AutoGenerateColumns="False"
                            OnRowEditing="gvRegion_RowEditing" OnRowDataBound="gvRegion_RowDataBound"
                            ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-striped table-hover">
                            <Columns>
                                <asp:BoundField HeaderText="Region Code" DataField="ID" ItemStyle-CssClass="hide1" HeaderStyle-CssClass="hide1">
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Description" DataField="Description">
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                
                                <asp:BoundField HeaderText="Active" DataField="IsActive">
                                    <ItemStyle Width="100px" Wrap="False" HorizontalAlign="Center" />
                                    <HeaderStyle Wrap="False" CssClass="text-center"/>
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
                                                        ToolTip="Click to Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>
                                                         <i class="bi bi-pencil"></i> Edit
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <button style="color: #dc3545;" type="button" class="dropdown-item" onclick='customDeleteDialouge("<%# Eval("ID") %>", "<%# HttpUtility.JavaScriptStringEncode(Convert.ToString(Eval("Description"))) %>")'>
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
        
        $(document).ready(function() {  
          $('#cartModal').modal('click');
        });

        function pageLoad() {
            $('#MainContent_gvSuggestedPriority> thead > tr:eq(0)').find('.no-sort').attr('data-orderable', 'false');

            var table = $("#MainContent_gvSuggestedPriority").DataTable({
                pageLength: 100,
                "order": [[1, "asc"]],
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
            var text = "Are you sure you want to delete '" + message + "' Region?";
            var title = "Delete '" + message + "' Region";

            $('#dvAlert').html(document.createTextNode(text));

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
                                url: "ListOfSuggestedPriorityNew.aspx/DeleteERROR",
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
