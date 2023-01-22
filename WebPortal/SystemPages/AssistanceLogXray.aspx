<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssistanceLogXray.aspx.cs" Inherits="WebPortal.SystemPages.AssistanceLogXray" %>
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
                    <li class="breadcrumb-item active">Need Assistance Log</li>
                </ol>
            </nav>
        </div>
    <section class="section">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card hdl-card">
                            <div class="card-header">
                                <h5 class="card-title">Need Assistance Log</h5>
                                <button type="button" class="btn btn-primary btn-sm" runat="server" onclick="ShowPanel('create')">
                                    <i class="bi bi-plus"></i> Add New
                                  </button>
                            </div>
                            <div class="card-body">
                                 <div class="table-responsive">
                                     <table id="myTable" class="table table-bordered table-striped hld-table" width="99.9%">
                                        <thead>
                                            <tr>
                                                <th class ="hide hide1">Id</th>
                                              <th>Date and Time</th>
                                              <th>Operator</th>
                                              <th>Operator Name</th>
                                              <th>Item Entering person</th>
                                              <th>Suggested Priority</th>
                                              <th>Location</th>
                                              <th>Area</th>
                                              <th>Modality</th>
                                              <th>Description of Issue</th>
                                              <th>Referred to / Responder</th>
                                              <th>Response</th>
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
                    <h5 id="popup_header" class="modal-title">Add New Assistance Log</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                              <span aria-hidden="true">&times;</span>
                    </button>
                      
                  </div>
                  <div class="modal-body">
                      <form novalidate="" action="" method="" id="AddErrorLog">
                            <div class="alert alert-success alert-dismissible fade show col-md-12" role="alert" style="display: none">
                                  <strong>Holy guacamole!</strong> A simple success alert—check it out!
                                <button type="button" class="close" data-bs-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>

                                <div class="alert alert-danger alert-dismissible fade show col-md-12 alert-danger-status" role="alert" style="display: none">
                                  <strong>Holy guacamole!</strong> A simple danger alert—check it out!
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
                                  <strong>Holy guacamole!</strong> A simple warning alert—check it out!
                                <button type="button" class="close" data-bs-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                      
                         <div class="row g-3">
                             <div class="col-md-6 form-group">
                                  <label class="form-label">Date <span class="text-danger" title="This is required">*</span> :</label>
                                   <input type="date" id="dateAssis" class="form-control form-control-sm requied-field data-member" data-member-identifier="Date">
                                 <div class="invalid-feedback">Please enter a valid date.</div>
                             </div>
                             <div class="col-md-6 form-group">
                                   <label class="form-label">Time <span class="text-danger" title="This is required">*</span> :</label>
                                   <input placeholder="Please Enter Operator" type="time" id="timeAssis" class="form-control form-control-sm requied-field data-member" data-member-identifier="Time">
                                 <div class="invalid-feedback">Please enter a valid time.</div>
                              </div>
                             
                             <div class="col-md-6 form-group">
                                 <label class="form-label">Operator <span class="text-danger" title="This is required">*</span> :</label>
                                 <select class="form-control form-control-sm requied-field data-member" id="OperatorIdy" aria-label="Operator" data-member-identifier="OperatorId">
                                 </select>
                                 <div class="invalid-feedback">Please select a valid operator.</div>
                            </div>
                             
                             <div class="col-md-6 form-group" id="OperatorNameDiv">
                                 <label class="form-label">Operator Name <span class="text-danger" title="">*</span> :</label>
                                 <input placeholder="Please Enter Operator Name" type="text" id="Operator_Name" class="form-control form-control-sm requied-field data-member" data-member-identifier="OperatorName">
                                 <div class="invalid-feedback">Please enter an operator name.</div>
                             </div>
                             
                             <div class="col-md-6 form-group" id="PersonNameDiv">
                                  <label class="form-label">Person Entering Item <span class="text-danger" title="This is required">*</span> :</label>
                                  <input placeholder="Please Enter Person Name" type="text" id="Item_Entering_person" class="form-control form-control-sm data-member" data-member-identifier="PersonName">
                                 <div class="invalid-feedback">Please enter a person name.</div>
                             </div>
                             <div class="col-md-6 form-group">
                                  <label class="form-label">Suggested Priority <span class="text-danger" title="This is required">*</span> :</label>
                                  <select class="form-control form-control-sm requied-field data-member" id="Suggested_Priority" aria-label="Suggested Priority" data-member-identifier="SuggestedPriorityId">
                                  </select>
                                 <div class="invalid-feedback">Please select a valid suggested priority.</div>
                              </div>
                              
                             <div class="col-md-6 form-group">
                                 <label class="form-label">Location <span class="text-danger" title="This is required">*</span> :</label>
                                 <select class="form-control form-control-sm requied-field data-member field-change-trigger" id="Location" aria-label="Location" data-member-identifier="LocationId">
                                 </select>
                                 <div class="invalid-feedback">Please select a valid location.</div>
                             </div>
                             <div class="col-md-6 form-group">
                                  <label class="form-label">Area <span class="text-danger" title="This is required">*</span> :</label>
                                  <select class="form-control form-control-sm requied-field data-member field-change-trigger" id="Area" aria-label="Area" data-member-identifier="AreaId">
                                  </select>
                                 <div class="invalid-feedback">Please select a valid area.</div>
                             </div>
                             <div class="col-md-6 form-group">
                                   <label class="form-label">Modality <span class="text-danger" title="This is required">*</span> :</label>
                                   <select class="form-control form-control-sm requied-field data-member field-change-trigger" id="Modality" aria-label="Modality" data-member-identifier="ModalityId">
                                   </select>
                                 <div class="invalid-feedback">Please select a valid modality.</div>
                              </div>
                             <div class="col-md-6 form-group">
                                  <label class="form-label">Description of Issue :</label>
                                  <input placeholder="Please Enter Description of Issue" type="text" id="Description_Issue" class="form-control form-control-sm data-member" data-member-identifier="DescriptionOfIssue">
                              </div>

                            <div class="col-md-6 form-group">
                                 <label class="form-label">Referred to / Responder :</label>
                                 <select class="form-control form-control-sm data-member field-change-trigger" id="Referred" aria-label="Referred to / Responder" data-member-identifier="ReferredTo_ResponderID">
                                 </select>
                            </div>
                             
                             <div class="col-md-6 form-group">
                                  <label class="form-label">Response :</label>
                                  <select class="form-control form-control-sm data-member" id="Response" aria-label="Response" data-member-identifier="ResponseId">
                                  </select>
                             </div>
                             
                             <div class="col-md-6 form-group">
                                  <label class="form-label" id="note_not_required">Notes :</label>
                                 <label class="form-label" id="note_required">Notes <span class="text-danger" title="This is required">*</span> :</label>
                                  <textarea class="form-control form-control-sm data-member" id="notes_re" rows="2" data-member-identifier="Notes"></textarea>
                                 <div class="invalid-feedback">Please enter a valid note.</div>
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

        var options = { weekday: false, year: 'numeric', month: 'long', day: 'numeric' };

        $(document).ready(function () {

            LoadTable();

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

             // operator select event
             $("#OperatorIdy").on('change', function () {    // 2nd (A)

                 var selected = $(this).find('option:selected');

                 if (selected.text().toLowerCase() == "direct") {
                     $('#OperatorNameDiv').show();
                     $('#Operator_Name').addClass('requied-field');

                     $('#PersonNameDiv').hide();
                     $('#Item_Entering_person').val("");
                     $('#Item_Entering_person').removeClass('requied-field');
                 } else {
                     $('#OperatorNameDiv').show();
                     $('#Operator_Name').addClass('requied-field');

                     $('#PersonNameDiv').show();
                     $('#Item_Entering_person').addClass('requied-field');
                 }

             });
            

             // operator select event
             $(".field-change-trigger").on('change', function () {    // 2nd (A)

                 var requiredCount = 0;


                 requiredCount = NoteRequiredDunction();

                 if (requiredCount > 0) {
                     $('#notes_re').addClass('requied-field');
                     $('#note_required').show();
                     $('#note_not_required').hide();
                 } else {
                     $('#notes_re').removeClass('requied-field');
                     $('#note_required').hide();
                     $('#note_not_required').show();
                 }

             });

             // response select event
             $("#Response").on('change', function () {    // 2nd (A)
                 var selected = $(this).find('option:selected');

                 $('#notes_re').removeClass('requied-field');

                 if (selected.text().toLowerCase() == "completed") {
                     document.getElementById("dateAssis").valueAsDate = new Date();
                     document.getElementById('timeAssis').value = new Date().toLocaleTimeString([], {
                         hourCycle: 'h23',
                         hour: '2-digit',
                         minute: '2-digit'
                     });
                     
                 }

                 var requiredCount = NoteRequiredDunction();

                 if (requiredCount > 0) {
                     $('#notes_re').addClass('requied-field');
                     $('#note_required').show();
                     $('#note_not_required').hide();
                 } else {
                     $('#notes_re').removeClass('requied-field');
                     $('#note_required').hide();
                     $('#note_not_required').show();
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

        function NoteRequiredDunction() {
            var requiredCount = 0;

            if ($('#Location').find('option:selected').text().toLowerCase() == "other") {
                requiredCount++;
            }

            if ($('#Area').find('option:selected').text().toLowerCase() == "other") {
                requiredCount++;
            }

            if ($('#Modality').find('option:selected').text().toLowerCase() == "other") {
                requiredCount++;
            }

            if ($('#Referred').find('option:selected').text().toLowerCase() == "other") {
                requiredCount++;
            }

            if ($('#Response').find('option:selected').text().toLowerCase() == "refered to 3rd party") {
                requiredCount++;
            }

            return requiredCount;
        }

        // get time with AM PM from date
        function formatAMPM(date) {
            var hours = date.getHours();
            var minutes = date.getMinutes();
            var ampm = hours >= 12 ? 'pm' : 'am';
            hours = hours % 12;
            hours = hours ? hours : 12; // the hour '0' should be '12'
            minutes = minutes < 10 ? '0' + minutes : minutes;
            var strTime = hours + ':' + minutes + ' ' + ampm;
            return strTime;
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
            $('.note_required').hide();

            $('#OperatorNameDiv').hide();
            $('#PersonNameDiv').hide();
            $('#Operator_Name').removeClass('requied-field');
            $('#Item_Entering_person').removeClass('requied-field');

            $('#notes_re').removeClass('requied-field');
            $('#note_required').hide();
            $('#note_not_required').show();


            $('#popup_header').text('');
            if (action == 'create') { // new workflow

                $('#btnSave').show();
                $('#btnEdit').hide();
                $('#popup_header').text('Add New Need Assistance Log');

                document.getElementById("dateAssis").valueAsDate = new Date();
                document.getElementById('timeAssis').value = new Date().toLocaleTimeString([], {
                    hourCycle: 'h23',
                    hour: '2-digit',
                    minute: '2-digit'
                });
            } else { // edit workflow

                $('#btnSave').hide();
                $('#btnEdit').show();

                $('#popup_header').text('Edit Need Assistance Log');
            }

            $('#largeModal').modal('show');
        }

        // clean all the dropdown values
        function ClearData() {
            $("#OperatorIdy").empty();
            $("#Suggested_Priority").empty();
            $("#Location").empty();
            $("#Area").empty();
            $("#Modality").empty();
            $("#Referred").empty();
            $("#Response").empty();
        }

        // clear all the form data
        function clearcomponent() {

            $("#Description_Issue").val("");
            $("#notes_re").val("");
            $("#Item_Entering_person").val("");
            $("#Operator_Name").val("");
            
        }

        // load dropdown row data
        function LoadRowData(action) {
            $.ajax({
                url: "AssistanceLogXray.aspx/LoadRowData",
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

                    console.log(result.d);

                    // Operator
                    $('#OperatorIdy').append($('<option></option>').val("").html("Select Operator"));
                    $.each(dataSet.Operators, function (i, p) {
                        $('#OperatorIdy').append($('<option></option>').val(p.ID).html(p.Description));
                    });

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
                    
                    // Area
                    $('#Area').append($('<option></option>').val("").html("Select Area"));
                    $.each(dataSet.Areas, function (i, p) {
                        $('#Area').append($('<option></option>').val(p.ID).html(p.Description));
                    });

                    // Referred
                    $('#Referred').append($('<option></option>').val("").html("Select Referred to / Responder"));
                    $.each(dataSet.ReferredTo, function (i, p) {
                        $('#Referred').append($('<option></option>').val(p.ID).html(p.Description));
                    });

                    // Referred
                    $('#Response').append($('<option></option>').val("").html("Select Response"));
                    $.each(dataSet.Responses, function (i, p) {
                        $('#Response').append($('<option></option>').val(p.ID).html(p.Description));
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

            if (data["ReferredTo_ResponderID"] == "")
                data["ReferredTo_ResponderID"] = 0;

            if (data["ResponseId"] == "")
                data["ResponseId"] = 0;

            var obj = { logData: data };

            $.ajax({
                url: "AssistanceLogXray.aspx/SaveData",
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
                    "url": "AssistanceLogXray.aspx/LoadTable",
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
                    {
                        'data': null, className: 'text-center no-sort', 'render': function (data, type, row)
                        {
                            return `${new Date(data.DateTimeLog).toDateString().split(' ').slice(1).join(' ')} &nbsp ${formatAMPM(new Date(data.DateTimeLog))}`
                        }
                    },
                    { 'data': 'Operators', className: 'text-center' },
                    { 'data': 'OperatorName', className: 'text-center' },
                    { 'data': 'PersonName', className: 'text-center' },
                    { 'data': 'SuggestedPriority', className: 'text-center' },
                    { 'data': 'Location', className: 'text-center' },
                    { 'data': 'Area', className: 'text-center' },
                    { 'data': 'Modality', className: 'text-center' },
                    { 'data': 'DescriptionOfIssue', className: 'text-center' },
                    { 'data': 'ReferredTo', className: 'text-center' },
                    { 'data': 'Responses', className: 'text-center' },
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

        // edit data loading
        function loadEditData() {

            var Qdata = {
                "Id": Number(EditId)
            }

            $.ajax({
                type: "POST",
                url: "AssistanceLogXray.aspx/GetDataById",
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

                    document.getElementById("dateAssis").valueAsDate = new Date(Editdetails.DateTimeLog);
                    document.getElementById('timeAssis').value = new Date(Editdetails.DateTimeLog).toLocaleTimeString([], {
                        hourCycle: 'h23',
                        hour: '2-digit',
                        minute: '2-digit'
                    });
                    

                    $("#OperatorIdy").val(Editdetails.OperatorId);
                    $("#Operator_Name").val(Editdetails.OperatorName);
                    $("#Item_Entering_person").val(Editdetails.PersonName);
                    $("#Suggested_Priority").val(Editdetails.SuggestedPriorityId);
                    $("#Location").val(Editdetails.LocationId);
                    $("#Area").val(Editdetails.AreaId);
                    $("#Modality").val(Editdetails.ModalityId);
                    $("#Description_Issue").val(Editdetails.DescriptionOfIssue);
                    $("#Referred").val(Editdetails.ReferredTo_ResponderID);
                    $("#Response").val(Editdetails.ResponseId);
                    $("#notes_re").val(Editdetails.Notes);


                    if (Editdetails.Operators.toLowerCase() == "direct") {
                        $('#OperatorNameDiv').show();
                        $('#Operator_Name').addClass('requied-field');

                        $('#PersonNameDiv').hide();
                        $('#Item_Entering_person').removeClass('requied-field');
                    } else {
                        $('#OperatorNameDiv').show();
                        $('#Operator_Name').addClass('requied-field');

                        $('#PersonNameDiv').show();
                        $('#Item_Entering_person').addClass('requied-field');
                    }

                    var requiredCount = NoteRequiredDunction();
                    if (requiredCount > 0) {
                        $('#notes_re').addClass('requied-field');
                        $('#note_required').show();
                        $('#note_not_required').hide();
                    } else {
                        $('#notes_re').removeClass('requied-field');
                        $('#note_required').hide();
                        $('#note_not_required').show();
                    }

                },
                error: function (data) {
                    $("#loader").hide();
                    onsole.log("Failed -");
                    console.log(data);
                    $('.alert-error-message-2').show();
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

            var requiredCount = NoteRequiredDunction();

            if (requiredCount > 0) {
                $('#notes_re').addClass('requied-field');
                $('#note_required').show();
                $('#note_not_required').hide();
            } else {
                $('#notes_re').removeClass('requied-field');
                $('#note_required').hide();
                $('#note_not_required').show();
            }

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
            debugger;
            var data = CreateDataObject();

            if (data["ReferredTo_ResponderID"] == "" || !data["ReferredTo_ResponderID"])
                data["ReferredTo_ResponderID"] = 0;

            if (data["ResponseId"] == "" || !data["ResponseId"])
                data["ResponseId"] = 0;

            var obj = { logData: data };
            obj.logData['Id'] = Number(EditId);

            $.ajax({
                url: "AssistanceLogXray.aspx/SaveEditData",
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

        // edit event trigger
        function EditData(event, Id) {
            event.preventDefault();

            EditId = Id;

            ShowPanel('Edit');

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

                            debugger;

                            var data = {
                                "Id": Number(Id)
                            }
                            $.ajax({
                                type: "POST",
                                url: "AssistanceLogXray.aspx/DeleteData",
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
                                            title: 'Warning !',
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

        
    </script>
</asp:Content>
