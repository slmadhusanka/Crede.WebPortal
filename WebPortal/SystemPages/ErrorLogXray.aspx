<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorLogXray.aspx.cs" Inherits="WebPortal.SystemPages.ErrorLogXray" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageStyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pagetitle">
        <h1>Transvaginal Transducer HLD Reprocessing log</h1>
        <nav>
            <ol class="breadcrumb">
                <li class="breadcrumb-item">Home</li>
                <li class="breadcrumb-item">Log Reports</li>
                <li class="breadcrumb-item active">Error Log</li>
            </ol>
        </nav>
    </div>
    <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card hdl-card">
                        <div class="card-header">
                            <h5 class="card-title">Error Log</h5>
                            <button type="button" class="btn btn-primary btn-sm" runat="server"  ID="btnAddNew" onclick="ShowPanel('create')">
                                <i class="bi bi-plus"></i> Add New
                              </button>
                        </div>
                        <div class="card-body">
                             <div class="table-responsive">
                                 <table id="myTable" class="table table-bordered table-striped hld-table" width="99.9%">
                                   <thead>
                                       <tr>
                                           <th class ="hide hide1">Id</th>
                                         <th>Date of Error</th>
                                         <th>Operator</th>
                                         <th>Person Entering Item</th>
                                         <th>Suggested Priority</th>
                                         <th>Location</th>
                                         <th>Modality</th>
                                         <th>Error Category	</th>
                                         <th>Description of Error</th>
                                         <th>Possible Consequence</th>
                                         <th>Remedial Action Taken MRP</th>
                                         <th>Remedial Action</th>
                                         <th>Notes</th>
                                         <th width="60px">Settings</th>
                                       </tr>
                                   </thead>
                                   <tbody>
                                   </tbody>
                                 </table>
                             </div>
                        </div>
                    </div>
                </div>
            </div>
        
             <div class="modal fade" id="largeModal" tabindex="-1">
                  <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 id="popup_header" class="modal-title">Add New Error</h5>
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                        </button>
                          
                      </div>
                      <div class="modal-body">
                          <form novalidate="" action="" method="" id="AddErrorLog">
                                <div class="alert alert-success alert-dismissible fade show col-md-12" role="alert" style="display: none">
                                 <!-- <strong>Holy guacamole!</strong> A simple success alert—check it out! -->
                                <button type="button" class="close" data-bs-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>

                                <div class="alert alert-danger alert-dismissible fade show col-md-12 alert-danger-status" role="alert" style="display: none">
                                   <!-- <strong>Holy guacamole!</strong> A simple danger alert—check it out! -->
                                <button type="button" class="close" data-bs-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                              
                              <div class="alert alert-danger alert-dismissible fade show col-md-12 alert-data-validation" role="alert" style="display: none">
                                  The following field(s) empty or contain invalid data. Please check your inputs.
                                  <button type="button" class="close" data-bs-dismiss="alert" aria-label="Close">
                                      <span aria-hidden="true">&times;</span>
                                  </button>
                              </div>
                              
                              <div class="alert alert-danger alert-dismissible fade show col-md-12 alert-error-message" role="alert" style="display: none">
                                  The system encountered an error while saving the data.
                                  Please report this issue to support@credetechnologies.com for assistance.
                                  <button type="button" class="close" data-bs-dismiss="alert" aria-label="Close">
                                      <span aria-hidden="true">&times;</span>
                                  </button>
                              </div>
                              
                              <div class="alert alert-danger alert-dismissible fade show col-md-12 alert-error-message-2" role="alert" style="display: none">
                                  The system encountered an error while receiving the data.
                                  Please report this issue to support@credetechnologies.com for assistance.
                                  <button type="button" class="close" data-bs-dismiss="alert" aria-label="Close">
                                      <span aria-hidden="true">&times;</span>
                                  </button>
                              </div>

                              <div class="alert alert-warning alert-dismissible fade show col-md-12" role="alert" style="display: none">
                                 <!-- <strong>Holy guacamole!</strong> A simple warning alert—check it out! -->
                                <button type="button" class="close" data-bs-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                          
                             <div class="row g-3">
                                 <div class="col-md-6 form-group">
                                      <label class="form-label">Date of Error <span class="text-danger" title="This is required">*</span> :</label>
                                       <input type="date" id="dateoferror" data-member-identifier="DateOfError" class="form-control form-control-sm requied-field data-member">
                                        <div class="invalid-feedback">Please enter a valid date.</div>
                                 </div>
                                 <div class="col-md-6 form-group">
                                       <label class="form-label">Operator <span class="text-danger" title="This is required">*</span> :</label>
                                       <input placeholder="Please Enter Operator Name" data-member-identifier="Operator" type="text" id="operator" class="form-control form-control-sm requied-field data-member">
                                       <div class="valid-feedback">A simple success alert—check good</div>
                                       <div class="invalid-feedback">Please enter a valid operator Name.</div>
                                  </div>
                                 <div class="col-md-6 form-group">
                                        <label class="form-label">Person Entering Item <span class="text-danger" title="This is required">*</span> :</label>
                                        <input placeholder="Please Enter Person Name" data-member-identifier="PersonEnteringItem" type="text" id="Entering_Item" class="form-control form-control-sm requied-field data-member">
                                        <div class="valid-feedback">A simple success alert—check good</div>
                                        <div class="invalid-feedback">Please enter a valid Person Name.</div>
                                   </div>
                                  <div class="col-md-6 form-group">
                                        <label class="form-label">Suggested Priority <span class="text-danger" title="This is required">*</span> :</label>
                                        <select class="form-control form-control-sm requied-field data-member" data-member-identifier="SuggestedPriorityID" id="Suggested_Priority" aria-label="Suggested Priority">
                                        </select>
                                      <div class="invalid-feedback">Please select a valid suggested priority.</div>
                                   </div>
                                 <div class="col-md-6 form-group">
                                     <label class="form-label">Location <span class="text-danger" title="This is required">*</span> :</label>
                                     <select class="form-control form-control-sm requied-field data-member" data-member-identifier="LocationID" id="Location" aria-label="Location">
                                     </select>
                                     <div class="invalid-feedback">Please select a valid location.</div>
                                 </div>
                                 <div class="col-md-6 form-group">
                                      <label class="form-label">Modality <span class="text-danger" title="This is required">*</span> :</label>
                                      <select class="form-control form-control-sm requied-field data-member" data-member-identifier="ModalityID" id="Modality" aria-label="Modality">
                                      </select>
                                     <div class="invalid-feedback">Please select a valid modality.</div>
                                 </div>
                                 <div class="col-md-6 form-group">
                                       <label class="form-label">Error Category <span class="text-danger" title="This is required">*</span> :</label>
                                       <select class="form-control form-control-sm requied-field data-member" data-member-identifier="ErrorCategoryID" id="Error_Category" aria-label="Error Category">
                                       </select>
                                     <div class="invalid-feedback">Please select a valid error category.</div>
                                  </div>
                                 <div class="col-md-6 form-group">
                                    <label class="form-label">Description of Error :</label>
                                    <input placeholder="Please Enter Description of Error" type="text" data-member-identifier="DescriptionOfError" id="Error_Description" class="form-control form-control-sm data-member">
                               </div>
                                 <div class="col-md-6 form-group">
                                    <label class="form-label">Possible Consequence <span class="text-danger" title="This is required">*</span> :</label>
                                    <select class="form-control form-control-sm requied-field data-member" data-member-identifier="PossibleConsequenceID" id="Possible_Consequence" aria-label="Possible Consequence">
                                    </select>
                                     <div class="invalid-feedback">Please select a valid possible consequence.</div>
                               </div>
                                <div class="col-md-6 form-group">
                                     <label class="form-label">Remedial Action Taken MRP <span class="text-danger" title="This is required">*</span> :</label>
                                     <select class="form-control form-control-sm requied-field data-member" data-member-identifier="RemedialActionTakenMRPID" id="RemedialActionTakenMRP" aria-label="Remedial Action Taken MRP">
                                     </select>
                                    <div class="invalid-feedback">Please select a valid remedial action taken MRP.</div>
                                </div>
                                 <div class="col-md-6 form-group">
                                     <label class="form-label">Remedial Action :</label>
                                     <input placeholder="Please Enter Remedial Action" type="text" data-member-identifier="RemedialAction" id="Remedial_Action" class="form-control form-control-sm data-member">
                                 </div>
                                 <div class="col-md-12 form-group">
                                      <label class="form-label">Notes :</label>
                                      <textarea class="form-control form-control-sm data-member" data-member-identifier="Notes" id="notes_re" rows="2"></textarea>
                                  </div>
                            </div>
                          </form>
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Close</button>
                          <button type="button" id ="btnSave" class="btn btn-primary btn-sm" onclick="SaveData()">Submit</button>
                          <button type="button" id ="btnEdit" class="btn btn-primary btn-sm" onclick="EditSaveData()">Submit</button>
                      </div>
                    </div>
                  </div>
                </div><!-- End Large Modal-->
    </section>
