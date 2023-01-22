<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebPortal.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="~/images/favicon.ico" type="image/x-icon" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=0.9" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <asp:PlaceHolder runat="server">
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/bootstrap.min.css")%>"></link>
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/themes/base/jquery-ui.min.css")%>"></link>
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/themes/base/jquery-ui.theme.min.css")%>"></link>
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/font-awesome.min.css")%>"></link>
        <!--Google fonts-->
        <link href="https://fonts.googleapis.com/css?family=Titillium+Web:200,200i,300,300i,400,400i,600,600i,700,700i,900" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/jquery.fancybox.css")%>"></link>
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/jquery.fancybox-buttons.css")%>"></link>
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/jquery.fancybox-thumbs.css")%>"></link>
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/jquery-confirm.min.css")%>"></link>
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/Site.css")%>"></link>
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/default.css")%>"></link>

        <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery-3.6.0.min.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/bootstrap.min.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery-ui-1.13.2.min.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery-ui-touch-punch.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery.mousewheel-3.0.6.pack.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery-ui-sliderAccess.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery.fancybox.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery.fancybox.pack.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery.fancybox-buttons.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery.fancybox-thumbs.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery.fancybox-media.js")%>'></script>
       <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery-confirm.min.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/jquery.validate.min.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/additional-methods.min.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/Scripts/inputmask/jquery.inputmask.js")%>'></script>

        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/clientjs@0.1.11/dist/client.min.js"></script>
    </asp:PlaceHolder>
        <script type="text/javascript">
           var jc;
            var client;
            $(document).ready(function () {
                
                client = new ClientJS();
                $(".various").fancybox({
                    maxWidth: 520,
                    maxHeight: 190,
                    fitToView: false,
                    width: '70%',
                    height: '70%',
                    autoSize: false,
                    closeClick: false,
                    openEffect: 'elastic',
                    closeEffect: 'elastic',
                    'hideOnContentClick': false,
                    'type': 'iframe',
                    beforeShow: function () {
                        console.log('h = ' + $('.fancybox-iframe').contents().find('#div_1').height());
                        this.width = '450px';
                        this.height = '530px';
                    }
                });
                
                $(".fancybox").fancybox();

                $('#UserName').on('change', function () {
                    $('#Form1').valid();
                });

                var browser = detect_browser();
                console.log(browser);
                if (!browser.isChrome) {
                    $('#divtxt').show();
                }
                else {
                    $('#divtxt').hide();
                }

                $('#Form1').on('submit', function () {
                    if ($(this).valid()) {
                        return true;
                    }
                    else {
                        return false;
                    }
                });
                $('#Form1').validate({
                    rules: {
                        UserName: {
                            required: true,
                            minlength: 3
                        },
                        Password: {
                            required: true,
                            onedigit: true,
                            onelowercase: true,
                            oneUppercase: true,
                            minlength: 8
                        }
                    },
                    messages: {
                        UserName: {
                            required: "Please enter valid Username"
                        },
                        Password: {
                            required: "Please enter valid Password"
                        }
                    },
                    submitHandler: function () {
                        $('#Form1')[0].submit();
                    },
                    highlight: function (element) {
                        $(element).closest('.form-group').removeClass('success').addClass('error');
                        $(element).closest('.form-group').css('border-bottom', '2px solid #a94442');
                    },
                    success: function (element) {
                        element.text('Looks good').addClass('valid')
                            .closest('.form-group').removeClass('error').addClass('success');
                        $(element).closest('.form-group').removeClass('error').addClass('success');
                        $(element).closest('.form-group').css('border-bottom', '2px solid #efefef');
                    },
                    errorElement: 'label',
                    errorClass: 'form-invalid',
                    errorPlacement: function (error, element) {
                        if (element.length) {
                            error.insertAfter(element);
                        } else {
                            error.insertAfter(element);
                        }
                    }
                });

                $(".iframe").fancybox({
                    type: 'iframe'
                });

                $('input.form-control').on('input', function () {
                    $('span.form-invalid').hide();
                });

                $('#hdnFingerPrint').val(client.getFingerprint());
                
                
                //password toggle
                $('.toggle').on('click', function() {
                    $('.container').stop().addClass('active');
                });
    
                $('.close').on('click', function() {
                    $('.container').stop().removeClass('active');
                });
    
                $(".toggle-password").click(function() {
    
                  $(this).toggleClass("fa-eye fa-eye-slash");
                  var input = $($(this).attr("toggle"));
                  if (input.attr("type") == "password") {
                    input.attr("type", "text");
                  } else {
                    input.attr("type", "password");
                  }
                });
            });

            function two_way_oauth(user_id, email, phone_number) {
                var content = '' +
                    '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top:15px;">' +
                    '<div class="form" id="div_id_otp_method">' +
                    '<div class="radio">' +
                    '<label><input type="radio" name="otp_method" value="sms">SMS Verification code to ' + modify_phone_number(phone_number) + '</label>' +
                    '</div>' +
                    '<div class="radio">' +
                    '<label><input type="radio" name="otp_method" value="email">Email Verification code to ' + modify_email(email) + '</label>' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-horizontal" id="div_id_otp_confirm" style="display:none;">' +
                    '<div class="form-group">' +
                    '<label class="control-label col-sm-4" for="txtOtp">Verification Code :</label>' +
                    '<div class="col-sm-8">' +
                    '<input type="text" class="form-control" id="txtOtp" autocomplete="off" placeholder="Enter your Verification Code here">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<div class="col-sm-offset-4 col-sm-8">' +
                    '<label><input type="checkbox" id="cbRemember"> Do not ask again on this browser/device.</label>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';
                jc = $.confirm({
                    title: '2-Step Verification.',
                    columnClass: 'col-md-offset-2 col-md-8',
                    content: content,
                    closeIcon: true,
                    closeIconClass: 'fa fa-close',
                    buttons: {
                        Send: {
                            text: '<i class="fa fa-check" aria-hidden="true"></i> Send',
                            btnClass: 'btn btn-success',
                            action: function () {
                                var SendBtn = this.buttons.Send;
                                var SubmitBtn = this.buttons.Submit;
                                var div_id_otp_method = this.$content.find('#div_id_otp_method').hide();
                                var div_id_otp_confirm = this.$content.find('#div_id_otp_confirm').show();
                                var _self = this;
                                var method = this.$content.find('input[type="radio"][name="otp_method"]:checked').val();
                                var method_value;
                                if (method == "sms") {
                                    method_value = phone_number;
                                }
                                else {
                                    method_value = email;
                                }
                                var data = {
                                    "UserID": Number(user_id),
                                    "method": method,
                                    "method_value": method_value
                                }
                              
                                $.ajax({
                                    url: "Login.aspx/SendOTP",
                                    type: "POST",
                                    contentType: 'application/json',
                                    async: false,
                                    data: JSON.stringify(data),
                                    beforeSend: function () {
                                        //$("#loader").show();
                                    },
                                    success: function (result) {
                                        //$("#loader").hide();
                                        if (result.d == "Message queued successfully") {
                                            div_id_otp_method.hide();
                                            div_id_otp_confirm.show();
                                            SendBtn.hide();
                                            SubmitBtn.show();
                                            _self.setTitle('2-Step Verification - Enter Verification Code.');
                                        }
                                    },
                                    error: function (err) {
                                        //$('#loader').hide();
                                        console.log("Failed , " + err);
                                    }
                                });
                                return false;
                            }
                        },
                        Submit: {
                            text: '<i class="fa fa-check" aria-hidden="true"></i> Submit',
                            btnClass: 'btn btn-success',
                            action: function () {
                                var data = {
                                    "UserID": Number(user_id),
                                    "otp": this.$content.find('#txtOtp').val().trim(),
                                    'remember': this.$content.find('#cbRemember').is(':checked'),
                                    'browser_fingerprint': client.getFingerprint()
                                }

                                $.ajax({
                                    url: "Login.aspx/VerifyOTP",
                                    type: "POST",
                                    contentType: 'application/json',
                                    async: false,
                                    data: JSON.stringify(data),
                                    beforeSend: function () {
                                        //$("#loader").show();
                                    },
                                    success: function (result) {
                                        //$("#loader").hide();
                                        if (result.d == "Success") {
                                            jc.close();
                                            __doPostBack('btnPassExpiredCheck', '');
                                        }
                                        else {
                                            $.alert(result.d);
                                        }
                                    },
                                    error: function (err) {
                                        //$('#loader').hide();
                                        console.log("Failed , " + err);
                                    }
                                });
                                return false;
                            }
                        }
                    },
                    onContentReady: function () {
                        //this.$content.find('#txtSurvey').val(SurveyName);
                        var Send = this.buttons.Send;
                        var SubmitBtn = this.buttons.Submit;
                        SubmitBtn.hide();
                        Send.disable();
                        this.$content.find('input[type="radio"][name="otp_method"]').on('change', function () {
                            if ($(this).val().length > 0) {
                                Send.enable();
                            }
                            else {
                                Send.disable();
                            }
                        });
                    }
                });
            }


            function fetch_user_phone_number(user_id, email) {
                var content = '' +
                    '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
                    '<div class="form-horizontal">' +
                    '<div id="div_id_fetch_telephone">' +
                    '<div class="form-group" style="margin-top: 11px;">' +
                    '<div class="col-sm-5">' +
                    '<div class="radio">' +
                    '<label class="control-label " for="txtOtp"><input type="radio" name="method" value="tel" checked>Mobile Number :</label>' +
                    '</div>' +
                    '</div>' +
                    '<div class="col-sm-7">' +
                    '<div class="input-group">' +
                    '<span class="input-group-addon">+1</span>' +
                    '<input type="text" class="form-control" id="txtPhoneNumber" autocomplete="off" placeholder="Enter your Phone Number here">' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<div class="col-sm-5">' +
                    '<div class="radio">' +
                    '<label class="control-label "><input type="radio" name="method" value="email"> Email Verification Code to : </label>' +
                    '</div>' +
                    '</div>' +
                    '<div class="col-sm-7" style="margin-top: 12px;">' +
                    modify_email(email) +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '<div id="div_id_otp_confirm" style="display:none;">' +
                    '</br>' +
                    '<div class="form-group">' +
                    '<label class="control-label col-sm-2" for="txtOtp"> Verification Code : </label>' +
                    '<div class="col-sm-10">' +
                    '<input type="password" class="form-control" id="txtOtp" autocomplete="off" placeholder="Enter your Verification Code here">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<div class="col-sm-offset-2 col-sm-10">' +
                    '<label><input type="checkbox" id="cbRemember"> Do not ask again on this browser/device.</label>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';
                jc = $.confirm({
                    title: '2-Step Verification.',
                    columnClass: 'col-md-offset-2 col-md-8',
                    content: content,
                    closeIcon: true,
                    closeIconClass: 'fa fa-close',
                    buttons: {
                        Send: {
                            text: '<i class="fa fa-check" aria-hidden="true"></i> Send',
                            btnClass: 'btn btn-success',
                            action: function () {
                                var SendBtn = this.buttons.Send;
                                var SubmitBtn = this.buttons.Submit;
                                var div_id_fetch_telephone = this.$content.find('#div_id_fetch_telephone').hide();
                                var div_id_otp_confirm = this.$content.find('#div_id_otp_confirm').show();
                                var _self = this;
                                var mobile_no = this.$content.find('#txtPhoneNumber').inputmask('unmaskedvalue').trim();
                                var send_method = this.$content.find('input[type="radio"][name="method"]:checked').val();
                                if (send_method == "tel" && mobile_no.length <= 9) {
                                    $.alert('Please enter a valid Phone number');
                                    return false;
                                }
                                var data = {
                                    "UserID": Number(user_id),
                                    "mobile": mobile_no,
                                    "send_method": send_method,
                                    "email": email
                                }
                                $.ajax({
                                    url: "Login.aspx/SendSMS",
                                    type: "POST",
                                    contentType: 'application/json',
                                    async: false,
                                    data: JSON.stringify(data),
                                    beforeSend: function () {
                                        //$("#loader").show();
                                    },
                                    success: function (result) {
                                        //$("#loader").hide();
                                        if (result.d == "Message queued successfully") {
                                            div_id_fetch_telephone.hide();
                                            div_id_otp_confirm.show();
                                            SendBtn.hide();
                                            SubmitBtn.show();
                                            _self.setTitle('2-Step Verification - Enter Verification Code.');
                                        }
                                    },
                                    error: function (err) {
                                        //$('#loader').hide();
                                        console.log("Failed , " + err);
                                    }
                                });
                                return false;
                            }
                        },
                        Submit: {
                            text: '<i class="fa fa-check" aria-hidden="true"></i> Submit',
                            btnClass: 'btn btn-success',
                            action: function () {
                                var data = {
                                    "UserID": Number(user_id),
                                    "otp": this.$content.find('#txtOtp').val().trim(),
                                    'remember': this.$content.find('#cbRemember').is(':checked'),
                                    'browser_fingerprint': client.getFingerprint()
                                }
                                $.ajax({
                                    url: "Login.aspx/VerifyOTP",
                                    type: "POST",
                                    contentType: 'application/json',
                                    async: false,
                                    data: JSON.stringify(data),
                                    beforeSend: function () {
                                        //$("#loader").show();
                                    },
                                    success: function (result) {
                                        //$("#loader").hide();
                                        if (result.d == "Success") {
                                            jc.close();
                                            __doPostBack('btnPassExpiredCheck', '');
                                        }
                                        else {
                                            $.alert(result.d);
                                        }
                                    },
                                    error: function (err) {
                                        //$('#loader').hide();
                                        console.log("Failed , " + err);
                                    }
                                });
                                return false;
                            }
                        }
                    },
                    onContentReady: function () {
                        //this.$content.find('#txtSurvey').val(SurveyName);
                        var SendBtn = this.buttons.Send;
                        var SubmitBtn = this.buttons.Submit;
                        SubmitBtn.hide();
                        //input mask
                        this.$content.find('#txtPhoneNumber').inputmask({ "mask": "(999) 999-9999" });
                    }
                });
            }

            function modify_phone_number(phone_number) {
                var phone_length = phone_number.length;
                var last_digits = phone_number.substr(phone_number.length - 4);
                var rest_length = phone_length - 4;
                var modified_numbers = '';
                for (var i = 0; i < rest_length; i++) {
                    modified_numbers += '*';
                }
                modified_numbers += last_digits;
                return modified_numbers;
            }

            function modify_email(email) {
                var email_parts = email.split('@');
                var first_part_length = email_parts[0].length;
                var first_digits = email_parts[0].substring(0, 4);
                var modified_email = first_digits;
                var loop_length = first_part_length - 4;
                for (var i = 0; i < loop_length; i++) {
                    modified_email += '*';
                }
                modified_email += '@' + email_parts[1];
                return modified_email;
            }

            function show_password_expire_warning(day_count) {
                jc = $.confirm({
                    title: 'Your password will expire soon.',
                    content: '<p class="passowrd-warnings">Your password will expire in ' + day_count + ' . Please change your password.</p>',
                    type: 'red',
                    columnClass: 'col-xs-12 col-sm-10 col-sm-offset-1 col-md-8 col-md-offset-2 col-lg-6 col-lg-offset-3',
                    typeAnimated: true,
                    buttons: {
                        ChangeNow: {
                            text: 'Change my Password Now',
                            btnClass: 'btn btn-primary',
                            action: function () {
                                $.ajax({
                                    type: "GET",
                                    url: "Login.aspx/ChangePasswordFlagUpdate",
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    error: function (error) {
                                        console.log("error: " + JSON.stringify(error));
                                    },
                                    success: function (result) {
                                        if (result.d) {
                                            jc.close();
                                            __doPostBack('btnLohinHdn', '');
                                        }
                                    }
                                });
                            }
                        },
                        Later: {
                            text: 'Remind me Later',
                            btnClass: 'btn btn-warning',
                            action: function () {
                                jc.close();
                                __doPostBack('btnLohinHdn', '');
                            }
                        },
                    }
                });
            }

            function show_password_expired_message() {
                jc = $.confirm({
                    title: 'Your password has expired.',
                    content: '<p class="passowrd-warnings">Your password has expired and must be changed.</p>' +
                        '<p class="passowrd-warnings">Click OK to continue and change your password now.</p>',
                    type: 'red',
                    typeAnimated: true,
                    buttons: {
                        Later: {
                            text: 'Ok',
                            btnClass: 'btn btn-warning',
                            action: function () {
                                jc.close();
                                __doPostBack('btnLohinHdn', '');
                            }
                        },
                    }
                });
            }

            function formisValid() {
                //debugger;
                return $('#Form1').valid();
            }

            $(document).keypress(function (event) {
                if (event.which == 13) {
                    if (jc === undefined && $('#Form1').valid()) {
                        __doPostBack('LoginButton', '');
                    } else if (!jc.isOpen() && $('#Form1').valid()) {
                        __doPostBack('LoginButton', '');
                    } else if (jc.isOpen() && 'Submit' in jc.buttons && !jc.buttons.Submit.isHidden) {
                        jc.$$Submit.trigger('click');
                    }
                }
            });

            $.validator.addMethod("onedigit", function (value) {
                return /^(?=.*\d)/.test(value)
            }, "Password contains at least one digit");

            $.validator.addMethod("onelowercase", function (value) {
                return /(?=.*[a-z])/.test(value)
            }, "Password contains at least one lower case");

            $.validator.addMethod("oneUppercase", function (value) {
                return /(?=.*[A-Z])/.test(value)
            }, "Password contains at least one Upper case");

            $.validator.addMethod("userEmail", function (value) {
                var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(value)
            }, "Please enter valid email");

            function detect_browser() {
                // Opera 8.0+
                var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;

                // Firefox 1.0+
                var isFirefox = typeof InstallTrigger !== 'undefined';

                // Safari 3.0+ "[object HTMLElementConstructor]" 
                var isSafari = /constructor/i.test(window.HTMLElement) || (function (p) { return p.toString() === "[object SafariRemoteNotification]"; })(!window['safari'] || (typeof safari !== 'undefined' && safari.pushNotification));

                // Internet Explorer 6-11
                var isIE = /*@cc_on!@*/false || !!document.documentMode;

                // Edge 20+
                var isEdge = !isIE && !!window.StyleMedia;

                // Chrome 1 - 71
                var isChrome = !!window.chrome && (!!window.chrome.webstore || !!window.chrome.runtime);

                // Blink engine detection
                var isBlink = (isChrome || isOpera) && !!window.CSS;
                var browserObj = {
                    'isOpera': isOpera,
                    'isFirefox': isFirefox,
                    'isSafari': isSafari,
                    'isIE': isIE,
                    'isEdge': isEdge,
                    'isChrome': isChrome,
                    'isBlink': isBlink
                }
                return browserObj;
            }

        </script>
