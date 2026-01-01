<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="CenturyRayonSchool.Login" %>

    <!DOCTYPE html>
    <html lang="en">

    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Login - Century Rayon High School</title>

        <!-- Bootstrap 5 CSS -->
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">

        <!-- Font Awesome -->
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">

        <!-- Custom CSS -->
        <link rel="stylesheet" href="css/login-modern.css">

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

        <script type="text/javascript">
            $(document).ready(function () {
                getLocalStorageValues();

                // Remember me functionality
                $("#ckb1").on("click", function (e) {
                    if ($("#ckb1").is(":checked")) {
                        var iserror = false;
                        var Uname = $('#<%= Uname.ClientID %>').val();

                        if (Uname.length == 0) {
                            iserror = true;
                        }

                        if (iserror == false) {
                            localStorage.century_username = Uname;
                            localStorage.century_ckb1 = $('#ckb1').val();
                        } else {
                            $('#ckb1').prop('checked', false);
                            showAlert("Please enter your username first");
                        }
                    } else {
                        localStorage.century_username = "";
                        localStorage.century_ckb1 = "";
                    }
                });

                // Password toggle functionality
                $('.password-toggle').on('click', function () {
                    const input = $(this).siblings('input');
                    const icon = $(this).find('i');

                    if (input.attr('type') === 'password') {
                        input.attr('type', 'text');
                        icon.removeClass('fa-eye').addClass('fa-eye-slash');
                    } else {
                        input.attr('type', 'password');
                        icon.removeClass('fa-eye-slash').addClass('fa-eye');
                    }
                });

                // Add loading spinner on login button click
                $('.btn-login-modern').on('click', function (e) {
                    if (Page_ClientValidate('LoginGroup')) {
                        $(this).html('<span>Signing In</span><span class="loading-spinner"></span>');
                    }
                });

                // Input focus animations
                $('.form-control-modern').on('focus', function () {
                    $(this).parent().find('.input-icon').addClass('active');
                }).on('blur', function () {
                    $(this).parent().find('.input-icon').removeClass('active');
                });
            });

            function showAlertModal() {
                var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'));
                myModal.show();
            }

            function showAlert(message) {
                $('#lblalertmessage').text(message);
                showAlertModal();
            }

            function getLocalStorageValues() {
                var century_username = localStorage.getItem('century_username');
                var century_ckb1 = localStorage.getItem('century_ckb1');

                if (century_ckb1 == "on") {
                    $('#<%= Uname.ClientID %>').val(century_username);
                    $('#ckb1').attr('checked', true);
                }
            }
        </script>
    </head>

    <body>
        <!-- Animated Background -->
        <div class="login-background"></div>

        <!-- Floating Shapes -->
        <div class="floating-shapes">
            <div class="shape"></div>
            <div class="shape"></div>
            <div class="shape"></div>
            <div class="shape"></div>
        </div>

        <!-- Main Container -->
        <div class="login-container">
            <div class="w-100">
                <!-- Header -->
                <div class="login-header">
                    <div class="school-logo">
                        <i class="fas fa-graduation-cap"></i>
                    </div>
                    <h1 class="school-name">Century Rayon High School</h1>
                    <p class="school-address">Kalyan - Ahmednagar Highway, Century Rayon Colony, Shahad, Ulhasnagar,
                        Maharashtra 421103</p>
                </div>

                <!-- Login Card -->
                <div class="login-card mx-auto">
                    <div class="card-header-modern">
                        <h3>Welcome Back</h3>
                        <p>Sign in to access your account</p>
                    </div>

                    <div class="card-body-modern">
                        <form runat="server">
                            <!-- Login Panel -->
                            <asp:Panel ID="LoginPanel" runat="server" DefaultButton="btn1_Login">
                                <!-- Username Field -->
                                <div class="form-group-modern">
                                    <label for="<%= Uname.ClientID %>">Username</label>
                                    <div class="input-wrapper">
                                        <i class="fas fa-user input-icon"></i>
                                        <asp:TextBox ID="Uname" runat="server" CssClass="form-control-modern"
                                            placeholder="Enter your username" autocomplete="username"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="Uname" ErrorMessage="Please enter your username"
                                        CssClass="validation-message" Display="Dynamic" ValidationGroup="LoginGroup">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <!-- Password Field -->
                                <div class="form-group-modern">
                                    <label for="<%= Upass.ClientID %>">Password</label>
                                    <div class="input-wrapper">
                                        <i class="fas fa-lock input-icon"></i>
                                        <asp:TextBox ID="Upass" runat="server" CssClass="form-control-modern"
                                            TextMode="Password" placeholder="Enter your password"
                                            autocomplete="current-password"></asp:TextBox>
                                        <span class="password-toggle">
                                            <i class="fas fa-eye"></i>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="Upass" ErrorMessage="Please enter your password"
                                        CssClass="validation-message" Display="Dynamic" ValidationGroup="LoginGroup">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <!-- Remember Me & Forgot Password -->
                                <div class="form-options">
                                    <div class="remember-me">
                                        <input id="ckb1" type="checkbox">
                                        <label for="ckb1">Remember Me</label>
                                    </div>
                                    <asp:LinkButton ID="lnkForgotPassword" runat="server" Text="Forgot Password?"
                                        CssClass="forgot-password" OnClick="lnkForgotPassword_Click"
                                        CausesValidation="false">
                                    </asp:LinkButton>
                                </div>

                                <!-- Buttons -->
                                <div class="button-group">
                                    <a href="index.aspx"
                                        class="btn-back-modern text-center text-decoration-none d-flex align-items-center justify-content-center"
                                        style="padding: 14px;">
                                        <i class="fas fa-arrow-left me-2"></i>Back to Home
                                    </a>
                                    <asp:Button ID="btn1_Login" runat="server" Text="Sign In"
                                        CssClass="btn-login-modern" OnClick="btn1_Login_Click"
                                        ValidationGroup="LoginGroup" style="width: 48%;" />
                                </div>
                            </asp:Panel>

                            <!-- Forgot Password Panel -->
                            <asp:Panel ID="ForgotPasswordPanel" runat="server" Visible="false"
                                DefaultButton="btnResetPassword">
                                <!-- Username Field -->
                                <div class="form-group-modern">
                                    <label for="<%= txtForgotUsername.ClientID %>">Username</label>
                                    <div class="input-wrapper">
                                        <i class="fas fa-user input-icon"></i>
                                        <asp:TextBox ID="txtForgotUsername" runat="server"
                                            CssClass="form-control-modern" placeholder="Enter your username">
                                        </asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtForgotUsername" ErrorMessage="Please enter your username"
                                        CssClass="validation-message" Display="Dynamic"
                                        ValidationGroup="ForgotPasswordGroup"></asp:RequiredFieldValidator>
                                </div>

                                <!-- New Password Field -->
                                <div class="form-group-modern">
                                    <label for="<%= txtNewPassword.ClientID %>">New Password</label>
                                    <div class="input-wrapper">
                                        <i class="fas fa-lock input-icon"></i>
                                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control-modern"
                                            TextMode="Password" placeholder="Enter new password"></asp:TextBox>
                                        <span class="password-toggle">
                                            <i class="fas fa-eye"></i>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtNewPassword" ErrorMessage="Please enter new password"
                                        CssClass="validation-message" Display="Dynamic"
                                        ValidationGroup="ForgotPasswordGroup"></asp:RequiredFieldValidator>
                                </div>

                                <!-- Confirm Password Field -->
                                <div class="form-group-modern">
                                    <label for="<%= txtConfirmPassword.ClientID %>">Confirm Password</label>
                                    <div class="input-wrapper">
                                        <i class="fas fa-lock input-icon"></i>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server"
                                            CssClass="form-control-modern" TextMode="Password"
                                            placeholder="Confirm new password"></asp:TextBox>
                                        <span class="password-toggle">
                                            <i class="fas fa-eye"></i>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="txtConfirmPassword" ErrorMessage="Please confirm password"
                                        CssClass="validation-message" Display="Dynamic"
                                        ValidationGroup="ForgotPasswordGroup"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                        ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"
                                        ErrorMessage="Passwords do not match" CssClass="validation-message"
                                        Display="Dynamic" ValidationGroup="ForgotPasswordGroup"></asp:CompareValidator>
                                </div>

                                <!-- Buttons -->
                                <div class="button-group">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn-back-modern"
                                        OnClick="btnBack_Click" CausesValidation="false" />
                                    <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password"
                                        CssClass="btn-login-modern" OnClick="btnResetPassword_Click"
                                        ValidationGroup="ForgotPasswordGroup" style="width: 48%;" />
                                </div>
                            </asp:Panel>
                        </form>
                    </div>
                </div>
            </div>
        </div>

        <!-- Alert Modal -->
        <div class="modal fade" id="alertmessagemodal" tabindex="-1" aria-labelledby="alertModalLabel"
            aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content" style="border-radius: 15px; overflow: hidden;">
                    <div class="modal-header"
                        style="background: linear-gradient(135deg, #702030 0%, #4a1520 100%); border: none;">
                        <h5 class="modal-title text-white" id="alertModalLabel">
                            <i class="fas fa-exclamation-circle me-2"></i>Alert
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"
                            aria-label="Close"></button>
                    </div>
                    <div class="modal-body" style="padding: 30px; text-align: center;">
                        <asp:Label Text="" runat="server" ID="lblalertmessage"
                            Style="font-size: 1.1rem; color: #333;" />
                    </div>
                    <div class="modal-footer" style="border: none; padding: 20px;">
                        <button type="button" class="btn btn-login-modern" data-bs-dismiss="modal"
                            style="width: auto; padding: 10px 30px;">OK</button>
                    </div>
                </div>
            </div>
        </div>

    </body>

    </html>