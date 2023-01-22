<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportContainer.aspx.cs" Inherits="WebPortal.ReportViewer.ReportContainer" ValidateRequest="false" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9">
    <title>Crede Clean Hands - View report</title>
    <link rel="shortcut icon" href="~/images/favicon.ico" type="image/x-icon" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--Bootstrap css-->
    <%: Styles.Render("~/bundles/bootstrap_css") %>
    <!--JQuery UI css-->
    <%: Styles.Render("~/bundles/jquery_ui_css") %>

    <!--Font awesome-->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" />

    <!--Google fonts-->
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web:200,200i,300,300i,400,400i,600,600i,700,700i,900" rel="stylesheet" />

    <!--Custom css files-->
      <%: Styles.Render("~/bundles/default_css") %>
      <%: Scripts.Render("~/bundles/bootstrap_js") %>
      <%: Scripts.Render("~/bundles/jquery_ui_js") %>

      <%: Scripts.Render("~/bundles/tinymce") %>

    <script type="text/javascript">
        if (!(window.console && console.log)) {
            console = {
                log: function () { },
                debug: function () { },
                info: function () { },
                warn: function () { },
                error: function () { }
            };
        }

        if (window.getComputedStyle != null) {
            var orginalGetComputedStyle = window.getComputedStyle;
            window.getComputedStyle = function (element, parm) {
                try {
                    return orginalGetComputedStyle(element, null);
                }
                catch (err) {
                    return orginalGetComputedStyle(document.getElementsByTagName("body")[0], null);
                }
            }
        }
        
        tinyMCE.baseURL = "/Scripts/tinymce/";
        tinyMCE.init({
            // General options
            height: 184,
            autoresize_min_height: 150,
            autoresize_max_height: 200,
            content_css: "../Content/style-custom.css",
            menubar: false,
            statusbar: false,
            mode: "textareas",
            selector: "#<%=txtMessage.ClientID %>",
            theme: "silver",
            toolbar: "forecolor | undo redo | bold italic | fontselect | fontsizeselect ",
            theme_advanced_fonts: "Arial=arial,helvetica,sans-serif;Courier New=courier new,courier,monospace;AkrutiKndPadmini=Akpdmi-n",
            theme_advanced_font_sizes: "10px,12px,14px,16px,24px",
            paste_auto_cleanup_on_paste: true,
            paste_remove_spans: true,
            paste_use_dialog: false,
            paste_convert_headers_to_strong: false,
            paste_strip_class_attributes: "all",
            paste_remove_styles: true,
            paste_retain_style_properties: "",
            plugins: ['autoresize'],
            paste_preprocess: function (pl, o) {    // Replace <div> with <p>
                o.content = o.content.replace(/<div>/gi, "<p>");
                o.content = o.content.replace(/<\/div>/gi, "</p>");
                o.content = o.content.replace(/<\r\n/gi, "\n");
                o.content = o.content.replace(/<\n\n/gi, "\n");
                o.content = o.content.replace(/<\n\n/gi, "\n");

                // Replace empty styles
                o.content = o.content.replace(/<style><\/style>/gi, "");
                //remove spaces
                o.content = o.content.replace(/&nbsp;/gi, "");
                o.wordContent = true;
            },
            setup: function (ed) {
                ed.on('init', function (ed) {
                    ed.pasteAsPlainText = true;
                });
            }
        }); 

        function SendButtonClick() {
            var DataValue = {
                "reportName": $("#<%=hdnReportName.ClientID %>").val()
            }

            //console.log(JSON.stringify(DataValue));

            $.ajax({
                type: "POST",
                url: "ReportContainer.aspx/bindEmail",
                data: JSON.stringify(DataValue),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (value) {
                    var emailContents = new Array();
                    emailContents = value.d;
                    //console.log("emailContents = " + emailContents);
                    //console.log("report name = " + $("#<%=hdnReportName.ClientID %>").val());
                    $("#<%=txtSubject.ClientID %>").val($("#<%=hdnReportName.ClientID %>").val() + " " + emailContents[0]);
                    //console.log('text = ' + emailContents[1]);
                    tinymce.get('<%=txtMessage.ClientID %>').setContent(emailContents[1])
                    $('#myModal').modal({
                        show: true,
                        backdrop: 'static',
                        keyboard: false
                    });
                },
                error: function (response) {
                    //alert(response);
                    console.log(response);
                }
            });
        }

        function ClosePopup() {
            console.log('in ClosePopup function');
            $('#<%=txtTo.ClientID %>').val("");
            $("#<%=txtSubject.ClientID %>").val("");
            $("#<%=txtMessage.ClientID %>").val("");
            $("#<%=hdn_message.ClientID %>").val("");
            $('#emailErrorEmpty').html('').hide();
            $('#emailErrorInvalid').html('').hide();
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        $(document).ready(function () {
            
        });

        var Email = {
            _validator: function (email) {
                return (/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i.test(email) && /^[a-z0-9!\#$%&\'*+\/=?^_`{|}~-]+(?:\.[a-z0-9!\#$%&\'*+\/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel)$/i.test(email));
            },
            isValid: function (email) {
                return Email._validator(email);
            }
        };


        function emailCheck() {

            var emailValue = $('#<%=txtTo.ClientID %>').val();
            //console.log("emailValue = " + emailValue);
            var emailSplit = emailValue.split(/[\s,]+/);
            //console.log("emailSplit = " + emailSplit);
            var invalidEmails = [], invalidMessages = '<br /> <span class="failureNotification">Invalid e-mails:</span>';
            var state = true;

            if ($('#<%=txtTo.ClientID %>').val() != "") {//if defined, non-empty string etc.
                $('#emailErrorEmpty').hide();

                invalidEmails = $(emailSplit).filter(function (idx) {
                    state = false;
                    //Email.isValid(this);
                    return !(Email.isValid(this));
                });

                if (invalidEmails.length > 0) {

                    for (var j = 0, len = invalidEmails.length; j < len; ++j) {
                        invalidMessages += '<em>' + invalidEmails[j] + '</em><br />';
                        $('#emailErrorInvalid').html(invalidMessages).show();
                    }

                    //stop submit action           
                    state = false;

                } else {
                    state = true;
                    $('#emailErrorInvalid').hide();
                }

            } else {//empty string, undefined etc.

                state = false;
                $('#emailErrorEmpty').show();

            }
            if (state) {
                //console.log('html = ' + tinymce.activeEditor.getContent());
                $('#<%=hdn_message.ClientID %>').val(tinymce.get('<%=txtMessage.ClientID %>').getContent());
            }
            return state;
            /* submits to server if action wasn't stopped above */
        }

        function ErrorLog(Message) {
            console.error(Message);
        }
    </script>
</head>
<body onkeydown = "return (event.keyCode!=13)">
    <div class="container" style="width:fit-content !important;  width: -moz-fit-content !important;">
        <form id="form1" runat="server">
            <asp:scriptmanager id="ScriptManager1" runat="server"   ScriptMode="Release">
            </asp:scriptmanager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="pull-left">
                                <asp:LinkButton OnClick="Button1_Click" ID="Button1" runat="server" CssClass="btn-circle btn btn-back">
                                    <i class="fa fa-arrow-left" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <h3 class="panel-title" id="lblrptName" runat="server">Report list</h3>
                            </div>
                            <div class="pull-right">
                                <button type="button" class="btn btn-primary" onclick="SendButtonClick();" id="btn" runat="server">
                                    <i class="fa fa-paper-plane-o" aria-hidden="true"></i> Send
                                </button>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body report-panal-page">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="tempclass" Height="100%" Width="100%">
                            </rsweb:ReportViewer>
                            <asp:HiddenField runat="server" ID="hdn" />
                            <asp:HiddenField ID="hdnReportId" runat="server" Visible="false" />
                            <asp:HiddenField ID="hdnReportCode" runat="server" Visible="false" />
                            <asp:HiddenField ID="hdnReportKey" runat="server" Visible="false" />
                            <asp:HiddenField ID="hdnReportCategory" runat="server" Visible="false" />
                            <asp:HiddenField ID="hdnReportName" runat="server"/>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

             <!-- Modal -->
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" onclick="ClosePopup();">&times;</button>
                                    <h4 class="modal-title">Send report</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="lblTo" runat="server" AssociatedControlID="txtTo" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">To <span class="red-star">*</span> :</asp:Label>
                                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                                <input id="txtTo" runat="server" class="form-control" name="email_addresses" placeholder="list emails, separated by commas" type="text" value="" />
                                                <div id="emailErrorEmpty" class="failureNotification" style="display:none;">Please enter an email address!</div>
                                                <div id="emailErrorInvalid">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblSubject" runat="server" AssociatedControlID="txtSubject" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Subject <span class="red-star">*</span> :</asp:Label>
                                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                                <input id="txtSubject" runat="server" class="form-control" name="email_addresses" type="text" value="" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblMessage" runat="server" AssociatedControlID="txtMessage" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Message <span class="red-star">*</span> :</asp:Label>
                                            <asp:HiddenField ID="hdnMessageLoad" runat="server"/>
                                            <div class="col-xs-12 col-sm-7 col-lg-8">
                                                <asp:textbox runat="server" id="txtMessage" CssClass="form-control textarea" textmode="MultiLine" Rows="3"></asp:textbox>
                                                <input type="hidden" id="hdn_message" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-warning" onclick="ClosePopup();">
                                        <i class="fa fa-times" aria-hidden="true"></i> Cancel
                                    </button>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="javascript:if(!emailCheck())return false" OnClick="btn_Click" CssClass="btn btn-success">
                                        <i class="fa fa-paper-plane-o" aria-hidden="true"></i> Send
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
    <div id="overlay" class="web_dialog_overlay">
    </div>
    
    <div id="dialog" class="web_dialog">
    </div>
    
    </form>
    </div>
</body>
</html>