</head>
<body>
 <section style="padding-top: 0 !important;" class="section register  min-vh-100 d-flex flex-column align-items-center justify-content-center py-5">
        <div class="container">
    <div class="page login-pan">
        <div class="headerWite">
            <asp:Image ID="Image3" runat="server" CssClass="img-responsive" ImageUrl="images/login-logo.png" />
        </div>
        <div class="mainLogin">
            <div class="accountInfoLogin">
              <div class="title">
                    Wentworth-Halton              
                </div>  
				<div class="text">
                    <p> Please enter your Username and Password.</p>
                    <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false"> </asp:HyperLink>
                </div>
                <div class="login">
                    <div class="alert alert-danger alert-dismissable fade in" ID="error_display" runat="server">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            <strong><asp:Literal ID="FailureText" runat="server"></asp:Literal></strong>
                        </div>
                    <form ID="Form1" runat="server" AutoPostBack="false" onsubmit="formisValid()" autocomplete="off">
                        <div class="username">
                            <asp:Label ID="lbl_invalid_username" runat="server" CssClass="alert alert-danger pg-alt" Visible="false"></asp:Label>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                            <asp:TextBox placeholder="Username" ID="UserName" runat="server" CssClass="textEntry" autocomplete="off" required="required"></asp:TextBox>
                        </div>
                         <div class="password">
                             <asp:Label ID="lbl_invalid_password" runat="server" CssClass="alert alert-danger pg-alt" Visible="false"></asp:Label>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            <asp:TextBox placeholder="Password" ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"  autocomplete="off" required="required"></asp:TextBox>
                            <span toggle="#Password" class="fa fa-fw fa-eye field-icon toggle-password"></span>
                        </div>
                    
                        <div class="submitButton">
                            <asp:LinkButton ID="LoginButton" type="submit" runat="server" CssClass="button" CommandName="Login" OnClientClick="javascript:if(!formisValid())return false;" OnClick="LoginButton_Click"><span>Sign in</span></asp:LinkButton>
                        </div>

                        <div class="loginl-ink">
                            <div class="Forgot-Password"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForgetUserId.aspx">Forgot your Username?</asp:HyperLink></div>
                            <div class="Forgot-UserID"><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/ForgetPassword.aspx">Forgot your Password?</asp:HyperLink></div>
                        </div>
                        <asp:LinkButton ID="btnLohinHdn" runat="server" OnClick="BtnHiddnLoginClick" CssClass="hide"></asp:LinkButton>
                                <asp:LinkButton ID="btnPassExpiredCheck" runat="server" OnClick="BtnHiddnCheckExpired" CssClass="hide"></asp:LinkButton>
                                <asp:HiddenField ID="hdnFingerPrint" runat="server" ClientIDMode="Static" />
                    </form>
                </div>
                <div class="reports-pan d-none">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/PublicReport.aspx">
                        <i class="fa fa-bar-chart" aria-hidden="true"></i> View reports
                    </asp:HyperLink>
                </div>
            </div>
         </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer-login">
        <div class="bottom_logo"> <a href="http://credetechnologies.com/" target="_blank">Crede Technologies Inc.</a>, Copyright  @ <%=DateTime.Now.Year.ToString() %> v <%=ConfigurationManager.AppSettings["VersionNumber"].ToString()%></div>
    </div>
        </div>
 </section>
</body>
</html>