<div class="showbox" id="loader" style="display: none">
    <div class="loader-new">
        <svg class="circular" viewBox="25 25 50 50">
            <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"/>
        </svg>
    </div>
</div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script>

        // global parameter for error count
        let errorCount = 0;

        // global parameter for table
        var table;
        // global parameter for edit
        var EditId;

        $(document).ready(function () {

            LoadTable();

            // detect all the required select field data changes
            $(".requied-field").on('change', function () {    // 2nd (A)
                errorCount = 0;

                if ($(this).val().length != 0) {

                    $(this).siblings('.invalid-feedback').hide();

                    $('.requied-field').each(function () {
                        if ($(this).val().length == 0) {
                            errorCount++;
                        }
                    });

                    if (errorCount == 0) {
                        $('.alert-data-validation').hide();
                    }
                }
                else {
                    $(this).siblings('.invalid-feedback').show();
                }
            });

            // detect all the required text field data changes
            $('.requied-field').on('input', function (e) {
                errorCount = 0;

                if ($(this).val().length != 0) {

                    $(this).siblings('.invalid-feedback').hide();

                    $('.requied-field').each(function () {
                        if ($(this).val().length == 0) {
                            errorCount++;
                        }
                    });

                    if (errorCount == 0) {
                        $('.alert-data-validation').hide();
                    }
                }
                else {
                    $(this).siblings('.invalid-feedback').show();
                }
            });

        });

        function GenerateTableSerch(select) {
            var div_md_6 = $("<div />", { class: "col-md-9 data-ser" });
            var form_inline = $("<div />", { class: "form-inline search-pan pull-right" });
            var search_box = $("<div />", { class: "search-box form-row" });
            var form_group_1 = $("<div />", { class: "form-group", style: "padding-right: 15px;" });
            var lbl_serch = $("<label />").text('Search by :').attr('for', 'ddlSearch');
            var form_group = $("<div />", { class: "form-group", style: "display: inline-flex;" });
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

        function getBool(val) {
            return !!JSON.parse(String(val).toLowerCase());
        }

        // load main table
        function LoadTable() {
            //var IsEditAllowed = $('#hdnIsEditAllowed').val();
            //var IsDeleteAllowed = $('#hdnIsDeleteAllowed').val();

            $.fn.dataTable.moment('MMM D, YYYY');

            table = $('#myTable').DataTable({
                "ajax": {
                    "type": "GET",
                    "url": "ErrorLogXray.aspx/LoadTable",
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "beforeSend": function (request) {
                        $("#loader").show();
                    },
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    },
                    "fnDrawCallback": function (oSettings) {
                        $("#MainContent_UpdateProg1").hide();
                        $("#loader").hide();
                    },
                    error: function (request, status, error) {
                        console.log(error);
                        $("#loader").hide();
                    }
                },
                "deferRender": true,
                pageLength: 100,
                dom: "lrt<'myinfo'i>p",
                initComplete: function () {
                    $("#loader").hide();

                    var select = $('<select class="form-control form-control-sm search-ddl" id="ddlSearch"/>');
                    $('<option />', { value: '', text: 'Any' }).appendTo(select);
                    $(this).find('thead > tr:eq(0) > th:visible').not('.no-sort').each(function () {

                        $('<option />', { value: $(this).index(), text: $(this).text() }).appendTo(select);
                    });
                    var div_md_6 = GenerateTableSerch(select);
                    $(".dataTables_length > label").wrap("<div class='col-md-3 data-sot'></div>");
                    $(".dataTables_length").append(div_md_6);

                    var column_no = 0;
                    $('#ddlSearch').on('change', function () {
                        column_no = Number($(this).val());
                        table.search('').columns().search('').draw();
                        $('#txtSearch').val("");
                    });

                    $('#txtSearch').on('input', function () {
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
                },
                "order": [[0, "Id"]],
                columns: [
                    { 'data': 'Id', className: 'hide hide1' },
                    { 'data': 'DateOfError' },
                    { 'data': 'Operator', className: 'text-center' },
                    { 'data': 'PersonEnteringItem', className: 'text-center' },
                    { 'data': 'SuggestedPriority', className: 'text-center'},
                    { 'data': 'Location', className: 'text-center'},
                    { 'data': 'Modality', className: 'text-center'},
                    { 'data': 'ErrorCategory', className: 'text-center' },
                    { 'data': 'DescriptionOfError', className: 'text-center'},
                    { 'data': 'PossibleConsequence', className: 'text-center'},
                    { 'data': 'RemedialActionTakenMRP', className: 'text-center'},
                    { 'data': 'RemedialAction', className: 'text-center'},
                    { 'data': 'Notes' },
                    {
                        'data': null, className: 'text-center no-sort', 'render': function (data, type, row) {
                            return '<div class="btn-group"> <button class="btn btn-outline-primary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">' + 
                                '<i class="bi bi-gear" ></i > </button >' +
                                '<ul class="dropdown-menu"> <li><a style="color: #212529;" class="dropdown-item" href="#" onclick="return EditData(event,\'' + data.Id + '\');"><i class="bi bi-pencil"></i> Edit</a></li>' +
                                '<li><a style="color: #dc3545;" class="dropdown-item" href="#" onclick="DeleteData(event,\'' + data.Id + '\');"><i class="bi bi-trash"></i> Delete</a></li> </ul> </div > ';
                        }
                    }
                ]
            });
        }

        // clean all the dropdown values
        function ClearData() {
            $("#Suggested_Priority").empty();
            $("#Location").empty();
            $("#Modality").empty();
            $("#Error_Category").empty();
            $("#Possible_Consequence").empty();
            $("#RemedialActionTakenMRP").empty();
        }

        // clear all the form data
        function clearcomponent() {

            $("#dateoferror").val("");
            $("#Error_Description").val("");
            $("#notes_re").val("");
            $("#operator").val("");
            $("#Remedial_Action").val("");
            $("#Suggested_Priority").val("");
            $("#Location").val("");
            $("#Modality").val("");
            $("#Error_Category").val("");
            $("#Possible_Consequence").val("");
            $("#RemedialActionTakenMRP").val("");
        }

        // pop-up model loading
        function ShowPanel(action) {

            // fresh the form emelents
            clearcomponent();
            LoadRowData(action);
            $('.invalid-feedback').hide();
            $('.alert-data-validation').hide();
            $('.alert-error-message').hide();
            $('.alert-error-message-2').hide();
            $('.alert-danger-status').hide();

            $('#popup_header').text('');
            if (action == 'create') { // new workflow
                
                $('#btnSave').show();
                $('#btnEdit').hide();
                $('#popup_header').text('Add New Error Log');

                document.getElementById("dateoferror").valueAsDate = new Date();
            } else { // edit workflow

                $('#btnSave').hide();
                $('#btnEdit').show();

                $('#popup_header').text('Edit Error Log');
            }

            $('#largeModal').modal('show');
        }
        

        function LoadRowData(action) {
            $.ajax({
                url: "ErrorLogXray.aspx/LoadRowData",
                type: "GET",
                contentType: 'application/json',
                beforeSend: function () {
                    $("#loader").show();
                },
                success: function (result) {
                    //reload table
                    $("#loader").hide();

                    // clear data
                    ClearData();

                    var dataSet = JSON.parse(result.d);
                    
                    // Suggested Priority
                    $('#Suggested_Priority').append($('<option></option>').val("").html("Select Suggested Priority"));
                    $.each(dataSet.SuggestedPriorities, function (i, p) {
                        $('#Suggested_Priority').append($('<option></option>').val(p.ID).html(p.Description));
                    });

                    // Location
                    $('#Location').append($('<option></option>').val("").html("Select Location"));
                    $.each(dataSet.Locations, function (i, p) {
                        $('#Location').append($('<option></option>').val(p.ID).html(p.Description));
                    });

                    // Modality
                    $('#Modality').append($('<option></option>').val("").html("Select Modality"));
                    $.each(dataSet.Modalities, function (i, p) {
                        $('#Modality').append($('<option></option>').val(p.ID).html(p.Description));
                    });

                    // Error Category
                    $('#Error_Category').append($('<option></option>').val("").html("Select Error Category"));
                    $.each(dataSet.ErrorCategories, function (i, p) {
                        $('#Error_Category').append($('<option></option>').val(p.ID).html(p.Description));
                    });

                    // Possible Consequence
                    $('#Possible_Consequence').append($('<option></option>').val("").html("Select Possible Consequence"));
                    $.each(dataSet.PossibleConsequences, function (i, p) {
                        $('#Possible_Consequence').append($('<option></option>').val(p.ID).html(p.Description));
                    });

                    // Remedial Action TakenMRP
                    $('#RemedialActionTakenMRP').append($('<option></option>').val("").html("Select Remedial Action Taken MRP"));
                    $.each(dataSet.RemedialActionTakenMRPs, function (i, p) {
                        $('#RemedialActionTakenMRP').append($('<option></option>').val(p.ID).html(p.Description));
                    });

                    // check if the action is edit mode, then after the row data are fetvhing, we called edit data loading 
                    if (action == 'Edit') {
                        loadEditData();
                    }

                },
                error: function (err) {
                    $('#loader').hide();
                    console.log("Failed , " + err);
                    $('.alert-danger-status').html('The system encountered an error while loading the data.Please report this issue to support@credetechnologies.com for assistance.').show();
                }
            });
        }

        // collect all dta and return the object
        function CreateDataObject() {
            var dataSet = {};
            $('.data-member').each(function () {
                const key = $(this).attr("data-member-identifier");
                const val = $(this).val();
                dataSet[key] = val;
            });

            return dataSet;
        }

        // save model data
        function SaveData() {
            
            // check validation
            errorCount = 0;
            $('.invalid-feedback').hide();
            $('.alert-data-validation').hide();
            $('.alert-error-message').hide();

            $('.requied-field').each(function () {
                if ($(this).val().length == 0) {
                    errorCount++;
                    $(this).siblings('.invalid-feedback').show();
                    $('.alert-data-validation').show();
                }
            });

            // if any validation error occurs we dismiss the operation
            if (errorCount > 0)
                return;

            var data = CreateDataObject();

            var obj = { logData: data };

            $.ajax({
                url: "ErrorLogXray.aspx/SaveData",
                type: "POST",
                contentType: 'application/json',
                data: JSON.stringify(obj),
                beforeSend: function () {
                    $("#loader").show();
                },
                success: function (result) {
                    $("#loader").hide();

                    if (result.d != "1") {
                        $('.alert-error-message').show();
                        return;
                    }

                    //reload table
                    table.ajax.reload(function () {
                        $("#loader").hide();
                        $('#largeModal').modal('hide');
                    });

                },
                error: function (err) {
                    $('#loader').hide();
                    console.log("Failed -");
                    console.log(err);
                    $('.alert-error-message').show();
                }
            });

        }

        function DeleteData(event, Id) {
            event.preventDefault();

            var jc = $.confirm({
                title: 'Confirm to Delete',
                content: 'Are you sure you want to delete?',
                buttons: {
                    No: {
                        text: '<i class="fa fa-times" aria-hidden="true"></i> Cancel',
                        btnClass: 'btn btn-warning',
                        action: function () {

                        }
                    },
                    Yes: {
                        text: '<i class="fa fa-check" aria-hidden="true"></i> Delete',
                        btnClass: 'btn btn-danger',
                        action: function () {

                            var data = {
                                "Id": Number(Id)
                            }
                            $.ajax({
                                type: "POST",
                                url: "ErrorLogXray.aspx/DeleteData",
                                contentType: 'application/json',
                                data: JSON.stringify(data),
                                dataType: 'json',
                                beforeSend: function () {
                                    $("#loader").show();
                                },
                                success: function (data) {
                                    if (data.d) {
                                        jc.close();
                                        table.ajax.reload(function () {
                                            $("#loader").hide();

                                        });
                                    }
                                    else {
                                        $("#loader").hide();
                                        $.alert({
                                            title: 'Warning',
                                            content: 'The system encountered an error while deleting the data. Please report this issue to support@credetechnologies.com for assistance.',
                                        });
                                    }
                                },
                                error: function (data) {
                                    $("#loader").hide();
                                    console.log('Error = ');
                                    console.log(data);
                                }
                            });
                            return false;
                        }
                    }
                }
            });
        }

        // edit data saving
        function EditSaveData() {
            // check validation
            errorCount = 0;
            $('.invalid-feedback').hide();
            $('.alert-data-validation').hide();
            $('.alert-error-message').hide();

            $('.requied-field').each(function () {
                if ($(this).val().length == 0) {
                    errorCount++;
                    $(this).siblings('.invalid-feedback').show();
                    $('.alert-data-validation').show();
                }
            });

            // if any validation error occurs we dismiss the operation
            if (errorCount > 0)
                return;

            var data = CreateDataObject();

            var obj = { logData: data };
            obj.logData['Id'] = Number(EditId);

            $.ajax({
                url: "ErrorLogXray.aspx/SaveEditData",
                type: "POST",
                contentType: 'application/json',
                data: JSON.stringify(obj),
                beforeSend: function () {
                    $("#loader").show();
                },
                success: function (result) {
                    $("#loader").hide();

                    if (result.d != "1") {
                        $('.alert-error-message').show();
                        return;
                    }

                    //reload table
                    table.ajax.reload(function () {
                        $("#loader").hide();
                        $('#largeModal').modal('hide');
                    });
                    
                },
                error: function (err) {
                    $('#loader').hide();
                    console.log("Failed -");
                    console.log(err);
                    $('.alert-error-message').show();
                }
            });
        }

        // edit data loading
        function loadEditData() {

            var Qdata = {
                "Id": Number(EditId)
            }

            $.ajax({
               
                type: "POST",
                url: "ErrorLogXray.aspx/GetDataById",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(Qdata),
                dataType: 'json',
                beforeSend: function () {
                    $("#loader").show();
                },
                
                success: function (data) {
                    
                    $("#loader").hide();

                    var result = data.d;
                    var Editdetails = JSON.parse(result);
                    var Dates = new Date(Editdetails.DateOfError);
                    var day = ("0" + Dates.getDate()).slice(-2);
                    var month = ("0" + (Dates.getMonth() + 1)).slice(-2);
                    Dates = Dates.getFullYear() + "-" + (month) + "-" + (day);
           
                    $("#dateoferror").val(Dates);
                    $("#operator").val(Editdetails.Operator);
                    $("#Entering_Item").val(Editdetails.PersonEnteringItem);
                    $("#Suggested_Priority").val(Editdetails.SuggestedPriorityID);
                    $("#Location").val(Editdetails.LocationID);
                    $("#Modality").val(Editdetails.ModalityID);
                    $("#Error_Category").val(Editdetails.ErrorCategoryID);
                    $("#Error_Description").val(Editdetails.DescriptionOfError);
                    $("#Possible_Consequence").val(Editdetails.PossibleConsequenceID);
                    $("#RemedialActionTakenMRP").val(Editdetails.RemedialActionTakenMRPID);
                    $("#Remedial_Action").val(Editdetails.RemedialAction);
                    $("#notes_re").val(Editdetails.Notes);

                },
                error: function (data) {
                    $("#loader").hide();
                    onsole.log("Failed -");
                    console.log(data);
                    $('.alert-error-message-2').show();
                }
            });
        }

        // edit event trigger
        function EditData(event, Id) {
            event.preventDefault();

            EditId = Id;

            ShowPanel('Edit');

        }

    
    </script>
</asp:Content>
